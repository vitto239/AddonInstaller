using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;

namespace jedynka
{
    public partial class MainWindow : Window
    {
        internal delegate void MetodsDelegate(AbstractMasterClass obj);
        private _Net48InstallClass Net48InstallClass;
        private _SqlInstallClass SqlInstallClass;
        private ObservableCollection<AbstractMasterClass> ListItems;

        public MainWindow()
        {
            InitializeComponent();            
            if (Net48InstallClass == null) { Net48InstallClass = new _Net48InstallClass(netInfotxb, netStatustxb, netProBar); }
            if (SqlInstallClass == null) { SqlInstallClass = new _SqlInstallClass(sqlInfotxb, sqlStatustxb, sqlProBar); }
            if (ListItems == null) { ListItems = new ObservableCollection<AbstractMasterClass>(); }
            ListItems.Add(Net48InstallClass); ListItems.Add(SqlInstallClass);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeLayout();
        }
        private void NextStep()
        {
            throw new NotImplementedException();
        }
        private void InitializeLayout()
        {
            Net48InstallClass.infoTxb.Background=Net48InstallClass.statusTxb.Background=Net48InstallClass.instaled ? Brushes.LightGreen : Brushes.LightPink;
            Net48InstallClass.statusTxb.Text= Net48InstallClass.instaled ? "is instaled ..." : "is not instaled ...";
            SqlInstallClass.infoTxb.Background = SqlInstallClass.statusTxb.Background = SqlInstallClass.instaled ? Brushes.LightGreen : Brushes.LightPink;
            SqlInstallClass.statusTxb.Text = SqlInstallClass.instaled ? "is instaled ..." : "is not instaled ...";
            
            btnAction.Background = (Net48InstallClass.instaled && SqlInstallClass.instaled) ? Brushes.LightGreen : Brushes.LightPink;
            btnAction.Content = (Net48InstallClass.instaled && SqlInstallClass.instaled) ? "Next ..." : "Install AdOn";
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            Net48InstallClass.instaled = false; // na potrzeby testów
            if (Net48InstallClass.instaled && SqlInstallClass.instaled) { NextStep(); }
            foreach (var item in ListItems)
            {   
                if (!item.instaled)
                {
                    if(!item.downloaded)
                    {
                        item.proBar.Visibility = !item.instaled ? Visibility.Visible : Visibility.Hidden;
                        DownloadItem(item);
                        
                    }                    
                }
            }
        }
        private void DownloadItem(AbstractMasterClass _item)
        {
            var WebDownloader = new WebClient();
            WebDownloader.DownloadFileCompleted += (sender, e) => WebDownloader_DownloadFileCompleted(sender, e, _item);
            WebDownloader.DownloadProgressChanged += (sender, e) => WebDownloader_DownloadProgressChanged(sender, e, _item);
            WebDownloader.DownloadFileAsync(new Uri(_item.link), _item.file);
        }

        private void WebDownloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e, AbstractMasterClass item)
        {
            
            
        }

        private void WebDownloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e, AbstractMasterClass item)
        {
            item.proBar.Value = e.ProgressPercentage;
        }
    }
}



//private void btnAction_Click(object sender, RoutedEventArgs e)
//{
//    netItem = listManager.GetListItem(0); sqlItem = listManager.GetListItem(1);
//    //netItem.instaled = false;sqlItem.instaled = false;  //  na potrzeby testu do usunięcia
//    if (netItem.instaled && sqlItem.instaled) { NextStep(); }
//    netProBar.Visibility = !netItem.instaled ? Visibility.Visible : Visibility.Hidden;
//    sqlProBar.Visibility = !sqlItem.instaled ? Visibility.Visible : Visibility.Hidden;
//    if (!netItem.instaled) { DownloadFile(netItem, nameof(netProBar)); }
//    if (!sqlItem.instaled) { DownloadFile(sqlItem, nameof(sqlProBar)); }

//}

//private void NetWebDownloader_DownloadFileCompleted1(object sender, AsyncCompletedEventArgs e)
//{
//    //netDownload = true; InstallApp(netDownload);
//}

//private void SqlWebDownloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
//{
//    //sqlDownload = true; InstallApp(sqlDownload);
//}
//private void SqlNetWebDownloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
//{
//    //sqlProBar.Value = e.ProgressPercentage;
//}

//private void NetWebDownloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
//{
//    //netProBar.Value = e.ProgressPercentage;
//}
//private void DownloadFile(_itemstructure itemStructure, string v)
//{
//    if (v == nameof(netProBar))
//    {
//        var link = itemStructure.link32 == null ? itemStructure.link64 : itemStructure.link32;
//        var file = Path.GetTempPath() + Path.GetFileName(new Uri(link).LocalPath);
//        if (File.Exists(file))
//        {
//            itemStructure.installfile = file; listManager.SetListItem(0, itemStructure); netItem = itemStructure;
//            netInfotxb.Background = Brushes.LightGreen; netProBar.Value = 100;
//            netDownload = true; InstallApp(netDownload);
//            return;
//        }
//        var WebDownloader = new WebClient();
//        WebDownloader.DownloadFileCompleted += NetWebDownloader_DownloadFileCompleted1;
//        WebDownloader.DownloadProgressChanged += NetWebDownloader_DownloadProgressChanged;
//        WebDownloader.DownloadFileAsync(new Uri(link), file);
//        itemStructure.installfile = file; listManager.SetListItem(0, itemStructure); netItem = itemStructure;


//    }
//    if (v == nameof(sqlProBar))
//    {
//        var link = itemStructure.link32 == null ? itemStructure.link64 : itemStructure.link32;
//        var file = Path.GetTempPath() + Path.GetFileName(new Uri(link).LocalPath);
//        if (File.Exists(file))
//        {
//            itemStructure.installfile = file; listManager.SetListItem(1, itemStructure); sqlItem = itemStructure;
//            sqlInfotxb.Background = Brushes.LightGreen; sqlProBar.Value = 100;
//            netDownload = true; //  testy
//            sqlDownload = true; InstallApp(sqlDownload);
//            return;
//        }
//        var WebDownloader = new WebClient();
//        WebDownloader.DownloadFileCompleted += SqlWebDownloader_DownloadFileCompleted;
//        WebDownloader.DownloadProgressChanged += SqlNetWebDownloader_DownloadProgressChanged;
//        WebDownloader.DownloadFileAsync(new Uri(link), file);
//        itemStructure.installfile = file; listManager.SetListItem(1, itemStructure); sqlItem = itemStructure;
//    }
//}
//private void InstallApp(bool status)
//{
//    if (netDownload != true || sqlDownload != true) { return; }

//    if (!netItem.instaled)
//    {
//        var link = netItem.link32 == null ? netItem.link64 : netItem.link32;
//        var file = Path.GetTempPath() + Path.GetFileName(new Uri(link).LocalPath);
//        var process = new Process
//        {
//            StartInfo =
//              {
//                  FileName = file,
//                  Arguments = netItem.installarguments
//              }
//        };
//        process.Start(); process.WaitForExit();
//    }
//    if (!sqlItem.instaled)
//    {
//        var link = sqlItem.link32 == null ? sqlItem.link64 : sqlItem.link32;
//        var file = Path.GetTempPath() + Path.GetFileName(new Uri(link).LocalPath);
//        using (var process = new Process())
//        {
//            var si = new ProcessStartInfo()
//            {
//                FileName = file,
//                Arguments = "/u /x:" + Path.GetTempPath() + "sqltemp"
//            }; process.StartInfo = si; process.Start(); process.WaitForExit();
//        }
//        using (var process = new Process())
//        {
//            var si = new ProcessStartInfo()
//            {
//                FileName = Path.GetTempPath() + "sqltemp\\setup.exe",
//                Arguments = sqlItem.installarguments,
//                UseShellExecute = false
//            };
//            process.StartInfo = si; var str = si.FileName + " " + si.Arguments;

//            process.Start(); process.WaitForExit();
//        }
//    }

//}