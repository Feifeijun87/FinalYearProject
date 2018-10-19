using Iveonik.Stemmers;
using NHunspell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AboditNLP;
using LuceneDirectory = Lucene.Net.Store.Directory;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.IO;

namespace ADM
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fyp"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)

        {
            string s = String.Format("{0,30}", "HELLO")+"HO";
            Label1.Text = s.ToString();
            if (this.IsPostBack)
            {
                
                if (Session["FileUpload1"] == null && Request.Files["FileUpload1"].ContentLength > 0)
                {
                    Session["FileUpload1"] = Request.Files["FileUpload1"];
                    Label1.Text = Path.GetFileName(Request.Files["FileUpload1"].FileName);

                }

                else if (Session["FileUpload1"] != null && (Request.Files["FileUpload1"].ContentLength == 0))
                {
                    HttpPostedFile file = (HttpPostedFile)Session["FileUpload1"];
                    Label1.Text = Path.GetFileName(file.FileName);

                }

                else if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    
                    Session["FileUpload1"] = Request.Files["FileUpload1"];
                    Label1.Text = Path.GetFileName(Request.Files["FileUpload1"].FileName);

                }


            }
            // string qewr= "Collection of unorganized of facts or items. Hello from, assd";

            // string[] req = qewr.Split(' ');
            //  string qwer="";
            //  foreach (string w in req)
            // {
            //       qwer+= w + ",";
            // }
            // txtAns.Text =stemWord(new EnglishStemmer(), "organize unorganized",1);
           // string sampleAns = "Collection of unorganized of facts or items".Trim().ToLower();
            //string keyword = "unorganized item".Trim().ToLower();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {   //get sample ans
            string sampleAns="", keyword="";
            conn.Open();
            SqlCommand cmd = new SqlCommand("prc_get_sample_ans", conn);
     //       cmd.Parameters.AddWithValue("@QuestionID", listRandomQuestionID[currCount]);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sampleAns = dtr.GetString(0).Trim().ToLower();
                keyword = dtr.GetString(1).Trim().ToLower();
            }
            conn.Close();
            //chg the sentence format
            string studAns = txtAns.Text.Trim().ToLower();
            studAns = chgWordFormat(studAns);
            sampleAns = chgWordFormat(sampleAns);
            //check exact match
            if (checkExact(sampleAns, studAns) == true)
                Label1.Text = "true";
            else if (checkKeyword(keyword, studAns) == true)
            {
                double editDistance = Double.Parse(levenshtein(studAns, sampleAns).ToString());
                
            }
            
        }

        protected Boolean checkExact(string sampleAns, string studAns)
        {
            if (studAns.Equals(sampleAns))
            {
                return true;
            }
            return false;
        }

        protected Boolean checkKeyword(string keyword, string studAns)
        {
            double matchRate = 0.00; // to determine the match ratio of keyword
            string[] separatedKeywords = keyword.Split(','); 
            int arrayLength = separatedKeywords.Length;
            double keywordLength = 0;
            double score = 0; // to determine how many keyword match
            string stemmedStudAns = stemWord(new EnglishStemmer(), studAns, 1); //stem the student answer sentence
            Label1.Text = stemmedStudAns;
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
                }
            }
            matchRate = score / keywordLength;
            if (matchRate > 0.6)
                return true;
            else
                return false;
        }

        protected string chgWordFormat(string oriString)
        {
            oriString = oriString.Replace(".", " ");
            oriString = oriString.Replace(",", " ");
            oriString = Regex.Replace(oriString, @"\s+", " ");

            return oriString;
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

        protected void Button2_Click(object sender, EventArgs e)
        {

            FileUpload img = (FileUpload)FileUpload1;
            Byte[] imgByte = null;
            if (img.HasFile && img.PostedFile != null)
            {
                //To create a PostedFile
                HttpPostedFile File = FileUpload1.PostedFile;
                //Create byte Array with file len
                imgByte = new Byte[File.ContentLength];
                //force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength);
            }
            conn.Open();
            string sql = "UPDATE Student SET ProfilePic= @ProfilePic WHERE StudentID='17WMR09512'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProfilePic", imgByte);
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();


        }
    }
}