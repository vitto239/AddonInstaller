using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace jedynka
{
    class _netframeworkchecker
    {
        internal bool CheckFramework()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    var x = (int)ndpKey.GetValue("Release");
                    if ((int)ndpKey.GetValue("Release") >= 528040) { return true; }
                }
            }
            return false;
        }
    }
}
