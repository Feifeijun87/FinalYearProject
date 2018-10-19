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
    public partial class LecCourse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["lecturerID"] != null)
                {
                    // put inside !ispostback , abo refresh will get error
                    // the sql use your own de also can
                    lblUserName.Text = Session["lecName"].ToString();
                    SqlCommand cmd = new SqlCommand("prc_get_course_enrolled", conn);
                    cmd.Parameters.AddWithValue("@LecturerID", Session["lecturerID"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    //bind data to repeater, if else i use to check sql got row or not nia
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
            if (e.CommandName == "select")
            {

                Label lblCourseName = new Label();
                lblCourseName = (Label)e.Item.FindControl("lblCourseName");
                string title = lblCourseName.Text;
                string[] splitted = title.Split('-');
                Response.Redirect("TutorialList.aspx?course=" + splitted[0].Trim() + "&name=" + splitted[1].ToString());
            }
            else
            {

                Label lblCourseName = new Label();
                lblCourseName = (Label)e.Item.FindControl("lblCourseName");
                string title = lblCourseName.Text;
                string[] splitted = title.Split('-');
                Response.Redirect("DropCourse.aspx?course=" + splitted[0].Trim());
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
            Response.Redirect("LecResultHome.aspx");
        }
        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }

        protected void btnEnroll_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseList.aspx");
        }
    }
}