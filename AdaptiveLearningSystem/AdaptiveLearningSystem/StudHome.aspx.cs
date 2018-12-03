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

        static string tutNum;
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            //studentID = Session["studID"].ToString();


            //course, coursename, tutNum, tutTitle, studID
            if (Session["studID"] != null)
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = Session["studName"].ToString();
                    //studentID = Session["StudentID"].ToString();
                    conn.Open();
                    string sql = @"SELECT A.CourseID,A.CourseName, A.TutorialNumber,A.ChapterName, A.ExpiryDate, A.NoOfQuestion, COUNT(n.AnswerID) AS 'Done Question'
                                    FROM(
                                    SELECT t.StartDate, t.ExpiryDate, t.TutorialID, t.TutorialNumber, t.ChapterName, c.CourseID, c.CourseName, t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium AS NoOfQuestion
                                    FROM  Tutorial t, Course c WHERE c.CourseID = t.CourseID AND t.Status = 1 AND CONVERT(Date, GetDate()) BETWEEN CONVERT(date, t.StartDate) AND CONVERT(date, t.ExpiryDate)
                                    GROUP BY t.StartDate, t.ExpiryDate, t.TutorialID, t.TutorialNumber, t.ChapterName, c.CourseID, c.CourseName, t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium)a
                                    LEFT JOIN CourseAvailable v ON A.CourseID = v.CourseID
                                    LEFT JOIN Intake i ON v.IntakeID = i.IntakeID
                                    LEFT JOIN TutorialGroup g ON v.TutorialGrpID = g.TutorialGrpID
                                    LEFT JOIN Student s ON i.IntakeID = s.IntakeID AND s.TutorialGroupID = g.TutorialGrpID
                                    LEFT JOIN TutorialCheck k ON k.StudentID = s.StudentID AND k.TutorialID = A.TutorialID  AND k.CheckID IS NULL
                                        LEFT JOIN Question q ON q.TutorialID = A.TutorialID
                                    LEFT JOIN StudAns n ON s.StudentID = n.StudentID AND n.QuestionID = q.QuestionID
                                    WHERE s.StudentID = @studID
                                    GROUP BY A.CourseID,A.CourseName, A.TutorialNumber,A.ChapterName, A.ExpiryDate, A.NoOfQuestion";
                    SqlCommand cmdGetResult = new SqlCommand(sql, conn);
                    cmdGetResult.Parameters.AddWithValue("@studID", Session["studID"].ToString());
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
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string courseID, coursename = "", tutNum, tutTitle = "", questDone, ttlQuest;
            int numQD, numTQ;
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

                Label lbltotalquest1 = new Label();
                lbltotalquest1 = (Label)e.Item.FindControl("lbltotalquest");
                ttlQuest = lbltotalquest1.Text;

                numQD = int.Parse(questDone);
                numTQ = int.Parse(ttlQuest);

                if(numTQ == numQD)
                {
                    Session["load"] = "once";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Tutorial already completed!'); window.location.href='StudHome.aspx';", true);

                }
                else
                {
                    Response.Redirect("AnsTut.aspx?tutNum=" + tutNum + "&courseID=" + courseID + "&coursename=" + coursename + "&chapname=" + tutTitle + "&questDone=" + questDone);

                }

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
        protected int calculatePercentage(int numerator, int denominator)
        {

            double result = Math.Round(((double)numerator / (double)denominator) * 100);
            int percentage = int.Parse(result.ToString());

            return percentage;
        }
    }
}