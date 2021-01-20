using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Tools;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing
{
 internal class SourceGeneratorContext<T> : IDisposable where T : ISourceGenerator
    {
        private SourceGeneratorContext(GeneratorExecutionContext generatorExecutionContext)
        {
            Options = new SourceGeneratorOptions<T>(generatorExecutionContext);
            GeneratorExecutionContext = generatorExecutionContext;
        }

        public SourceGeneratorOptions<T> Options { get; }
        public GeneratorExecutionContext GeneratorExecutionContext { get; }

        public void Dispose()
        {
        }

        public static SourceGeneratorContext<T> Create(GeneratorExecutionContext context)
        {
            var sourceGenContext = new SourceGeneratorContext<T>(context);

            if (sourceGenContext.Options.EnableDebug)
            {
                if (!Debugger.IsAttached)
                {
                    Debugger.Launch();
                }
            }
            return sourceGenContext;
        }

        public void ApplyDesignTimeFix(string content, string hintName)
        {
            if (Options.IntellisenseFix)
            {
                var path = Path.Combine(Options.IntermediateOutputPath, hintName + ".generated.cs");
                File.WriteAllText(path, content, Encoding.UTF8);
            }
        }

        public GeneratedSource GenerateErrorSourceCode(Exception exception, ClassDeclarationSyntax classDeclaration)
        {
            var context = $"[{typeof(T).Name} - {classDeclaration.Identifier.Text}]";

            var templateString = ResourceReader.GetResource("ErrorModel.cstemplate");
            templateString = templateString.Replace("//Error",
                $"#error {context} {exception.Message} | Logfile: {Options.LogPath}");

            return new GeneratedSource(templateString, classDeclaration.Identifier.Text);
        }
    }
}