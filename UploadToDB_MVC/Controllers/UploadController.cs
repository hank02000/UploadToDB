using MVC_Upload.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace MVC_Upload.Controllers
{
    public class UploadController : Controller
    {
        static string ConnectString = WebConfigurationManager.ConnectionStrings["OperaContext"].ToString();


        // GET: Upload
        public ActionResult Index()
        {
            using (OperaDbEntities db = new OperaDbEntities())
            {
                var result = db.FileTB.ToList();
                return View(result);
            }
        }

        #region Download        
        //public ActionResult Download(int id)
        //{
        //    byte[] bytes;
        //    string fileName, contentType;
        //    using (SqlConnection con = new SqlConnection(ConnectString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandText = "select * from FileTB where ID=@ID";
        //            cmd.Parameters.AddWithValue("@ID", id);
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                sdr.Read();
        //                bytes = (byte[])sdr["file"];
        //                contentType = sdr["CONTENT_TYPE"].ToString();
        //                fileName = sdr["FileName"].ToString();
        //            }
        //        }
        //    }

        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = contentType;
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Response.End();

        //    return Content("<script>alert('sucess!');</script>");
        //}
        #endregion

        #region Upload        
        //[HttpPost]
        //public ActionResult Upload(HttpPostedFileBase file)
        //{
        //    if (file.ContentLength > 0)
        //    {
        //        //var fileName = Path.GetFileName(file.FileName);
        //        //var path = Path.Combine(Server.MapPath("~/FileUploads"), fileName);
        //        //file.SaveAs(path);

        //        //upload
        //        string filename = Path.GetFileName(file.FileName);
        //        string contentType = file.ContentType;

        //        using (Stream fs = file.InputStream)
        //        {
        //            using (BinaryReader br = new BinaryReader(fs))
        //            {
        //                byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //                //string constr = ConfigurationManager.ConnectionStrings["CN"].ConnectionString;
        //                using (SqlConnection con = new SqlConnection(ConnectString))
        //                {
        //                    string query = "insert into FileTB values (@CONTENT_TYPE,@FileName,@File)";
        //                    using (SqlCommand cmd = new SqlCommand(query))
        //                    {
        //                        cmd.Connection = con;
        //                        cmd.Parameters.AddWithValue("@CONTENT_TYPE", contentType);
        //                        cmd.Parameters.AddWithValue("@FileName", filename);
        //                        cmd.Parameters.AddWithValue("@File", bytes);
        //                        con.Open();
        //                        cmd.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    return RedirectToAction("Index");
        //}
        #endregion


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            UpDownProcess.UpDownProcess Upload = new UpDownProcess.UpDownProcess();
            if (Upload.Upload(ConnectString, file))
            {
                TempData["Message"] = "上傳成功 !";
            }
            else
            {
                TempData["AlertMessage"] = "Defect !";
            }
            return RedirectToAction("Index");
        }


        public ActionResult Download(int id)
        {
            UpDownProcess.UpDownProcess Download = new UpDownProcess.UpDownProcess();
            Hashtable H = Download.Download(ConnectString, id);

            if (H != null)
            {
                TempData["AlertMessage"] = "開始下載";
            }
            else
            {
                TempData["AlertMessage"] = "下載失敗";
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = H["contentType"].ToString();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + H["fileName"].ToString());
            Response.BinaryWrite((byte[])H["bytes"]);
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
        }




    }
}