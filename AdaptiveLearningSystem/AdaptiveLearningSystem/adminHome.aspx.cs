using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdaptiveLearningSystem
{
    public partial class adminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentRegister.aspx");
        }

        protected void btnAddLecturer_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecturerRegister.aspx");
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminHome.aspx");
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCourse.aspx");
        }

        protected void btnActivateCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivateCourse.aspx");
        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {

            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }
    }
}