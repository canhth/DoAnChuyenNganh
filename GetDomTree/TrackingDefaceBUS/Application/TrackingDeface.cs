using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using TrackingDefaceDAO;
using TrackingDefaceDTO;
using System.Xml;

namespace TrackingDefaceBUS.Application
{
    public class TrackingDeface
    {
        WebDAO objectWebDAO = new WebDAO();
        ImageContentDAO objectImageContentDAO = new ImageContentDAO();
        TextContentDAO objectTextContentDAO = new TextContentDAO();
        TextContentBUS objectTextContentBUS = new TextContentBUS();
        ImageContentBUS objectImageContetBUS = new ImageContentBUS();
        Utils.UtilsHtmlAgility objectUtils = new Utils.UtilsHtmlAgility();
        

        public bool TrackingDefaceWebSite()
        {
            DataTable dtTableWeb = objectWebDAO.GetAll();
            int webCount = dtTableWeb.Rows.Count;
            for (int i = 0; i < webCount ; i++)
            {
                int webID = Int32.Parse(dtTableWeb.Rows[i]["WebID"].ToString());
                string url = dtTableWeb.Rows[i]["URL"].ToString();
                string _textContent = objectUtils.GetContent(url);               
                /* Check text content Website */
                TextTracking(webID, _textContent, url);

                /* Check image content Website */
                ImageTracking(webID);

            }
            return true;
        }

        /*  Checking text content of Website  */
        public void TextTracking(int webID, string content, string url)
        {
            TextContent textContent = objectTextContentDAO.GetContentWebIDByWebID(webID);
            if (textContent.Content != null)
            {
                string webContent = textContent.Content;
                string result = "";
                if (objectUtils.SoSanh(webContent, content))
                {
                    result = "Safe";
                    Console.WriteLine("Website :" + url + "  dang an toan.");
                }
                else
                {
                    result = "Warning";
                    Utils.SendEmail.SendeMail();
                    Console.WriteLine("Website :" + url + "  da bi thay doi noi dung.");
                }

                objectTextContentBUS.UpdateTextContent(webID, content, result);
            }
            else
            {
                string result = "Safe";
                objectTextContentBUS.InsertTextContent(webID, content, result);
            }
        }

        /*  Checking image content of Website  */
        public void ImageTracking(int WebID )
        {
            StreamReader streamReader = new StreamReader(@"D:/TestImage/SerializationOverview.xml");
            ImageContent imageContent = objectImageContentDAO.GetContentImageIDByID(WebID);
            string newContent = streamReader.ReadToEnd();
            streamReader.Close();

            List<string> NewListImage = new List<string>();
            using (Stream loadstream = new FileStream(@"D:/TestImage/SerializationOverview.xml", FileMode.Open))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                NewListImage = (List<string>)serializer.Deserialize(loadstream);
                loadstream.Close();
            }

            imageContent.webID = WebID;
            if (imageContent.Content != null)
            {
                // Get Old List from Database
                List<string> OldListImage = new List<string>();
                var docXML = new XmlDocument();
                docXML.LoadXml(imageContent.Content);
                XmlNodeList nl = docXML.SelectNodes("ArrayOfString");
                XmlNode root = nl[0];
                foreach (XmlNode xnode in root.ChildNodes)
                {
                    OldListImage.Add(xnode.InnerText);
                }

                // Get New List from WebSite
               

                //NewListImage = Utils.UtilsHtmlAgility.ImageList;
            
                SoSanhImage(OldListImage, NewListImage);

                objectImageContetBUS.UpdateImageContent(imageContent.webID, newContent, imageContent.id);
            }
            else
            {
                objectImageContetBUS.InsertImageContent(imageContent.webID, newContent);
            }
        }


        /*  So sanh trong so Image   */
        public string SoSanhImage(List<string> oldList, List<string> newList)
        {
            string result = "";
            int caseResult = 0;

            if (newList.Count == 0)
                caseResult = 2;
            else
            {
                // In old list but not in new list. 
                var inOldListButNotInNewList = oldList.Except(newList).ToList();
                
                // In new list but not in old list.
                var inNewListButNotInOldList = newList.Except(oldList).ToList();

                // Khong co gi thay doi
                if (inOldListButNotInNewList.Count == 0 && inNewListButNotInOldList.Count == 0)
                {
                    Console.WriteLine("Hinh anh cua Website khong bi thay doi");
                    caseResult = 5;
                }

                // Co su thay doi

                if (inOldListButNotInNewList.Count > 0 && inNewListButNotInOldList.Count == 0)
                {
                    Console.WriteLine("Old List > New List - Hinh anh da bi mat di ");
                    if (inOldListButNotInNewList.Count > 1 / 4 * oldList.Count)
                        caseResult = 1;
                }
                if (inOldListButNotInNewList.Count == 0 && inNewListButNotInOldList.Count > 0)
                {
                    Console.WriteLine("Old List < New List. Hinh anh da duoc them vao");
                    if (inNewListButNotInOldList.Count > 1 / 4 * oldList.Count)
                        caseResult = 3;
                }
                
                if (inOldListButNotInNewList.Count > 0 && inNewListButNotInOldList.Count > 0)
                {
                    Console.WriteLine("WebSite bi thay doi noi dung");
                    if (inOldListButNotInNewList.Count > 1 / 4 * oldList.Count && inNewListButNotInOldList.Count > 1 / 4 * oldList.Count)
                    {
                        Console.WriteLine("Hinh anh bi thay doi qua 1/4");
                        caseResult = 4;
                    }
                }
            }     

            switch (caseResult)
            {
                case 1:
                    result += " So luong hinh anh da bi mat qua 1/4 ";
                    break;
                case 2:
                    result += "So luong hinh anh da bi mat het";
                    break;
                case 3:
                    result += " So luong hinh anh them vao vuot qua 1/4";
                    break;
                case 4:
                    result += " Noi dung hinh anh bi xao tron qua nhieu. Vuot nguong cho phep ";
                    break;
                case 5:
                    result += "Hinh Anh khong bi thay doi";
                    break;
            }

            Console.WriteLine(result);
            return result;
        }
    }
}
