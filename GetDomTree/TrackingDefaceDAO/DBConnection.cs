using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TrackingDefaceDAO
{
    public class DBConnection
    {
        protected SqlConnection conn;

        public DBConnection()
        {
            try
            {
                conn = new SqlConnection("Server= .; Database= TRACKING_DEFACE;"
                + "Integrated Security=SSPI;");
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
