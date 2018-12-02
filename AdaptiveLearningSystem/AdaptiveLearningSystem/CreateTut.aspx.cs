using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace AdaptiveLearningSystem
{
    public partial class CreateTut : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        static string[] question = new string[100];
        static string[] answer = new string[100];
        static string[] key = new string[100];
        static int[] time = new int[100];
        static int[] level = new int[100];
        static int totalCount, currCount;
        static int easy, medium, hard, tutNumInt;
        string sql, questionID = "";
        static string courseID,coursename, tutorialID;
        static string tutNumGet;
        static string tutTitleGet;
        List<string> listQuest = new List<string>();
        List<string> listAns = new List<string>();
        List<string> listKey = new List<string>();
        List<int> listTime = new List<int>();
        List<int> listlevel = new List<int>();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = Session["lecName"].ToString();
                    Label4.Text = "currcount= " + currCount + "// total count= " + totalCount;
                    sql = " SELECT DISTINCT c.CourseID + ' ' + c.CourseName AS Course FROM CourseAvailable a, Course c WHERE a.LecturerID = @lecID AND a.CourseID = c.CourseID AND a.Status =1";
                    SqlCommand cmdGetCourse = new SqlCommand(sql, conn);
                    cmdGetCourse.Parameters.AddWithValue("@lecID", Session["lecturerID"].ToString());
                    DataTable dt = new DataTable();
                    cmdGetCourse.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdGetCourse;
                    conn.Close();
                    conn.Open();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlCourse.DataTextField = "Course";
                        ddlCourse.DataValueField = "Course";
                        ddlCourse.DataSource = dt;
                        ddlCourse.DataBind();
                        ddlCourse.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlCourse.SelectedIndex = 0;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('No course found'); window.location.href='LecHome.aspx';", true);
                    }
                    conn.Close();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


        protected void Button1_Click(object sender, EventArgs e) //back
        {
            clearLabel();
            string questGet = txtQues.Text.Trim();
            string ansGet = txtAns.Text.Trim();
            string keywGet = txtKeyword.Text.Trim();
            int timeGet = ddlCompleteTime.SelectedIndex;
            int levelGet = ddlLevel.SelectedIndex;


            if (currCount > 0)
            {

                if (questGet == "" && (ansGet != "" || keywGet != ""))
                {
                    lblQuestEnter.Text = "Please fill in the question!";
                }
                else if (ansGet == "" && (questGet != "" || keywGet != ""))
                {
                    lblAnsEnter.Text = "Please fill in the sample answer!";
                }
                else if (keywGet == "" && (questGet != "" || ansGet != ""))
                {
                    lblKeyEnter.Text = "Please fill in the keyword!";
                }
                else if (checkKeyword(ansGet, keywGet) == false)
                {
                    lblKeyEnter.Visible = true;
                    lblKeyEnter.Text = "The keyword must exist in the answer provided!";
                }
                else
                {
                    if (questGet != "" && ansGet != "" && keywGet != "") //means got a question
                    { //overwrite/store in currcount

                        question[currCount] = questGet;
                        answer[currCount] = ansGet;
                        key[currCount] = keywGet;
                        time[currCount] = timeGet;
                        level[currCount] = levelGet;

                        if (currCount == totalCount)
                        {
                            ++totalCount;
                        }
                    }
                    currCount -= 1;


                    //Label1.Text = currCount.ToString()+","+ question[currCount] + "," + answer[currCount] + ", " + key[currCount];

                    lblQNum.Text = (currCount + 1).ToString();
                    txtQues.Text = question[currCount].ToString();
                    txtAns.Text = answer[currCount].ToString();
                    txtKeyword.Text = key[currCount].ToString();
                    ddlCompleteTime.SelectedIndex = time[currCount];
                    ddlLevel.SelectedIndex = level[currCount];


                    //Label1.Text = "curr= " + currCount + "total= " + totalCount + "Q = "+ question[currCount];

                }

            }
            else
            {
                btnRemove.Visible = false;
                btnBack.Visible = false;
            }

            if(currCount==0)
            {
                btnRemove.Visible = false;
                btnBack.Visible = false;
            }
            Label4.Text = "currcount= " + currCount + "// total count= " + totalCount;

        }

        protected Boolean checkKeyword(string ans, string keywd)
        {
            int count = 0;
            char delimiters = ' ';
            string[] splitArray = keywd.Split(delimiters);
            for (int i = 0; i < splitArray.Length; i++)
            {
                if (ans.Contains(splitArray[i]))
                {
                   
                }
                else
                {
                    count += 1;
                }
            }

            if(count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void Button2_Click(object sender, EventArgs e) //next
        {
            clearLabel();
            string questGet = txtQues.Text.Trim();
            string ansGet = txtAns.Text.Trim();
            string keywGet = txtKeyword.Text.Trim();
            int timeGet = ddlCompleteTime.SelectedIndex;
            int levelGet = ddlLevel.SelectedIndex;

            
            if (questGet == "")
            {
                lblQuestEnter.Visible = true;
                lblQuestEnter.Text = "Please fill in the question!";
            }
            else if (ansGet == "")
            {
                lblAnsEnter.Visible = true;
                lblAnsEnter.Text = "Please fill in the sample answer!";
            }
            else if (keywGet == "")
            {
                lblKeyEnter.Visible = true;
                lblKeyEnter.Text = "Please fill in the keyword!";
            }
            else if(checkKeyword(ansGet,keywGet) == false)
            {
                lblKeyEnter.Visible = true;
                lblKeyEnter.Text = "The keyword must exist in the answer provided!";
            }
            else
            {
                question[currCount] = questGet;
                answer[currCount] = ansGet;
                key[currCount] = keywGet;
                time[currCount] = timeGet;
                level[currCount] = levelGet;
                
                currCount += 1;
                lblQNum.Text = "";
                lblQNum.Text = (currCount + 1).ToString();

                if (currCount < totalCount)
                {
                    txtQues.Text = question[currCount].ToString();
                    txtAns.Text = answer[currCount].ToString();
                    txtKeyword.Text = key[currCount].ToString();
                    ddlCompleteTime.SelectedIndex = time[currCount];
                    ddlLevel.SelectedIndex = level[currCount];
                    clearLabel();

                }
                else
                {
                    if(currCount==totalCount)
                    {

                    }
                    else
                    {
                        ++totalCount;
                    }

                    txtQues.Text = "";
                    txtAns.Text = "";
                    txtKeyword.Text = "";
                    ddlCompleteTime.SelectedIndex = 0;
                    ddlLevel.SelectedIndex = 0;
                    clearLabel();
                }

                btnRemove.Visible = true;
                btnBack.Visible = true;

            }
            Label4.Text = "currcount= " + currCount + "// total count= " + totalCount;
        }

        protected void clearLabel()
        {
            lblQuestEnter.Text = "";
            lblAnsEnter.Text = "";
            lblKeyEnter.Text = "";
        }

        protected void Button3_Click(object sender, EventArgs e) //reset
        {
            txtQues.Text = "";
            txtAns.Text = "";
            txtKeyword.Text = "";
            ddlCompleteTime.SelectedIndex = 0;
            ddlLevel.SelectedIndex = 0;
            clearLabel();
        }

        protected Boolean checkTutNumTitle()
        {
            string tutNumGet = txtTutNum.Text.Trim();
            string tutTitleGet = txtTutName.Text.Trim();
            int tutNumValid = 0;

            if (tutNumGet == "")
            {
                lblTutNumEnter.Text = "Please fill in the tutorial number!";
                return false;
            }
            else if (tutTitleGet == "")
            {
                lblTutTitleEnter.Text = "Please fill in the tutorial title!";
                return false;
            }
            else
            {
                foreach (Char c in tutNumGet)
                {
                    if (char.IsDigit(c) == false)
                    {
                        tutNumValid += 1;

                    }
                }

                if (tutNumValid > 0)
                {
                    lblTutNumEnter.Text = "Please fill in digit only!";
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        protected void txtTutNum_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e) //finish
        {
            tutNumGet = txtTutNum.Text.Trim();
            tutTitleGet = txtTutName.Text.Trim();
            string questGet = txtQues.Text.Trim();
            string ansGet = txtAns.Text.Trim();
            string keywGet = txtKeyword.Text.Trim();
            int timeGet = ddlCompleteTime.SelectedIndex;
            easy = 0;
            medium = 0;
            hard = 0;

            int levelGet = ddlLevel.SelectedIndex;
            if (questGet == "" && ansGet == "" && keywGet == "" && question.Length == 0)
            {//no quest avai
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter at least one question'); window.location.href='CreateTut.aspx';", true);

            }
            else
            {
                clearLabel();
                if (questGet == "" && (ansGet != "" || keywGet != ""))
                {
                    lblQuestEnter.Text = "Please fill in the question!";
                }
                else if (ansGet == "" && (questGet != "" || keywGet != ""))
                {
                    lblAnsEnter.Text = "Please fill in the sample answer!";
                }
                else if (keywGet == "" && (questGet != "" || ansGet != ""))
                {
                    lblKeyEnter.Text = "Please fill in the keyword!";
                }
                else if (checkKeyword(ansGet, keywGet) == false)
                {
                    lblKeyEnter.Visible = true;
                    lblKeyEnter.Text = "The keyword must exist in the answer provided!";
                }
                else
                {
                    if (questGet != "" && ansGet != "" && keywGet != "") //means got a question
                    { //overwrite/store in currcount

                        question[currCount] = questGet;
                        answer[currCount] = ansGet;
                        key[currCount] = keywGet;
                        time[currCount] = timeGet;
                        level[currCount] = levelGet;

                        currCount += 1;
                        if (currCount > totalCount) //new quest
                        {
                            ++totalCount;

                        }
                        currCount -= 1;
                    }

                    
                if (checkTutNumTitle() == true)
                {
                    //get tut ID
                    conn.Open();
                    sql = "SELECT COUNT(TutorialID) FROM [Tutorial]";
                    SqlCommand cmdTutCount = new SqlCommand(sql, conn);
                    int tutID = (int)cmdTutCount.ExecuteScalar();
                    tutID += 1;
                    tutorialID = "T" + tutID.ToString();
                    //lblNoCourseFound.Text = "TutID: " + tutorialID;

                    //Check tutorial existance (tut num)
                    courseID = ddlCourse.SelectedItem.Text;
                    char delimiters = ' ';
                    string[] splitArray = courseID.Split(delimiters);
                    courseID = splitArray[0];

                    for (int i = 1; i < splitArray.Length; i++)
                    {
                        coursename += splitArray[i] + " "; //coursename
                    }
                    coursename = coursename.TrimEnd();

                    sql = "SELECT TutorialNumber FROM [Tutorial] WHERE CourseID = @courseID";
                    SqlCommand cmdGetTutNum = new SqlCommand(sql, conn);
                    cmdGetTutNum.Parameters.AddWithValue("@courseID", courseID);
                    SqlDataReader dtr = cmdGetTutNum.ExecuteReader();
                    int dbTutNum;
                    int valid = 0;
                    tutNumInt = int.Parse(tutNumGet);
                    while (dtr.Read())
                    {
                        dbTutNum = dtr.GetInt32(0);
                        if (tutNumInt == dbTutNum)
                        {
                            valid += 1;
                        }
                    }

                    conn.Close();
                    if (valid > 0)
                    {
                        lblTutNumEnter.Visible = true;
                        lblTutNumEnter.Text = "Tutorial number already existed!";
                    }


                    if (valid == 0) //tut num valid
                    {
                        //done
                        //calc level
                        for (int i = 0; i < totalCount; i++)
                        {
                            if (level[i] == 0)
                            {
                                easy += 1;
                            }
                            else if (level[i] == 1)
                            {
                                medium += 1;
                            }
                            else
                            {
                                hard += 1;
                            }
                        }

                            //here
                            List<string> listQuestPrev = new List<string>();
                            List<string> listAnsPrev = new List<string>();
                            List<string> listKeyPrev = new List<string>();
                            List<int> listTimePrev = new List<int>();
                            List<string> listlevelPrev = new List<string>();
                            List<string> listTimeText = new List<string>();

                            string txt;

                            listQuestPrev = question.ToList();
                            listAnsPrev = answer.ToList();
                            listKeyPrev = key.ToList();
                            for (int i = 0; i < totalCount; i++)
                            {
                                if (level[i] == 0)
                                {
                                    listlevelPrev.Add("Easy");
                                }
                                else if (level[i] == 1)
                                {
                                    listlevelPrev.Add("Medium");
                                }
                                else
                                {
                                    listlevelPrev.Add("Difficult");
                                }

                                
                               // listTimeText.Add()
                            }

                            listTimePrev = time.ToList();
                            int plus;
                            Dictionary<string, Dictionary<string,Dictionary<string, Dictionary<string, Dictionary<string, string>>>>> final =
                  new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>>();

                             for (int i = 0; i < totalCount; i++)
                            {
                                Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> combined =
                                new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();

                                Dictionary<string, Dictionary<string, Dictionary<string, string>>> inner = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

                                Dictionary<string, Dictionary<string, string>> inner2 = new Dictionary<string, Dictionary<string, string>>();
                                Dictionary<string, string> inner3 = new Dictionary<string, string>();

                                plus = listTimePrev[i] + 1;
                                txt = plus.ToString();

                                inner3.Add((plus).ToString(),
                                    listlevelPrev[i].ToString());

                                inner2.Add(listKeyPrev[i].ToString(),
                                       inner3);
                                inner.Add(listAnsPrev[i], inner2);

                                plus = i + 1;
                                txt = plus.ToString();

                                combined.Add(listQuestPrev[i].ToString(), inner);
                                final.Add((txt).ToString(), combined);

                               // combined.Clear();
                            //    inner.Clear();
                              //  inner2.Clear();
                              //  inner3.Clear();
                            }

                            categoryRepeater.DataSource = final;
                            categoryRepeater.DataBind();
                            final.Clear();


                            Label4.Text = "currcount= " + currCount + "// total count= " + totalCount;

                        lblCompEasyNum.Text = easy.ToString();
                        lblCompMedNum.Text = medium.ToString();
                        lblCompDiffNum.Text = hard.ToString();
                            //UpdatePanel5.Visible = false;
                            //compQuestNum.Visible = true;
                        ModalPopupExtender3.Show();
                    }
                }
            }


            }
        }



        protected void btnContactCancel_Click(object sender, EventArgs e)
        {
            lblCompErrorMsg.Text = "";
            lblCompErrorMsg.Visible = false;
           // UpdatePanel5.Visible = true;
           // compQuestNum.Visible = false;
            ModalPopupExtender3.Hide();

        }

        protected void btnChgContact_Click(object sender, EventArgs e) //done
        {
            string easyGet = txtEasy.Text;
            string mediumGet = txtMed.Text;
            string hardGet = txtDifficult.Text;
            int easyGetInt = 0, mediumGetInt = 0, HardGetInt = 0, valid = 0;

            lblCompErrorMsg.Visible = false;

            if (easyGet == "" || mediumGet == "" || hardGet == "")
            {
                lblCompErrorMsg.Visible = true;
                lblCompErrorMsg.Text = "Please enter a digit";
            }
            else
            {
                if (checkDigit(easyGet))
                {
                    easyGetInt = int.Parse(easyGet);
                    if ((easyGetInt == 0 && easy == 0) || easyGetInt <= easy)
                    {

                    }
                    else
                    {
                        valid += 1;
                    }
                }
                else
                { //not int
                    valid += 1;
                }

                if (checkDigit(mediumGet))
                {
                    mediumGetInt = int.Parse(mediumGet);
                    if ((mediumGetInt == 0 && medium == 0) || mediumGetInt <= medium)
                    {

                    }
                    else
                    {
                        valid += 1;
                    }
                }
                else
                {
                    valid += 1;
                }

                if (checkDigit(hardGet))
                {
                    HardGetInt = int.Parse(hardGet);
                    if ((HardGetInt == 0 && hard == 0) || HardGetInt <= hard)
                    {

                    }
                    else
                    {
                        valid += 1;
                    }
                }
                else
                {
                    valid += 1;
                }

                if (valid > 0)
                {
                    lblCompErrorMsg.Visible = true;
                    lblCompErrorMsg.Text = "Compulsory question must be at least one and within range!";

                }
                else
                {
                    //store db
                    int status = 1;
                    //insert tutorial
                    sql = "insert into [Tutorial] values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
                    conn.Open();
                    SqlCommand cmdAddTut = new SqlCommand(sql, conn);
                    cmdAddTut.Parameters.AddWithValue("@p1", tutorialID); //tut ID
                    cmdAddTut.Parameters.AddWithValue("@p2", tutNumInt); //tut num
                    cmdAddTut.Parameters.AddWithValue("@p3", tutTitleGet); //chap name
                    cmdAddTut.Parameters.AddWithValue("@p4", easyGetInt); //comp easy
                    cmdAddTut.Parameters.AddWithValue("@p5", mediumGetInt); //comp medium
                    cmdAddTut.Parameters.AddWithValue("@p6", HardGetInt); //comp hard
                    cmdAddTut.Parameters.AddWithValue("@p7", status); //status
                    cmdAddTut.Parameters.AddWithValue("@p8", ""); //duration
                    cmdAddTut.Parameters.AddWithValue("@p9", ""); //startdate
                    cmdAddTut.Parameters.AddWithValue("@p10", ""); //exp date
                    cmdAddTut.Parameters.AddWithValue("@p11", courseID); //course ID
                    cmdAddTut.ExecuteNonQuery();

                    //Label1.Text = "Db Tutorial success";

                    //insert question     
                    //get quest ID
                    //get tut ID

                    sql = "SELECT COUNT(QuestionID) FROM [Question]";
                    SqlCommand cmdQuesCount = new SqlCommand(sql, conn);
                    int questID = (int)cmdQuesCount.ExecuteScalar();
                    string levelTxt = "", txt = "";
                    //Label2.Text = "TutID: " + tutorialID;

                    for (int i = 0; i < totalCount; i++)
                    {
                        questID += 1;
                        questionID = "Q" + questID.ToString();

                        if (level[i] == 0)
                        {
                            levelTxt = "easy";
                        }
                        else if (level[i] == 1)
                        {
                            levelTxt = "medium";
                        }
                        else
                        {
                            levelTxt = "difficult";
                        }


                        sql = "insert into [Question] values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";
                        //conn.Open();
                        SqlCommand cmdAddQuest = new SqlCommand(sql, conn);
                        cmdAddQuest.Parameters.AddWithValue("@p1", questionID); //quest ID
                        cmdAddQuest.Parameters.AddWithValue("@p2", question[i]); //quest
                        cmdAddQuest.Parameters.AddWithValue("@p3", levelTxt); //level
                        cmdAddQuest.Parameters.AddWithValue("@p4", answer[i]); //sample ans
                        cmdAddQuest.Parameters.AddWithValue("@p5", key[i]); //keyw
                        cmdAddQuest.Parameters.AddWithValue("@p6", tutorialID); //tutorialID
                        cmdAddQuest.Parameters.AddWithValue("@p7", (time[i] + 1)); //time
                        cmdAddQuest.Parameters.AddWithValue("@p8", status); //status
                        cmdAddQuest.ExecuteNonQuery();

                        txt += "Db Question" + (i + 1) + "success";
                    }
                    Label4.Text = txt;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Tutorial Created Successfully'); window.location.href='TutorialList.aspx?course=" + courseID + "&name=" + coursename+"';", true);
                    //Response.Redirect("TutorialList.aspx?course=" + courseID + "&name=" + coursename);

                }

            }

        }

        protected Boolean checkDigit(string num)
        {
            foreach (Char c in num)
            {
                if (char.IsDigit(c) == false)
                {
                    return false;

                }
            }
            return true;
        }

        protected void Button1_Click2(object sender, EventArgs e)
        {

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (currCount != totalCount)
            {
                listQuest = question.ToList();
                listAns = answer.ToList();
                listKey = key.ToList();
                listlevel = level.ToList();
                listTime = time.ToList();

                listQuest.RemoveAt(currCount);
                listAns.RemoveAt(currCount);
                listKey.RemoveAt(currCount);
                listlevel.RemoveAt(currCount);
                listTime.RemoveAt(currCount);

                totalCount -= 1;
                if (currCount > totalCount)
                {
                    currCount -= 1;
                }

                question = listQuest.ToArray();
                answer = listAns.ToArray();
                key = listKey.ToArray();
                level = listlevel.ToArray();
                time = listTime.ToArray();

                if (currCount < totalCount)
                {
                    txtQues.Text = question[currCount].ToString();
                    txtAns.Text = answer[currCount].ToString();
                    txtKeyword.Text = key[currCount].ToString();
                    ddlCompleteTime.SelectedIndex = time[currCount];
                    ddlLevel.SelectedIndex = level[currCount];
                    clearLabel();

                }
                else
                {
                    //++totalCount;
                    txtQues.Text = "";
                    txtAns.Text = "";
                    txtKeyword.Text = "";
                    ddlCompleteTime.SelectedIndex = 0;
                    ddlLevel.SelectedIndex = 0;
                    lblQNum.Text = "";
                    lblQNum.Text = (currCount + 1).ToString();

                    clearLabel();
                }
            }
            else
            {
                txtQues.Text = "";
                txtAns.Text = "";
                txtKeyword.Text = "";
                ddlCompleteTime.SelectedIndex = 0;
                ddlLevel.SelectedIndex = 0;
                clearLabel();
            }

        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

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

        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecResultHome.aspx");
        }
        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }


    }
}