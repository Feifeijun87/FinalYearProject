using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace AdaptiveLearningSystem
{
    public partial class TutorialList : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
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
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            

            lblCourseName.Text = Request.QueryString["course"] + " " + Request.QueryString["name"];
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
                Calendar2.Visible = false;
                Calendar1.Visible = false;
                popoutActivate.Show();

            }
            else if (e.CommandName == "edit")
            {
                Label lblCourseName = new Label();
                lblCourseName = (Label)e.Item.FindControl("lblChapterName");
                string title = lblCourseName.Text;
                string[] splitted = title.Split(':');
                string chapterName = splitted[1].Trim();
                splitted = splitted[0].Split(' ');
                string tutNum = splitted[1].Trim();
                string courseID = Request.QueryString["course"].ToString();
                string coursename = Request.QueryString["name"].ToString();

                Response.Redirect("EditTut.aspx?course=" + courseID +"&coursename=" + coursename + "&tutNum=" + tutNum + "&tutTitle=" + chapterName);

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

        protected void btnCreateTutorial_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateTut.aspx");
        }

        protected void btnActivateTutorial_Click(object sender, EventArgs e)
        {

            if (!txtEndDate.Text.Equals(String.Empty) && !txtStartDate.Text.Equals(String.Empty))
            {
                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);
                int days = int.Parse((endDate - startDate).TotalDays.ToString());
                if (days > 0)
                {

                    SqlCommand cmd = new SqlCommand("prc_activate_tutorial", conn);
                    cmd.Parameters.AddWithValue("@StartDate", Session["startDate"].ToString());
                    cmd.Parameters.AddWithValue("@ExpiryDate", Session["endDate"].ToString());
                    cmd.Parameters.AddWithValue("@CourseID", Request.QueryString["course"].ToString());
                    cmd.Parameters.AddWithValue("@ChapterName", Session["chapterName"].ToString());
                    cmd.Parameters.AddWithValue("@Duration", days);
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
                {
                    lblActvateError.Visible = true;
                    lblActvateError.Text = "Invalid Date Range";
                }
            }
            else
            {
                lblActvateError.Visible = true;
                lblActvateError.Text = "Date cannot be blank";
            }
        }

        protected void btnActivateCancel_Click(object sender, EventArgs e)
        {
            popoutActivate.Hide();
        }

        protected void btnAddTut_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateTut.aspx");
        }

        protected void btnStartDate_Click(object sender, EventArgs e)
        {
            if (Calendar1.Visible == true)
                Calendar1.Visible = false;
            if (Calendar2.Visible == false)
                Calendar2.Visible = true;
            else
                Calendar2.Visible = false;
        }

        protected void btnEndDate_Click(object sender, EventArgs e)
        {
            if (Calendar2.Visible == true)
                Calendar2.Visible = false;
            if (Calendar1.Visible == false)
                Calendar1.Visible = true;
            else
                Calendar1.Visible = false;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Session["endDate"] = Calendar1.SelectedDate.ToString("MM/dd/yyyy");
            txtEndDate.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
            Calendar1.Visible = false;
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            Session["startDate"] = Calendar2.SelectedDate.ToString("MM/dd/yyyy");
            txtStartDate.Text = Calendar2.SelectedDate.ToString("dd/MM/yyyy");
            Calendar2.Visible = false;
        }
    }
}