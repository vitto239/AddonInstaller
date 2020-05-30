using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace jedynka
{
    internal class _Net48InstallClass:AbstractMasterClass
    {
        public override string file { get; internal set; }
        public override bool downloaded { get; internal set; }
        public override string extractFolder { get; }
        public override bool instaled { get; internal set; }
        public override string link { get; internal set; }
        public override string extractArgument { get; internal set; }
        public override TextBlock infoTxb { get; internal set; }
        public override TextBlock statusTxb { get; internal set; }
        public override ProgressBar proBar { get; internal set; }

        public _Net48InstallClass()
        {

        }
        public _Net48InstallClass(TextBlock _netInfotxb, TextBlock _netStatustxb, ProgressBar _netProBar)
        {
            infoTxb = _netInfotxb;
            infoTxb.Text = "Net Framework 4.8";
            instaled = new _netframeworkchecker().CheckFramework();            
            link = "https://download.visualstudio.microsoft.com/download/pr/014120d7-d689-4305-befd-3cb711108212/0fd66638cde16859462a6243a4629a50/ndp48-x86-x64-allos-enu.exe";
            file = Path.GetTempPath() + Path.GetFileName(new Uri(link).LocalPath);
            extractFolder = Path.GetTempPath() + "net48installTemp";
            extractArgument = file + "/u /norestart";
            downloaded = isDownloaded();
            statusTxb = _netStatustxb;
            proBar = _netProBar;
        }

    }
}