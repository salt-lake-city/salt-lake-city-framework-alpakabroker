using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Tools;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            using var sourceGenContext = SourceGeneratorContext<Generator>.Create(context);

            if (context.SyntaxReceiver is AlpakaEventSyntaxReceiver actorSyntaxReciver)
            {
                foreach (var @event in actorSyntaxReciver.Events)
                {
                    var source = GenerateAlpakaEventReceiver(@event, sourceGenContext);

                    context.AddSource(source.FileName, source.SourceCode);
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            //if (!Debugger.IsAttached)
              //Debugger.Launch();
#endif

            context.RegisterForSyntaxNotifications(() => new AlpakaEventSyntaxReceiver());

        }
        
        private GeneratedSource GenerateAlpakaEventReceiver(ClassDeclarationSyntax alpakaEventSyntax, SourceGeneratorContext<Generator> context)
        {
            var alpakaEventModel = AlpakaEventModelFactory.From(alpakaEventSyntax, context.GeneratorExecutionContext.Compilation);

            var templateString = ResourceReader.GetResource("AlpakaEventReceiver.sbncs");

            var result = TemplateGenerator.Execute(templateString, alpakaEventModel);
            return new GeneratedSource(result, alpakaEventModel.ClassName);
        }
    }
}