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
    public class ImageContentBUS
    {
        ImageContentDAO objectImageContent = new ImageContentDAO();    

        public void InsertImageContent(int webID, string content)
        {
            ImageContent imageContent = new ImageContent();
            DataTable dtAllImageContent = objectImageContent.GetAll();
            imageContent.id = dtAllImageContent.Rows.Count + 1;
            imageContent.Content = content;
            imageContent.webID = webID;

            objectImageContent.Insert(imageContent);
        }

        public void UpdateImageContent(int webID, string content, int id)
        {
            ImageContent imageContent = new ImageContent();
            imageContent.id = id;
            imageContent.Content = content;
            imageContent.webID = webID;
            objectImageContent.Update(imageContent);
        }

        public bool DeleteImageContent(int webID)
        {
            return objectImageContent.Delete(webID);
        }
    }
}
