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
        Define define = new Define();
        public class Object
        {
            public DataTable dtTableWeb { get; set; }
            public int webIndex { get; set; }
        }
        public bool TrackingDefaceWebSite(DataTable dtTableWeb, int webIndex)
        {
            int numberTree = 0;
            int webID = Int32.Parse(dtTableWeb.Rows[webIndex]["WebID"].ToString());
            string url = dtTableWeb.Rows[webIndex]["URL"].ToString();
            string _textContent = objectUtils.GetContent(url);
            string banText = dtTableWeb.Rows[webIndex]["BanText"].ToString();

            /* Web bi mat het noi dung */
            if (_textContent.Length < 500)
                define.CayQuyetDinh(5, webID, _textContent);
            else
            {
                /* Web co chua tu ngu nhay cam */
                Utils.CIBMSearcher BMS = new Utils.CIBMSearcher(banText, false);
                int index = BMS.Search(_textContent, 0);
                if (index >= 0)
                {
                    Console.WriteLine("Web site cua ban chua tu ngu nhay cam la :" + banText);
                    numberTree += 2;
                }
                /* If website is Warning Before */
                if (dtTableWeb.Rows[webIndex]["WebStatus"].ToString() == "Warning")
                {
                    TextTracking(webID, _textContent, url);
                    ImageTracking(webID);
                    define.CayQuyetDinh(0, webID, _textContent);
                }
                else
                {
                    /* Check text content Website */
                    numberTree += TextTracking(webID, _textContent, url);
                    /* Check image content Website */
                    numberTree += ImageTracking(webID);
                    define.CayQuyetDinh(numberTree, webID, _textContent);
                }
            }
            return true;
        }

        /*  Checking text content of Website  */
        public int TextTracking(int webID, string content, string url)
        {
            TextContent textContent = objectTextContentDAO.GetContentWebIDByWebID(webID);
            Web webObject = new Web();
            int number = 0;
            if (textContent.Content != null)
            {
                string webContent = textContent.Content;

                //int ketqua = Utils.BoyerMoore.boyerMooreHorsepool(webContent, content);

                if (objectUtils.SoSanh(webContent, content))
                {
                    Console.WriteLine("Website :" + url + "  dang an toan.");
                }
                else
                {
                    number += 1;
                    Utils.SendEmail.SendeMail();
                    Console.WriteLine("Website :" + url + "  da bi thay doi noi dung.");
                }
            }
            else
            {
                string result = "Safe";
                objectTextContentBUS.InsertTextContent(webID, content, result);
            }
            return number;
        }

        /*  Checking image content of Website  */
        public int ImageTracking(int WebID)
        {
            int numberTree = 0;
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

                // NewListImage = Utils.UtilsHtmlAgility.ImageList;

                numberTree = SoSanhImage(OldListImage, NewListImage);

                objectImageContetBUS.UpdateImageContent(imageContent.webID, newContent, imageContent.id);
            }
            else
            {
                objectImageContetBUS.InsertImageContent(imageContent.webID, newContent);
            }
            return numberTree;
        }


        /*  So sanh trong so Image   */
        public int SoSanhImage(List<string> oldList, List<string> newList)
        {
            string result = "";
            int numberTree = 0;
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
                    numberTree += 1;
                    break;
                case 2:
                    result += "So luong hinh anh da bi mat het";
                    numberTree += 5;
                    break;
                case 3:
                    result += " So luong hinh anh them vao vuot qua 1/4";
                    numberTree += 1;
                    break;
                case 4:
                    result += " Noi dung hinh anh bi xao tron qua nhieu. Vuot nguong cho phep ";
                    numberTree += 2;
                    break;
                case 5:
                    result += "Hinh Anh khong bi thay doi";
                    break;
            }

            Console.WriteLine(result);
            return numberTree;
        }
    }
}
