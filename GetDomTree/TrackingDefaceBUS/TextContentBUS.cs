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


namespace TrackingDefaceBUS
{
    public class TextContentBUS 
    {
        TextContentDAO objectTextContent = new TextContentDAO();
        //Utils.UtilsHtmlAgility utils = new Utils.UtilsHtmlAgility();      

        public bool TrackingDeface (DataGridView dtGridWeb)
        {
            
            int webCount = dtGridWeb.RowCount;           
            for (int i = 0; i < webCount - 1; i++ )
            {
                int webID = (int)dtGridWeb.Rows[i].Cells[0].Value;
                string url = dtGridWeb.Rows[i].Cells[2].Value.ToString();
                string content = Utils.UtilsHtmlAgility.GetContent(url);
                TextContent textContent = objectTextContent.GetContentWebIDByWebID(webID);
                
                /* Check Web has content */
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

                    UpdateTextContent(webID, content, result);
                }
                else
                {
                    string result = "Safe";
                    InsertTextContent(webID, content, result);
                } 
            }
                return true;
        }

        public void InsertTextContent(int webID, string content, string result)
        {
            TextContent textContent = new TextContent();
            DataTable dtAllTextContent = objectTextContent.GetAll();
            textContent.ContentID = dtAllTextContent.Rows.Count + 1;
            textContent.Content =  content;
            textContent.TextResult = result;
            textContent.TimeCheck = DateTime.Now;
            textContent.WebID = webID;
           
            objectTextContent.Insert(textContent);
        }

        public void UpdateTextContent (int webID, string content, string result)
        {
            TextContent textContent = new TextContent();
            textContent.TimeCheck = DateTime.Now;
            textContent.Content = content;
            textContent.TextResult = result;
            textContent.WebID = webID;
            objectTextContent.Update(textContent);
        }

        public bool DeleteTextContent(int id)
        {
            return objectTextContent.Delete(id);
        }
    }
}
