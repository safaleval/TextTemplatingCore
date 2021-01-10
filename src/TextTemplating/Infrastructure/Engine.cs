using System;
using System.Linq;
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
        
    }
}
