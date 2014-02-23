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
        private Gallery _gallery;
        public Gallery Gallery  //egenskap   
        {
            get
            {
                if (_gallery == null)
                {
                    _gallery = new Gallery();
                }
                return _gallery; 
                
                //finns det något i _gallery? Är det null returneras default(det till höger)
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowImageList();
            var BigImg = Request.QueryString["bild"];
            Trace.Warn(BigImg);
            BigImage.ImageUrl = BigImg;
        }

        private void ShowImageList()
        {
            //Ange datakälla för repeaterkontrollen
            ThumbnailsRepeater.DataSource = Gallery.GetImageNames();
            //Säger till alla formulärets kontrolle att binda till sina datakällor
            DataBind();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (FileUpload.HasFile && FileUpload.PostedFile.ContentType.StartsWith("image/"))
                {
                    try
                    {   //FileName = filnamn utan sökväg med extention
                        string fileName = Gallery.SaveImage(FileUpload.FileContent, FileUpload.FileName);//istället för 107
                        BigImage.ImageUrl = fileName;
                        string newUrl = "Default.aspx?bild=~/"+fileName;
                        Response.Redirect(newUrl);

                        ShowImageList();
                    }
                    catch (Exception)
                    {
                        var validator = new CustomValidator
                        {
                            IsValid = false,
                            ErrorMessage = "Ett fel inträffade då bilden skulle överföras."
                        };
                        Page.Validators.Add(validator);
                    }
                }
            }
        }
    }
}