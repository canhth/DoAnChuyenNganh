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

        public bool TrackingDefaceWebSite()
        {
            DataTable dtTableWeb = objectWebDAO.GetAll();
            int webCount = dtTableWeb.Rows.Count;
            for (int i = 0; i < webCount - 1; i++)
            {
                int webID = Int32.Parse(dtTableWeb.Rows[i]["WebID"].ToString());
                string url = dtTableWeb.Rows[i]["URL"].ToString();
                string _textContent = Utils.UtilsHtmlAgility.GetContent(url);
                StreamReader streamReader = new StreamReader(@"D:/TestImage/SerializationOverview.xml");
                string xmlOfTextFile = streamReader.ReadToEnd();
                
                /* Check text content Website */
                TextTracking(webID, _textContent, url);

                /* Check image content Website */
                ImageTracking(webID);
                streamReader.Close();
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
                if (Utils.UtilsHtmlAgility.SoSanh(webContent, content))
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
            string content = streamReader.ReadToEnd();
            if (imageContent.Content != null)
            {
                objectImageContetBUS.UpdateImageContent(imageContent.webID, content, imageContent.id);
            }
            else
            {
                objectImageContetBUS.InsertImageContent(imageContent.webID, content);
            }

            //var docXML = new XmlDocument();
            //docXML.LoadXml(imageContent.Content);
            //XmlNodeList nl = docXML.SelectNodes("ArrayOfString");
            //XmlNode root = nl[0];
            //foreach (XmlNode xnode in root.ChildNodes)
            //{
            //    string name = xnode.Name;
            //    string value = xnode.InnerText;
            //    Console.WriteLine("Value: " + value);
            //}
        }

    }
}
