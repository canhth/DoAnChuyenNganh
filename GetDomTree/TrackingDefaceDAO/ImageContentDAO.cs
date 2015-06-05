using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingDefaceDTO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace TrackingDefaceDAO
{
    public class ImageContentDAO : DBConnection
    {
        public ImageContentDAO() : base() { }
        ///* Get all */
        public DataTable GetAll()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Select ID from IMAGE_CONTENT ", conn);
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
        public ImageContent GetContentImageIDByID(int webID)
        {
            ImageContent content = new ImageContent();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                
                SqlCommand cmd = new SqlCommand("select ID, Content, WebID from IMAGE_CONTENT "
                                                + " where WebID = @WebID  ", conn);
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = webID;
                //cmd.Parameters.Add("@ID", SqlDbType.Int).Value = webID;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    content.id = (int)rd["ID"];
                    content.webID = (int)rd["WebID"];
                    content.Content = rd["Content"].ToString().Trim();
                    //System.Diagnostics.Trace.WriteLine(rd["Content"].ToString());                  
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

        public bool Insert(ImageContent imageContent)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into IMAGE_CONTENT values(@ID, @Content, @WebID)", conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = imageContent.id;
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = imageContent.webID;
                cmd.Parameters.Add("@Content", SqlDbType.Xml).Value = (new XmlTextReader(imageContent.Content, XmlNodeType.Document, null));
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
        public bool Update(ImageContent imageContent)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Update IMAGE_CONTENT Set Content = @Content, WebID = @WebID where ID = @ID", conn);
                cmd.Parameters.Add("@Content", SqlDbType.Xml).Value = (new XmlTextReader(imageContent.Content, XmlNodeType.Document, null));
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = imageContent.webID;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = imageContent.id;
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
        public bool Delete(int ID)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE from IMAGE_CONTENT where ID = @ID ", conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
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
