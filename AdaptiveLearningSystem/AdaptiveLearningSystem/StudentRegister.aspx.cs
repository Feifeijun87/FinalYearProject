using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdaptiveLearningSystem
{
    public partial class StudentRegister : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                ddlGroup.SelectedIndex = 0;
                hideErrorTage();
                //lblUserName.Text = Session["lecName"].ToString();
                SqlCommand cmd = new SqlCommand("prc_get_intake_list", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlIntake.DataTextField = "IntakeID";
                    ddlIntake.DataValueField = "IntakeID";
                    ddlIntake.DataSource = dt;
                    ddlIntake.DataBind();
                    ddlIntake.Items.Insert(0, new ListItem("Please Select", String.Empty));
                    ddlIntake.SelectedIndex = 0;
                }
            }
            else
            {

                if (imgupload.HasFile && imgupload.PostedFile != null)
                {
                    Session["imgupload"] = imgupload.PostedFile;
                    Session["imgSize"] = imgupload.FileBytes.Length;
                    lblImgUpload.Text = Path.GetFileName(Request.Files["imgupload"].FileName);
                    if (lblErrorImage.Visible == true)
                        lblErrorImage.Visible = false;
                }
                else if (Session["imgupload"] != null)
                {
                    HttpPostedFile file = (HttpPostedFile)Session["imgupload"];
                    lblImgUpload.Text = Path.GetFileName(file.FileName);
                    if (lblErrorImage.Visible == true)
                        lblErrorImage.Visible = false;
                }
            }

        }

        protected Boolean checkDuplicated()
        {
            conn.Open();
            string sql = "SELECT StudentID FROM [Student] WHERE StudentID = @studentID";
            SqlCommand cmdCheckID = new SqlCommand(sql, conn);
            cmdCheckID.Parameters.AddWithValue("@studentID", txtID.Text.Trim());
            SqlDataReader dt = cmdCheckID.ExecuteReader();
            if (dt.HasRows)
            {
                lblErrorID.Visible = true;
                lblErrorID.Text = "Student ID existed.";
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
            sql = "SELECT StudentID FROM [Student] WHERE Email = @email";
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

        protected Boolean checkRegex()
        {
            if (!Regex.IsMatch(txtID.Text.Trim(), "^[0-9]{2}[A-Za-z]{3}[0-9]{5}$"))
            {
                lblErrorID.Visible = true;
                lblErrorID.Text = "Invalid Student ID Format.";
                return false;
            }
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
            HttpPostedFile file = (HttpPostedFile)Session["imgupload"];
            string fileName = Path.GetFileName(file.FileName);
            if (!Regex.IsMatch(fileName, "(.jpeg|.JPEG|.gif|.GIF|.JPG|.jpg|.bitmap|.BITMAP)$"))
            {
                Session["imgupload"] = null;
                Session["imgSize"] = null;
                imgupload.Dispose();
                lblImgUpload.Text = "No File Chosen";
                lblErrorImage.Text = "Only .jpg/.bitmap/.gif file type are allowed.";
                lblErrorImage.Visible = true;
                return false;
            }
            if (Int32.Parse(Session["imgSize"].ToString()) > 2097152)
            {
                Session["imgupload"] = null;
                Session["imgSize"] = null;
                imgupload.Dispose();
                lblImgUpload.Text = "No File Chosen";
                lblErrorImage.Text = "The file size cannot be more than 2MB.";
                lblErrorImage.Visible = true;
                return false;
            }

            return true;
        }

        protected Boolean checkEmpty()
        {
            if (txtID.Text == String.Empty)
            {
                lblErrorID.Visible = true;
                lblErrorID.Text = "This field cannot be empty.";
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
            else if (ddlIntake.SelectedIndex == 0)
            {
                lblErrorIntake.Visible = true;
                lblErrorIntake.Text = "This field cannot be empty.";
                return false;
            }
            else if (ddlGroup.SelectedIndex == 0)
            {
                lblErrorGroup.Visible = true;
                lblErrorGroup.Text = "This field cannot be empty.";
                return false;
            }
            else if (Session["imgupload"] == null)
            {
                lblErrorImage.Visible = true;
                lblErrorImage.Text = "This field cannot be empty.";
                return false;
            }
            return true;
        }

        protected void hideErrorTage()
        {
            lblErrorID.Visible = false;
            lblErrorName.Visible = false;
            lblErrorIC.Visible = false;
            lblErrorContact.Visible = false;
            lblErrorAddress.Visible = false;
            lblErrorEmail.Visible = false;
            lblErrorIntake.Visible = false;
            lblErrorGroup.Visible = false;
            lblErrorImage.Visible = false;

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

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {

            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

            hideErrorTage();
            if (checkEmpty() == true && checkRegex() == true && checkDuplicated() == true)
            {
                Byte[] imgByte = null;
                //To create a PostedFile
                HttpPostedFile File = (HttpPostedFile)Session["imgupload"];
                //Create byte Array with file len
                imgByte = new Byte[File.ContentLength];
                //force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength);

                SqlCommand cmd = new SqlCommand("prc_insert_new_student", conn);
                cmd.Parameters.AddWithValue("@StudentID", txtID.Text.Trim());
                cmd.Parameters.AddWithValue("@StudentName", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@ICNo", txtIC.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactNo", txtContact.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@IntakeID", ddlIntake.SelectedValue);
                cmd.Parameters.AddWithValue("@TutorialGroup", ddlGroup.SelectedValue);
                cmd.Parameters.AddWithValue("@ProfilePic", imgByte);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter insertStudent = new SqlDataAdapter();
                insertStudent.UpdateCommand = cmd;
                insertStudent.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Student Added Successfully'); window.location.href='StudentRegister.aspx';", true);
            }
        }
    }
}