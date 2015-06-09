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
