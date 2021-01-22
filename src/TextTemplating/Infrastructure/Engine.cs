using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using TextTemplating.T4.Parsing;
using TextTemplating.T4.Preprocessing;

namespace TextTemplating.Infrastructure
{
    public class Engine
    {
        private readonly ITextTemplatingEngineHost _host;
        private readonly RoslynCompilationService _compilationService;

        public Engine(ITextTemplatingEngineHost host, RoslynCompilationService compilationService)
        {
            _host = host;
            _compilationService = compilationService;
        }

        // todo add to cli tool
        /// <summary>
        /// convert tt template to c# code
        /// </summary>
        /// <param name="content">tt template</param>
        /// <param name="className">cs class</param>
        /// <param name="classNamespace">cs namespace</param>
        /// <returns>cs cs code with references</returns>
        public PreprocessResult PreprocessT4Template(string content, string className, string classNamespace)
        {
            var result = new Parser(_host).Parse(content);
            var transformation = new PreprocessTextTransformation(className, classNamespace, result, _host);
            var preprocessedContent = transformation.TransformText();

            var preprocessed = new PreprocessResult
            {
                References = result.References.Distinct().ToArray(),
                PreprocessedContent = preprocessedContent
            };

            return preprocessed;
        }

        /// <summary>
        /// run t4 template
        /// </summary>
        /// <param name="content">tt template</param>
        /// <returns></returns>
        public string ProcessT4Template(string content)
        {
            var className = "GeneratedClass";
            var classNamespace = "Generated";
            var assemblyName = "Generated";

            var preResult = PreprocessT4Template(content, className, classNamespace);

            //var compiler = new RoslynCompilationService(_host);
            var transformationAssembly = _compilationService.Compile(assemblyName, preResult);


            var transformationType = transformationAssembly.GetType(classNamespace + "." + className);

            var transformation = Activator.CreateInstance(transformationType) as TextTransformationBase;//(TextTransformationBase)

            transformation.Host = _host;
            return transformation.TransformText();
        }

        string output = "";
        public string ProcessCSXTemplate(string content, string filePath,
        IMetadataResolveable resolver, ProjectMetadata projmeta)
        {
            var references = new List<MetadataReference>();
            // project references
            references.AddRange(resolver.ResolveMetadataReference());
            // assembly instruction           
            output = projmeta.OutputPath;
            var opt =
                    ScriptOptions.Default

                      .WithReferences(_host.StandardAssemblyReferences)
                      .WithMetadataResolver(ScriptMetadataResolver.Default.WithSearchPaths(RuntimeEnvironment.GetRuntimeDirectory()))
                      //   .AddImports(_host.StandardImports) //no standard imports
                      .WithFilePath(filePath);

            var refFiltd = references.Where((item, index) =>
            !_host.StandardAssemblyReferences.Any(
                   x => item.Display.Contains(x + ".dll"))
                   //dont load dlls that re already included in standard
                   ).ToList();
            // foreach (var item in rr)
            // { Console.WriteLine("ref:" + item.Display); }
            // opt = opt.WithReferences(rr).AddImports(_host.StandardImports); //system object not defined
            foreach (var dep in refFiltd)
            {
                // Logger.Debug("Adding reference to a runtime dependency => " + runtimeDependency);
                opt = opt.AddReferences(MetadataReference.CreateFromFile(dep.Display));
            }
            var loader = new InteractiveAssemblyLoader();
            try
            {
                CSharpScript.EvaluateAsync(content, options: opt, loader)
                .ContinueWith(s => s.Result).Wait();
            }
            catch (CompilationErrorException ex)
            {
                if (ex.Message.Contains(")"))
                {   var m = ex.Message.Split(")");
                    ttConsole.WriteError(m[0] + ")");
                    ttConsole.WriteError(m[1]); 
                }else{ ttConsole.WriteError(ex.Message);}
                ttConsole.WriteNormal("");
                ttConsole.WriteError(ex.StackTrace);
            }
            return "";
        }

        // string r = @"(?:^|\n)(\s*(:?using))\s+((?<attribute>\w*(:?\.)*\w*));";

    }
}
