﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ADM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        static string[] question = new string[100];
        static string[] answer = new string[100];
        static string[] key = new string[100];
        static int[] time = new int[100];
        static int[] level = new int[100];
        static int totalCount, currCount;
        static string tryy = "";
        int easy = 0, medium = 0, hard = 0;
        int durationInt = 0;
        string sql, tutorialID, courseID, questionID = "";
        string tutNumGet;
        string tutTitleGet;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e) //back
        {
            clearLabel();

            if (currCount > 0)
            {
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
            else
            {
                //Label2.Text = " Now currcount = " + currCount;
                question[currCount] = questGet;
                answer[currCount] = ansGet;
                key[currCount] = keywGet;
                time[currCount] = timeGet;
                level[currCount] = levelGet;


                currCount += 1;

                //Label2.Text = " next currcount = " + currCount;
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
                    ++totalCount;
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

        protected void txtDifficult_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtTutNum_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e) //finish
        {
            if (question.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter at least one question'); window.location.href='CreateTut.aspx';", true);

            }
            else
            {
                tutNumGet = txtTutNum.Text.Trim();
                tutTitleGet = txtTutName.Text.Trim();
                string questGet = txtQues.Text.Trim();
                string ansGet = txtAns.Text.Trim();
                string keywGet = txtKeyword.Text.Trim();
                int timeGet = ddlCompleteTime.SelectedIndex;
                int levelGet = ddlLevel.SelectedIndex;

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
                else
                {
                    if (questGet != "" && ansGet != "" && keywGet != "")
                    {
                        question[currCount] = questGet;
                        answer[currCount] = ansGet;
                        key[currCount] = keywGet;
                        time[currCount] = timeGet;
                        level[currCount] = levelGet;

                        currCount += 1;

                        ++totalCount;
                    }

                    txtQues.Text = "";
                    txtAns.Text = "";
                    txtKeyword.Text = "";
                    ddlCompleteTime.SelectedIndex = 0;
                    ddlLevel.SelectedIndex = 0;
                    clearLabel();

                    if (checkTutNumTitle() == true)
                    {
                        //get tut ID
                        conn.Open();
                        sql = "SELECT COUNT(TutorialID) FROM [Tutorial]";
                        SqlCommand cmdTutCount = new SqlCommand(sql, conn);
                        int tutID = (int)cmdTutCount.ExecuteScalar();
                        tutID += 1;
                        tutorialID = "T" + tutID.ToString();
                        //Label2.Text = "TutID: " + tutorialID;

                        //Check tutorial existance (tut num)
                        courseID = ddlCourse.SelectedItem.Text;
                        char delimiters = ' ';
                        string[] splitArray = courseID.Split(delimiters);
                        courseID = splitArray[0];

                        sql = "SELECT TutorialNumber FROM [Tutorial] WHERE CourseID = @courseID";
                        SqlCommand cmdGetTutNum = new SqlCommand(sql, conn);
                        cmdGetTutNum.Parameters.AddWithValue("@courseID", courseID);
                        SqlDataReader dtr = cmdGetTutNum.ExecuteReader();
                        int dbTutNum;
                        int valid = 0;
                        int tutNumInt = int.Parse(tutNumGet);
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
                            lblTutNumEnter.Text = "Tutorial number already existed!";
                        }

                        //check duration
                        string duration = txtDuration.Text;
                        int durationValid = 0;
                        foreach (Char c in duration) //check is digit or not
                        {
                            if (char.IsDigit(c) == false)
                            {
                                durationValid += 1;

                            }
                        }

                        if (txtDuration.Text == "")
                        {
                            lblDurationEnter.Text = "Please fill in the duration!";
                            durationValid += 1;
                        }
                        else
                        {
                            durationInt = int.Parse(duration);
                            if (durationValid == 0)
                            {
                                if (durationInt <= 0)
                                {
                                    lblDurationEnter.Text = "Please enter a valid digit (Minimum duration is 1 day)";
                                    durationValid += 1;
                                }
                            }

                        }

                        if (valid == 0 && durationValid == 0) //tut num valid
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

                            lblCompEasyNum.Text = easy.ToString();
                            lblCompMedNum.Text = medium.ToString();
                            lblCompDiffNum.Text = hard.ToString();
                            ModalPopupExtender3.Show();
                        }
                    }
                    conn.Close();



                }
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

            if (checkDigit(easyGet))
            {
                easyGetInt = int.Parse(easyGet);
                if ((easyGetInt == 0 && easy != 0) || easyGetInt > easy)
                {
                    valid += 1;
                }
            }
            else
            {
                valid += 1;
            }

            if (checkDigit(mediumGet))
            {
                mediumGetInt = int.Parse(easyGet);
                if ((mediumGetInt == 0 && medium != 0) || mediumGetInt > medium)
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
                HardGetInt = int.Parse(easyGet);
                if ((HardGetInt == 0 && hard != 0) || HardGetInt > hard)
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
                //store db
                int status = 1;
                //insert tutorial
                sql = "insert into [Tutorial] values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
                conn.Open();
                SqlCommand cmdAddTut = new SqlCommand(sql, conn);
                cmdAddTut.Parameters.AddWithValue("@p1", tutorialID); //tut ID
                cmdAddTut.Parameters.AddWithValue("@p2", int.Parse(tutNumGet)); //tut num
                cmdAddTut.Parameters.AddWithValue("@p3", tutTitleGet); //chap name
                cmdAddTut.Parameters.AddWithValue("@p4", easyGetInt); //comp easy
                cmdAddTut.Parameters.AddWithValue("@p5", mediumGetInt); //comp medium
                cmdAddTut.Parameters.AddWithValue("@p6", HardGetInt); //comp hard
                cmdAddTut.Parameters.AddWithValue("@p7", status); //status
                cmdAddTut.Parameters.AddWithValue("@p8", durationInt); //duration
                cmdAddTut.Parameters.AddWithValue("@p9", ""); //startdate
                cmdAddTut.Parameters.AddWithValue("@p10", ""); //exp date
                cmdAddTut.Parameters.AddWithValue("@p11", courseID); //course ID
                cmdAddTut.ExecuteNonQuery();

                Label1.Text = "Db Tutorial success";

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
                    cmdAddQuest.Parameters.AddWithValue("@p6", (time[i] + 1)); //time
                    cmdAddQuest.Parameters.AddWithValue("@p7", tutorialID); //tutorialID
                    cmdAddQuest.Parameters.AddWithValue("@p8", status); //status
                    cmdAddQuest.ExecuteNonQuery();

                    txt += "Db Question" + (i + 1) + "success";
                }
                Label2.Text = txt;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Tutorial Created Successfully'); window.location.href='CreateTut.aspx';", true);

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
    }
}