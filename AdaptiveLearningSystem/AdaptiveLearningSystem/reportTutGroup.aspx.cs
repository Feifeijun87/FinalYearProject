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
    public partial class reportTutGroup : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        static string tutGroupID, lecID, intakeID, courseID, course, tutGroup, tutorial, tutNum, studentID, coursename, tutTitle;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                Label lblStudID = new Label();
                lblStudID = (Label)e.Item.FindControl("lblStudentID");
                studentID = lblStudID.Text;
                Session["tutIntakeID"] = intakeID;
                Session["tutCourse"] = course;
                Session["tutTutorial"] = tutorial;
                Session["tutTutGroup"] = tutGroupID;
                Response.Redirect("StudIndiResult.aspx?course=" + courseID + "&coursename=" + coursename + "&tutNum=" + ("T" + tutNum) + "&tutTitle=" + tutTitle + "&studID=" + studentID);


            }
        }

        string sql;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
                {
                        if (Session["tutIntakeID"] != null)
                        {
                            intakeID = Session["tutIntakeID"].ToString();
                            course = Session["tutCourse"].ToString();
                            tutorial = Session["tutTutorial"].ToString();
                            tutGroupID = Session["tutTutGroup"].ToString();

                            Session["tutIntakeID"] = null;
                            Session["tutCourse"] = null;
                            Session["tutTutorial"] = null;
                            Session["tutTutGroup"] = null;

                        }
                        else
                        {
                            intakeID = Request.QueryString["intake"].ToString();//intakeID
                            course = Request.QueryString["course"].ToString();//BASCXXXX Title
                            tutorial = Request.QueryString["tutorial"].ToString();//T3 XXXX
                            tutGroupID = Request.QueryString["tutGroup"].ToString();//tutgroupID
                        }

                        conn.Open();
                        sql = "SELECT TutorialGrpName FROM TutorialGroup WHERE TutorialGrpID = @tutGroupID";
                        SqlCommand cmdGetTutGroup = new SqlCommand(sql, conn);
                        cmdGetTutGroup.Parameters.AddWithValue("@tutGroupID", tutGroupID);
                        SqlDataReader dtr = cmdGetTutGroup.ExecuteReader();
                        while (dtr.Read())
                        {
                            tutGroup = dtr.GetString(0);
                        }
                        conn.Close();

                        lecID = Session["lecturerID"].ToString();
                        lblCourse.Text = course.ToString();
                        lblTutorial.Text = tutorial.ToString();
                        lblIntake.Text = intakeID.ToString();
                        lblTutGroup.Text = tutGroup.ToString();

                        lblCourse2.Text = lblCourse.Text;
                        lblTutorial2.Text = lblTutorial.Text;
                        lblIntake2.Text = lblIntake.Text;
                        lblTutGroup2.Text = lblTutGroup.Text;
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

                        sql = "SELECT s.StudentID, s.StudentName, SUM(a.Points) AS 'Total Score' FROM Student s, StudAns a, Tutorial t, Question q, TutorialGroup g, CourseAvailable c, Course v, Intake i WHERE t.CourseID = v.CourseID AND c.CourseID = v.CourseID AND c.LecturerID = @lecID AND c.IntakeID = @intakeID AND t.CourseID = @courseID AND t.TutorialNumber = @tutNum AND g.TutorialGrpID = @tutGroup AND q.TutorialID = t.TutorialID AND q.QuestionID = a.QuestionID AND a.StudentID = s.StudentID AND i.IntakeID = c.IntakeID AND s.IntakeID = i.IntakeID AND s.TutorialGroupID = g.TutorialGrpID AND c.TutorialGrpID = g.TutorialGrpID GROUP BY s.StudentID, s.StudentName ORDER BY[Total Score] DESC";
                        SqlCommand cmdGetResult = new SqlCommand(sql, conn);
                        cmdGetResult.Parameters.AddWithValue("@courseID", courseID);
                        cmdGetResult.Parameters.AddWithValue("@tutNum", tutNum);
                        cmdGetResult.Parameters.AddWithValue("@tutGroup", tutGroupID);
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
                Response.Redirect("Login.aspx");
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
                    //Panel2.Visible = true;
                    Panel2.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=TutGroupResult.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    // Panel2.Visible = false;
                }
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