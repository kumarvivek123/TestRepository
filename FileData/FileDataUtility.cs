using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyTools;

namespace FileData
{
    public class FileDataUtility : ICommandLineUtility
    {
        //This dictionary holds the list of commands and their associated actions
        public Dictionary<string[], Func<string[], string>> CommandsActions { get; set; }
        public FileDetails FileDetails { get; set; }
        public FileDataUtility()
        {
            CommandsActions = new Dictionary<string[], Func<string[], string>>();
            FileDetails = new FileDetails();
        }

        //Loads Commands and Associated actions during runtime
        public void LoadCommandsActions()
        {
            CommandsActions.Add(GetArgumentGroup("VersionArgumentGroup"), (args) =>
            {
                return FileDetails.Version(args[args.Length - 1]);
            });

            CommandsActions.Add(GetArgumentGroup("SizeArgumentGroup"), (args) =>
            {
                return FileDetails.Size(args[args.Length - 1]).ToString();
            });

        }

        // This validate checks whether the command is valid or not
        public FileDataResult Validate(string[] args)
        {
            if (args == null || args.Length == 0 || args.Length != 2)
            {
                return new FileDataResult()
                {
                    Status = false,
                    ResultMessage = ConfigurationManager.AppSettings["InvalidNumberOfArgs"]
                };
            }
            if ((!GetArgumentGroup("VersionArgumentGroup").Any(v => v.Equals(args[0])) && !GetArgumentGroup("SizeArgumentGroup").Any(v => v.Equals(args[0]))) || args[1] == string.Empty)
            {
                return new FileDataResult()
                {
                    Status = false,
                    ResultMessage = ConfigurationManager.AppSettings["InvalidArgs"]
                };
            }
            return new FileDataResult()
            {
                Status = true
            };
        }
        //Execute the necessary action
        public FileDataResult Execute(string[] args)
        {
            FileDataResult result = new FileDataResult();
            result.Status = false;
            result.ResultMessage = ConfigurationManager.AppSettings["InvalidOp"];
            foreach (var commandKey in CommandsActions.Keys)
            {
                if (commandKey.Any(c => c.Equals(args[0])))
                {
                    result.Status = true;
                    result.ResultMessage = CommandsActions[commandKey].Invoke(args);
                    break;
                }
            }
            return result;
        }

        //Get the version and size related commands from app.config
        public string[] GetArgumentGroup(string key)
        {
            NameValueCollection keyCollection = (NameValueCollection)ConfigurationManager.GetSection(key);
            return keyCollection.AllKeys;
        }

    }
}
