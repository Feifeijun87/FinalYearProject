using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using Iveonik.Stemmers;
using System.Text.RegularExpressions;
using System.Data;
using IronPython.Hosting;

namespace AdaptiveLearningSystem
{
    public partial class AnsTut : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);

        static string[] temp = new string[100];

        static int totalCount, currCount, randomQIDCount;
        static int compEasy = 0, compMed = 0, compHard = 0;
        static int easyDone = 0, medDone = 0, hardDone = 0;
        static int easyLeft = 0, medLeft = 0, hardLeft = 0, totalQuestLeft = 0;
        static string tutorialID;
        static string courseID, studID, tutNum, courseName, chapName;

        static List<string> listQuestID = new List<string>();
        static List<string> listQuest = new List<string>();
        static List<string> listLevel = new List<string>();
        static List<int> listTime = new List<int>();
        static List<string> listEasyQuestionID = new List<string>(); //easy question in db
        static List<string> listMedQuestionID = new List<string>();
        static List<string> listHardQuestionID = new List<string>();
        static List<Boolean> listCompletedQuestion = new List<Boolean>();
        static List<string> listEasyDoneQID = new List<string>(); //easy done by user
        static List<string> listMedDoneQID = new List<string>();
        static List<string> listHardDoneQID = new List<string>();
        static List<string> listTempEasyQID = new List<string>(); //quest that user didnt do
        static List<string> listTempMedQID = new List<string>();
        static List<string> listTempHardQID = new List<string>();
        static List<string> listTemp = new List<string>();
        static List<string> listRandomQuestionID = new List<string>();
        static List<string> listRandomQuestion = new List<string>();
        static List<int> listRandomTime = new List<int>();
        static List<string> listRandomLevel = new List<string>();
        static int mm = 0, ss = 0, questdone;
        static int secUsed = 0;
        static string tutCheckID;
        static int pgload;

        protected void Button2_Click(object sender, EventArgs e) //next
        {
            string ansGet = txtAns.Text;
            if (ansGet == "")
            {
                lblAnsEnter.Text = "Please enter your answer!";
                //lblAnsEnter.Text = pgload.ToString();
            }
            else
            {
                
                Timer1.Enabled = false;

                //your code here
                //time = secUsed
                if (markingAns() == true)
                {
                    lblAnsEnter.Visible = false;
                    string tt = listRandomLevel[currCount].Trim().ToString();

                    if (listRandomLevel[currCount].Trim().ToString() == "easy")
                    {
                        easyLeft--;
                    }
                    else if (listRandomLevel[currCount].Trim().ToString() == "medium")
                    {
                        medLeft--;
                    }
                    else
                    {
                        hardLeft--;
                    }

                    if (easyLeft == 0 && medLeft == 0 && hardLeft == 0)
                    {
                        //update tutorial check
                        conn.Open();
                        string sql = "SELECT COUNT(CheckID) FROM [TutorialCheck]";
                        SqlCommand cmdTutCount = new SqlCommand(sql, conn);
                        int checkID = (int)cmdTutCount.ExecuteScalar();
                        checkID += 1;
                        tutCheckID = "C" + checkID.ToString();
                        conn.Close();

                        int status = 1;
                        sql = "insert into [TutorialCheck] values (@p1,@p2,@p3,@p4,@p5,@p6)";
                        conn.Open();
                        SqlCommand cmdAddTut = new SqlCommand(sql, conn);
                        cmdAddTut.Parameters.AddWithValue("@p1", tutCheckID); //check ID
                        cmdAddTut.Parameters.AddWithValue("@p2", status); //status
                        cmdAddTut.Parameters.AddWithValue("@p3", studID); //stud ID
                        cmdAddTut.Parameters.AddWithValue("@p4", tutorialID); //tutorialID
                        cmdAddTut.Parameters.AddWithValue("@p5", DateTime.Now.ToShortDateString()); //complete date
                        cmdAddTut.Parameters.AddWithValue("@p6", DateTime.Now.ToLongTimeString()); //complete time          
                        cmdAddTut.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You has completed this tutorial'); window.location.href='StudHome.aspx';", true);

                        //back to tut page

                    }
                    else
                    {
                        currCount += 1;
                        questdone += 1;
                        lblQNum.Text = "";
                        lblAnsEnter.Text = "";
                        lblQNum.Text = (questdone + 1).ToString();
                        lblQuest.Text = listRandomQuestion[currCount].ToString();
                        mm = listRandomTime[currCount];
                        ss = 0;
                        secUsed = 0;
                        txtAns.Text = String.Empty;
                        Timer1.Enabled = true;
                    }
                }
                else
                {
                    lblAnsEnter.Text = "Incorrect. Please try again";
                    lblAnsEnter.Visible = true;
                    Timer1.Enabled = true;
                }

            }
        }

        protected Boolean markingAns()
        {
            string sampleAns = "", keyword = "";
            double matchRate = 0;
            //check answer empty
            string studAns = txtAns.Text.Trim();
            if (studAns == String.Empty)
            {
                //lblAnsEnter.Text = "Please enter your answer!";
                lblAnsEnter.Text = pgload.ToString();
                return false;
            }

            //get sample ans         
            conn.Open();
            SqlCommand cmd = new SqlCommand("prc_get_sample_ans", conn);
            cmd.Parameters.AddWithValue("@QuestionID", listRandomQuestionID[currCount]);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sampleAns = dtr.GetString(0).Trim().ToLower();
                keyword = dtr.GetString(1).Trim().ToLower();
            }
            conn.Close();
            //chg the sentence format
            studAns = txtAns.Text.Trim().ToLower();
            studAns = chgWordFormat(studAns);
            sampleAns = chgWordFormat(sampleAns);
            //make exact matching
            if (checkExact(sampleAns, studAns) == true)
            {
                string[] separatedKeywords = keyword.Split(',');
                insertDB(separatedKeywords.Length+1,100);
                return true;
            }
            //check keyword.
            
            else 
            {
                int mark = checkKeyword(keyword, studAns);
                if (mark > 0)
                {
                    //check sentence matching rate
                    double editDistance = Double.Parse(levenshtein(studAns, sampleAns).ToString());
                    matchRate = 100 - ((editDistance / sampleAns.Length) * 100);
                    if (matchRate > 60)
                        mark++;

                    insertDB(mark, matchRate);
                    return true;
                }
                else
                    return false;
            }
        }

        protected void insertDB(int mark,double matchRate)
        {
            int timeBonus = computeMarkForTimeUsed();
            int totalMark = timeBonus + mark;
            string doneDate = DateTime.Now.ToString("MM/dd/yyyy");
            string doneTime = DateTime.Now.ToLongTimeString();
            //get current row count of studans table
            int rowCount = 0;
            conn.Open();
            string sql = "SELECT COUNT(AnswerID) FROM [StudAns]";
            SqlCommand cmdCheckID = new SqlCommand(sql, conn);
            SqlDataReader dt = cmdCheckID.ExecuteReader();
            if (dt.HasRows)
                while (dt.Read())
                    rowCount = dt.GetInt32(0) + 1;
            conn.Close();

            //insert into studans table
            SqlCommand cmdInsert = new SqlCommand("prc_insert_new_student_answer", conn);
            cmdInsert.Parameters.AddWithValue("@AnswerID", "ANS" + rowCount.ToString());
            cmdInsert.Parameters.AddWithValue("@Answer", txtAns.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@TimeSpent", secUsed);
            cmdInsert.Parameters.AddWithValue("@MatchPercent", matchRate.ToString("N2"));
            cmdInsert.Parameters.AddWithValue("@Points", totalMark);
            cmdInsert.Parameters.AddWithValue("@QuestionID", listRandomQuestionID[currCount]);
            cmdInsert.Parameters.AddWithValue("@StudentID", Session["studID"].ToString());
            cmdInsert.Parameters.AddWithValue("@DateComplete", doneDate);
            cmdInsert.Parameters.AddWithValue("@TimeComplete", doneTime);
            cmdInsert.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter insertAnswer = new SqlDataAdapter();
            insertAnswer.UpdateCommand = cmdInsert;
            insertAnswer.UpdateCommand.ExecuteNonQuery();
            cmdInsert.Dispose();
            conn.Close();
        }

        protected int computeMarkForTimeUsed()
        {
            double timeLimitInSec = Double.Parse(listTime[currCount].ToString()) * 60;

            if (Double.Parse(secUsed.ToString()) <= timeLimitInSec)
                return 1;
            else
                return 0;              
        }

        protected string chgWordFormat(string oriString)
        {
            oriString = oriString.Replace(".", " ");
            oriString = oriString.Replace(",", " ");
            oriString = Regex.Replace(oriString, @"\s+", " ");

            return oriString;
        }

        protected Boolean checkExact(string sampleAns, string studAns)
        {
            if (studAns.Equals(sampleAns))
            {
                return true;
            }
            return false;
        }

        protected int checkKeyword(string keyword, string studAns)
        {
            int mark = 0;
            double matchRate = 0.00; // to determine the match ratio of keyword
            string[] separatedKeywords = keyword.Split(',');
            int arrayLength = separatedKeywords.Length;
            double keywordLength = 0;
            double score = 0; // to determine how many keyword match
            string stemmedStudAns = stemWord(new EnglishStemmer(), studAns, 1); //stem the student answer sentence
            foreach (string key in separatedKeywords) // for each keyword 
            {

                string[] splittedKeyword = key.Split(' '); //split the keyword string
                foreach (string word in splittedKeyword)
                {
                    keywordLength += 1;
                    if (studAns.Contains(word)) // if contain the keyword
                        score += 1;
                    else if (studAns.Contains(stemWord(new EnglishStemmer(), word, 2).Trim()))
                        score += 1;
                    else if (stemmedStudAns.Contains(stemWord(new EnglishStemmer(), word, 2).Trim())) // stem the keyword and check against the stemmed student answer
                        score += 1;
                    else
                    {
                        string[] studAnsList = studAns.Split(' ');
                        if (wordnetCheckKeyword(word, studAnsList))
                            score += 1;
                    }

                    
                }
                matchRate = score / keywordLength;
                if (matchRate > 0.6)
                    mark++;

            }
            return mark;
            
        }

        protected string stemWord(IStemmer stemmer, string oriString, int type) //type=1 to stem sentence(answer), 2 to stem a word (keyword)
        {
            string stemmedString = "";
            if (type == 1)
            {
                string[] splittedString = oriString.Split(' ');

                foreach (string word in splittedString)
                {
                    stemmedString += stemmer.Stem(word) + " ";
                }
            }
            else
            {
                return stemmer.Stem(oriString);
            }
            return stemmedString;

        }

        private Boolean wordnetCheckKeyword(string keyword, string[] studAns)
        {
            var engine = Python.CreateEngine();
            var searchPaths = engine.GetSearchPaths();
            searchPaths.Add(@"C:\Python27");
            searchPaths.Add(@"C:\Python27\Lib");
            searchPaths.Add(@"C:\Python27\libs");
            searchPaths.Add(@"C:\Python27\Lib\site-packages");
            searchPaths.Add(@"C:\Users\ASUSPC\Documents\Visual Studio 2015\Projects\ADM\AdaptiveLearningSystem\packages\IronPython.2.7.9\lib");
            searchPaths.Add(@"C:\Users\ASUSPC\Documents\Visual Studio 2015\Projects\ADM\AdaptiveLearningSystem\packages\IronPython.2.7.9\lib\net45\IronPython.SQLite.dll");
            searchPaths.Add(@"C:\Users\ASUSPC\Documents\Visual Studio 2015\Projects\ADM\AdaptiveLearningSystem\packages\IronPython.2.7.9\wordMatching.py");

            engine.SetSearchPaths(searchPaths);
            var mainfile = @"C:\Users\ASUSPC\Documents\Visual Studio 2015\Projects\ADM\AdaptiveLearningSystem\packages\IronPython.2.7.9\wordMatching.py";
            var scope = engine.CreateScope();
            scope.SetVariable("sentence", studAns);
            scope.SetVariable("keyword", keyword);
            engine.CreateScriptSourceFromFile(mainfile).Execute(scope);

            return(scope.GetVariable("result"));  

            
        }

        private int levenshtein(string studAns, string sampleAns)
        {

            if (string.IsNullOrEmpty(studAns))
            {
                if (!string.IsNullOrEmpty(sampleAns))
                {
                    return sampleAns.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(sampleAns))
            {
                if (!string.IsNullOrEmpty(studAns))
                {
                    return studAns.Length;
                }
                return 0;
            }

            int cost;
            int[,] d = new int[studAns.Length + 1, sampleAns.Length + 1];
            int min1;
            int min2;
            int min3;

            for (int i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (int i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (int i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (int j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = (studAns[i - 1] != sampleAns[j - 1]) ? 1 : 0;

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }

        protected void btnScore_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("prc_get_stud_result_per_ques", conn);
            cmd.Parameters.AddWithValue("@StudentID", Session["studID"].ToString());
            cmd.Parameters.AddWithValue("@TutorialNumber", tutNum);
            cmd.Parameters.AddWithValue("@CourseID", courseID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            ModalPopupExtender1.Show();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            txtAns.Text = "";
        }

        protected void Button4_Click(object sender, EventArgs e)//done
        {
            string ansGet = txtAns.Text;

                if (ansGet == "")
                {
                    //lblAnsEnter.Text = "Please enter your answer!";
                    //lblAnsEnter.Text = pgload.ToString();
                   
                    listQuestID.Clear();
                    listQuest.Clear();
                    listLevel.Clear();
                    listTime.Clear();
                    listEasyQuestionID.Clear();
                    listMedQuestionID.Clear();
                    listHardQuestionID.Clear();
                    listCompletedQuestion.Clear();
                    listEasyDoneQID.Clear();
                    listMedDoneQID.Clear();
                    listHardDoneQID.Clear();
                    listTemp.Clear();
                    listTempEasyQID.Clear();
                    listTempMedQID.Clear();
                    listTempHardQID.Clear();
                    totalCount = 0;
                    currCount = 0;
                    randomQIDCount = 0;
                    compEasy = 0;
                    compMed = 0;
                    compHard = 0;
                    easyDone = 0;
                    medDone = 0;
                    hardDone = 0;
                    easyLeft = 0;
                    medLeft = 0;
                    hardLeft = 0;
                    totalQuestLeft = 0;
                    listRandomQuestionID.Clear();
                    listRandomQuestion.Clear();
                    listRandomTime.Clear();
                    listRandomLevel.Clear();
                    Response.Redirect("StudHome.aspx");
                }
                else
                {
                    Timer1.Enabled = false;

                    if (markingAns() == true)
                    {
                        //your code here
                        //time = secUsed

                        if (listRandomLevel[currCount].ToString() == "easy")
                        {
                            easyLeft--;
                        }
                        else if (listRandomLevel[currCount].ToString() == "medium")
                        {
                            medLeft--;
                        }
                        else
                        {
                            hardLeft--;
                        }

                        if (easyLeft == 0 && medLeft == 0 && hardLeft == 0)
                        {
                            //update tutorial check
                            conn.Open();
                            string sql = "SELECT COUNT(CheckID) FROM [TutorialCheck]";
                            SqlCommand cmdTutCount = new SqlCommand(sql, conn);
                            int checkID = (int)cmdTutCount.ExecuteScalar();
                            checkID += 1;
                            tutCheckID = "C" + checkID.ToString();

                            int status = 1;
                            sql = "insert into [TutorialCheck] values (@p1,@p2,@p3,@p4,@p5,@p6)";
                            conn.Open();
                            SqlCommand cmdAddTut = new SqlCommand(sql, conn);
                            cmdAddTut.Parameters.AddWithValue("@p1", tutCheckID); //check ID
                            cmdAddTut.Parameters.AddWithValue("@p2", status); //status
                            cmdAddTut.Parameters.AddWithValue("@p3", studID); //stud ID
                            cmdAddTut.Parameters.AddWithValue("@p4", tutorialID); //tutorialID
                            cmdAddTut.Parameters.AddWithValue("@p5", DateTime.Now.ToString("MM/dd/yyyy")); //complete date
                            cmdAddTut.Parameters.AddWithValue("@p6", DateTime.Now.ToLongTimeString()); //complete time          
                            cmdAddTut.ExecuteNonQuery();
                            conn.Close();
                            listQuestID.Clear();
                            listQuest.Clear();
                            listLevel.Clear();
                            listTime.Clear();
                            listEasyQuestionID.Clear();
                            listMedQuestionID.Clear();
                            listHardQuestionID.Clear();
                            listCompletedQuestion.Clear();
                            listEasyDoneQID.Clear();
                            listMedDoneQID.Clear();
                            listHardDoneQID.Clear();
                            listTemp.Clear();
                            listTempEasyQID.Clear();
                            listTempMedQID.Clear();
                            listTempHardQID.Clear();
                            totalCount = 0;
                            currCount = 0;
                            randomQIDCount = 0;
                            compEasy = 0;
                            compMed = 0;
                            compHard = 0;
                            easyDone = 0;
                            medDone = 0;
                            hardDone = 0;
                            easyLeft = 0;
                            medLeft = 0;
                            hardLeft = 0;
                            totalQuestLeft = 0;
                            listRandomQuestionID.Clear();
                            listRandomQuestion.Clear();
                            listRandomTime.Clear();
                            listRandomLevel.Clear();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You has completed this tutorial'); window.location.href='StudHome.aspx';", true);

                        }
                        mm = 0;
                        ss = 0;
                        listQuestID.Clear();
                        listQuest.Clear();
                        listLevel.Clear();
                        listTime.Clear();
                        listEasyQuestionID.Clear();
                        listMedQuestionID.Clear();
                        listHardQuestionID.Clear();
                        listCompletedQuestion.Clear();
                        listEasyDoneQID.Clear();
                        listMedDoneQID.Clear();
                        listHardDoneQID.Clear();
                        listTemp.Clear();
                        listTempEasyQID.Clear();
                        listTempMedQID.Clear();
                        listTempHardQID.Clear();

                        totalCount = 0;
                        currCount = 0;
                        randomQIDCount=0;
                        compEasy = 0;
                        compMed = 0;
                        compHard = 0;
                        easyDone = 0;
                        medDone = 0;
                        hardDone = 0;
                        easyLeft = 0;
                        medLeft = 0;
                        hardLeft = 0;
                        totalQuestLeft = 0;
                        listRandomQuestionID.Clear();
                        listRandomQuestion.Clear();
                        listRandomTime.Clear();
                        listRandomLevel.Clear();
                        Response.Redirect("StudHome.aspx");
                    }
                    else
                    {
                        lblAnsEnter.Text = "Incorrect. Please try again";
                        lblAnsEnter.Visible = true;
                        Timer1.Enabled = true;
                    }
                }
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
            
        }

        Random rnd = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["studID"] != null)
            {
                lblUserName.Text = Session["studName"].ToString();
                if (!IsPostBack)
                {

                    if (Session["checkAns"].ToString() == "fromHome")
                    {
                        mm = 0;
                        ss = 0;
                        listQuestID.Clear();
                        listQuest.Clear();
                        listLevel.Clear();
                        listTime.Clear();
                        listEasyQuestionID.Clear();
                        listMedQuestionID.Clear();
                        listHardQuestionID.Clear();
                        listCompletedQuestion.Clear();
                        listEasyDoneQID.Clear();
                        listMedDoneQID.Clear();
                        listHardDoneQID.Clear();
                        listTemp.Clear();
                        listTempEasyQID.Clear();
                        listTempMedQID.Clear();
                        listTempHardQID.Clear();

                        totalCount = 0;
                        currCount = 0;
                        randomQIDCount = 0;
                        compEasy = 0;
                        compMed = 0;
                        compHard = 0;
                        easyDone = 0;
                        medDone = 0;
                        hardDone = 0;
                        easyLeft = 0;
                        medLeft = 0;
                        hardLeft = 0;
                        totalQuestLeft = 0;
                        listRandomQuestionID.Clear();
                        listRandomQuestion.Clear();
                        listRandomTime.Clear();
                        listRandomLevel.Clear();

                        Session["checkAns"] = "refreshPg";
                        mm = 0;
                        ss = 0;
                        listQuestID.Clear();
                        listQuest.Clear();
                        listLevel.Clear();
                        listTime.Clear();
                        listEasyQuestionID.Clear();
                        listMedQuestionID.Clear();
                        listHardQuestionID.Clear();
                        listCompletedQuestion.Clear();
                        listEasyDoneQID.Clear();
                        listMedDoneQID.Clear();
                        listHardDoneQID.Clear();
                        listTemp.Clear();
                        listTempEasyQID.Clear();
                        listTempMedQID.Clear();
                        listTempHardQID.Clear();

                        totalCount = 0;
                        currCount = 0;
                        randomQIDCount = 0;
                        compEasy = 0;
                        compMed = 0;
                        compHard = 0;
                        easyDone = 0;
                        medDone = 0;
                        hardDone = 0;
                        easyLeft = 0;
                        medLeft = 0;
                        hardLeft = 0;
                        totalQuestLeft = 0;
                        listRandomQuestionID.Clear();
                        listRandomQuestion.Clear();
                        listRandomTime.Clear();
                        listRandomLevel.Clear();
                        studID = Session["studID"].ToString();
                    mm = 0;
                    ss = 0;
                    Timer1.Enabled = false;

                    tutNum = Request.QueryString["tutNum"].ToString();
                    courseID = Request.QueryString["courseID"].ToString();
                    courseName = Request.QueryString["coursename"].ToString();
                    chapName = Request.QueryString["chapname"].ToString();
                    questdone = int.Parse(Request.QueryString["questDone"].ToString());

                    lblCourse.Text = courseID + " " + courseName;
                    lblTutorial.Text = tutNum;
                    lblTitle.Text = chapName;

                    //get tut ID
                    conn.Open();
                    string sql = "SELECT TutorialID FROM Tutorial WHERE TutorialNumber = @tutNum AND CourseID = @courseID";
                    SqlCommand cmdGetTutID = new SqlCommand(sql, conn);
                    cmdGetTutID.Parameters.AddWithValue("@tutNum", tutNum);
                    cmdGetTutID.Parameters.AddWithValue("@courseID", courseID);
                    SqlDataReader dtr = cmdGetTutID.ExecuteReader();
                    while (dtr.Read())
                    {
                        tutorialID = dtr.GetString(0);
                    }
                    conn.Close();


                    conn.Open();

                    //get quest
                    sql = "SELECT q.QuestionID, q.Question, q.Level, q.TimeLimit  FROM Question q, Tutorial t WHERE t.TutorialNumber = @tutNum AND t.CourseID = @courseID AND t.TutorialID = q.TutorialID AND q.Status = 1";
                    SqlCommand cmdGetQuest = new SqlCommand(sql, conn);
                    cmdGetQuest.Parameters.AddWithValue("@tutNum", tutNum);
                    cmdGetQuest.Parameters.AddWithValue("@courseID", courseID);
                    dtr = cmdGetQuest.ExecuteReader();
                    int i = 0;
                    while (dtr.Read())
                    {
                        listQuestID.Add(dtr.GetString(0));
                        listQuest.Add(dtr.GetString(1));
                        listLevel.Add(dtr.GetString(2));
                        listTime.Add(dtr.GetInt32(3));
                    }
                    string hh = "";
                    for (int k = 0; k < listQuestID.Count; k++)
                    {
                        hh += listQuestID[k];
                    }
                    conn.Close();
                    conn.Open();
                    //get comp level
                    sql = "SELECT [CompulsaryEasy], [CompulsaryMedium], [CompulsaryHard] FROM [Tutorial] WHERE TutorialNumber = @tutNum AND CourseID = @courseID AND Status =1";
                    SqlCommand cmdGetComp = new SqlCommand(sql, conn);
                    cmdGetComp.Parameters.AddWithValue("@tutNum", tutNum);
                    cmdGetComp.Parameters.AddWithValue("@courseID", courseID);
                    dtr = cmdGetComp.ExecuteReader();

                    while (dtr.Read())
                    {
                        compEasy = dtr.GetInt32(0);
                        compMed = dtr.GetInt32(1);
                        compHard = dtr.GetInt32(2);
                    }

                    //get each level de qID
                    i = 0;
                    for (int j = 0; j < listQuest.Count; j++)
                    {
                        if (listLevel[j].ToString().ToLower().Contains("easy"))
                        {
                            listEasyQuestionID.Add(listQuestID.ElementAt(j));
                        }
                        else if (listLevel[j].ToString().ToLower().Contains("medium"))
                        {
                            listMedQuestionID.Add(listQuestID.ElementAt(j));
                        }
                        else
                        {
                            listHardQuestionID.Add(listQuestID.ElementAt(j));
                        }

                    }
                    conn.Close();

                    //  Label1.Text = "easy = " + listEasyQuestionID.Count + ", med = " + listMedQuestionID.Count + ",hard = " + listHardQuestionID.Count;

                    //get done how many quest done 

                    for (int j = 0; j < listQuestID.Count; j++)
                    {
                        sql = "SELECT * FROM [StudAns] WHERE QuestionID = @QuestionID AND StudentID = @StudentID";
                        SqlCommand cmdQuesDoneCount = new SqlCommand(sql, conn);
                        conn.Open();
                        cmdQuesDoneCount.Parameters.AddWithValue("@QuestionID", listQuestID[j].ToString());
                        cmdQuesDoneCount.Parameters.AddWithValue("@StudentID", studID);
                        dtr = cmdQuesDoneCount.ExecuteReader();

                        if (dtr.Read())
                        {
                            listCompletedQuestion.Add(true); //store done questID
                            if (listLevel[j].ToString().ToLower().Contains("easy"))
                            {
                                easyDone += 1;
                                listEasyDoneQID.Add(listQuestID.ElementAt(j));

                            }
                            else if (listLevel[j].ToString().ToLower().Contains("medium"))
                            {
                                medDone += 1;
                                listMedDoneQID.Add(listQuestID.ElementAt(j));
                            }
                            else
                            {
                                hardDone += 1;
                                listHardDoneQID.Add(listQuestID.ElementAt(j));
                            }

                        }
                        else
                        {
                            listCompletedQuestion.Add(false);
                        }
                        conn.Close();
                    }

                    //totalquestleft
                    easyLeft = compEasy - easyDone;
                    medLeft = compMed - medDone;
                    hardLeft = compHard - hardDone;
                    totalQuestLeft = easyLeft + medLeft + hardLeft;


                    if (easyLeft != 0)
                    {
                        setQuestion(listEasyQuestionID, listTempEasyQID, listEasyDoneQID, easyLeft);
                    }
                    if (medLeft != 0)
                    {
                        setQuestion(listMedQuestionID, listTempMedQID, listMedDoneQID, medLeft);
                    }
                    if (hardLeft != 0)
                    {
                        setQuestion(listHardQuestionID, listTempHardQID, listHardDoneQID, hardLeft);
                    }

                    //random sequence
                    listRandomQuestionID = RandomizeStrings(listRandomQuestionID.ToArray());
                    string txt = "";

                    for (i = 0; i < listRandomQuestionID.Count; i++)
                    {
                        for (int j = 0; j < listQuestID.Count; j++)
                        {
                            if (listRandomQuestionID[i] == listQuestID[j])
                            {
                                listRandomQuestion.Add(listQuest[j]);
                                listRandomTime.Add(listTime[j]);
                                listRandomLevel.Add(listLevel[j]);
                            }
                        }

                    }
                    // listQuest = listQuest.OrderBy(d => listRandomQuestionID.IndexOf().ToList();
                    txt = "";
                    for (i = 0; i < listRandomQuestionID.Count; i++)
                    {
                        txt += listRandomQuestionID[i] + "." + listRandomQuestion[i] + ",";
                    }

                    Label1.Text = "final random:" + txt;

                    lblQNum.Text = (questdone + 1).ToString();
                    lblQuest.Text = listRandomQuestion[currCount].ToString();
                    mm = listRandomTime[currCount];
                    //lblTimeCount.Text = mm.ToString();
                    Timer1.Enabled = true;
                }
                    else
                    {
                        Timer1.Enabled = false;
                        lblCourse.Text = courseID + " " + courseName;
                        lblTutorial.Text = tutNum;
                        lblTitle.Text = chapName;
                        string txt = "";
                        for (int i = 0; i < listRandomQuestionID.Count; i++)
                        {
                            txt += listRandomQuestionID[i]+ ",";
                        }

                        Label1.Text = "  || final random:" + txt;

                        lblQNum.Text = (questdone + 1).ToString();
                        lblQuest.Text = listRandomQuestion[currCount].ToString();
                        mm = listRandomTime[currCount];
                        //lblTimeCount.Text = mm.ToString();
                        Timer1.Enabled = true;
                    }
            }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }



        protected void setQuestion(List<string> listLevelQuestionID, List<string> listTempQID, List<string> listDoneQID, int compLeft)
        {
            int i;
            //callSetEasy
            //setEasyFunc
            //listTempEasyQID = listEasyQuestionID;
            foreach (string item in listLevelQuestionID)
            {
                listTempQID.Add(item);
            }

            var nonintersect = listTempQID.Except(listDoneQID).Union(listDoneQID.Except(listTempQID)).ToList();
            listTempQID.Clear();
            listTempQID.AddRange(nonintersect);

            string txt = "";
            for (i = 0; i < listTempQID.Count; i++)
            {
                txt += listTempQID[i].ToString();
            }



            if (listTempQID.Count == listLevelQuestionID.Count) // no do dao easy
            {
                if (compLeft == listLevelQuestionID.Count) // no extra quest
                {
                    for (i = 0; i < listTempQID.Count; i++)
                    {
                        listRandomQuestionID.Add(listTempQID[i]);
                        //randomQIDCount++;
                    }
                    //random in sequence

                }
                else //got extra quest
                {
                    temp = randomCompulsoryQuest(compLeft, listTempQID);
                    for (i = 0; i < temp.Length; i++)
                    {
                        listRandomQuestionID.Add(temp[i]);
                        //randomQIDCount++;
                    }
                    //select compEasy from easyQID then store in random
                }
            }
            else if (listTempQID.Count != listLevelQuestionID.Count) //means done some question
            {
                if (compLeft == listTempQID.Count) //left de quest = comp left
                {
                    for (i = 0; i < listTempQID.Count; i++)
                    {
                        listRandomQuestionID.Add(listTempQID[i]);
                        //randomQIDCount++;
                    }
                    //
                }
                else //left quest>comp
                {
                    temp = randomCompulsoryQuest(compLeft, listTempQID);
                    //call random (select 2 from 3)
                    for (i = 0; i < temp.Length; i++)
                    {
                        listRandomQuestionID.Add(temp[i]);
                        //randomQIDCount++;
                    }


                }
            }
        }

        //random func 1
        protected string[] randomCompulsoryQuest(int compNum, List<string> questionRandom)
        {
            //string[] tempRandom = new string[compNum];
            List<string> listNumbers = new List<string>();
            int number;
            for (int i = 0; i < compNum; i++)
            {
                do
                {
                    number = rnd.Next(0, 0 + questionRandom.Count);
                } while (listNumbers.Contains(questionRandom[number]));
                listNumbers.Add(questionRandom[number]);
            }
            // tempRandom = listNumbers.ToArray();
            return listNumbers.ToArray();

        }


        static Random _random = new Random();

        static List<string> RandomizeStrings(string[] arr)
        {
            listRandomQuestionID.Clear();
            listRandomQuestion.Clear();
            listRandomTime.Clear();
            listRandomLevel.Clear();

            List<KeyValuePair<int, string>> list =
                new List<KeyValuePair<int, string>>();
            // Add all strings from array.
            // ... Add new random int each time.
            foreach (string s in arr)
            {
                list.Add(new KeyValuePair<int, string>(_random.Next(), s));
            }
            // Sort the list by the random number.
            var sorted = from item in list
                         orderby item.Key
                         select item;
            // Allocate new string array.
            string[] result = new string[arr.Length];
            // Copy values to array.
            int index = 0;
            foreach (KeyValuePair<int, string> pair in sorted)
            {
                result[index] = pair.Value;
                index++;
            }
            // Return copied array.
            return result.ToList(); ;
        }


        protected void ResultLinkButton_Click(object sender, EventArgs e)
        {

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int mmshow, ssshow;

            ss--;
            secUsed++;

            if (ss < 0)
            {
                mm--;
                ss = 59;
            }
            mmshow = mm;
            ssshow = ss;

            if (mm < 0)
            {
                mmshow = 0;
                ssshow = 00;
            }
            
            lblTimeCount.Text = mmshow.ToString() + ":" + ssshow.ToString("D2");
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudHome.aspx");
        }

        protected void ResultLinkButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect("StudResult.aspx");
        }

        protected void ProfilesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudProfile.aspx");
        }

        protected void LogOutLinkButton_Click(object sender, EventArgs e)
        {
            Session.Clear();//clear session
            Session.Abandon();//Abandon session

            Response.Redirect("Login.aspx");
        }

    }
}