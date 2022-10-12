using ConvertToPng;

namespace convert_to_png
{
    class Helper_FlieLogic
    {
        public static TaskProgress? taskProgress;

        public static void createTaskProgress(MainWindow win)
        {
            taskProgress = new TaskProgress(win);
        }
        public static void start3dMaxProcess(string fileName)
        {
            taskProgress!.CreateTaskProgress(Helper_3dMaxFile.loadAll3dMaxFile(fileName));

            int curCount = 0;
            Helper_3dMaxFile.all3dMaxFileConvertToPng(ref curCount);
        }

        public static void startBlendProcess(string fileName)
        {
            taskProgress!.CreateTaskProgress(Helper_blenderFile.loadBlendFile(fileName));

            int curCount = 0;
            Helper_blenderFile.allBlenderFileConvertToPng(ref curCount);
        }

        public static void startAllProcess(string fileName)
        {
            int count = 0;
            count += Helper_3dMaxFile.loadAll3dMaxFile(fileName);
            count += Helper_blenderFile.loadBlendFile(fileName);
            taskProgress!.CreateTaskProgress(count);

            int curCount = 0;
            Helper_3dMaxFile.all3dMaxFileConvertToPng(ref curCount);
            Helper_blenderFile.allBlenderFileConvertToPng(ref curCount);
        }
    }
}
