using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADM
{
    public partial class LecturerRegister : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTitle.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlTitle.SelectedIndex = 0;
                ddlPosition.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlPosition.SelectedIndex = 0;
                hideErrorTage();
                //lblUserName.Text = Session["lecName"].ToString();
                SqlCommand cmd = new SqlCommand("prc_get_faculty_list", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlFaculty.DataTextField = "FacultyName";
                    ddlFaculty.DataValueField = "FacultyID";
                    ddlFaculty.DataSource = dt;
                    ddlFaculty.DataBind();
                    ddlFaculty.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlFaculty.SelectedIndex = 0;
                }
            }
        }

        protected void hideErrorTage()
        {
            lblErrorUsername.Visible = false;
            lblErrorName.Visible = false;
            lblErrorIC.Visible = false;
            lblErrorContact.Visible = false;
            lblErrorAddress.Visible = false;
            lblErrorEmail.Visible = false;
            lblErrorOffice.Visible = false;
            lblErrorPosition.Visible = false;
            lblErrorTitle.Visible = false;
            lblErrorFaculty.Visible = false;
            lblErrorImage.Visible = false;

        }


        protected Boolean checkEmpty()
        {
            if (txtUsername.Text == String.Empty)
            {
                txtUsername.Visible = true;
                txtUsername.Text = "This field cannot be empty.";
                return false;
            }
            else if (txtName.Text == String.Empty)
            {
                lblErrorName.Visible = true;
                lblErrorName.Text = "This field cannot be empty.";
                return false;
            }
            else if (txtIC.Text == String.Empty)
            {
                lblErrorIC.Visible = true;
                lblErrorIC.Text = "This field cannot be empty.";
                return false;
            }
            else if (txtContact.Text == String.Empty)
            {
                lblErrorContact.Visible = true;
                lblErrorContact.Text = "This field cannot be empty.";
                return false;
            }
            else if (txtAddress.Text == String.Empty)
            {
                lblErrorAddress.Visible = true;
                lblErrorAddress.Text = "This field cannot be empty.";
                return false;
            }
            else if (txtEmail.Text == String.Empty)
            {
                lblErrorEmail.Visible = true;
                lblErrorEmail.Text = "This field cannot be empty.";
                return false;
            }
            else if (txtOffice.Text == String.Empty)
            {
                lblErrorOffice.Visible = true;
                lblErrorOffice.Text = "This field cannot be empty.";
                return false;
            }
            else if (ddlTitle.SelectedIndex == 0)
            {
                lblErrorTitle.Visible = true;
                lblErrorTitle.Text = "This field cannot be empty.";
                return false;
            }
            else if (ddlPosition.SelectedIndex == 0)
            {
                lblErrorPosition.Visible = true;
                lblErrorPosition.Text = "This field cannot be empty.";
                return false;
            }
            else if (ddlFaculty.SelectedIndex == 0)
            {
                lblErrorFaculty.Visible = true;
                lblErrorFaculty.Text = "This field cannot be empty.";
                return false;
            }
            else if (!imgupload.HasFiles)
            {
                lblErrorImage.Visible = true;
                lblErrorImage.Text = "This field cannot be empty.";
                return false;
            }
            return true;
        }

        protected Boolean checkRegex()
        {
            string name = txtName.Text.Trim();
            foreach (Char c in name)
            {
                if (!Char.IsLetter(c) && !Char.IsWhiteSpace(c))
                {
                    lblErrorName.Visible = true;
                    lblErrorName.Text = "Only letter is allowed.";
                    return false;
                }
            }
            if (!Regex.IsMatch(txtIC.Text.Trim(), "^[0-9]{12}$"))
            {
                lblErrorIC.Visible = true;
                lblErrorIC.Text = "Invalid IC Format.";
                return false;
            }

            if (!Regex.IsMatch(txtContact.Text.Trim(), "^01[012346789][0-9]{7,8}$"))
            {
                lblErrorContact.Visible = true;
                lblErrorContact.Text = "Invalid Contact Number Format.";
                return false;
            }

            try
            {
                MailAddress m = new MailAddress(txtEmail.Text.Trim());

            }
            catch (FormatException)
            {
                lblErrorEmail.Visible = true;
                lblErrorEmail.Text = "Invalid email format.";
                return false;
            }

            if (!Regex.IsMatch(txtOffice.Text.Trim(), "^[A-Za-z][0-9]{3}$"))
            {
                txtOffice.Visible = true;
                txtOffice.Text = "Invalid Contact Number Format.";
                return false;
            }

            return true;
        }

        protected Boolean checkDuplicated()
        {
            conn.Open();
            string sql = "SELECT LecturerID FROM [Lecturer] WHERE Username = @Username";
            SqlCommand cmdCheckID = new SqlCommand(sql, conn);
            cmdCheckID.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
            SqlDataReader dt = cmdCheckID.ExecuteReader();
            if (dt.HasRows)
            {
                lblErrorUsername.Visible = true;
                lblErrorUsername.Text = "Lecturer username existed.";
                conn.Close();
                return false;
            }
            conn.Close();
            conn.Open();
            sql = "SELECT StudentID FROM [Student] WHERE ICNo = @ic";
            SqlCommand cmdCheckStudIC = new SqlCommand(sql, conn);
            cmdCheckStudIC.Parameters.AddWithValue("@ic", txtIC.Text.Trim());
            dt = cmdCheckStudIC.ExecuteReader();
            if (dt.HasRows)
            {
                lblErrorIC.Visible = true;
                lblErrorIC.Text = "NRIC existed.";
                conn.Close();
                return false;
            }
            conn.Close();
            conn.Open();
            sql = "SELECT LecturerID FROM [Lecturer] WHERE ICNo = @ic";
            SqlCommand cmdCheckLecIC = new SqlCommand(sql, conn);
            cmdCheckLecIC.Parameters.AddWithValue("@ic", txtIC.Text.Trim());
            dt = cmdCheckLecIC.ExecuteReader();
            if (dt.HasRows)
            {
                lblErrorIC.Visible = true;
                lblErrorIC.Text = "NRIC existed.";
                conn.Close();
                return false;
            }
            conn.Close();
            conn.Open();
            sql = "SELECT LectuerID FROM [Lecturer] WHERE Email = @email";
            SqlCommand cmdCheckEmail = new SqlCommand(sql, conn);
            cmdCheckEmail.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            dt = cmdCheckEmail.ExecuteReader();
            if (dt.HasRows)
            {
                lblErrorEmail.Visible = true;
                lblErrorEmail.Text = "Email existed.";
                conn.Close();
                return false;
            }


            conn.Close();
            return true;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Session["imgupload"] == null && imgupload.HasFile)
            {
                Session["imgupload"] = imgupload;
                lblImgUpload.Text = imgupload.FileName; // get the name 
            }
            else if (imgupload.HasFile)
            {
                Session["imgupload"] = imgupload;
                lblImgUpload.Text = imgupload.FileName;
            }
            hideErrorTage();
            if (checkEmpty() == true && checkRegex() == true && checkDuplicated() == true)
            {
                FileUpload img = (FileUpload)imgupload;
                Byte[] imgByte = null;
                if (img.HasFile && img.PostedFile != null)
                {
                    //To create a PostedFile
                    HttpPostedFile File = imgupload.PostedFile;
                    //Create byte Array with file len
                    imgByte = new Byte[File.ContentLength];
                    //force the control to load data in array
                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                }
                int lecCount = 0;
                conn.Open();
                string sql = "SELECT COUNT(LectuerID) FROM [Lecturer]";
                SqlCommand cmdGetLecCount = new SqlCommand(sql, conn);
                SqlDataReader dt = cmdGetLecCount.ExecuteReader();
                if (dt.HasRows)
                    lecCount = dt.GetInt32(0) + 1;
                else
                    lecCount++;

                SqlCommand cmd = new SqlCommand("prc_insert_new_lecturer)", conn);
                cmd.Parameters.AddWithValue("@@LecturerID", "L" + lecCount.ToString());
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@LecName", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@ICNo", txtIC.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactNo", txtContact.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@OfficeLocation", txtOffice.Text.Trim());
                cmd.Parameters.AddWithValue("@Title", ddlTitle.SelectedValue);
                cmd.Parameters.AddWithValue("@Position", ddlPosition.SelectedValue);
                cmd.Parameters.AddWithValue("@FacultyID", ddlFaculty.SelectedValue);
                cmd.Parameters.AddWithValue("@ProfilePic", imgByte);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter insertLecturer = new SqlDataAdapter();
                insertLecturer.UpdateCommand = cmd;
                insertLecturer.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Lecturer Added Successfully'); window.location.href='LecturerRegister.aspx';", true);
            }
        }


        protected void btnAddStudent_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddLecturer_Click(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {

        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {

        }
    }

}