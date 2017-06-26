using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UpDownProcess
{
    public class UpDownProcess : System.Web.UI.Page
    {
        //static string ConnectString = WebConfigurationManager.ConnectionStrings["OperaContext"].ToString();


        public bool Upload(string ConnectString, dynamic file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string contentType = file.ContentType;

                    using (Stream fs = file.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
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
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }



        public Hashtable Download(string ConnectString, int id)        
        {            
            Hashtable result = new Hashtable();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from FileTB where ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["file"];
                        contentType = sdr["CONTENT_TYPE"].ToString();
                        fileName = sdr["FileName"].ToString();

                        result.Add("bytes", (byte[])sdr["file"]);
                        result.Add("contentType", sdr["CONTENT_TYPE"].ToString());
                        result.Add("fileName", sdr["FileName"].ToString());
                    }
                }
            }
            
            return result;
        }




    }
}