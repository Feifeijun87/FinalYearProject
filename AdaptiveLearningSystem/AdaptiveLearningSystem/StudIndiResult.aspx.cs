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
    public partial class StudIndiResult : System.Web.UI.Page
    {

        static string courseID, sql, tutTitle, courseName, studentID, tutNum;

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

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
                    Response.AddHeader("content-disposition", "attachment;filename=StudIndiResult.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Panel2.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null || Session["studID"] != null)
            {
                if (!IsPostBack)
                {
                    //course, coursename, tutNum, tutTitle, studID
                    lblUserName.Text = Session["studName"].ToString();

                    courseID = Request.QueryString["course"].ToString();
                    courseName = Request.QueryString["coursename"].ToString();
                    tutNum = Request.QueryString["tutNum"].ToString();
                    studentID = Request.QueryString["studID"].ToString();
                    tutTitle = Request.QueryString["tutTitle"].ToString();
                    conn.Open();
                    sql = "SELECT s.StudentID,s.StudentName,t.TutorialGrpName,s.IntakeID FROM Student s, TutorialGroup t WHERE StudentID = @studID AND s.TutorialGroupID = t.TutorialGrpID";
                    SqlCommand cmdGetStud = new SqlCommand(sql, conn);
                    cmdGetStud.Parameters.AddWithValue("@studID", studentID);
                    SqlDataReader dtr = cmdGetStud.ExecuteReader();

                    while (dtr.Read())
                    {
                        lblStudID.Text = dtr.GetString(0);
                        lblStudName.Text = dtr.GetString(1);
                        lblTutGroup.Text = dtr.GetString(2);
                        lblIntake.Text = dtr.GetString(3);
                    }
                    conn.Close();


                    lblCourse.Text = courseID.ToString() + " " + courseName.ToString();
                    lblTutorial.Text = tutNum.ToString();
                    lblTitle.Text = tutTitle.ToString();

                    lblCourse2.Text = lblCourse.Text;
                    lblTutorial2.Text = lblTutorial.Text;
                    lblTitle2.Text = lblTitle.Text;
                    lblStudID2.Text = lblStudID.Text;
                    lblStudName2.Text = lblStudName.Text;
                    lblTutGroup2.Text = lblTutGroup.Text;
                    lblIntake2.Text = lblIntake.Text;

                    StringBuilder sb = new StringBuilder(tutNum);
                    sb.Remove(0, 1);
                    tutNum = sb.ToString();
                    sql = "SELECT q.Question, a.Answer, a.TimeSpent, a.MatchPercent, a.Points, q.SampleAns FROM Question q, StudAns a, Tutorial t WHERE t.CourseID = @courseID AND t.TutorialNumber = @tutNum AND a.StudentID = @studID AND q.QuestionID = a.QuestionID GROUP BY q.Question, a.Answer, a.TimeSpent, a.MatchPercent, a.Points, q.SampleAns ";

                    SqlCommand cmdGetResult = new SqlCommand(sql, conn);
                    cmdGetResult.Parameters.AddWithValue("@courseID", courseID);
                    cmdGetResult.Parameters.AddWithValue("@tutNum", tutNum);
                    cmdGetResult.Parameters.AddWithValue("@studID", studentID);
                    DataTable dt = new DataTable();
                    cmdGetResult.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdGetResult;
                    conn.Close();
                    conn.Open();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //NoResultPanel.Visible = false;
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                        Repeater2.DataSource = dt;
                        Repeater2.DataBind();
                    }
                    else
                    {
                        container.Visible = false;
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

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void lblBack_Click(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                Response.Redirect("reportTutGroup.aspx", false);
            }
            else if (Session["studID"] != null)
            {
                Response.Redirect("StudResult.aspx", false);
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