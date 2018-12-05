using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace AdaptiveLearningSystem
{
    public partial class ActivateCourse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] != null)
                {
                    hideErrorLbl();
                    SqlCommand cmd = new SqlCommand("prc_get_course_admin", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlCourse.DataTextField = "CourseID";
                        ddlCourse.DataValueField = "CourseID";
                        ddlCourse.DataSource = dt;
                        ddlCourse.DataBind();
                        ddlCourse.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlCourse.SelectedIndex = 0;

                    }

                    cmd = new SqlCommand("prc_get_intake_list", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlIntake.DataTextField = "IntakeID";
                        ddlIntake.DataValueField = "IntakeID";
                        ddlIntake.DataSource = dt;
                        ddlIntake.DataBind();
                        ddlIntake.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlIntake.SelectedIndex = 0;

                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected Boolean checkEmpty()
        {
            if (ddlCourse.SelectedIndex == 0)
            {
                lblErrorCourse.Visible = true;
                lblErrorCourse.Text = "This field cannot be empty.";
                return false;
            }
            else if (ddlIntake.SelectedIndex == 0)
            {
                lblErrorIntake.Visible = true;
                lblErrorIntake.Text = "This field cannot be empty.";
                return false;
            }
            return true;

        }

        protected void hideErrorLbl()
        {
            lblErrorCourse.Visible = false;
            lblErrorIntake.Visible = false;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            hideErrorLbl();
            string courseID = ddlCourse.SelectedValue.Trim();
            string IntakeID = ddlIntake.SelectedValue.Trim();
            int rowCount = 0;


            if (checkEmpty() == true)
            {
                SqlConnection conn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
                conn.Open();
                string sql = "SELECT COUNT(CourseAvaID) FROM [CourseAvailable]";
                SqlCommand cmdGetRowCount = new SqlCommand(sql, conn);
                SqlDataReader dt = cmdGetRowCount.ExecuteReader();
                if (dt.HasRows)
                    while (dt.Read())
                        rowCount = dt.GetInt32(0);

                conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand("prc_get_tutorialGroup", conn);
                cmd.Parameters.AddWithValue("@IntakeID", IntakeID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dtr = cmd.ExecuteReader();
                while (dtr.Read())
                {
                    rowCount += 1;
                    SqlCommand cmd2 = new SqlCommand("prc_insert_new_courseAvailable", conn2);
                    cmd2.Parameters.AddWithValue("@CourseAvaID", "CAV" + rowCount.ToString());
                    cmd2.Parameters.AddWithValue("@IntakeID", IntakeID);
                    cmd2.Parameters.AddWithValue("@TutorialGrpID", dtr.GetString(0).Trim());
                    cmd2.Parameters.AddWithValue("@CourseID", courseID);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    conn2.Open();
                    SqlDataAdapter insertCourseAva = new SqlDataAdapter();
                    insertCourseAva.UpdateCommand = cmd2;
                    insertCourseAva.UpdateCommand.ExecuteNonQuery();
                    cmd2.Dispose();
                    conn2.Close();
                }
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