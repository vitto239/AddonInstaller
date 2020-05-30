using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace jedynka
{
    class _sqlchecker
    {
        internal bool CheckSql2014Ex()
        {
            var architecture = new _architecturechecker();
            var xx =architecture.is64BitOperatingSystem;
            var yy =architecture.is64BitProcess;

            if (yy && xx)
            {
                return sql6432bit();
            }
            else if (!yy && xx)
            {
                return wow6432();
            }
            return false;
            bool sql6432bit()
            {
                const string subkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL";
                using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(subkey))
                {
                    if (ndpKey != null)
                    {
                        foreach (var item in ndpKey.GetValueNames())
                        {
                            var y = ndpKey.GetValue(item);
                            if (y.ToString().Contains("MSSQL12")) { return true; }
                        }
                    }
                }
                return false;
            }
            bool wow6432()
            {
                const string subkey = @"SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server\Instance Names\SQL";

                using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(subkey))
                {
                    if (ndpKey != null)
                    {
                        foreach (var item in ndpKey.GetValueNames())
                        {
                            var y = ndpKey.GetValue(item);
                            if (y.ToString().Contains("MSSQL12")) { return true; }
                        }
                    }
                }
                return false;
            }
        }
    }
}
