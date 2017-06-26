using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UpDownProcess
{
    public class UpDownProcess
    {
        static string ConnectString = WebConfigurationManager.ConnectionStrings["OperaContext"].ToString();


        public bool Upload(HttpPostedFileBase file)
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








    }
}