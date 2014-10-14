using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Jade.Core.Util;

namespace Aby.Easy
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/html";
            if (IsGetFile)
            {
                List<FileModel> list = new List<FileModel>();
                var ds = Directory.GetDirectories(path);
                foreach (string d in ds)
                {
                    list.Add(new FileModel
                    {
                        fileName = d.Substring(d.LastIndexOf("\\")+1),
                        isFolder = true
                    });
                }
                var fs = Directory.GetFiles(path);
                foreach (string d in fs)
                {
                    list.Add(new FileModel
                    {
                        fileName = d.Substring(d.LastIndexOf("\\") + 1),
                        isFolder = false
                    });
                }
                Response.Write(Jade.Core.JSON.JsonConvert.SerializeObject(list));
            }
            else if (IsRemoveFile && Files!=null)
            {
                Files.ForEach(o =>
                {
                    var ps = o.Split(';');
                    if (ps[1] == "true")
                    {
                        Directory.Delete(path + "/" + ps[0], true);
                    }
                    else
                    {
                        File.Delete(path + "/" + ps[0]);
                    }
                });
                Response.Write(Jade.Core.JSON.JsonConvert.SerializeObject(new Jade.Core.Entity.JsonReturn()));
            }
            else if (IsUploadFile) {
                var f = Request.Files[0];
                if (f != null && f.ContentLength > 0)
                {
                    string fn = Path.GetFileName(f.FileName);
                    if (File.Exists(path + "/" + fn))
                    {
                        fn = fn.Substring(0, fn.LastIndexOf('.')) + "(" + DateTime.Now.Ticks + ")" + Path.GetExtension(fn);
                    }
                    f.SaveAs(path + "/" + fn);
                    Response.Write(Jade.Core.JSON.JsonConvert.SerializeObject(new Jade.Core.Entity.JsonReturn { exception = false }));
                }
                else
                {
                    Response.Write(Jade.Core.JSON.JsonConvert.SerializeObject(new Jade.Core.Entity.JsonReturn
                    {
                        exception = true,
                        errors = "文件为空!"
                    }));
                }
            }
        }

        public List<string> Files
        {
            get
            {
                var f = Request["files"];
                if (!string.IsNullOrEmpty(f))
                {
                    return (List<string>)Convert.ChangeType(f, typeof(List<string>));
                }
                return null;
            }
        }

        public string path
        {
            get {
                string p = Request["basePath"];
                if (!string.IsNullOrEmpty(p))
                {
                    return Server.MapPath(p);
                }
                else {
                    return null;
                }
            }
        }


        public bool IsUploadFile
        {
            get
            {
                return Request["action"].ToLower() == "uploadfile";
            }
        }
        public bool IsGetFile
        {
            get
            {
                return Request["action"].ToLower() == "getfile";
            }
        }
        public bool IsRemoveFile
        {
            get
            {
                return Request["action"].ToLower() == "removefile";
            }
        }

    }

    public class FileModel {
        public string fileName;
        public bool isFolder;
    }

}