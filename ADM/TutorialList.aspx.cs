using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADM
{
    public partial class TutorialList : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["lecturerID"] != null)
                {
                    lblUserName.Text = Session["lecName"].ToString();
                    SqlCommand cmd = new SqlCommand("prc_get_tutorial_list", conn);
                    cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["course"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        NoResultPanel.Visible = false;
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                    }
                    else
                    {
                        container.Visible = false;
                        filler.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "activate")
            {
                Label lblCourseName = new Label();
                lblCourseName = (Label)e.Item.FindControl("lblChapterName");
                string title = lblCourseName.Text;
                string[] splitted = title.Split(':');
                Session["chapterName"] = splitted[1].Trim();
                popoutActivate.Show();

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

        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {

        }
        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }

        protected void btnCreateTutorial_Click(object sender, EventArgs e)
        {

        }
        
            protected void btnActivateTutorial_Click(object sender, EventArgs e)
        {
            string days = txtDays.Text.Trim();
            Boolean check = true;
            for(int i=0; i < days.Length; i++)
            {
                if (!Char.IsDigit(days[i]))
                    check = false;
            }
            if (check == true)
            {

                string startDate = DateTime.Now.ToString("MM/dd/yyyy");
                string expiryDate = DateTime.Now.AddDays(Double.Parse(days)).ToString("MM/dd/yyyy");
                SqlCommand cmd = new SqlCommand("prc_activate_tutorial", conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["course"].ToString());
                cmd.Parameters.AddWithValue("@ChapterName", Session["chapterName"].ToString());
                cmd.Parameters.AddWithValue("@Duration", int.Parse(days));
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter writePass = new SqlDataAdapter();
                writePass.UpdateCommand = cmd;
                writePass.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Tutorial Activated Successfully'); window.location.href='LecHome.aspx';", true);
                popoutActivate.Hide();
            }
            else
                lblActvateError.Text = "Only digit is acceptable";
            
        }
    }
}