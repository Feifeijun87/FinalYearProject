using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Drawing;

namespace AdaptiveLearningSystem
{
    public partial class StudHome : System.Web.UI.Page
    {

        static string studentID = "", tutNum;
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            //studentID = Session["studID"].ToString();
            Session["studID"] = "abc";
            if (!IsPostBack)
            {
                //course, coursename, tutNum, tutTitle, studID
                if (Session["studID"] != null)
                {
                    //studentID = Session["StudentID"].ToString();
                    studentID = "17WMR09512";
                    Session["StudentID"] = studentID;
                    conn.Open();
                    string sql = "SELECT A.CourseID,A.CourseName, A.TutorialNumber,A.ChapterName, A.ExpiryDate, A.NoOfQuestion,COUNT(z.AnswerID) AS 'Done Question' FROM( SELECT t.StartDate, t.ExpiryDate, t.TutorialID, t.TutorialNumber, t.ChapterName, c.CourseID, c.CourseName, t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium AS NoOfQuestion FROM  Tutorial t, Course c WHERE c.CourseID = t.CourseID AND t.Status = 1 GROUP BY t.StartDate, t.ExpiryDate, t.TutorialID, t.TutorialNumber, t.ChapterName, c.CourseID, c.CourseName, t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium)a LEFT JOIN CourseAvailable v ON A.CourseID = v.CourseID LEFT JOIN Intake i ON v.IntakeID = i.IntakeID LEFT JOIN TutorialGroup g ON g.TutorialGrpID = v.TutorialGrpID LEFT JOIN Student s ON s.IntakeID = i.IntakeID AND s.TutorialGroupID = g.TutorialGrpID LEFT JOIN TutorialCheck k ON s.StudentID = k.StudentID AND a.TutorialID = k.TutorialID LEFT JOIN Question q ON q.TutorialID = a.TutorialID LEFT JOIN StudAns z ON s.StudentID = z.StudentID AND z.QuestionID = q.QuestionID WHERE v.Status = 1 AND s.StudentID = @studID AND k.CheckID IS NULL group by A.TutorialID,A.StartDate, A.ExpiryDate, A.TutorialNumber,A.ChapterName,A.CourseID,A.CourseName, A.NoOfQuestion,k.CheckID";
                    SqlCommand cmdGetResult = new SqlCommand(sql, conn);
                    cmdGetResult.Parameters.AddWithValue("@studID", studentID);
                    DataTable dt = new DataTable();
                    cmdGetResult.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdGetResult;
                    //SqlDataReader dtr = cmdGetResult.ExecuteReader();
                    conn.Close();
                    conn.Open();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                        lblNoData.Text = "Available tutorials : ";
                    }
                    else
                    {
                        container.Visible = false;
                        lblNoData.Text = "No tutorial available";
                    }

                    conn.Close();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string courseID, coursename = "", tutNum, tutTitle = "", questDone;

            if (e.CommandName == "select") //edit
            {
                Label lblCourseID1 = new Label();
                lblCourseID1 = (Label)e.Item.FindControl("lblCourseID");
                courseID = lblCourseID1.Text;

                Label lblCoursename1 = new Label();
                lblCoursename1 = (Label)e.Item.FindControl("lblCoursename");
                coursename = lblCoursename1.Text;

                Label lblTutorial1 = new Label();
                lblTutorial1 = (Label)e.Item.FindControl("lblTutorial");
                tutNum = lblTutorial1.Text;

                Label lblTutName1 = new Label();
                lblTutName1 = (Label)e.Item.FindControl("lblTutName");
                tutTitle = lblTutName1.Text;

                Label lblDone1 = new Label();
                lblDone1 = (Label)e.Item.FindControl("lblDone");
                questDone = lblDone1.Text;



                Response.Redirect("AnsTut.aspx?tutNum=" + tutNum + "&courseID=" + courseID + "&coursename=" + coursename + "&chapname=" + tutTitle + "&questDone=" + questDone);

            }
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudHome.aspx");
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudProfile.aspx");
        }




        protected void ResultLinkButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect("StudResult.aspx");
        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }
    }
}