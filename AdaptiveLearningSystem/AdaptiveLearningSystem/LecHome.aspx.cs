using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using System.Web.Configuration;
using System.Web.Security;
using System.Data.SqlClient;

namespace AdaptiveLearningSystem
{
    public partial class LecHome : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = Session["lecName"].ToString();
                    SqlCommand cmd = new SqlCommand("prc_tutorial_track", conn);
                    cmd.Parameters.AddWithValue("@lecturerID", Session["lecturerID"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        NoResultPanel.Visible = false;
                        rptrTutorialTrack.DataSource = dt;
                        rptrTutorialTrack.DataBind();


                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }



        }

        protected int calculatePercentage(int numerator, int denominator)
        {

            double result = Math.Round(((double)numerator / (double)denominator) * 100);
            int percentage = int.Parse(result.ToString());

            return percentage;
        }
        protected void rptrTutorialTrack_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

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
    }
}