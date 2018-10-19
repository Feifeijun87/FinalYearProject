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
        static string sql,  intakeID, courseID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
                {
                    sql = "SELECT v.IntakeID FROM CourseAvailable v WHERE v.Status = 1 AND v.LecturerID = @lecID GROUP BY v.IntakeID";
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
                        lblIntake.Visible = true;
                        ddlIntake.Visible = true;
                        lblNoIntake.Visible = false;

                        ddlIntake.DataTextField = "IntakeID";
                        ddlIntake.DataValueField = "IntakeID";
                        ddlIntake.DataSource = dt;
                        ddlIntake.DataBind();
                        ddlIntake.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlIntake.SelectedIndex = 0;

                    }
                    else
                    {
                        lblIntake.Visible = false;
                        ddlIntake.Visible = false;
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
            lblCourse.Visible = false;
            ddlCourse.Visible = false;
            lblTutorial.Visible = false;
            ddlTutorial.Visible = false;
            lblTutGroup.Visible = false;
            ddlTutGroup.Visible = false;
            lblNoTutGroup.Visible = false;
            ddlIntake.SelectedIndex = 0;
            btnDone.Visible = false;
            //Label1.Text = "";
            //Label1.Text = (radReportSelect.SelectedValue).ToString();

        }

        protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
        {
            intakeID = ddlIntake.SelectedValue;
            sql = " SELECT DISTINCT c.CourseID + ' ' + c.CourseName AS Course FROM CourseAvailable a, Course c WHERE a.IntakeID = @intakeID AND a.LecturerID = @lecID AND a.CourseID = c.CourseID AND a.Status =1";
            SqlCommand cmdGetCourse = new SqlCommand(sql, conn);
            cmdGetCourse.Parameters.AddWithValue("@intakeID", intakeID);
            cmdGetCourse.Parameters.AddWithValue("@lecID", Session["lecturerID"].ToString());
            DataTable dt = new DataTable();
            cmdGetCourse.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmdGetCourse;
            conn.Close();
            conn.Open();
            sda.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblCourse.Visible = true;
                ddlCourse.Visible = true;

                ddlCourse.DataTextField = "Course";
                ddlCourse.DataValueField = "Course";
                ddlCourse.DataSource = dt;
                ddlCourse.DataBind();
                ddlCourse.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlCourse.SelectedIndex = 0;
                lblNoIntake.Visible = false;
            }
            else
            {
                //lblNoIntake.Visible = true;
            }
            conn.Close();

        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sql here
            sql = "SELECT DISTINCT  CONVERT(varchar,'T') + CONVERT(varchar,t.TutorialNumber ) + ' ' + t.ChapterName AS Tutorial FROM Tutorial t WHERE  t.CourseID = @courseID AND t.Status = 1";
            SqlCommand cmdGetTut = new SqlCommand(sql, conn);
            string course = ddlCourse.SelectedItem.Text;
            char delimiters = ' ';
            string[] splitArray = course.Split(delimiters);
            courseID = splitArray[0];

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
            if (radReportSelect.SelectedValue == "tutGroup")
            {
                Response.Redirect("reportTutGroup.aspx?intake=" + ddlIntake.SelectedValue.ToString() + "&course=" + ddlCourse.SelectedValue.ToString() + "&tutorial= " + ddlTutorial.SelectedValue.ToString() + "&tutGroup=" + ddlTutGroup.SelectedValue.ToString());
                //Label1.Text =" intake = " + ddlIntake.SelectedValue.ToString() + " & course = " + ddlCourse.SelectedValue.ToString() +" & tutorial = " + ddlTutorial.SelectedValue.ToString() + " & tutGroup = " + ddlTutGroup.SelectedValue.ToString();


            }
            else if (radReportSelect.SelectedValue == "quest")
            {
                Response.Redirect("reportQuest.aspx?intake=" + ddlIntake.SelectedValue.ToString() + "&course=" + ddlCourse.SelectedValue.ToString() + "&tutorial= " + ddlTutorial.SelectedValue.ToString());
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radReportSelect.SelectedValue == "tutGroup")
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
            else if (radReportSelect.SelectedValue == "quest")
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