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
                    if(Session["lecProfilePic"]!=null)
                        LecProfileImg.ImageUrl = "~/profilePic.ashx?id=" + Session["username"].ToString() + "&type=lec";
                    else
                        LecProfileImg.ImageUrl = Page.ResolveUrl("~/images/defaultProfileImg.jpg");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected Boolean checkPass()
        {
            if(txtOldPass.Text.Trim()==String.Empty&& txtNewPass.Text.Trim() == String.Empty)
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Both field is required.";
                return false;
            }
            else if (Session["lecPass"].ToString() != txtOldPass.Text.ToString())
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
            else if(!Regex.IsMatch(txtNewPass.Text.Trim(), "^[a-zA-Z0-9]"))
            {
                lblPassErrorMsg.Visible = true;
                lblPassErrorMsg.Text = "Only letter and digit allowed for new password.";
                return false;
            }
            else if (txtNewPass.Text.Trim().Length<8)
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
            
            if(checkPass()==true)
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Password Changed Successfully'); window.location.href='LecProfile.aspx;", true);


            }
        }

        protected void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ChgPwdBtn_Click(object sender, EventArgs e)
        {

        }

        protected Boolean checkOffice()
        {
            if (txtNewOffice.Text == String.Empty)
            {
                lblOfficeErrorMsg.Visible = true;
                lblOfficeErrorMsg.Text = "This field cannot be empty.";
                return false;
            }

            else if (!Regex.IsMatch(txtNewOffice.Text.Trim(), "^[A-Za-z][0-9]{3}$"))
            {
                lblOfficeErrorMsg.Visible = true;
                lblOfficeErrorMsg.Text = "Invalid Office Location Format.";
                return false;
            }
            
            return true;
        }

        protected void btnChgOffice_Click(object sender, EventArgs e)
        {
            if (checkOffice() == true)
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
        }

        protected void ChgOfficeBtn_Click(object sender, EventArgs e)
        {


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

        protected void MyCourseLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecCourse.aspx");
        }

        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecResultHome.aspx");
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

        protected void btnOfficeCancel_Click(object sender, EventArgs e)
        {
            lblOfficeErrorMsg.Visible = false;
            ModalPopupExtender2.Hide();
        }
    }
}