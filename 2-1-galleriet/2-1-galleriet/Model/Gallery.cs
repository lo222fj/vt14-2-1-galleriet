using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Drawing;
//using System.Drawing;

namespace _2_1_galleriet
{
    public class Gallery
    {
        private static readonly string PhysicalUploadedImagesPath;
        private static readonly Regex AprovedExtention;
        private static readonly Regex SantizePath;

        static Gallery()
        {
            PhysicalUploadedImagesPath = Path.Combine(
                //Fysisk sökväg till applikationsrot
                AppDomain.CurrentDomain.GetData("APPBASE").ToString(),
                @"Content\Images"
                );
            AprovedExtention = new Regex(@"^.*\.(gif|jpg|png)$");

            var invalidChars = new string(Path.GetInvalidFileNameChars());

            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            //För att sedan ersätta otillåtna tecken använd metoden Regex.Replace().
        }
        public static bool IsValidImage(Image image)
        {
            if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                return true;
            else
                return false;
        }

        public static bool ImageExists(string name)
        {
            var path = PhysicalUploadedImagesPath + "\\" + name;
            if (File.Exists(path))
            {
                return true;
            }
            else
                return false;
        }
        public string SaveImage(Stream stream, string fileName)
        {
          
            bool exists = ImageExists(fileName);

            var image = System.Drawing.Image.FromStream(stream);
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            string bigImageUrl = "Content/Images/" + fileName;
            string imagePath = PhysicalUploadedImagesPath + "\\" + fileName;
            string thumbnailPath = PhysicalUploadedImagesPath + "\\ThumbNails\\" + fileName;

            if (IsValidImage(image) == false)
            {
                throw new ArgumentException();
            }
            
            if (exists)
            {
                int i;
                                
                for (i = 2; exists == true; i += 1)
                {
                    imagePath = PhysicalUploadedImagesPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "(" + i + ")" + Path.GetExtension(fileName);
                    if (File.Exists(imagePath))
                    {
                        exists = true;
                    }
                    else
                    {
                        exists = false;
                        imagePath = PhysicalUploadedImagesPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "(" + i + ")" + Path.GetExtension(fileName);
                        thumbnailPath = PhysicalUploadedImagesPath + "\\ThumbNails\\" + Path.GetFileNameWithoutExtension(fileName) + "(" + i + ")" + Path.GetExtension(fileName);
                        bigImageUrl = "Content/Images/" + Path.GetFileNameWithoutExtension(fileName) + "(" + i + ")" + Path.GetExtension(fileName);
                    }
                }

          
               /* var containsDouble = PhysicalUploadedImagesPath.Contains("(");
                var position = PhysicalUploadedImagesPath.IndexOf("(");
                var nextPosition = PhysicalUploadedImagesPath.ToArray();
                var currentNr = nextPosition[position+1];*/

          
                //imagePath =PhysicalUploadedImagesPath + "\\"+Path.GetFileNameWithoutExtension(fileName) + "(2)" + Path.GetExtension(fileName);
               // thumbnailPath = PhysicalUploadedImagesPath + "\\ThumbNails\\" + Path.GetFileNameWithoutExtension(fileName) + "(2)" + Path.GetExtension(fileName);
                //bigImageUrl = "Content/Images/" + Path.GetFileNameWithoutExtension(fileName) + "(2)" + Path.GetExtension(fileName);
                
            }
            image.Save(imagePath);
            thumbnail.Save(thumbnailPath);
           
            return bigImageUrl;
        }

        public static IEnumerable<FileInfo> GetImageNames()
        {
            //hämtar info om katalogen som bilderna ligger i. 
            //DirectoryInfo kräver fysisk sökväg
            var directory = new DirectoryInfo(PhysicalUploadedImagesPath);
            //ber kataloginfon om info om filerna i den
            var files = directory.GetFiles();

            return files;
        }

    }
}