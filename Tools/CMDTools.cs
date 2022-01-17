using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ReinstallSys.Tools
{
    internal class CMDTools
    {
		public static string SC(string strCMD)
		{
			Process process = new();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = "/c " + strCMD;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardInput = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.CreateNoWindow = true;
			process.Start();
			string text = process.StandardOutput.ReadToEnd();
			process.Close();
			return text;
		}
		public static string SC(string strCMD, string WorkDir)
		{
			Process process = new();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = "/c " + strCMD;
			startInfo.Verb = "runas";
			startInfo.WorkingDirectory = WorkDir;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardInput = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.CreateNoWindow = true;
			process.Start();
			string text = process.StandardOutput.ReadToEnd();
			process.Close();
			return text;
		}

		public static Task<int> SCAsync(string strCMD)
		{
			var tcs = new TaskCompletionSource<int>();
			Process process = new();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = "/c " + strCMD;
			startInfo.Verb = "runas";
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardInput = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.CreateNoWindow = true;
			process.EnableRaisingEvents = true;
			process.Exited += (sender, args) =>
			{
				tcs.SetResult(process.ExitCode);
				process.Dispose();
			};
			process.Start();
			return tcs.Task;
		}
		public static Task<int> SCAsyncInWorkDir(string strCMD, string WorkDir, EventHandler ExitedEvent)
		{
			var tcs = new TaskCompletionSource<int>();
			Process process = new();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = "/c " + strCMD;
			startInfo.Verb = "runas";
			startInfo.UseShellExecute = false;
			startInfo.WorkingDirectory = WorkDir;
			startInfo.RedirectStandardInput = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.CreateNoWindow = true;
			process.EnableRaisingEvents = true;
			process.Exited += (sender, args) =>
			{
				tcs.SetResult(process.ExitCode);
				process.Dispose();
			};
			process.Exited += ExitedEvent;
			process.Start();
			return tcs.Task;
		}
		public static Task<int> MsiexecAsyncInWorkDir(string strCMD, string WorkDir, EventHandler ExitedEvent)
		{
			var tcs = new TaskCompletionSource<int>();
			Process process = new();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.FileName = "msiexec";
			startInfo.Arguments = strCMD;
			startInfo.Verb = "runas";
			startInfo.UseShellExecute = false;
			startInfo.WorkingDirectory = WorkDir;
			startInfo.RedirectStandardInput = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.CreateNoWindow = false;
			process.EnableRaisingEvents = true;
			process.Exited += (sender, args) =>
			{
				tcs.SetResult(process.ExitCode);
				process.Dispose();
			};
			process.Exited += ExitedEvent;
			process.Start();
			return tcs.Task;
		}
	}
}
