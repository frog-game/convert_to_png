using convert_to_png;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ConvertToPng
{
    class Helper_blenderFile
    {
        private static CIVFileList m_fileBlendList = new CIVFileList();
      
        public static int loadBlendFile(string directoryPath)
        {
            m_fileBlendList.buildFileList(directoryPath, (FileInfo fileInfo) =>
            {
                string fileExtension = Path.GetExtension(fileInfo.Name).ToLower();
                if (fileExtension == Global.FileBlenderExt)
                    return true;
                return false;
            });

            return m_fileBlendList.m_fileItems.Count;
        }

        public static void allBlenderFileConvertToPng(ref int curCount)
        {
            foreach (CIVFileList.FileItem fileItem in m_fileBlendList.m_fileItems)
            {
              BlenderFilePreview preview = BlenderFilePreview.CreatePreviewFromFile(fileItem.fullPathName!);

                PngBitmapEncoder png = new PngBitmapEncoder();
                WriteableBitmap bitmap = new WriteableBitmap(preview.Width, preview.Height, 96.0, 96.0, PixelFormats.Pbgra32, null);
                bitmap.WritePixels(new Int32Rect(0, 0, preview.Width, preview.Height), preview.Pixels, preview.Stride, 0);
                bitmap.Freeze();
                png.Frames.Add(BitmapFrame.Create(bitmap));
                using (Stream stm = File.Create(fileItem.fullPathNameNoExtension + Global.PngExt))
                {
                    png.Save(stm);
                    Helper_FlieLogic.taskProgress!.UpdateTaskProgress(curCount++);
                }
            }
        }
    }
}
