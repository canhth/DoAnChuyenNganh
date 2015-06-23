using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingDefaceDTO;
using System.Data;
using System.Data.SqlClient;

namespace TrackingDefaceDAO
{
    public class EmailDAO : DBConnection
    {
        public EmailDAO() : base() { }

        public DataTable GetAll()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from EMAIL", conn);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        /* Get Content by WebID */
        public Email GetEmailByisHost(bool isHost)
        {
            Email email = new Email();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("select id, port, host, timeSend, email, passWords, enableSSL from EMAIL "
                                                + " where isHost = @isHost ", conn);
                cmd.Parameters.Add("@isHost", SqlDbType.Bit).Value = isHost;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    email.id = (int)rd["id"];
                    email.port = rd["port"].ToString().Trim();
                    email.host = rd["host"].ToString().Trim();
                    email.timeSend = (int)rd["timeSend"];
                    email.email = rd["email"].ToString().Trim();
                    email.passWords = rd["passWords"].ToString().Trim();
                    email.enableSSL = (bool)rd["enableSSL"];
                    rd.Close();
                }
                conn.Close();
                return email;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        /* Insert in to Database */
        public bool Insert(Email  email)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into EMAIL values(@id, @port, @host, @timeSend, @email, @passWords, @enableSSL, @isHost)", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = email.id;
                cmd.Parameters.Add("@port", SqlDbType.NVarChar).Value = email.port;
                cmd.Parameters.Add("@host", SqlDbType.NVarChar).Value = email.host;
                cmd.Parameters.Add("@timeSend", SqlDbType.Int).Value = email.timeSend;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email.email;
                cmd.Parameters.Add("@passWords", SqlDbType.NVarChar).Value = email.passWords;
                cmd.Parameters.Add("@enableSSL", SqlDbType.Bit).Value = email.enableSSL;
                cmd.Parameters.Add("@isHost", SqlDbType.Bit).Value = email.isHost;
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /* Update in to Database */
        public bool Update(Email email)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Update EMAIL Set port = @port, host = @host,"
                                    + " timeSend = @timeSend, email = @email, "
                                    + " passWords = @passWords, enableSSL = @enableSSL, isHost = @isHost "
                                    + " where id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = email.id;
                cmd.Parameters.Add("@port", SqlDbType.NVarChar).Value = email.port;
                cmd.Parameters.Add("@host", SqlDbType.NVarChar).Value = email.host;
                cmd.Parameters.Add("@timeSend", SqlDbType.Int).Value = email.timeSend;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email.email;
                cmd.Parameters.Add("@passWords", SqlDbType.NVarChar).Value = email.passWords;
                cmd.Parameters.Add("@enableSSL", SqlDbType.Bit).Value = email.enableSSL;
                cmd.Parameters.Add("@isHost", SqlDbType.Bit).Value = email.isHost;
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /* Delete object from database */
        public bool Delete(string emailID)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE from EMAIL where id = @emaiID ", conn);
                cmd.Parameters.Add("@emaiID", SqlDbType.Int).Value = emailID;
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
