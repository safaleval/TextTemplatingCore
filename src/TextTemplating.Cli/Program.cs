using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TextTemplating.Infrastructure;
using TextTemplating;

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

            app.HelpOption(helpTemplate);
            try
            {
                if (args.Length == 1)
                {
                    return ProcessFile(args);
                }
                else
                {//else check commands
                    return app.Execute(args);
                }
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

        private static int ProcessFile(string[] args)
        {
            bool tt = isSuppFile(args[0], "tt");
            bool csx = isSuppFile(args[0], "csx");
            string f = args[0];
            if (args.Length == 1
            && (tt || csx))
            {
                ttConsole.WriteNormal("Executing file: " + f);
                if (tt) { return AppCommands.ProcessTTFile(f); }
                if (csx) { return AppCommands.ProcessCSXFile(f); }
            }
            return 0;
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