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
    public partial class LecCourse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["lecturerID"] != null)
                {
                    lblUserName.Text = Session["lecName"].ToString();
                    SqlCommand cmd = new SqlCommand("prc_get_course_enrolled", conn);
                    cmd.Parameters.AddWithValue("@LecturerID", Session["lecturerID"].ToString());
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
            if (e.CommandName == "select")
            {
                
                Label lblCourseName = new Label();
                lblCourseName = (Label)e.Item.FindControl("lblCourseName");
                string title = lblCourseName.Text;
                string[] splitted = title.Split(' ');
                Response.Redirect("TutorialList.aspx?course=" + splitted[0].Trim());
            }
            else
            {

                Label lblCourseName = new Label();
                lblCourseName = (Label)e.Item.FindControl("lblCourseName");
                string title = lblCourseName.Text;
                string[] splitted = title.Split(' ');
                Response.Redirect("DropCourse.aspx?course=" + splitted[0].Trim());
            }
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {
        }


        protected void TutorialLinkButton_Click(object sender, EventArgs e)
        {

        }
        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }

        protected void btnEnroll_Click(object sender, EventArgs e)
        {

        }

        protected void btnDrop_Click(object sender, EventArgs e)
        {

        }
    }
}