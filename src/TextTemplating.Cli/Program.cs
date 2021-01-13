using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TextTemplating.Infrastructure;

namespace TextTemplating.Tools
{
    class Program
    {
        public static IServiceProvider DI;

        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IMetadataResolveable, MsBuildProjectMetadataResolver>();
            services.AddSingleton<ITextTemplatingEngineHost, CommandLineEngineHost>();
            services.AddSingleton<RoslynCompilationService>();
            services.AddSingleton<Engine>();
            DI = services.BuildServiceProvider();
        }

        static int Main(string[] args)
        {
            ConfigureServices();
            const string helpTemplate = "-h|--help";
            var app = new CommandLineApplication
            {
                Description = "A simple Text Template Transformer for .Net Core"
            };

            var process = app.Command("proc", AppCommands.ProcessCommand);
            var transform = app.Command("trans", AppCommands.TransformCommand);
            // var file = app.Argument("tt", "Path to CSX/tt script");
            app.HelpOption(helpTemplate);
            try
            {
                bool tt = isSuppFile(args[0], "tt");
                bool csx = isSuppFile(args[0], "csx");
                if (args.Length == 1
                && (tt || csx))
                {
                    Console.WriteLine("executing file:" + args[0]);
                    if (tt) { AppCommands.ProcessTTFile(args[0]); }
                }
                //else check commands
                return app.Execute(args);
            }
            catch (CommandParsingException e)
            {
                app.ShowHelp(e.Command.Name);
                return 1;
            }
            catch (IOException e)
            {
                switch (e)
                {
                    case ProjectNotFoundException pe:
                    case FileNotFoundException fe:
                    case DirectoryNotFoundException de:
                        Console.WriteLine(e.Message);
                        break;
                }

                return 1;
            }
        }

        static bool isSuppFile(string s, string type)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var val = s.ToLower();
                var tt = val.ToLower().EndsWith(type);
                if (tt)
                { return true; }
            }
            return false;
        }
    }
}