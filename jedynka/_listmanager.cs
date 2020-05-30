using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

namespace jedynka
{
    class _listmanager
    {
        private readonly _netframeworkchecker frameworkCheck;
        private readonly _sqlchecker sqlCheck;
        private readonly _architecturechecker architectureCheck;
        ObservableCollection<_itemstructure> ListItems;
        public _listmanager()
        {
            if (ListItems == null) { ListItems = new ObservableCollection<_itemstructure>(); }
            if (frameworkCheck == null) { frameworkCheck = new _netframeworkchecker(); }
            if (sqlCheck == null) { sqlCheck = new _sqlchecker(); }
            if (architectureCheck == null) { architectureCheck = new _architecturechecker(); }

            var sb = new StringBuilder();
            sb.Append("/Action=Install /IAcceptSQLServerLicenseTerms=True /ADDCURRENTUSERASSQLADMIN=true /FEATURES=SQLENGINE,REPLICATION,SNAC_SDK");
            sb.Append(" /INSTANCENAME = " + '"' + "SQLEXPRESS2014EX" + '"');
            sb.Append(" /INSTANCEID = " + '"' + "SQLEXPRESS2014EX" + '"');
            sb.Append(" /SQLSVCACCOUNT = " + '"' + "NT Service\\MSSQL$SQLEXPRESS2014EX" + '"');
            sb.Append(" /SECURITYMODE = " + '"' + "SQL" + '"');
            sb.Append(" /SAPWD = " + '"' + "Sql12345" + '"' + " /QS");

            ListItems.Add(new _itemstructure
            {
                name = "Net Framework 4.8",
                instaled = frameworkCheck.CheckFramework(),
                downloaded = false,
                link64 = "https://download.visualstudio.microsoft.com/download/pr/014120d7-d689-4305-befd-3cb711108212/0fd66638cde16859462a6243a4629a50/ndp48-x86-x64-allos-enu.exe",
                link32 = null,
                installfile = "setup.exe",
                installarguments = "/u"
            }); ;
            ListItems.Add(new _itemstructure
            {
                name = "Sql Server 2014 express",
                instaled = sqlCheck.CheckSql2014Ex(),
                downloaded = false,
                link64 = (architectureCheck.is64BitOperatingSystem) ? "https://download.microsoft.com/download/E/A/E/EAE6F7FC-767A-4038-A954-49B8B05D04EB/Express%2064BIT/SQLEXPR_x64_ENU.exe" : null,
                link32 = (!architectureCheck.is64BitOperatingSystem) ? "https://download.microsoft.com/download/E/A/E/EAE6F7FC-767A-4038-A954-49B8B05D04EB/Express%2032BIT/SQLEXPR_x86_ENU.exe" : null,
                installfile = "setup.exe",
                installarguments = sb.ToString()
            });
        }
        public _netframeworkchecker FrameworkCheck
        {
            get
            {
                return frameworkCheck;
            }
        }
        public _sqlchecker SqlCheck
        {
            get
            {
                return sqlCheck;
            }
        }
        public _architecturechecker ArchitectureCheck
        {
            get
            {
                return architectureCheck;
            }
        }
        public _itemstructure GetListItem(int numberItem)
        {
            return ListItems[numberItem];
        }
        public void SetListItem(int numberItem,_itemstructure _struct)
        {
            ListItems[numberItem] = _struct;
        }
    }
}
