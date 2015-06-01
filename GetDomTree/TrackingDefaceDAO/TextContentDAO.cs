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

        ///* Get all */
        public DataTable GetAll()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Select ContentID from TEXT_CONTENT", conn);
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
        public TextContent GetContentWebIDByWebID(int webID)
        {
            TextContent content = new TextContent();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("select Content, WebID from TEXT_CONTENT "
                                                + " where WebID = @WebID ", conn);
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = webID;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    content.WebID = (int)rd["WebID"];
                    content.Content = rd["Content"].ToString().Trim();
                    rd.Close();
                }
                conn.Close();
                return content;
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
                //cmd.Parameters.Add("@Content", SqlDbType.NText).Value = textContent.Content;
                cmd.Parameters.AddWithValue(@"Content", textContent.Content);
                cmd.Parameters.Add("@TimeCheck", SqlDbType.DateTime).Value = textContent.TimeCheck;
                cmd.Parameters.Add("@TextResult", SqlDbType.NVarChar).Value = textContent.TextResult;
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = textContent.WebID;
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
                                    + "TextResult = @TextResult"
                                    + " where WebID = @WebID", conn);
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = textContent.WebID;
                cmd.Parameters.Add("@Content", SqlDbType.NText).Value = textContent.Content;
                cmd.Parameters.Add("@TimeCheck", SqlDbType.DateTime).Value = textContent.TimeCheck;
                cmd.Parameters.Add("@TextResult", SqlDbType.NVarChar).Value = textContent.TextResult;
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
