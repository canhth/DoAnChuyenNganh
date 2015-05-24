using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingDefaceDTO
{
    public class Users
    {
        public int userID      { get; set; }
        public string userName { get; set; }
        public string passWords { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }

        public Users() { }

        public Users(int userid, string username, string password, string fullname, string mail)
        {
            this.userID = userid;
            this.userName = username;
            this.passWords = password;
            this.fullName = fullname;
            this.email = mail;
        }
    }

    

}
