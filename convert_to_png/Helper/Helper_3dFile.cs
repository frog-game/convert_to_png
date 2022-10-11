using convert_to_png;
using NPOI.HPSF;
using OpenMcdf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ConvertToPng
{
    class Helper_3dFile
    {
        private static CIVFileList m_file3dMaxList = new CIVFileList();
        public static void loadAll3dMaxFile(string protobufPath)
        {
            m_file3dMaxList.buildFileList(protobufPath, (FileInfo fileInfo) =>
            {
                string fileExtension = Path.GetExtension(fileInfo.Name).ToLower();
                if (fileExtension == Global.File3dMaxExt && !Path.GetFileName(fileInfo.Name).StartsWith("~$"))
                    return true;
                return false;
            });

        }

        public static void all3dMaxFileConvertToPng()
        {
            foreach (CIVFileList.FileItem fileItem in m_file3dMaxList.m_fileItems)
            {
                byte[]? thumbbytes = null;
                try
                {
                    using (CompoundFile file = new CompoundFile(fileItem.fullPathName))
                    {
                        PropertySet ps = new PropertySet(file.RootStorage.GetStream("\x0005SummaryInformation").GetData());
                        SummaryInformation information = new SummaryInformation(ps);
                        thumbbytes = information.Thumbnail;
                    }
                }
                catch (Exception exception)
                {
                    if (((exception is IOException) || (exception is UnauthorizedAccessException)) || (exception is OutOfMemoryException))
                    {
                        throw;
                    }
                    return;
                }
                if (thumbbytes == null)
                {
                    return;
                }
                MaxFilePreview preview = new MaxFilePreview(thumbbytes);

                PngBitmapEncoder png = new PngBitmapEncoder();
                WriteableBitmap bitmap = new WriteableBitmap(preview.Width, preview.Height, 96.0, 96.0, PixelFormats.Pbgra32, null);
                bitmap.WritePixels(new Int32Rect(0, 0, preview.Width, preview.Height), preview.Pixels, preview.Stride, 0);
                bitmap.Freeze();
                png.Frames.Add(BitmapFrame.Create(bitmap));
                using (Stream stm = File.Create(fileItem.fullPathNameNoExtension + Global.PngExt))
                {
                    png.Save(stm);
                }
            }
        }


    }
}
