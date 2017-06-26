using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadTest
{
    public partial class Test : System.Web.UI.Page
    {
        static string ConnectString = WebConfigurationManager.ConnectionStrings["OperaContext"].ToString();
        SqlConnection connect = new SqlConnection(ConnectString);
        SqlCommand command = new SqlCommand();
        StringBuilder LSTR_SQL = new StringBuilder();
        DataTable tb = new DataTable();
        List<SqlParameter> pars = new List<SqlParameter>();
        SqlDataAdapter adapter = null;
        SqlTransaction transaction = null;



        protected void Page_Load(object sender, EventArgs e)
        {
            SetGV();
        }

        protected void Upload_Click(object sender, EventArgs e)
        {

            //seek
            /*
            connect.Open();
            LSTR_SQL.Append("select * from  operas");
            pars.Add(new SqlParameter("Name", "value"));
            adapter = new SqlDataAdapter(LSTR_SQL.ToString(), connect);
            adapter.Fill(tb);
            */


            try
            {
                /*
                if (File.PostedFile.ContentLength <= 0)
                {
                    lblErrMsg.Text = "請指定欲上傳的檔案路徑 \r\n";
                    return;
                }
                if (!File.FileName.EndsWith(".pdf"))
                {
                    lblErrMsg.Text += "請上傳pdf檔 \r\n";
                    return;
                }
                */

                //add
                //connect.Open();
                //LSTR_SQL.Append("insert into FileTB (FileName)");
                //LSTR_SQL.Append("values('test')");
                //transaction = connect.BeginTransaction();
                //command = new SqlCommand(LSTR_SQL.ToString(), connect, transaction);
                //int result = command.ExecuteNonQuery();
                //transaction.Commit();



                //upload
                UpDownProcess.UpDownProcess Upload = new UpDownProcess.UpDownProcess();
                if (Upload.Upload(ConnectString, File.PostedFile))
                {
                    lblErrMsg.Text = "上傳成功";
                    SetGV();
                }
                else
                {
                    lblErrMsg.Text = "上傳失敗";
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                command.Dispose();
                connect.Close();
            }
        }









        protected void DownloadFile_Click(object sender, EventArgs e)
        {
            //string GUID = (sender as LinkButton).CommandArgument;

            UpDownProcess.UpDownProcess Download = new UpDownProcess.UpDownProcess();
            Hashtable H = Download.Download(ConnectString, Convert.ToInt16(txtFileId.Text));

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = H["contentType"].ToString();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + H["fileName"].ToString());
            Response.BinaryWrite((byte[])H["bytes"]);
            Response.Flush();
            Response.End();
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            //string GUID = (sender as LinkButton).CommandArgument;
            //UpDownProcess.UpDownProcess Download = new UpDownProcess.UpDownProcess();
            //Hashtable H = Download.Download(ConnectString, Convert.ToInt16(GUID));
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = H["contentType"].ToString();
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + H["fileName"].ToString());
            //Response.BinaryWrite((byte[])H["bytes"]);
            //Response.Flush();
            //Response.End();
        }


        public void SetGV()
        {
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                string query = "select * from  FileTB ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        adapter.Fill(tb);
                        gv1.DataSource = tb;
                        gv1.DataBind();
                    }
                }
            }
        }



    }
}