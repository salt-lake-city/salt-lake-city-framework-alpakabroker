using System;
using Microsoft.CodeAnalysis;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing
{
 internal class SourceGeneratorContext<T> : IDisposable where T : ISourceGenerator
    {
        private SourceGeneratorContext(GeneratorExecutionContext generatorExecutionContext)
        {
            GeneratorExecutionContext = generatorExecutionContext;
        }

        public GeneratorExecutionContext GeneratorExecutionContext { get; }

        public void Dispose()
        {
        }

        public static SourceGeneratorContext<T> Create(GeneratorExecutionContext context)
        {
            var sourceGenContext = new SourceGeneratorContext<T>(context);
            return sourceGenContext;
        }
    }
}