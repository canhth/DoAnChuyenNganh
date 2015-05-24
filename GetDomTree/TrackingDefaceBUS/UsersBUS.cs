using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingDefaceDAO;
using TrackingDefaceDTO;
using System.Data;

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
