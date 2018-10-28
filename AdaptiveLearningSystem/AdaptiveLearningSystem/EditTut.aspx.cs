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
    public partial class EditTut : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        static string[] qid = new string[100];
        static string[] question = new string[100];
        static string[] answer = new string[100];
        static string[] key = new string[100];
        static int[] time = new int[100];
        static int[] level = new int[100];
        static int[] status = new int[100];
        static string[] leveltext = new string[100];
        static int totalCount, currCount;
        static string tryy = "";
        static int easy, medium, hard, tutNumInt;
        static int durationInt = 0;
        string sql, questionID = "";
        static string tutorialID, lecID;
        static string tutNumGet;
        static string tutTitleGet;
        static int checkFinish = 0;
        List<string> listQuest = new List<string>();
        List<string> listAns = new List<string>();
        List<string> listKey = new List<string>();
        List<int> listTime = new List<int>();
        List<int> listlevel = new List<int>();
        static int oriQuestCount;
        static string courseID, coursename, tutNum, tutTitle, tutID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lecturerID"] != null)
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = Session["lecName"].ToString();
                    currCount = 0;
                    //lecID = Session["lecturerID"].ToString();
                    courseID = Request.QueryString["course"].ToString();
                    coursename = Request.QueryString["coursename"].ToString();
                    tutNum = Request.QueryString["tutNum"].ToString();
                    tutTitle = Request.QueryString["tutTitle"].ToString();
                    lblCourse.Text = courseID + " " + coursename;
                    txtTutNum.Text = tutNum;
                    txtTutName.Text = tutTitle;

                    //get tutID
                    conn.Open();
                    string sql = "SELECT TutorialID FROM Tutorial WHERE TutorialNumber = @tutNum AND CourseID = @courseID";
                    SqlCommand cmdGetTutID = new SqlCommand(sql, conn);
                    cmdGetTutID.Parameters.AddWithValue("@tutNum", tutNum);
                    cmdGetTutID.Parameters.AddWithValue("@courseID", courseID);
                    SqlDataReader dtr = cmdGetTutID.ExecuteReader();
                    while (dtr.Read())
                    {
                        tutID = dtr.GetString(0);
                    }
                    conn.Close();

                    sql = "SELECT q.QuestionID,q.Question,q.SampleAns,q.Keyword,q.TimeLimit,q.Level FROM Tutorial t, Question q WHERE q.TutorialID = @tutID AND q.Status = 1 GROUP BY q.QuestionID,q.Question,q.SampleAns,q.Keyword,q.TimeLimit,q.Level ORDER BY q.QuestionID ASC";
                    SqlCommand cmdGetQuest = new SqlCommand(sql, conn);
                    cmdGetQuest.Parameters.AddWithValue("@tutID", tutID);
                    conn.Open();
                    //oriQuestCount = 0;
                    dtr = cmdGetQuest.ExecuteReader();
                    while (dtr.Read())
                    {
                        qid[oriQuestCount] = dtr.GetString(0);
                        question[oriQuestCount] = dtr.GetString(1);
                        answer[oriQuestCount] = dtr.GetString(2);
                        key[oriQuestCount] = dtr.GetString(3);
                        time[oriQuestCount] = dtr.GetInt32(4) - 1;
                        leveltext[oriQuestCount] = dtr.GetString(5);
                        oriQuestCount += 1;

                    }
                    conn.Close();

                    for (int i = 0; i < oriQuestCount; i++)
                    {
                        if (leveltext[i].Contains("easy"))
                        {
                            level[i] = 0;
                        }
                        else if (leveltext[i].Contains("medium"))
                        {
                            level[i] = 1;
                        }
                        else
                        {
                            level[i] = 2;
                        }
                    }


                    txtQues.Text = question[currCount];
                    txtAns.Text = answer[currCount];
                    txtKeyword.Text = key[currCount];
                    ddlCompleteTime.SelectedIndex = time[currCount];
                    ddlLevel.SelectedIndex = level[currCount];
                    totalCount = oriQuestCount;

                    string txt = "";
                    for (int i = 0; i < oriQuestCount; i++)
                    {
                        txt += i + "=" + qid[i];
                    }
                    Label4.Text = txt;

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
        }

        protected void Button2_Click(object sender, EventArgs e) //next
        {
            clearLabel();
            checkFinish = 0;
            string questGet = txtQues.Text.Trim();
            string ansGet = txtAns.Text.Trim();
            string keywGet = txtKeyword.Text.Trim();
            int timeGet = ddlCompleteTime.SelectedIndex;
            int levelGet = ddlLevel.SelectedIndex;

            if (questGet == "")
            {
                lblQuestEnter.Text = "Please fill in the question!";
            }
            else if (ansGet == "")
            {
                lblAnsEnter.Text = "Please fill in the sample answer!";
            }
            else if (keywGet == "")
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
                    if (currCount == totalCount)
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


            }
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
            int levelGet = ddlLevel.SelectedIndex;
            easy = 0;
            medium = 0;
            hard = 0;

            if (questGet == "" && ansGet == "" && keywGet == "" && question.Length == 0)
            {//no quest avai
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter at least one question'); window.location.href='CreateTut.aspx';", true);

            }
            else
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

                        currCount += 1;
                    }

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

                    char delimiters = ' ';
                    string[] splitArray = courseID.Split(delimiters);
                    courseID = splitArray[0];

                    sql = "SELECT TutorialNumber,TutorialID FROM [Tutorial] WHERE CourseID = @courseID";
                    SqlCommand cmdGetTutNum = new SqlCommand(sql, conn);
                    cmdGetTutNum.Parameters.AddWithValue("@courseID", courseID);
                    SqlDataReader dtr = cmdGetTutNum.ExecuteReader();
                    int dbTutNum;
                    string dbTutID;
                    int valid = 0;
                    tutNumInt = int.Parse(tutNumGet);
                    while (dtr.Read())
                    {
                        dbTutNum = dtr.GetInt32(0);
                        dbTutID = dtr.GetString(1);

                        if (tutNumInt == dbTutNum)
                        {
                            if (tutID != dbTutID)
                            {
                                valid += 1;
                            }


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

                        checkFinish = 1;
                        lblCompEasyNum.Text = easy.ToString();
                        lblCompMedNum.Text = medium.ToString();
                        lblCompDiffNum.Text = hard.ToString();
                        ModalPopupExtender3.Show();
                    }
                }



            }
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

            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnContactCancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3.Hide();
        }

        protected void btnChgContact_Click(object sender, EventArgs e) //done
        {
            string easyGet = txtEasy.Text;
            string mediumGet = txtMed.Text;
            string hardGet = txtDifficult.Text;
            int easyGetInt = 0, mediumGetInt = 0, HardGetInt = 0, valid = 0;
            int status;
            string leveltxt = "";

            if (easyGet == "" || mediumGet == "" || hardGet == "")
            {
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
                    lblCompErrorMsg.Text = "Compulsory question must be at least one and within range!";

                }
                else
                {

                    if (totalCount > oriQuestCount) //after edit, more question appeared
                    {
                        for (int i = 0; i < oriQuestCount; i++)
                        {
                            if (level[i] == 0)
                            {
                                leveltxt = "easy";
                            }
                            else if (level[i] == 1)
                            {
                                leveltxt = "medium";
                            }
                            else
                            {
                                leveltxt = "difficult";
                            }

                            //update 
                            sql = "UPDATE Question SET Question = @question, SampleAns = @ans, Keyword = @keyword, Level = @level, TimeLimit = @timelimit WHERE QuestionID = @questID";
                            SqlCommand cmdUpdate = new SqlCommand(sql, conn);
                            cmdUpdate.Parameters.AddWithValue("@question", question[i]);
                            cmdUpdate.Parameters.AddWithValue("@ans", answer[i]);
                            cmdUpdate.Parameters.AddWithValue("@keyword", key[i]);
                            cmdUpdate.Parameters.AddWithValue("@level", leveltxt);
                            cmdUpdate.Parameters.AddWithValue("@timelimit", time[i] + 1);
                            cmdUpdate.Parameters.AddWithValue("@questID", qid[i]);
                            conn.Open();
                            cmdUpdate.ExecuteNonQuery();
                            conn.Close();
                        }

                        sql = "SELECT COUNT(QuestionID) FROM [Question]";
                        SqlCommand cmdQuesCount = new SqlCommand(sql, conn);
                        int questID = (int)cmdQuesCount.ExecuteScalar();

                        for (int i = oriQuestCount; i < totalCount; i++)
                        {
                            //insert new quest

                            questID += 1;
                            questionID = "Q" + questID.ToString();

                            if (level[i] == 0)
                            {
                                leveltxt = "easy";
                            }
                            else if (level[i] == 1)
                            {
                                leveltxt = "medium";
                            }
                            else
                            {
                                leveltxt = "difficult";
                            }

                            status = 1;
                            sql = "insert into [Question] values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";
                            //conn.Open();
                            SqlCommand cmdAddQuest = new SqlCommand(sql, conn);
                            cmdAddQuest.Parameters.AddWithValue("@p1", questionID); //quest ID
                            cmdAddQuest.Parameters.AddWithValue("@p2", question[i]); //quest
                            cmdAddQuest.Parameters.AddWithValue("@p3", leveltxt); //level
                            cmdAddQuest.Parameters.AddWithValue("@p4", answer[i]); //sample ans
                            cmdAddQuest.Parameters.AddWithValue("@p5", key[i]); //keyw
                            cmdAddQuest.Parameters.AddWithValue("@p6", tutorialID); //tutorialID
                            cmdAddQuest.Parameters.AddWithValue("@p7", (time[i] + 1)); //time
                            cmdAddQuest.Parameters.AddWithValue("@p8", status); //status
                            cmdAddQuest.ExecuteNonQuery();
                        }

                    }
                    else // question less than ori
                    {
                        for (int i = 0; i < totalCount; i++) //update same / =
                        {
                            //update
                            if (level[i] == 0)
                            {
                                leveltxt = "easy";
                            }
                            else if (level[i] == 1)
                            {
                                leveltxt = "medium";
                            }
                            else
                            {
                                leveltxt = "difficult";
                            }

                            //update 
                            sql = "UPDATE Question SET Question = @question, SampleAns = @ans, Keyword = @keyword, Level = @level, TimeLimit = @timelimit WHERE QuestionID = @questID";
                            SqlCommand cmdUpdate = new SqlCommand(sql, conn);
                            cmdUpdate.Parameters.AddWithValue("@question", question[i]);
                            cmdUpdate.Parameters.AddWithValue("@ans", answer[i]);
                            cmdUpdate.Parameters.AddWithValue("@keyword", key[i]);
                            cmdUpdate.Parameters.AddWithValue("@level", leveltxt);
                            cmdUpdate.Parameters.AddWithValue("@timelimit", time[i] + 1);
                            cmdUpdate.Parameters.AddWithValue("@questID", qid[i]);
                            conn.Open();
                            cmdUpdate.ExecuteNonQuery();
                            conn.Close();
                        }

                        if (totalCount < oriQuestCount)
                        {
                            //update (set status =0)
                            for (int i = totalCount; i < oriQuestCount; i++)
                            {
                                status = 0;
                                sql = "UPDATE Question SET Status = 0 WHERE QuestionID = @questID";
                                SqlCommand cmdUpdate = new SqlCommand(sql, conn);
                                cmdUpdate.Parameters.AddWithValue("@questID", qid[i]);
                                conn.Open();
                                cmdUpdate.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    //insert tutorial
                    sql = "UPDATE Tutorial SET TutorialNumber = @tutNum, ChapterName = @tutTitle, CompulsaryEasy = @compEasy, CompulsaryMedium = @compMedium, CompulsaryHard = @compHard WHERE TutorialID = @tutID";
                    SqlCommand cmdUpdateTut = new SqlCommand(sql, conn);
                    cmdUpdateTut.Parameters.AddWithValue("@tutNum", tutNumInt); //tut num
                    cmdUpdateTut.Parameters.AddWithValue("@tutTitle", tutTitleGet); //chap name
                    cmdUpdateTut.Parameters.AddWithValue("@compEasy", easyGetInt); //comp easy
                    cmdUpdateTut.Parameters.AddWithValue("@compMedium", mediumGetInt); //comp medium
                    cmdUpdateTut.Parameters.AddWithValue("@compHard", HardGetInt); //comp hard
                    cmdUpdateTut.Parameters.AddWithValue("@tutID", tutID); //tut ID
                    conn.Open();
                    cmdUpdateTut.ExecuteNonQuery();
                    conn.Close();

                }
                //lblNoCourseFound.Text = txt;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Tutorial Edited Successfully'); window.location.href='EditTutHome.aspx';", true);

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
            if (currCount != totalCount) //cannot remove blank and last item
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
                //status[currCount] = 0;

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
                    ++totalCount;
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

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }

        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecResultHome.aspx");
        }
    }
}