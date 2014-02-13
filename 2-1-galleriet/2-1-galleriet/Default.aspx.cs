using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2_1_galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            
            string upploadedFile;
            upploadedFile = FileUpload.PostedFile.FileName;
            string path = Server.MapPath(@"~/Content/Images") +"\\"+upploadedFile;
            
            FileName.Text = FileUpload.PostedFile.FileName;
            FileContent.Text = FileUpload.PostedFile.ContentType;
            FileSize.Text = FileUpload.PostedFile.ContentLength.ToString();
            Path.Text = path;

            FileUpload.PostedFile.SaveAs(path);
            //FileUpload.PostedFile.SaveAs();

            //FileUpload.SaveAs(@"Content/Images");

            //System.IO.File.
            //FileUpload.FileContent.CopyTo(
        }
    }
}