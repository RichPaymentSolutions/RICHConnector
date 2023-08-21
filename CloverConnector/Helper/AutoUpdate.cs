using AutoUpdaterDotNET;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using static AutoUpdaterDotNET.AutoUpdater;

namespace RICH_Connector.Helper
{

    public class AutoUpdate : Window
    {
        private static System.Timers.Timer updateCheckTimer;
        private bool startApp = true; 

        private bool IsUpdate { get; set; }

        public delegate void NewVersionEventHandler(bool isUpdate);

        public event NewVersionEventHandler HandleOutNewVersion;

        public AutoUpdate(bool _startApp)
        {
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.ApplicationExitEvent += AutoUpdaterOnApplicationExitEvent;
            // for custom file
             AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            this.StartAutoUpdate();

            IsUpdate = false;
            _startApp = true;
        }


        private void StartAutoUpdate()
        {
            DateTime now = DateTime.Now;
            DateTime nextCheckTime = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            if (nextCheckTime <= now)
                nextCheckTime = nextCheckTime.AddDays(1);

            double interval = (nextCheckTime - now).TotalMilliseconds;

            // Initialize the timer
            updateCheckTimer = new System.Timers.Timer(interval);
            updateCheckTimer.Elapsed += UpdateCheckTimer_Elapsed;
            updateCheckTimer.AutoReset = true;
            updateCheckTimer.Start();
        }

        private void UpdateCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.CheckUpdate();
        }

        public void CheckUpdate()
        {
             AutoUpdater.Start("https://api.getrichpos.com/upload/file?name=update.json");
            //AutoUpdater.Start("https://raw.githubusercontent.com/dongquoctien/updater/main/Update.xml");
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.IsUpdateAvailable)
                {
                    HandleOutNewVersion(true);

                    string content = HttpHelper.GetStringFromUrl($"{args.ChangelogURL}");
                    DialogResult dialogResult;
                    if (args.Mandatory.Value)
                    {
                        dialogResult =
                            System.Windows.Forms.MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. 
                                {Environment.NewLine} Note: 
                                {Environment.NewLine} {content}
                                {Environment.NewLine} This is required update. Press Ok to begin updating the application.",
                                @"Update Available",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }
                    else
                    {
                        dialogResult =
                             System.Windows.Forms.MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. 
                                {Environment.NewLine} Note: 
                                {Environment.NewLine} {content}
                                {Environment.NewLine} Do you want to update the application now?",
                                @"Update Available",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);
                    }

                    // Uncomment the following line if you want to show standard update dialog instead.
                    // AutoUpdater.ShowUpdateForm(args);

                    if (dialogResult.Equals(System.Windows.Forms.DialogResult.Yes) || dialogResult.Equals(System.Windows.Forms.DialogResult.OK))
                    {
                        try
                        {
                            if (AutoUpdater.DownloadUpdate(args))
                            {
                                System.Windows.Application.Current.Shutdown();
                            }
                        }
                        catch (Exception exception)
                        {
                            System.Windows.Forms.MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    if (startApp != true) {
                        System.Windows.Forms.MessageBox.Show(@"There is no update available please try again later.", @"No update available",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HandleOutNewVersion(false);
                    }
                   
                }
            }
            else
            {
                if (startApp != true)
                {
                    if (args.Error is WebException)
                    {
                        System.Windows.Forms.MessageBox.Show(
                            @"There is a problem reaching update server. Please check your internet connection and try again later.",
                            @"Update Check Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(args.Error.Message,
                            args.Error.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                HandleOutNewVersion(false);
            }
            startApp = false;
        }

        private void AutoUpdaterOnApplicationExitEvent()
        {
            // Handle any cleanup or actions before exiting the application
            System.Windows.Application.Current.Shutdown();
        }

        private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = json.item.version,
                ChangelogURL = json.item.changelog,
                DownloadURL = json.item.url,
                Mandatory = new Mandatory
                {
                    Value = ((string)json.item.mandatory).ToLower()=="true",
                   // UpdateMode = json.item.mandatory.mode,
                   // MinimumVersion = json.item.mandatory.minVersion
                },
                //CheckSum = new CheckSum
                //{
                //    Value = json.item.checksum.value,
                //    HashingAlgorithm = json.item.checksum.hashingAlgorithm
                //}
            };
        }
    }
}
