using System;
using System.IO;

namespace ReinstallSys.Tools
{
    internal class DirTools
    {
        public static bool CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
            return true;
        }
    }
}
