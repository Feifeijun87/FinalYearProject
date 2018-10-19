using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ADM
{
    public class profilePic : IHttpHandler
    {
        //line 56 there

        public void ProcessRequest(HttpContext context)
        {
            string username, userType;
            if (context.Request.QueryString["id"] != null)
            {
                username = context.Request.QueryString["id"];
                userType = context.Request.QueryString["type"];
            }
            else
                throw new ArgumentException("No parameter specified");

            context.Response.ContentType = "image/jpeg";
            Stream strm = ShowEmpImage(username,userType);
            byte[] buffer = new byte[4096];
            int byteSeq = strm.Read(buffer, 0, 4096);

            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = strm.Read(buffer, 0, 4096);
            }
            //context.Response.BinaryWrite(buffer);
        }

        public Stream ShowEmpImage(string username, string userType)
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
            object img = null;
            if (userType.Equals("lec"))
            {
                SqlCommand cmd = new SqlCommand("prc_get_lec_profile_pic", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                img = cmd.ExecuteScalar();
            }
            else
            {
                //insert the retrieve image sql for student
            }
            try
            {
                return new MemoryStream((byte[])img);
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}