using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToPng
{
    internal class CIVFileList
    {
		public class FileItem
		{
            public string? fullPathNameNoExtension;//不带后缀的全路径 -->D:\ResFile\src3dFile\灯笼
            public string? fileName;//带后缀的文件名 -->灯笼.max
            public string? fullPathName; //带后缀的全路径 -->D:\ResFile\src3dFile\灯笼.max
        }

		public delegate bool FileFilter(FileInfo fileInfo);
		public string? m_directoryPath { get; set; }
		public List<FileItem> m_fileItems { get; set; } = new List<FileItem>();

		public int buildFileList(string strDirectory, FileFilter filter)
		{
			m_directoryPath = strDirectory;
			m_fileItems = new List<FileItem>();
			buildFileListRecursive(strDirectory, filter);
			return m_fileItems.Count;
		}

		private void buildFileListRecursive(string strDirectory,FileFilter filter)
		{ 
            DirectoryInfo dirInfo = new DirectoryInfo(strDirectory);
			if (!dirInfo.Exists) return;

            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
				buildFileListRecursive(strDirectory, filter);
            }

			foreach (FileInfo file in dirInfo.GetFiles())
			{
				if (!filter(file)) continue;

				FileItem fileItem = new FileItem();
                fileItem.fileName = file.Name;
                fileItem.fullPathName = file.FullName;
                fileItem.fullPathNameNoExtension = Path.Combine(Path.GetDirectoryName(file.FullName)!, Path.GetFileNameWithoutExtension(file.Name));

				m_fileItems.Add(fileItem);
			}
        }
	}
}
