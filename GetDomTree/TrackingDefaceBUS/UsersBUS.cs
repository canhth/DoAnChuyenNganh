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
using System.Drawing;
using System.Windows.Forms;
using TrackingDefaceDAO;
using TrackingDefaceDTO;

namespace TrackingDefaceBUS
{
    public class UsersBUS
    {
        UsersDAO objectUser = new UsersDAO();

        public DataTable GetAllRecords()
        {
            return objectUser.GetAll();
        }

        public bool InsertUser (Users user)
        {
            return objectUser.Insert(user);
        }

        public bool UpdateUser (Users user)
        {
            return objectUser.Update(user);
        }

        public bool DeleteUser (string userID)
        {
            return objectUser.Delete(userID);
        }
    }
}
