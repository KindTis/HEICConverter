using Microsoft.Extensions.CommandLineUtils;
using System;
using System.IO;

namespace HEICConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string workDir = "";
            bool deleteOriginal = false;

            CommandLineApplication argParser = new CommandLineApplication(throwOnUnexpectedArg: false);
            var dirOption = argParser.Option("-w | --workdir <Directory>", "Working Directory", CommandOptionType.SingleValue);
            var deleteOption = argParser.Option("-d | --delete", "delete original", CommandOptionType.NoValue);
            argParser.HelpOption("-? | -h | --help");
            argParser.OnExecute(() => {
                workDir = dirOption.Value();
                deleteOriginal = deleteOption.HasValue();
                return 0;
            });
            argParser.Execute(args);

            if (workDir.Length == 0)
                return;

            if (!Directory.Exists(workDir))
            {
                Console.WriteLine("dosen't exist dir: " + workDir);
                return;
            }

            ImageConverter converter = new ImageConverter();
            converter.Excution(workDir, deleteOriginal);
        }
    }
}
