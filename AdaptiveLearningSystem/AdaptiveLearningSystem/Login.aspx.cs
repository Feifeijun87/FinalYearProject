using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace AdaptiveLearningSystem
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                StudentPanel.Visible = false;
                MainPanel.Visible = true;
                LecturerPanel.Visible = false;
            }


        }

        protected void btnStudent_Click(object sender, EventArgs e)
        {
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Successful');</script>");

            CategoryPanel.Visible = false;
            StudentPanel.Visible = true;
        }

        protected void btnLecturer_Click(object sender, EventArgs e)
        {
            CategoryPanel.Visible = false;
            LecturerPanel.Visible = true;


        }

        protected void btnLecturerSignIn_Click(object sender, EventArgs e)
        {
            if (txtLecturerID.Text.Equals("admin") && txtLecturerPassword.Text.Equals("admin"))
            {
                Session["admin"] = "admin";
                Response.Redirect("adminHome.aspx");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("prc_lec_details", conn);
                cmd.Parameters.AddWithValue("@Username", txtLecturerID.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtLecturerPassword.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader lecturerRead = cmd.ExecuteReader();
                if (!lecturerRead.HasRows)
                {
                    lblError.Style.Add("display", "inherit");
                    lblError.Text = "Invalid ID or Password.";
                }
                else
                {
                    if (lecturerRead.Read())
                    {
                        Session["lecturerID"] = lecturerRead.GetString(0).ToString();
                        Session["username"] = lecturerRead.GetString(1).ToString();
                        Session["lecPass"] = lecturerRead.GetString(2).ToString();
                        Session["lecName"] = lecturerRead.GetString(3).ToString();
                        Session["ICNo"] = lecturerRead.GetString(4).ToString();
                        Session["contactNo"] = lecturerRead.GetString(5).ToString();
                        Session["address"] = lecturerRead.GetString(6).ToString();
                        Session["email"] = lecturerRead.GetString(7).ToString();
                        Session["officeLoc"] = lecturerRead.GetString(8).ToString();
                        Session["lecTitle"] = lecturerRead.GetString(9).ToString();
                        Session["position"] = lecturerRead.GetString(10).ToString();
                        Session["lecFaculty"] = lecturerRead.GetString(12).ToString();
                        Session["facultyName"] = lecturerRead.GetString(13).ToString();
                        if (!lecturerRead.IsDBNull(lecturerRead.GetOrdinal("ProfilePic")))
                            Session["lecProfilePic"] = "true";
                        
                        Response.Redirect("LecHome.aspx");
                    }
                }
                conn.Close();
            }
        }


        protected void btnStudentSignIn_Click(object sender, EventArgs e)
        {
            if (txtStudentID.Text.Equals("admin") && txtStudentPassword.Text.Equals("admin"))
            {
                Session["admin"] = "admin";
                Response.Redirect("adminHome.aspx");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("prc_stud_details", conn);
                cmd.Parameters.AddWithValue("@StudentID", txtStudentID.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtStudentPassword.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader studentRead = cmd.ExecuteReader();
                if (!studentRead.HasRows)
                {
                    lblError.Style.Add("display", "inherit");
                    lblError.Text = "Invalid ID or Password.";
                }
                else
                {
                    if (studentRead.Read())
                    {
                        Session["studID"] = studentRead.GetString(0).ToString();
                        Session["studPass"] = studentRead.GetString(1).ToString();
                        Session["studName"] = studentRead.GetString(2).ToString();
                        Session["studICNo"] = studentRead.GetString(3).ToString();
                        Session["studContact"] = studentRead.GetString(4).ToString();
                        Session["studAddress"] = studentRead.GetString(5).ToString();
                        Session["studEmail"] = studentRead.GetString(6).ToString();
                        Session["studGroup"] = studentRead.GetString(8).ToString();
                        Session["studProgramme"] = studentRead.GetString(10).ToString();
                        Session["studFacultyID"] = studentRead.GetString(11).ToString();
                        Session["studFacultyName"] = studentRead.GetString(12).ToString();
                        if (!studentRead.IsDBNull(studentRead.GetOrdinal("ProfilePic")))
                            Session["studProfilePic"] = "true";
                        Response.Redirect("StudHome.aspx");
                    }
                }
                conn.Close();
            }
        }

        protected void lbtnStudForgetPass_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentForgetPass.aspx");
        }

        protected void lbtnLecForgetPass_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecForgetPass.aspx");
        }
    }
}