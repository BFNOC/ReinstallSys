using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Tools
{
    internal class ProcessTools
    {
        /// <summary>
        /// 强行结束进程
        /// </summary>
        /// <param name="ProcessName"></param>
        public static void Taskkill(string ProcessName)
        {
            try
            {
                using (Process P = new Process())
                {
                    P.StartInfo = new ProcessStartInfo()
                    {
                        FileName = "taskkill",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = "/F /IM \"" + ProcessName + "\""
                    };
                    P.Start();
                    P.WaitForExit(60000);
                }
            }
            catch
            {
                using (Process P = new Process())
                {
                    P.StartInfo = new ProcessStartInfo()
                    {
                        FileName = "tskill",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = "\"" + ProcessName + "\" /A /V"
                    };
                    P.Start();
                    P.WaitForExit(60000);
                }
            }
        }
    }
}
