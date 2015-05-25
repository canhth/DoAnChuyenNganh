using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using TrackingDefaceDAO;
using TrackingDefaceDTO;

namespace TrackingDefaceBUS
{
    public class WebBUS 
    {

        WebDAO objectWeb = new WebDAO();
        Utils.UtilsHtmlAgility utils = new Utils.UtilsHtmlAgility();

        public DataTable GetAllRecords()
        {
            return objectWeb.GetAll();
        }
        public DataTable GetAllWebEnable( int isEnable)
        {
            return objectWeb.GetWebSiteEnable(isEnable);
        }
        public Web getWebbyNameSite (string nameSite)
        {
            return objectWeb.GetWebbyName(nameSite);
        }
        public string listString(string url)
        {
            return utils.GetChildLink(url);
        }

        public bool InsertUser(Web web)
        {
            return objectWeb.Insert(web);
        }

        public bool UpdateUser(Web web)
        {
            return objectWeb.Update(web);
        }

        public bool DeleteUser(string webID)
        {
            return objectWeb.Delete(webID);
        }

        

    }
}
