﻿using RICH_Connector.API;
using RICH_Connector.Helper;
using RICH_Connector.Clover;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Connector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;
        private const string appName = "RICH Connector";
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private RICH_Connector.Helper.AutoUpdate updater;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                Application.Current.Shutdown();
            }

            this.InitNotifyIcon();
            this.InitServer();
            base.OnStartup(e);

            updater = new AutoUpdate(true);
            updater.HandleOutNewVersion += Updater_HandleOutNewVersion;
            updater.CheckUpdate();

        }

        private void Updater_HandleOutNewVersion(bool isUpdate)
        {
            var itemCheckUpdate = this.notifyIcon.ContextMenuStrip.Items[1];
            if (isUpdate)
            {
                var icon = new System.Drawing.Icon("Resources/update.ico");
                itemCheckUpdate.Image = icon.ToBitmap();
            }
            else
            {
                itemCheckUpdate.Image = null;
            }
        }


        private void InitServer()
        {
            var server = new Server();
            server.Restart();
        }

        private void InitNotifyIcon()
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            this.notifyIcon.Icon = new System.Drawing.Icon("Resources/rich.ico");
            this.notifyIcon.Visible = true;
            this.notifyIcon.Text = appName;

            this.notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyIcon.ContextMenuStrip.Items.Add("Refresh", null, this.HandleRestartApplication);
            this.notifyIcon.ContextMenuStrip.Items.Add("Check for updates", null, this.HandleCheckForUpdates);
            this.notifyIcon.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            this.notifyIcon.ContextMenuStrip.Items.Add("Exit", null, this.HandleExit);
        }

        private void HandleExit(object? sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            PrintHtmlHelper.Kill();
        }

        private void HandleRestartApplication(object? sender, EventArgs e)
        {
            PrintHtmlHelper.Kill();

            var pathApplication = GetApplicationFolderPath() + "\\" + GetApplicationName() + ".exe"; // + "\\" + GetApplicationName();
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = @$"/C choice /C Y /N /D Y /T 1 & START "" "" ""{pathApplication}""";
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            Process.GetCurrentProcess().Kill();
        }

        private string GetApplicationName()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly == null) // This may happen if the entry assembly is not available (e.g., in certain app domains or contexts)
            {
                // Fallback to the calling assembly in such cases
                assembly = Assembly.GetCallingAssembly();
            }

            return assembly.GetName().Name;
        }

        private string GetApplicationFolderPath()
        {
            string assemblyLocation = Assembly.GetEntryAssembly().Location;
            return Path.GetDirectoryName(assemblyLocation);
        }

        private void HandleCheckForUpdates(object? sender, EventArgs e)
        {
            // todo: check update 
            updater.CheckUpdate();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.notifyIcon.Dispose();
            CloverClient.Instance.Disconnect();
            var prc = new ProcManager();
            prc.KillByPort(5000); //prc.KillbyPort(port);
            
            base.OnExit(e);
        }

    }

    public class PRC
    {
        public int PID { get; set; }
        public int Port { get; set; }
        public string Protocol { get; set; }
    }
    public class ProcManager
    {
        public void KillByPort(int port)
        {
            var processes = GetAllProcesses();
            if (processes.Any(p => p.Port == port))
                try
                {
                    Process.GetProcessById(processes.First(p => p.Port == port).PID).Kill();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            else
            {
                Console.WriteLine("No process to kill!");
            }
        }

        public List<PRC> GetAllProcesses()
        {
            var pStartInfo = new ProcessStartInfo();
            pStartInfo.FileName = "netstat.exe";
            pStartInfo.Arguments = "-a -n -o";
            pStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            pStartInfo.UseShellExecute = false;
            pStartInfo.RedirectStandardInput = true;
            pStartInfo.RedirectStandardOutput = true;
            pStartInfo.RedirectStandardError = true;

            var process = new Process()
            {
                StartInfo = pStartInfo
            };
            process.Start();

            var soStream = process.StandardOutput;

            var output = soStream.ReadToEnd();
            if (process.ExitCode != 0)
                throw new Exception("somethign broke");

            var result = new List<PRC>();

            var lines = Regex.Split(output, "\r\n");
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("Proto"))
                    continue;

                var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var len = parts.Length;
                if (len > 2)
                    result.Add(new PRC
                    {
                        Protocol = parts[0],
                        Port = int.Parse(parts[1].Split(':').Last()),
                        PID = int.Parse(parts[len - 1])
                    });


            }
            return result;
        }
    }
}
