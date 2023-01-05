using RICH_Connector.API;
using RICH_Connector.Clover;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
            this.notifyIcon.ContextMenuStrip.Items.Add("Exit", null, this.HandleExit);
        }

        private void HandleExit(object? sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.notifyIcon.Dispose();
            CloverClient.Instance.Disconnect();
            base.OnExit(e);
        }

    }
}
