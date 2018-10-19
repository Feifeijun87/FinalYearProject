using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.Configuration;

namespace AdaptiveLearningSystem
{
    public partial class LecProfile : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["lecturerID"] != null)
                {
                    lblUserName.Text = Session["lecName"].ToString();
                    lblUserName.Text = Session["lecName"].ToString();
                    lblTutorName.Text = Session["lecTitle"].ToString() + " " + Session["lecName"].ToString();
                    lblPosition.Text = Session["position"].ToString();
                    lblContact.Text = Session["contactNo"].ToString();
                    lblEmail.Text = Session["email"].ToString();
                    lblFaculty.Text = Session["facultyName"].ToString();
                    lblOfficeLoc.Text = Session["officeLoc"].ToString();
                    //take lecturer image by using username , student can use other, just chg in sql
                    //for student, change the type to other than lec, then go profilePic.ashx.cs
                    LecProfileImg.ImageUrl = "~/profilePic.ashx?id=" + Session["username"].ToString() + "&type=lec";
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnChgNewPass_Click(object sender, EventArgs e)
        {
            lblPassErrorMsg.Visible = false;
            if (Session["lecPass"].ToString() != txtOldPass.Text.ToString())
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Invalid Old Password.";
            }
            else if (txtNewPass.Text != txtConfirmPass.Text)
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Password not match.";
            }
            else
            {
                Session["lecPass"] = txtConfirmPass.Text;
                SqlCommand cmd = new SqlCommand("prc_lec_chgPass", conn);
                cmd.Parameters.AddWithValue("@username", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@password", txtConfirmPass.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter writePass = new SqlDataAdapter();
                writePass.UpdateCommand = cmd;
                writePass.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Password Changed Successfully'); window.location.href='LecProfile.aspx';", true);


            }
        }

        protected void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ChgPwdBtn_Click(object sender, EventArgs e)
        {

        }

        protected void btnChgOffice_Click(object sender, EventArgs e)
        {
            Session["officeLoc"] = txtNewOffice.Text;
            SqlCommand cmd = new SqlCommand("prc_lec_ChgOffice", conn);
            cmd.Parameters.AddWithValue("@username", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@OfficeLoc", txtNewOffice.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter writePass = new SqlDataAdapter();
            writePass.UpdateCommand = cmd;
            writePass.UpdateCommand.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Office Location Changed Successfully'); window.location.href='LecProfile.aspx';", true);

        }

        protected void ChgOfficeBtn_Click(object sender, EventArgs e)
        {

        }

        protected void btnChgContact_Click(object sender, EventArgs e)
        {
            Session["contactNo"] = txtNewContact.Text;
            SqlCommand cmd = new SqlCommand("prc_lec_ChgContact", conn);
            cmd.Parameters.AddWithValue("@username", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@ContactNo", txtNewContact.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter writePass = new SqlDataAdapter();
            writePass.UpdateCommand = cmd;
            writePass.UpdateCommand.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Contact Number Changed Successfully'); window.location.href='LecProfile.aspx';", true);
        }

        protected void ChgPhoneBtn_Click(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecHome.aspx");
        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecProfile.aspx");
        }

        protected void TutorialLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseList.aspx");
        }
        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }
    }
}