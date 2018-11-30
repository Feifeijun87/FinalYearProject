using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AdaptiveLearningSystem
{
    public partial class StudProfile : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["studID"] != null)
            {
                if (!IsPostBack)
                {
                    string groupname="";

                    conn.Open();
                    string sql = "SELECT g.TutorialGrpName FROM TutorialGroup g WHERE g.TutorialGrpID = @groupID";
                    SqlCommand cmdGetGroupID = new SqlCommand(sql, conn);
                    cmdGetGroupID.Parameters.AddWithValue("@groupID", Session["studGroup"].ToString());
                    SqlDataReader dtr = cmdGetGroupID.ExecuteReader();
                    while (dtr.Read())
                    {
                        groupname = dtr.GetString(0);
                    }
                    conn.Close();

                    //lblUserName.Text = Session["lecName"].ToString();
                    lblUserName.Text = Session["studName"].ToString();
                    lblStudName.Text = Session["studName"].ToString();
                    lblProgramme.Text = Session["studProgramme"].ToString();
                    lblContact.Text = Session["studContact"].ToString();
                    lblEmail.Text = Session["studEmail"].ToString();
                    lblFaculty.Text = Session["studFacultyName"].ToString();
                    lblTutGrp.Text = groupname.ToString();
                    //take lecturer image by using username , student can use other, just chg in sql
                    //for student, change the type to other than lec, then go profilePic.ashx.cs
                    if (Session["studProfilePic"] != null)
                        LecProfileImg.ImageUrl = "~/profilePic.ashx?id=" + Session["studID"].ToString() + "&type=stud";
                    else
                        LecProfileImg.ImageUrl = Page.ResolveUrl("~/images/defaultProfileImg.jpg");
                    
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            
        }

        protected Boolean checkPass()
        {
            if (txtOldPass.Text.Trim() == String.Empty && txtNewPass.Text.Trim() == String.Empty)
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Both field is required.";
                return false;
            }
            else if (!Session["studPass"].ToString().Trim().Equals(txtOldPass.Text.ToString()))
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Invalid Old Password.";
                return false;
            }
            else if (txtNewPass.Text != txtConfirmPass.Text)
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Password not match.";
                return false;
            }
            else if (txtNewPass.Text.Equals(txtOldPass.Text))
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "New password cannot same with old password.";
                return false;
            }
            else if (!Regex.IsMatch(txtNewPass.Text.Trim(), "^[a-zA-Z0-9]"))
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Only letter and digit allowed for new password.";
                return false;
            }
            else if (txtNewPass.Text.Trim().Length < 8)
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "New password required at least 8 characters";
                return false;
            }

            return true;
        }

        protected void btnChgNewPass_Click(object sender, EventArgs e)
        {
            lblPassErrorMsg.Visible = false;

            if (checkPass() == true)
            {
                Session["studPass"] = txtConfirmPass.Text;
                string sql = "UPDATE Student  SET Password = @password WHERE StudentID = @studID";
                SqlCommand cmdUpdate = new SqlCommand(sql, conn);
                cmdUpdate.Parameters.AddWithValue("@password", txtConfirmPass.Text);
                cmdUpdate.Parameters.AddWithValue("@studID", Session["studID"].ToString());
                conn.Open();
                cmdUpdate.ExecuteNonQuery();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Password Changed Successfully'); window.location.href='StudProfile.aspx';", true);


            }
        }


        protected Boolean checkContact()
        {
            if (txtNewContact.Text == String.Empty)
            {
                lblContactErrorMsg.Visible = true;
                lblContactErrorMsg.Text = "This field cannot be empty.";
                return false;
            }
            else if (!Regex.IsMatch(txtNewContact.Text.Trim(), "^01[012346789][0-9]{7,8}$"))
            {
                lblContactErrorMsg.Visible = true;
                lblContactErrorMsg.Text = "Invalid Contact Number Format.";
                return false;
            }
            return true;
        }

        protected void btnChgContact_Click(object sender, EventArgs e)
        {
            if (checkContact() == true)
            {
                Session["studContact"] = txtNewContact.Text;
                string sql = "UPDATE Student SET ContactNo = @contactno WHERE StudentID = @studID";
                SqlCommand cmdUpdate = new SqlCommand(sql, conn);
                cmdUpdate.Parameters.AddWithValue("@contactno", txtNewContact.Text);
                cmdUpdate.Parameters.AddWithValue("@studID", Session["studID"].ToString());
                conn.Open();
                cmdUpdate.ExecuteNonQuery();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Contact Number Changed Successfully'); window.location.href='StudProfile.aspx';", true);
            }
        }

        protected void ChgPhoneBtn_Click(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudHome.aspx");
        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudProfile.aspx");
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


        protected void btnPassCancel_Click(object sender, EventArgs e)
        {
            lblPassErrorMsg.Visible = false;
            ModalPopupExtender1.Hide();
        }

        protected void btnContactCancel_Click(object sender, EventArgs e)
        {
            lblContact.Visible = false;
            ModalPopupExtender3.Hide();
        }



        protected void ResultLinkButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect("StudResult.aspx");
        }


    }
}