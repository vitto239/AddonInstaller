using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace jedynka
{
    internal abstract class AbstractMasterClass
    {
        internal delegate void MetodsDelegate();
        public abstract string extractFolder { get; }
        public abstract bool downloaded { get; internal set; }
        public abstract string file { get; internal set; }
        public abstract bool instaled { get; internal set; }
        public abstract string link { get; internal set; }
        public abstract string extractArgument { get; internal set; }
        public abstract TextBlock infoTxb { get; internal set; }
        public abstract TextBlock statusTxb { get; internal set; }
        public abstract ProgressBar proBar { get; internal set; }        
        internal bool isDownloaded()
        {
            return File.Exists(file);
        }
        internal void Extract(AbstractMasterClass item)
        {
            item.proBar.Visibility = Visibility.Hidden;
            item.statusTxb.Text = "Downloaded"; item.downloaded = true;
            if (infoTxb.Text.Contains("Framework")) { return; }
            item.statusTxb.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new MetodsDelegate(() =>
            {
                using (var p = new Process())
                {
                    var si = new ProcessStartInfo()
                    {
                        FileName = file,
                        Arguments = extractArgument,
                        UseShellExecute = false
                    };
                    p.StartInfo = si; p.Start(); p.WaitForExit();
                }
            }
            ));
            
        }
    }
}
