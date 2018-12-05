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
    public partial class LecResultHome : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        static string sql,lecID, intakeID, courseID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
                {
                    lecID = Session["lecturerID"].ToString();
                    lblUserName.Text = Session["lecName"].ToString();
                    sql = "SELECT DISTINCT c.CourseID + c.CourseName AS Course FROM CourseAvailable v, Course c WHERE v.Status = 1 AND v.LecturerID = @lecID AND v.CourseID = c.CourseID GROUP BY c.CourseID + c.CourseName";
                    SqlCommand cmdGetIntake = new SqlCommand(sql, conn);
                    cmdGetIntake.Parameters.AddWithValue("@lecID", Session["lecturerID"].ToString());
                    DataTable dt = new DataTable();
                    cmdGetIntake.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdGetIntake;
                    conn.Open();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblCourse.Visible = true;
                        ddlCourse.Visible = true;
                        lblNoIntake.Visible = false;

                        ddlCourse.DataTextField = "Course";
                        ddlCourse.DataValueField = "Course";
                        ddlCourse.DataSource = dt;
                        ddlCourse.DataBind();
                        ddlCourse.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlCourse.SelectedIndex = 0;

                    }
                    else
                    {
                        lblCourse.Visible = false;
                        ddlCourse.Visible = false;
                        lblNoIntake.Visible = true;
                    }
                    conn.Close();
                }

            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblNoIntake.Visible = false;
            lblNoTutorial.Visible = false;
            lblIntake.Visible = false;
            ddlIntake.Visible = false;
            lblTutorial.Visible = false;
            ddlTutorial.Visible = false;
            lblTutGroup.Visible = false;
            ddlTutGroup.Visible = false;
            lblNoTutGroup.Visible = false;
            btnDone.Visible = false;

            if (lblNoIntake.Visible == false)
            {
                ddlCourse.SelectedIndex = 0;
            }
           
            //Label1.Text = "";
            //Label1.Text = (radReportSelect.SelectedValue).ToString();

        }

        protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
        {
            intakeID = ddlIntake.SelectedValue;

            sql = "SELECT DISTINCT  CONVERT(varchar,'T') + CONVERT(varchar,t.TutorialNumber ) + ' ' + t.ChapterName AS Tutorial  FROM Tutorial t WHERE  t.CourseID = @courseID AND t.Status = 1";
            SqlCommand cmdGetCourse = new SqlCommand(sql, conn);
            cmdGetCourse.Parameters.AddWithValue("@courseID", courseID);
            DataTable dt = new DataTable();
            cmdGetCourse.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmdGetCourse;
            conn.Close();
            conn.Open();
            sda.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblNoTutorial.Visible = false;
                lblTutorial.Visible = true;
                ddlTutorial.Visible = true;
                ddlTutorial.DataTextField = "Tutorial";
                ddlTutorial.DataValueField = "Tutorial";
                ddlTutorial.DataSource = dt;
                ddlTutorial.DataBind();
                ddlTutorial.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlTutorial.SelectedIndex = 0;
            }
            else
            {
                lblNoTutorial.Visible = true;
            }
            conn.Close();

        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sql here
            sql = "SELECT v.IntakeID  FROM CourseAvailable v WHERE v.Status = 1 AND v.LecturerID = @lecID AND v.CourseID = @courseID GROUP BY v.IntakeID";
            SqlCommand cmdGetTut = new SqlCommand(sql, conn);
            string course = ddlCourse.SelectedItem.Text;
            char delimiters = ' ';
            string[] splitArray = course.Split(delimiters);
            courseID = splitArray[0];
            cmdGetTut.Parameters.AddWithValue("@lecID", Session["lecturerID"].ToString());
            cmdGetTut.Parameters.AddWithValue("@courseID", courseID);
            DataTable dt = new DataTable();
            cmdGetTut.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmdGetTut;
            conn.Close();
            conn.Open();
            sda.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblIntake.Visible = true;
                ddlIntake.Visible = true;
                ddlIntake.DataTextField = "IntakeID";
                ddlIntake.DataValueField = "IntakeID";
                ddlIntake.DataSource = dt;
                ddlIntake.DataBind();
                ddlIntake.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlIntake.SelectedIndex = 0;
                lblNoTutorial.Visible = false;

            }
            else
            {
                lblNoTutorial.Visible = true;
            }
        }
        protected void ddlTutGroup_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddlTutGroup.SelectedIndex != 0)
            {
                btnDone.Visible = true;
            }
            else
            {
                btnDone.Visible = false;
            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            if (radReportSelect.SelectedValue == "tutGroup") // std perf by tut grp
            {
                Response.Redirect("reportStdPerfbyTutGroup.aspx?intake=" + ddlIntake.SelectedValue.ToString() + "&course=" + ddlCourse.SelectedValue.ToString() + "&tutorial= " + ddlTutorial.SelectedValue.ToString() + "&tutGroup=" + ddlTutGroup.SelectedValue.ToString());
            }
            else if (radReportSelect.SelectedValue == "quest") //quest perf by prog
            {
                Response.Redirect("reportQuestbyProg.aspx?intake=" + ddlIntake.SelectedValue.ToString() + "&course=" + ddlCourse.SelectedValue.ToString() + "&tutorial= " + ddlTutorial.SelectedValue.ToString());
            }
            else if(radReportSelect.SelectedValue == "tutProg") //std perf by prog
            {
                Response.Redirect("reportStdPerfbyProg.aspx?intake=" + ddlIntake.SelectedValue.ToString() + "&course=" + ddlCourse.SelectedValue.ToString() + "&tutorial= " + ddlTutorial.SelectedValue.ToString());
            }
            else if(radReportSelect.SelectedValue== "questGroup") //quest perf by tut grp
            {
                Response.Redirect("reportQuestbyTutGroup.aspx?intake=" + ddlIntake.SelectedValue.ToString() + "&course=" + ddlCourse.SelectedValue.ToString() + "&tutorial= " + ddlTutorial.SelectedValue.ToString() + "&tutGroup=" + ddlTutGroup.SelectedValue.ToString());
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radReportSelect.SelectedValue == "tutGroup" || radReportSelect.SelectedValue == "questGroup")
            {
                lblNoTutorial.Visible = false;
                //sql here
                sql = "SELECT g.TutorialGrpName,g.TutorialGrpID FROM CourseAvailable v,TutorialGroup g WHERE g.TutorialGrpID = v.TutorialGrpID AND v.Status = 1 AND v.CourseID = @CourseID AND v.LecturerID = @LecturerID AND v.IntakeID = @IntakeID GROUP BY g.TutorialGrpName,g.TutorialGrpID";
                SqlCommand cmdGetTutGroup = new SqlCommand(sql, conn);
                cmdGetTutGroup.Parameters.AddWithValue("@CourseID", courseID);
                cmdGetTutGroup.Parameters.AddWithValue("@LecturerID", Session["lecturerID"].ToString());
                cmdGetTutGroup.Parameters.AddWithValue("@IntakeID", intakeID);
                DataTable dt = new DataTable();
                cmdGetTutGroup.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmdGetTutGroup;
                conn.Close();
                conn.Open();
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblTutGroup.Visible = true;
                    ddlTutGroup.Visible = true;
                    ddlTutGroup.DataTextField = "TutorialGrpName";
                    ddlTutGroup.DataValueField = "TutorialGrpID";
                    ddlTutGroup.DataSource = dt;
                    ddlTutGroup.DataBind();
                    ddlTutGroup.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlTutGroup.SelectedIndex = 0;

                    lblTutGroup.Visible = true;
                    ddlTutGroup.Visible = true;
                    btnDone.Visible = false;

                }
                else
                {
                    lblTutGroup.Visible = false;
                    ddlTutGroup.Visible = false;
                    lblNoTutGroup.Visible = true;
                }
                conn.Close();
            }
            else
            {
                lblTutGroup.Visible = false;
                ddlTutGroup.Visible = false;
                btnDone.Visible = true;
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
    }
}