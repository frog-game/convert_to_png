using System;

namespace convert_to_png
{
    class TaskProgress
    {
        private MainWindow? mainWin;
        private int maxCount;

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);
        private UpdateProgressBarDelegate? updatePbDelegate;
        public TaskProgress(MainWindow win)
        {
            mainWin = win;
        }

        public void CreateTaskProgress(int maxcount)
        {
            maxCount = maxcount;
            mainWin!.taskProgress.Maximum = 100;
            mainWin!.taskProgress.Value = 0;

            updatePbDelegate = new UpdateProgressBarDelegate(mainWin.taskProgress.SetValue);
        }

        public void UpdateTaskProgress(int value)
        {
            int count = (value +1) / maxCount * 100;
            mainWin!.Dispatcher.Invoke(updatePbDelegate, System.Windows.Threading.DispatcherPriority.Background, new object[] { System.Windows.Controls.ProgressBar.ValueProperty, Convert.ToDouble(count) });
        }

    }
}
