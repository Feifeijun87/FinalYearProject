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
    public partial class StudResult : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["studID"] != null)
            {

                if (!IsPostBack)
                {
                    lblUserName.Text = Session["studName"].ToString();
                    //course, coursename, tutNum, tutTitle, studID
                    //studentID = Session["StudentID"].ToString();
                    BindData();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            
        }

        protected void BindData()
        {
            conn.Open();
            string sql = "SELECT c.CourseID AS 'Course Code', c.CourseName AS 'Course Name',CONVERT(varchar,'T')+CONVERT(varchar,t.TutorialNumber) AS 'Tutorial Number', t.ChapterName AS 'Chapter Name',  t.CompulsaryEasy+t.CompulsaryHard+t.CompulsaryMedium AS 'Total Questions', k.Status as 'Completed' FROM Student s,Intake i, TutorialGroup g, Tutorial t, Course c, CourseAvailable v, TutorialCheck k WHERE s.IntakeID = i.IntakeID AND s.TutorialGroupID = g.TutorialGrpID AND g.TutorialGrpID = v.TutorialGrpID AND v.IntakeID = i.IntakeID AND c.CourseID = v.CourseID AND s.StudentID = k.StudentID AND k.TutorialID = t.TutorialID AND c.CourseID = t.CourseID AND v.Status = 1 AND t.Status = 1 AND s.StudentID = @studID AND k.Status = 1 GROUP BY s.StudentID, t.StartDate, t.ExpiryDate, t.TutorialID, t.TutorialNumber,t.ChapterName,c.CourseID,c.CourseName,t.CompulsaryEasy + t.CompulsaryHard + t.CompulsaryMedium,k.Status";
           // string sql = "SELECT * FROM Student";
            SqlCommand cmdGetResult = new SqlCommand(sql, conn);
            cmdGetResult.Parameters.AddWithValue("@studID", Session["studID"].ToString());
            //cmdGetResult.Parameters.AddWithValue("@StudentID1", studentID);
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
                lblNoData.Text = "No result available";
            }

            conn.Close();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
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
                    Response.Redirect("StudIndiResult.aspx?course=" + row.Cells[1].Text.ToString() + "&coursename=" + row.Cells[2].Text.ToString() + "&tutNum=" + row.Cells[3].Text.ToString() + "&tutTitle=" + row.Cells[4].Text.ToString() + "&studID=" + Session["studID"].ToString());

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