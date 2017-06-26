using System;
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
                string filename = Path.GetFileName(File.PostedFile.FileName);
                string contentType = File.PostedFile.ContentType;

                using (Stream fs = File.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        //string constr = ConfigurationManager.ConnectionStrings["CN"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(ConnectString))
                        {
                            string query = "insert into FileTB values (@CONTENT_TYPE,@FileName,@File)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@CONTENT_TYPE", contentType);
                                cmd.Parameters.AddWithValue("@FileName", filename);
                                cmd.Parameters.AddWithValue("@File", bytes);
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
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
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from FileTB where ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", txtFileId.Text);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["file"];
                        contentType = sdr["CONTENT_TYPE"].ToString();
                        fileName = sdr["FileName"].ToString();
                    }
                }
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }






}