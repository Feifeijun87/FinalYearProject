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

namespace AdaptiveLearningSystem
{
    public partial class EditTutHome : System.Web.UI.Page
    {
        static string lecID;
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);


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

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string course, courseID, coursename = "", tutorial, tutNum, tutTitle = "";

            Label lblCourse1 = new Label();
            lblCourse1 = (Label)e.Item.FindControl("lblCourse");
            course = lblCourse1.Text;
            Label lblTutorial1 = new Label();
            lblTutorial1 = (Label)e.Item.FindControl("lblTutorial");
            tutorial = lblTutorial1.Text;

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

            for (int i = 3; i < splitArray1.Length; i++)
            {
                tutTitle += splitArray1[i] + " "; //coursename
            }
            tutTitle = tutTitle.TrimEnd();

            if (e.CommandName == "select") //edit
            {

                Response.Redirect("EditTut.aspx?course=" + courseID + "&coursename=" + coursename + "&tutNum=" + tutNum + "&tutTitle=" + tutTitle);

            }
            else
            {
                string sql = "UPDATE Tutorial SET Status = '0' WHERE TutorialNumber = @tutNum AND CourseID = @courseID";
                SqlCommand cmdUpdate = new SqlCommand(sql, conn);
                cmdUpdate.Parameters.AddWithValue("@tutNum", tutNum);
                cmdUpdate.Parameters.AddWithValue("@courseID", courseID);
                conn.Open();
                cmdUpdate.ExecuteNonQuery();
                // SqlDataAdapter writePass = new SqlDataAdapter();
                // writePass.UpdateCommand = cmdUpdate;
                ////writePass.UpdateCommand.ExecuteNonQuery();
                //cmdUpdate.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Tutorial Deleted Successfully'); window.location.href='EditTutHome.aspx';", true);

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["lecturerID"] = "L1";
            string sql;

            if (!IsPostBack)
            {
                if (Session["lecturerID"] != null)
                {
                    lecID = Session["lecturerID"].ToString();
                    sql = "SELECT c.CourseID + ' ' + c.CourseName AS 'Course', t.TutorialNumber, t.ChapterName,t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium AS 'No Of Question' FROM Tutorial t, CourseAvailable a, Course c WHERE a.LecturerID = @lecID AND a.CourseID = c.CourseID AND t.CourseID = c.CourseID AND t.Status=1 GROUP BY c.CourseID ,c.CourseName , t.TutorialNumber, t.ChapterName,t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium ORDER BY c.CourseID, t.TutorialNumber ASC";
                    SqlCommand cmdGetResult = new SqlCommand(sql, conn);
                    cmdGetResult.Parameters.AddWithValue("@lecID", lecID);
                    DataTable dt = new DataTable();
                    cmdGetResult.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdGetResult;
                    conn.Open();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();

                    }
                    else
                    {
                        container.Visible = false;
                        //filler.Visible = false;
                    }
                    conn.Close();
                }
            }
        }
    }
    
}