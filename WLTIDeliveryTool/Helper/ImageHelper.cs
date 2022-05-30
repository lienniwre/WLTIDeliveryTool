using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace WLTIDeliveryTool.Helper
{
    public class ImageHelper
    {
        private readonly HttpPostedFileBase PostedFile;

        public ImageHelper()
        {

        }
        public ImageHelper(HttpPostedFileBase postedFile)
        {
            PostedFile = postedFile;
        }

        public byte[] ToByte(int maxWidth, int maxHeight)
        {

            Stream stream = PostedFile.InputStream;

            Image image = Image.FromStream(stream);

            Bitmap resizedImage = ResizeImage(image, image.Width, image.Height, maxWidth, maxHeight);
            var memoryStream = new MemoryStream();
            resizedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Position = 0;
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            var g = binaryReader.ReadBytes((int)memoryStream.Length);

            stream.Dispose();
            image.Dispose();

            return g;

        }

        public bool Isvalid
        {
            get
            {
                if (PostedFile != null)
                {
                    string fileName = Path.GetFileName(PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    int fileSize = PostedFile.ContentLength;

                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".bmp"
                             || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".png")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }



        public static Bitmap ResizeImage(Image image, int width, int height, int maxWidth, int maxHeight)
        {

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);


            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }


            }

            return destImage;
        }

    }
}