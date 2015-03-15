using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace NaverDictionary
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 기존에 실행된 프로그램을 찾아서 종료 후 실행
            Process currentProcess = Process.GetCurrentProcess();

            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith(Properties.Settings.Default.title1))
                {
                    if (process.Id != currentProcess.Id)
                    {
                        process.Kill();
                    }
                }
                if (process.ProcessName.StartsWith(Properties.Settings.Default.title2))
                {
                    if (process.Id != currentProcess.Id)
                    {
                        process.Kill();
                    }
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NaverDic());
        }
    }


}
