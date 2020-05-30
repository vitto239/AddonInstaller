using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;

namespace jedynka
{
    internal class _SqlInstallClass:AbstractMasterClass
    {
        public override string extractFolder { get; }
        public override bool instaled { get; internal set; }
        public override string link { get; internal set; }
        public override string file { get; internal set; }
        public override bool downloaded { get; internal set; }
        public override string extractArgument { get; internal set; }
        public override TextBlock infoTxb { get; internal set; }
        public override TextBlock statusTxb { get; internal set; }
        public override ProgressBar proBar { get; internal set; }

        public _SqlInstallClass(TextBlock _sqlInfotxb, TextBlock _sqlStatustxb, ProgressBar _sqlProBar)
        {
            infoTxb = _sqlInfotxb;
            infoTxb.Text = "SQL 2014 EXPRESS";
            instaled = new _sqlchecker().CheckSql2014Ex();            
            link = new _architecturechecker().is64BitOperatingSystem ?
                "https://download.microsoft.com/download/E/A/E/EAE6F7FC-767A-4038-A954-49B8B05D04EB/Express%2064BIT/SQLEXPR_x64_ENU.exe" :
                "https://download.microsoft.com/download/E/A/E/EAE6F7FC-767A-4038-A954-49B8B05D04EB/Express%2032BIT/SQLEXPR_x86_ENU.exe";
            file = Path.GetTempPath() + Path.GetFileName(new Uri(link).LocalPath);
            extractFolder = Path.GetTempPath() + "sqlinstallTemp";
            extractArgument ="/u /x:" + extractFolder;
            downloaded = isDownloaded();
            statusTxb = _sqlStatustxb;
            proBar = _sqlProBar;
        }
    }
}