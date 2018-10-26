using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;

namespace AdaptiveLearningSystem
{
    public partial class reportQuest : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        static string tutGroupID, lecID, intakeID, tutID, courseID, course, tutGroup, tutorial, tutNum, studentID, coursename, tutTitle;

        protected void btnEnroll_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecResultHome.aspx");
        }

        protected void lblBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecResultHome.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    Panel2.Visible = true;
                    Panel2.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=TutQuestResult.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Panel2.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = Session["lecName"].ToString();

                    intakeID = Request.QueryString["intake"].ToString();//intakeID
                    course = Request.QueryString["course"].ToString();//BASCXXXX Title
                    tutorial = Request.QueryString["tutorial"].ToString();//T3 XXXX

                    lecID = Session["lecturerID"].ToString();
                    lblCourse.Text = course.ToString();
                    lblTutorial.Text = tutorial.ToString();
                    lblIntake.Text = intakeID.ToString();

                    lblCourse2.Text = lblCourse.Text;
                    lblTutorial2.Text = lblTutorial.Text;
                    lblIntake2.Text = lblIntake.Text;

                    coursename = "";
                    tutTitle = "";

                    char delimiters = ' ';
                    string[] splitArray = course.Split(delimiters);
                    courseID = splitArray[0]; //courseID
                    for (int i = 1; i < splitArray.Length; i++)
                    {
                        coursename += splitArray[i] + " "; //coursename
                    }
                    coursename = coursename.TrimEnd();

                    string[] splitArray1 = tutorial.Split(delimiters);
                    tutNum = splitArray1[1];

                    for (int i = 2; i < splitArray1.Length; i++)
                    {
                        tutTitle += splitArray1[i] + " "; //coursename
                    }
                    tutTitle = tutTitle.TrimEnd();

                    StringBuilder sb = new StringBuilder(tutNum);
                    sb.Remove(0, 1);
                    tutNum = sb.ToString();

                    conn.Open();
                    string sql = "SELECT TutorialID FROM Tutorial WHERE TutorialNumber = @tutNum AND CourseID = @courseID";
                    SqlCommand cmdGetTutID = new SqlCommand(sql, conn);
                    cmdGetTutID.Parameters.AddWithValue("@tutNum", tutNum);
                    cmdGetTutID.Parameters.AddWithValue("@courseID", courseID);
                    SqlDataReader dtr = cmdGetTutID.ExecuteReader();
                    while (dtr.Read())
                    {
                        tutID = dtr.GetString(0);
                    }
                    conn.Close();

                    sql = "SELECT a.Question,a.Level, COUNT(n.AnswerID) AS 'Total Answer',ISNULL(AVG(n.Points),0) AS 'Average Points' FROM (SELECT q.QuestionID, v.IntakeID, v.TutorialGrpID, t.TutorialID, q.Question, q.Level FROM Question q, Tutorial t, Course c, CourseAvailable v WHERE q.TutorialID = t.TutorialID AND t.CourseID = c.CourseID AND c.CourseID = v.CourseID AND t.TutorialID = @tutID AND v.LecturerID = @lecID AND v.IntakeID = @intakeID GROUP BY q.QuestionID, v.IntakeID, v.TutorialGrpID, t.TutorialID, q.Question, q.Level) a LEFT JOIN Intake i on i.IntakeID = a.IntakeID LEFT JOIN TutorialGroup g on g.TutorialGrpID = a.TutorialGrpID LEFT JOIN Student s on i.IntakeID = s.IntakeID AND s.TutorialGroupID = g.TutorialGrpID LEFT JOIN Tutorial t on t.TutorialID = a.TutorialID LEFT JOIN StudAns n on s.StudentID = n.StudentID AND n.QuestionID = a.QuestionID AND CONVERT(date, n.DateComplete) BETWEEN CONVERT(date, t.StartDate) AND CONVERT(date, t.ExpiryDate) GROUP BY a.Question, a.Level";

                    SqlCommand cmdGetResult = new SqlCommand(sql, conn);
                    cmdGetResult.Parameters.AddWithValue("@tutID", tutID);
                    cmdGetResult.Parameters.AddWithValue("@lecID", lecID);
                    cmdGetResult.Parameters.AddWithValue("@intakeID", intakeID);
                    DataTable dt = new DataTable();
                    cmdGetResult.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdGetResult;
                    conn.Close();
                    conn.Open();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                        Repeater2.DataSource = dt;
                        Repeater2.DataBind();
                        NoResultPanel.Visible = false;
                    }
                    else
                    {
                        container.Visible = false;
                        NoResultPanel.Visible = true;
                        btnBack.Visible = false;
                        lblSavePDF.Visible = false;
                        //filler.Visible = false;
                    }
                    conn.Close();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
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