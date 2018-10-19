using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace AdaptiveLearningSystem
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ASUS\SQLSERVER;Initial Catalog=fyp;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            int getp=levenshtein("ant", "audnt");

            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 10; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            string mystring = "17WMR09522";
            //Label1.Text = builder.ToString();
            if (Regex.IsMatch(mystring, "^[0-9]{2}[A-Za-z]{3}[0-9]{5}$"))
            {
                Label1.Text = "qwer";
            }
            else
                Label1.Text = "rewq";

        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
          

        }

        private int levenshtein(string a, string b)
        {

            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            int cost;
            int[,] d = new int[a.Length + 1, b.Length + 1];
            int min1;
            int min2;
            int min3;

            for (int i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (int i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (int i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (int j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = (a[i - 1] != b[j - 1]) ? 1 : 0;

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void TutorialLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection connection = null;

                FileUpload img = (FileUpload)FileUpload1;
                Byte[] imgByte = null;
                if (img.HasFile && img.PostedFile != null)
                {
                    //To create a PostedFile
                    HttpPostedFile File = FileUpload1.PostedFile;
                    //Create byte Array with file len
                    imgByte = new Byte[File.ContentLength];
                    //force the control to load data in array
                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                }
                // Insert the employee name and image into db
                string conn = ConfigurationManager.ConnectionStrings["fyp"].ConnectionString;
                connection = new SqlConnection(conn);

                connection.Open();
                string sql = "UPDATE Lecturer SET ProfilePic = @eimg WHERE LecturerID='L1'";
                SqlCommand cmd = new SqlCommand(sql, connection);
        
                cmd.Parameters.AddWithValue("@eimg", imgByte);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();




        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string id = "L1";
            Image1.ImageUrl="~/profilePic.ashx?id=" + id+"&type=lec";
        }
    }
}