using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;


namespace _2_1_galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowImageList();
        }

        private void ShowImageList()
        {
            //hämtar info om katalogen som bilderna ligger i. 
            //DirectoryInfo kräver fysisk sökväg
            var directory = new DirectoryInfo(Server.MapPath(@"~/Content/Images"));
            //ber kataloginfon om info om filerna i den
            var files = directory.GetFiles();

            //Ange datakälla för repeaterkontrollen
            ThumbnailsRepeater.DataSource = files;
            //Säger till alla formulärets kontrolle att binda till sina datakällor
            DataBind();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Trace.Warn("Klick!");
                string upploadedFile;
                //filnamn hos användaren
                upploadedFile = FileUpload.PostedFile.FileName;

                //fysisk sökväg till katalog samt filnamn från användaren
                string pathImageDirectory = Server.MapPath(@"~/Content/Images");
                string path = pathImageDirectory + "\\" + upploadedFile;
                
                if(File.Exists(path))
                {
                    var previousName = Path.GetFileNameWithoutExtension(upploadedFile);
                    var newName = previousName + "(2)";
                    path = pathImageDirectory + "\\" + newName+Path.GetExtension(path);
                }
                
                //sparar filen på den fysiska sökvägen med samma filnamn som hos användaren
                FileUpload.PostedFile.SaveAs(path);
                ShowImageList();

                makeThumbnail(upploadedFile);

                //SKriver ut info om uppladdad fil. för min skull. ha lite koll
                ShowInfo(path);
                
            }
        }

        private void makeThumbnail(string upploadedFile)
        {
            //deklarerar 
            Stream stream = FileUpload.FileContent;

            var image = System.Drawing.Image.FromStream(stream);
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            var pathThumbnail = Server.MapPath(@"~/Content/Images/ThumbNails") + "\\" + "tnail" + upploadedFile;
            thumbnail.Save(pathThumbnail);
        }

        private void ShowInfo(string path)
        {
            FileName.Text = FileUpload.PostedFile.FileName;
            FileContent.Text = FileUpload.PostedFile.ContentType;
            FileSize.Text = FileUpload.PostedFile.ContentLength.ToString();
            FilePath.Text = path;
        }
    }
}