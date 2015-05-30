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
    public class WebDAO :DBConnection
    {
        public WebDAO() : base() {}

        /* Get all records WEb */
        public DataTable GetAll()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Select WebID, NameSite, URL, IPPublic, Phones, isEnable from WEB", conn);
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
        public DataTable GetWebSiteEnable(int isEnable)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("select * from WEB "
                                                + " where isEnable = @isEnable ", conn);
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = isEnable;
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

        /* Get list web isEnable tracking */
        public Web GetWebbyName(string nameSite)
        {
            Web web = new Web();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("select * from WEB "
                                                + " where NameSite = @nameSite ", conn);
                cmd.Parameters.Add("@nameSite", SqlDbType.NVarChar).Value = nameSite;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    web.webID = (int)rd["WebID"];
                    web.nameSite = rd["NameSite"].ToString().Trim();
                    web.uRL = rd["URL"].ToString().Trim();
                    web.ipPulbic = rd["IPPublic"].ToString().Trim();
                    web.ipPrivate = rd["IPPrivate"].ToString().Trim();
                    web.webPriority = (int)rd["WebPriority"];
                    web.phones = rd["Phones"].ToString().Trim();
                    web.emails = rd["Emails"].ToString().Trim();
                    web.searchText = rd["searchText"].ToString().Trim();
                    web.webStatus = rd["WebStatus"].ToString().Trim();
                    web.banText = rd["BanText"].ToString().Trim();
                    web.isEnable = (bool)rd["isEnable"];
                    rd.Close();
                }
                conn.Close();
                return web;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        /* Insert Website in to Database */
        public bool Insert(Web web)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into WEB values(@WebID, @NameSite, @URL,"
                                                + "@IPPublic, @IPPrivate, @WebPriority, @Phones, " +
                                                " @Emails, @searchText, @WebStatus, @BanText, @isEnable)", conn);
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = web.webID;
                cmd.Parameters.Add("@NameSite", SqlDbType.NVarChar).Value = web.nameSite;
                cmd.Parameters.Add("@URL", SqlDbType.NVarChar).Value = web.uRL;
                cmd.Parameters.Add("@IPPublic", SqlDbType.NVarChar).Value = web.ipPulbic;
                cmd.Parameters.Add("@IPPrivate", SqlDbType.NVarChar).Value = web.ipPrivate;
                cmd.Parameters.Add("@WebPriority", SqlDbType.Int).Value = web.webPriority;
                cmd.Parameters.Add("@Phones", SqlDbType.NVarChar).Value = web.phones;
                cmd.Parameters.Add("@Emails", SqlDbType.NVarChar).Value = web.emails;
                cmd.Parameters.Add("@searchText", SqlDbType.NVarChar).Value = web.searchText;
                cmd.Parameters.Add("@WebStatus", SqlDbType.NVarChar).Value = web.webStatus;
                cmd.Parameters.Add("@BanText", SqlDbType.NVarChar).Value = web.banText;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = web.isEnable;


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
        /* Update Web in to Database */
        public bool Update(Web web)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Update WEB Set NameSite = @NameSite, URL = @URL, IPPublic = @IPPublic," 
                                    + " IPPrivate = @IPPrivate , WebPriority = @Webpriority,"
                                    + " Phones = @Phones, Emails = @Emails, searchText = @searchText, "
                                    + "WebStatus = @WebStatus, BanText = @BanText, isEnable = @isEnable"
                                    + " where WebID = @WebID", conn);
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = web.webID;
                cmd.Parameters.Add("@NameSite", SqlDbType.NVarChar).Value = web.nameSite;
                cmd.Parameters.Add("@URL", SqlDbType.NVarChar).Value = web.uRL;
                cmd.Parameters.Add("@IPPublic", SqlDbType.NVarChar).Value = web.ipPulbic;
                cmd.Parameters.Add("@IPPrivate", SqlDbType.NVarChar).Value = web.ipPrivate;
                cmd.Parameters.Add("@WebPriority", SqlDbType.Int).Value = web.webPriority;
                cmd.Parameters.Add("@Phones", SqlDbType.NVarChar).Value = web.phones;
                cmd.Parameters.Add("@Emails", SqlDbType.NVarChar).Value = web.emails;
                cmd.Parameters.Add("@searchText", SqlDbType.NVarChar).Value = web.searchText;
                cmd.Parameters.Add("@WebStatus", SqlDbType.NVarChar).Value = web.webStatus;
                cmd.Parameters.Add("@BanText", SqlDbType.NVarChar).Value = web.banText;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = web.isEnable;
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

        /* Delete object Website by webID from database */
        public bool Delete(int webID)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE from WEB where WebID = @WebID ", conn);
                cmd.Parameters.Add("@WebID", SqlDbType.Int).Value = webID;
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
