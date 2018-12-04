using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace AdaptiveLearningSystem
{
    public partial class LecForgetPass : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetPanel.Visible = false;
            }
        }

        protected string randomCode()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 6; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["email"] = txtEmail.Text.Trim();

            SqlCommand cmd = new SqlCommand("prc_check_email", conn);
            cmd.Parameters.AddWithValue("@email", Session["email"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader lecturerRead = cmd.ExecuteReader();
            if (!lecturerRead.HasRows)
            {
                lblError.Style.Add("display", "inherit");
                lblError.Text = "Email Not Exists.";
                conn.Close();
            }
            else
            {
                conn.Close();
                sendEmail();
            }
        }

        protected void sendEmail()
        {
            Session["resetCode"] = randomCode();

            string fromaddr = "AdaptiveLearningSystem.FYP@gmail.com";
            string toaddr = Session["email"].ToString();
            string password = "thisispassword";

            MailMessage msg = new MailMessage();
            msg.Subject = "Forget Password (Adaptive Learning System)";
            msg.From = new MailAddress(fromaddr);
            msg.Body = @"Dear Mr/Miss,
                                 
You have requested to reset your password. The following code is essential to reset your password.

Code:" + Session["resetCode"].ToString() + @"
                                  
If you did not request for password reset, please kindly ignore this message.

Thank You.";
            msg.To.Add(new MailAddress(toaddr));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);
            resetPanel();

        }

        protected void resetPanel()
        {
            ResetPanel.Visible = true;
            EmailPanel.Visible = false;
            lblDesc.Text = "A recovery mail has sent to your email address.";
            MainPanel.Attributes["class"] = "ForgetPanelDown";
        }
        protected void btnBackLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {

            lblError.Style.Add("display", "none");

            if (!Session["resetCode"].Equals(txtCode.Text.Trim()))
            {
                lblError.Style.Add("display", "inherit");
                lblError.Text = "Invalid Code.";
            }
            else if (txtNewPass.Text.Trim() == String.Empty || txtConfirmPass.Text.Trim() == String.Empty)
            {
                lblError.Style.Add("display", "inherit");
                lblError.Text = "The password cannot be empty.";
            }
            else if (!txtConfirmPass.Text.Equals(txtNewPass.Text))
            {
                lblError.Style.Add("display", "inherit");
                lblError.Text = "The passwords does not match.";
            }
            else
            {
                SqlCommand cmd = new SqlCommand("prc_chg_pass_by_email", conn);
                cmd.Parameters.AddWithValue("@email", Session["email"].ToString());
                cmd.Parameters.AddWithValue("@password", txtConfirmPass.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter writePass = new SqlDataAdapter();
                writePass.UpdateCommand = cmd;
                writePass.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                Session.Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Password Changed Successfully'); window.location.href='Login.aspx';", true);
            }
        }
    }
}