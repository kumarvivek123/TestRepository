using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using ThirdPartyTools;

namespace FileData
{
    public static class Program
    {
        // public static ICommandLineUtility CommandLineUtility { get; set; }
        public static void Main(string[] args)
        {
            try
            {
                // Complete functionality is encapsulated in FileDataUtility which is implementing ICommandLineUtility
                ICommandLineUtility FileUtility = new FileDataUtility();
                FileUtility.LoadCommandsActions();
                var validationResult = FileUtility.Validate(args);
                if (validationResult.Status)
                {
                    var executionResult = FileUtility.Execute(args);
                    Console.WriteLine(executionResult.ResultMessage);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(validationResult.ResultMessage);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}

