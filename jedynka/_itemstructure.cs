using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace jedynka
{
   struct _itemstructure
    {
        internal bool downloaded;

        internal string name { get; set; }
        internal string link64 { get; set; }
        internal string link32 { get; set; }
        internal bool instaled { get; set; }
        internal string installfile { get; set; }
        internal string installarguments { get; set; }
    }
}
