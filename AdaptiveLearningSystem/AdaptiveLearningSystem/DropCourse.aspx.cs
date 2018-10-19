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
    public partial class DropCourse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["lecturerID"] != null)
                {
                    hideErrorLbl();
                    lblUserName.Text = Session["lecName"].ToString();
                    SqlCommand cmd = new SqlCommand("prc_get_intake_enrolled_course", conn);
                    cmd.Parameters.AddWithValue("@lecturerID", Session["lecturerID"].ToString());
                    cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["course"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlProgramme.DataTextField = "IntakeID";
                        ddlProgramme.DataValueField = "IntakeID";
                        ddlProgramme.DataSource = dt;
                        ddlProgramme.DataBind();
                        ddlProgramme.Items.Insert(0, new ListItem("Please Select", String.Empty));
                        ddlProgramme.SelectedIndex = 0;
                        ddlProgramme.Enabled = true;

                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }


        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecProfile.aspx");
        }

        protected void TutorialLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseList.aspx");
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecHome.aspx");
        }

        protected void MyCourseLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecCourse.aspx");
        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }

        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecResultHome.aspx");
        }
        protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("prc_get_group_enrolled_course", conn);
            cmd.Parameters.AddWithValue("@lecturerID", Session["lecturerID"].ToString());
            cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["course"].ToString());
            cmd.Parameters.AddWithValue("@IntakeID", ddlProgramme.SelectedValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTutorialGroup.DataTextField = "TutorialGrpName";
                ddlTutorialGroup.DataValueField = "TutorialGrpID";
                ddlTutorialGroup.DataSource = dt;
                ddlTutorialGroup.DataBind();
                ddlTutorialGroup.Items.Insert(0, new ListItem("Please Select", String.Empty));
                ddlTutorialGroup.SelectedIndex = 0;
                ddlTutorialGroup.Enabled = true;
            }
        }

        protected void btnDrop_Click(object sender, EventArgs e)
        {
            hideErrorLbl();
            if (checkEmpty() == true)
            {

                SqlCommand cmd = new SqlCommand("prc_drop_course", conn);
                cmd.Parameters.AddWithValue("@lecturerID", Session["lecturerID"].ToString());
                cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["course"].ToString());
                cmd.Parameters.AddWithValue("@IntakeID", ddlProgramme.SelectedValue);
                cmd.Parameters.AddWithValue("@TutorialGrpID", ddlTutorialGroup.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter writePass = new SqlDataAdapter();
                writePass.UpdateCommand = cmd;
                writePass.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Course Drop Successfully'); window.location.href='LecHome.aspx';", true);

            }
        }
        protected void hideErrorLbl()
        {
            lblErrorGroup.Visible = false;
            lblerrorProgramme.Visible = false;
        }

        protected Boolean checkEmpty()
        {
            if (ddlProgramme.SelectedIndex == 0)
            {
                lblerrorProgramme.Visible = true;
                lblerrorProgramme.Text = "This field cannot be empty.";
                return false;
            }
            else if (ddlTutorialGroup.SelectedIndex == 0)
            {
                lblErrorGroup.Visible = true;
                lblErrorGroup.Text = "This field cannot be empty.";
                return false;
            }
            return true;

        }
    }
}