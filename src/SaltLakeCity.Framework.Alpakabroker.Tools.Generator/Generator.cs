using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // check that the users compilation references the expected library 
            if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals("SaltLakeCity.Framework.Alpakabroker", StringComparison.OrdinalIgnoreCase)))
            {
                //context.ReportDiagnostic();
            }
            
            using var sourceGenContext = SourceGeneratorContext<Generator>.Create(context);
            if (context.SyntaxReceiver is not AlpakaEventSyntaxReceiver actorSyntaxReciver) return;

            foreach (var @event in actorSyntaxReciver.Events)
            {
                var alpakaEventModel = AlpakaEventModelFactory.From(@event, sourceGenContext.GeneratorExecutionContext.Compilation);
                var alpakaEventEmitter = TemplateGenerator.GenerateAlpakaEventEmitter(alpakaEventModel);
                var alpakaEventReceiverBase =
                    TemplateGenerator.GenerateAlpakaEventEventReceiverBase(alpakaEventModel);

                
                context.AddSource(alpakaEventEmitter.FileName, alpakaEventEmitter.SourceCode);
                context.AddSource(alpakaEventReceiverBase.FileName, alpakaEventReceiverBase.SourceCode);
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
    }
}