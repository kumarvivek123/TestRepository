using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileData
{
    interface ICommandLineUtility
    {
        //This dictionary holds the list of commands and their associated actions
        Dictionary<string[], Func<string[],string>> CommandsActions { get; set; }
        // This validate checks whether the command is valid or not
        FileDataResult Validate(string[] command);
        //Loads Commands and Associated actions during runtime
        void LoadCommandsActions();
        //Execute the necessary action
        FileDataResult Execute(string[] command);
    }


}
