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
    public class TextContentDAO : DBConnection
    {
        public TextContentDAO() : base() { }

        /* Get all */
        public DataTable GetAll()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from TEXT_CONTENT");
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
        public DataTable GetContentByWebID( string webID)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("select Content from TEXT_CONTENT "
                                                + " inner join WEB on WEB.WebID = TEXT_CONTENT.WebID"
                                                + " where TEXT_CONTENT.WebID = @webID ", conn);
                cmd.Parameters.Add("@webID", SqlDbType.Int).Value = webID;
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

        public bool Insert(TextContent textContent)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into TEXT_CONTENT values(@ContetnID, @Content, @TimeCheck, @TextResult, @WebID)", conn);
                cmd.Parameters.Add("@ContetnID", SqlDbType.Int).Value = textContent.ContentID;
                cmd.Parameters.Add("@Content", SqlDbType.NVarChar).Value = textContent.Content;
                cmd.Parameters.Add("@TimeCheck", SqlDbType.DateTime).Value = textContent.TimeCheck;
                cmd.Parameters.Add("@TextResult", SqlDbType.NVarChar).Value = textContent.TextResult;
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = textContent.webid.webID;
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
        /* Update user in to Database */
        public bool Update(TextContent textContent)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Update TEXT_CONTENT Set Content = @Content, TimeCheck = @TimeCheck,"
                                    + "TextResult = @TextResult, Email = @Email"
                                    + " where ContetnID = @ContetnID", conn);
                cmd.Parameters.Add("@ContetnID", SqlDbType.Int).Value = textContent.ContentID;
                cmd.Parameters.Add("@Content", SqlDbType.NVarChar).Value = textContent.Content;
                cmd.Parameters.Add("@TimeCheck", SqlDbType.DateTime).Value = textContent.TimeCheck;
                cmd.Parameters.Add("@TextResult", SqlDbType.NVarChar).Value = textContent.TextResult;
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = textContent.webid.webID;
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

        /* Delete object User by userID from database */
        public bool Delete(string ContentID)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE from TEXT_CONTENT where ContentID = @ContentID ", conn);
                cmd.Parameters.Add("@ContentID", SqlDbType.Int).Value = ContentID;
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
