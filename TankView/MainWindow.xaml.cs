﻿using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TankLib.CASC;
using TankLib.CASC.Helpers;
using TankView.ViewModel;

namespace TankView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public SynchronizationContext ViewContext { get; }

        public NGDPPatchHosts NGDPPatchHosts { get; set; }
        public RecentLocations RecentLocations { get; set; }
        public ProgressInfo ProgressInfo { get; set; }
        public CASCSettings CASCSettings { get; set; }
        public GUIDCollection GUIDTree { get; set; }

        public static CASCConfig Config;
        public static CASCHandler CASC;

        public bool IsReady { get; set; } = true;

        private ProgressReportSlave ProgressSlave = new ProgressReportSlave();

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindow()
        {
            ViewContext = SynchronizationContext.Current;

            NGDPPatchHosts = new NGDPPatchHosts();
            RecentLocations = new RecentLocations();
            ProgressInfo = new ProgressInfo();
            CASCSettings = new CASCSettings();

            ProgressSlave.OnProgress += UpdateProgress;

            if (!NGDPPatchHosts.Any(x => x.Active))
            {
                NGDPPatchHosts[0].Active = true;
            }

            InitializeComponent();
            DataContext = this;
            // FolderView.ItemsSource = ;
            // FolderItemList.ItemsSource = ;
        }

        private void UpdateProgress(object sender, ProgressChangedEventArgs @event)
        {
            ViewContext.Send(x =>
            {
                if (x is ProgressChangedEventArgs evt)
                {
                    if (evt.UserState != null && evt.UserState is string state)
                    {
                        ProgressInfo.State = state;
                    }
                    ProgressInfo.Percentage = evt.ProgressPercentage;
                }
            }, @event);
        }

        public string ModuloTitle {
            get {
                return "TankView";
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void NGDPHostChange(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                foreach (PatchHost node in NGDPPatchHosts.Where(x => x.Active && x.GetHashCode() != (menuItem.DataContext as PatchHost).GetHashCode()))
                {
                    node.Active = false;
                }

                CollectionViewSource.GetDefaultView(NGDPPatchHosts).Refresh();
            }
        }

        private void OpenNGDP(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException(nameof(OpenNGDP));
        }

        private void OpenCASC(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.EnsurePathExists = true;
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OpenCASC(dialog.FileName);
            }
        }

        private void OpenRecent(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string path = menuItem.DataContext as string;
                RecentLocations.Add(path);
                CollectionViewSource.GetDefaultView(RecentLocations).Refresh();

                if (path?.StartsWith("ngdp://") == true)
                {
                    OpenNGDP(path);
                }
                else
                {
                    OpenCASC(path);
                }
            }
        }

        private void OpenNGDP(string path)
        {
            RecentLocations.Add(path);
            CollectionViewSource.GetDefaultView(RecentLocations).Refresh();
            throw new NotImplementedException(nameof(OpenNGDP));
        }

        private void OpenCASC(string path)
        {
            RecentLocations.Add(path);
            CollectionViewSource.GetDefaultView(RecentLocations).Refresh();

            IsReady = false;
            NotifyPropertyChanged(nameof(IsReady));

            Config = null;
            CASC = null;
            GUIDTree = null;
            NotifyPropertyChanged(nameof(GUIDTree));
            GC.Collect();

            Task.Run(delegate
            {
                Config = CASCConfig.LoadLocalStorageConfig(path, true, true);

                CASC = CASCHandler.Open(Config, ProgressSlave);

                BuildTree();

                ProgressSlave.ReportProgress(0, "Idle");

                ViewContext.Send(new SendOrPostCallback(delegate
                {
                    IsReady = true;
                    NotifyPropertyChanged(nameof(IsReady));
                }), null);
            });
        }

        private void BuildTree()
        {
            GUIDTree = new GUIDCollection(Config, CASC, ProgressSlave);
            NotifyPropertyChanged(nameof(GUIDTree));
        }

        private void ChangeActiveNode(object sender, RoutedEventArgs e)
        {
            if(e.Handled)
            {
                return;
            }

            if (sender is TreeViewItem item)
            {
                Folder folder = item.DataContext as Folder;
                GUIDTree.SelectedEntries = folder.Files;
                NotifyPropertyChanged(nameof(GUIDTree));
                e.Handled = true;
            }
        }

        private void ExtractFiles(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException(nameof(ExtractFiles));
        }
    }
}
