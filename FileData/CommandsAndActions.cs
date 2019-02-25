using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileData
{

    class CommandsAndActions
    {
        public Dictionary<string[], Action<string[]>> CommandAction { get; set; }
    }
}
