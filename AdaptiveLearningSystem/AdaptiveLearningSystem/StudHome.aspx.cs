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
            if (Session["studID"] != null)
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = Session["studName"].ToString();
                    //course, coursename, tutNum, tutTitle, studID
                    conn.Open();
                    string sql = "SELECT A.CourseID, A.CourseName AS 'Course Name', A.TutorialNumber AS 'Tutorial Number', A.ChapterName AS 'ChapterName',  A.NoOfQuestion AS 'No Of Question' FROM( SELECT t.StartDate, t.ExpiryDate, t.TutorialID, t.TutorialNumber, t.ChapterName, c.CourseID, c.CourseName, t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium AS NoOfQuestion FROM  Tutorial t, Course c WHERE c.CourseID = t.CourseID AND t.Status = 1 GROUP BY t.StartDate, t.ExpiryDate, t.TutorialID, t.TutorialNumber, t.ChapterName, c.CourseID, c.CourseName, t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium)a LEFT JOIN CourseAvailable v ON A.CourseID = v.CourseID LEFT JOIN Intake i ON v.IntakeID = i.IntakeID LEFT JOIN TutorialGroup g ON g.TutorialGrpID = v.TutorialGrpID LEFT JOIN Student s ON s.IntakeID = i.IntakeID AND s.TutorialGroupID = g.TutorialGrpID LEFT JOIN TutorialCheck k ON s.StudentID = k.StudentID AND a.TutorialID = k.TutorialID WHERE v.Status = 1 AND s.StudentID = @studID AND k.CheckID IS NULL group by A.TutorialID,A.StartDate, A.ExpiryDate, A.TutorialNumber,A.ChapterName,A.CourseID,A.CourseName, A.NoOfQuestion,k.CheckID";
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
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.Visible = false;
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;

                    //tutnum, courseID
                    Response.Redirect("AnsTut.aspx?tutNum=" + row.Cells[3].Text.ToString() + "&courseID=" + row.Cells[1].Text.ToString() + "&coursename=" + row.Cells[2].Text.ToString() + "&chapname=" + row.Cells[4].Text.ToString());

                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
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