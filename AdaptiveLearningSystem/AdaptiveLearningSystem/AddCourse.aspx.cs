using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace AdaptiveLearningSystem
{
    public partial class AddCourse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    hideErrorLbl();
                }
            }
        }

        protected Boolean checkEmpty()
        {
            if (txtCourseID.Text.Equals(String.Empty))
            {
                lblErrorCourseID.Visible = true;
                lblErrorCourseID.Text = "This field cannot be empty.";
                return false;
            }
            else if (txtCourseName.Text.Equals(String.Empty))
            {
                lblErrorCourseName.Visible = true;
                lblErrorCourseName.Text = "This field cannot be empty.";
                return false;
            }

            else if (ddlCreditHour.SelectedIndex == 0)
            {
                lblErrorCreditHours.Visible = true;
                lblErrorCreditHours.Text = "This field cannot be empty.";
                return false;
            }
            return true;

        }

        protected void hideErrorLbl()
        {
            lblErrorCourseID.Visible = false;
            lblErrorCourseName.Visible = false;
            lblErrorCreditHours.Visible = false;
        }

        protected Boolean checkFormat()
        {
            string courseID = txtCourseID.Text;
            string courseName = txtCourseName.Text;
            string creditHour = ddlCreditHour.SelectedValue.Trim();

            if (!Regex.IsMatch(courseID.Trim(), "^[A-Za-z]{4}[0-9]{4}$"))
            {
                lblErrorCourseID.Visible = true;
                lblErrorCourseID.Text = "Invalid Course ID Format.";
                return false;
            }

            else
            {
                foreach(Char c in courseName)
                {
                    if (!Char.IsLetter(c) && !Char.IsWhiteSpace(c))
                    {
                        lblErrorCourseName.Visible = true;
                        lblErrorCourseName.Text = "Course Name cannot contain numeric character.";
                        return false;
                    }
                }
                
            }

            char lastChar = courseID[courseID.Length - 1];

            if(int.Parse(lastChar.ToString())!=int.Parse(creditHour))
            {
                lblErrorCreditHours.Visible = true;
                lblErrorCreditHours.Text = "Credit Hour(s) does not match with course ID";
                return false;
            }

            return true;
        }

        protected Boolean checkDuplicate()
        {
            conn.Open();
            string sql = "SELECT CourseID FROM [Course] WHERE CourseID = @courseID";
            SqlCommand cmdCheckID = new SqlCommand(sql, conn);
            cmdCheckID.Parameters.AddWithValue("@courseID", txtCourseID.Text.Trim());
            SqlDataReader dt = cmdCheckID.ExecuteReader();
            if (dt.HasRows)
            {
                lblErrorCourseID.Visible = true;
                lblErrorCourseID.Text = "Course ID existed.";
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            hideErrorLbl();
            string courseID = txtCourseID.Text.Trim();
            string courseName = txtCourseName.Text.Trim();
            string creditHour = ddlCreditHour.SelectedValue.Trim();

            if (checkEmpty() == true && checkFormat() == true && checkDuplicate() == true)
            {
                SqlCommand cmd = new SqlCommand("prc_insert_new_course", conn);
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                cmd.Parameters.AddWithValue("@CourseName", courseName);
                cmd.Parameters.AddWithValue("@CreditHour", int.Parse(creditHour));

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter insertStudent = new SqlDataAdapter();
                insertStudent.UpdateCommand = cmd;
                insertStudent.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Course Added Successfully'); window.location.href='AddCourse.aspx';", true);
            }
        }
        

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentRegister.aspx");
        }

        protected void btnAddLecturer_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecturerRegister.aspx");
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminHome.aspx");
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCourse.aspx");
        }

        protected void btnActivateCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivateCourse.aspx");
        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {

            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }
    }
}