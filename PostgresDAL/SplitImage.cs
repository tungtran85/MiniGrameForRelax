using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace PostgresDAL
{
    public static class SplitImage
    {
        public static void SplitImageDiretory(string diretoryName, int rowCount, int colCount)
        {
            foreach (string fileName in Directory.GetFiles(diretoryName))
            {
                SplitImageFile(fileName, rowCount, colCount);
            }
        }

        /// <summary>
        /// Thư mục cắt hình ảnh hàng quy định và cột, hình ảnh cắt được lưu trữ trong thư mục tập tin ảnh gốc cùng tên
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        public static void SplitImageFile(string fileName, int rowCount, int colCount)
        {
            Image image = Image.FromFile(fileName);

            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = System.IO.Path.GetExtension(fileName);
            string fileDirectory = System.IO.Path.GetDirectoryName(fileName);

            int colWidth = image.Width / colCount;
            int rowHeight = image.Height / rowCount;

            int i = 0;
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    Bitmap newBmp = new Bitmap(colWidth, rowHeight, PixelFormat.Format24bppRgb);
                    newBmp.MakeTransparent();
                    Graphics newBmpGraphics = Graphics.FromImage(newBmp);
                    newBmpGraphics.Clear(Color.Transparent);
                    newBmpGraphics.DrawImage(image, new Rectangle(0, 0, colWidth, rowHeight),
                        new Rectangle(col * colWidth, row * rowHeight, colWidth, rowHeight), GraphicsUnit.Pixel);
                    newBmpGraphics.Save();
                    string newDirectory = fileDirectory + "\\" + fileNameWithoutExtension;
                    if (!Directory.Exists(newDirectory))
                    {
                        Directory.CreateDirectory(newDirectory);
                    }
                    string newfileName = newDirectory + "\\" + fileNameWithoutExtension + "_" + i.ToString() + fileExtension;
                    newBmp.Save(newfileName, ImageFormat.Png);

                    i++;
                }
            }
        }

        public static string SplitImageURL4X4(string pathDir, string id, string imageUrl)
        {
            string fileExtension = CheckExistsImageUrl(pathDir, id);
            if (!string.IsNullOrEmpty(fileExtension)) //TODO: Exist file
            {
                return fileExtension;
            }
            //string fileName = String.Empty;
            int rowCount = 4;
            int colCount = 4;
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(imageUrl);
            MemoryStream ms = new MemoryStream(bytes);
            Image image = System.Drawing.Image.FromStream(ms);
            //Image image = Image.FromFile(fileName);

            //string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
            //string fileExtension = System.IO.Path.GetExtension(fileName);
            fileExtension = GetExtensionFromUrl(imageUrl);
            //string fileDirectory = System.IO.Path.GetDirectoryName(fileName);

            int colWidth = image.Width / colCount;
            int rowHeight = image.Height / rowCount;

            int i = 0;
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    Bitmap newBmp = new Bitmap(colWidth, rowHeight, PixelFormat.Format24bppRgb);
                    newBmp.MakeTransparent();
                    Graphics newBmpGraphics = Graphics.FromImage(newBmp);
                    newBmpGraphics.Clear(Color.Transparent);
                    newBmpGraphics.DrawImage(image, new Rectangle(0, 0, colWidth, rowHeight), new Rectangle(col * colWidth, row * rowHeight, colWidth, rowHeight), GraphicsUnit.Pixel);
                    newBmpGraphics.Save();
                    //string newDirectory = fileDirectory + "\\" + fileNameWithoutExtension;
                    string newDirectory = pathDir + "\\" + id;
                    if (!Directory.Exists(newDirectory))
                    {
                        Directory.CreateDirectory(newDirectory);
                    }
                    string newfileName = newDirectory + "\\" + id + "_" + i.ToString() + fileExtension;
                    newBmp.Save(newfileName, ImageFormat.Png);
                    i++;
                }
            }
            return fileExtension;
        }

        public static string StoreImageToLocal(string id, string imageUrl)
        {
            return string.Empty;
        }

        public static string GetExtensionFromUrl(string imageUrl)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                int index = imageUrl.LastIndexOf('.');
                if (imageUrl.Length > index)
                {
                    var strEx = imageUrl.Substring(index);
                    if (strEx.Length == 3 || strEx.Length == 4)
                        return strEx;
                }
            }
            return string.Empty;
        }

        public static string CheckExistsImageUrl(string folderPath, string id)
        {
            string tmpPath = string.Format("{0}//{1}", folderPath, id);
            if (Directory.Exists(tmpPath))
            {
                if (Directory.GetFiles(tmpPath).Length > 0)
                {
                    return System.IO.Path.GetExtension(Directory.GetFiles(tmpPath).FirstOrDefault());
                }
            }
            return string.Empty;
        }
    }
}
