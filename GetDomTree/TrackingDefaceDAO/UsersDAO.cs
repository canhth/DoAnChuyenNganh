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
    public class UsersDAO : DBConnection
    {
        public UsersDAO() : base() { }

        /* Get all records Users */
        public DataTable GetAll()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("selecxxxx");
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

        /* Get record by userName --> autocomplete */
        //public DataTable GetByUser( string userName)
        //{
        //    try
        //    {
        //        if (conn.State != ConnectionState.Open)
        //            conn.Open();
        //        SqlCommand cmd = new SqlCommand("select MaKH as 'Mã Khách Hàng', TenKH as 'Tên Khách Hàng', DiaChi as 'Địa Chỉ', CongNo as 'Công Nợ', TenLKH as 'Loại Khách Hàng' from KHACHHANG "
        //                                        + " inner join LOAIKHACHHANG on KHACHHANG.MaLKH = LOAIKHACHHANG.MaLKH"
        //                                        + " where TenKH Like '%'+@tenkh+'%' ", conn);
        //        cmd.Parameters.Add("@tenkh", SqlDbType.NVarChar).Value = userName;
        //        SqlDataAdapter adap = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        adap.Fill(dt);
        //        conn.Close();
        //        return dt;
        //    }
        //    catch (Exception)
        //    {
        //        conn.Close();
        //        throw;
        //    }
        //}

        /* Insert User in to Database */
        public bool Insert(Users user)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Users values(@UserID, @UserName, @PassWords, @FullName, @Email)", conn);
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = user.userID;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.userName;
                cmd.Parameters.Add("@PassWords", SqlDbType.NVarChar).Value = user.passWords;
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.email;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.fullName;
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
        public bool Update(Users user)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Update User Set UserName = @UserName, PassWords = @PassWords,"
                                    + "FullName = @FullName, Email = @Email"
                                    + " where UserID = @UserID", conn);
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = user.userID;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.userName;
                cmd.Parameters.Add("@PassWords", SqlDbType.NVarChar).Value = user.passWords;
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.fullName;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.email;
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
        public bool Delete(string userID)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE from Users where UserID = @UserID ", conn);
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
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
        /*  */
    }
}
