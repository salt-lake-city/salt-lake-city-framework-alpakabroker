using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Templates;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // check that the users compilation references the expected library 
            if (!context.Compilation.ReferencedAssemblyNames.Any(ai => ai.Name.Equals("SaltLakeCity.Framework.Alpakabroker.Abstractions", StringComparison.OrdinalIgnoreCase)))
            {
                //context.ReportDiagnostic();
            }
            
            using var sourceGenContext = SourceGeneratorContext<Generator>.Create(context);
            if (context.SyntaxReceiver is not AlpakaEventSyntaxReceiver actorSyntaxReciver) return;

            var alpakaEventModels = new List<AlpakaEventModel>();
            foreach (var @event in actorSyntaxReciver.Events)
            {
                var alpakaEventModel = AlpakaEventModelFactory.From(@event, sourceGenContext.GeneratorExecutionContext.Compilation);
                alpakaEventModels.Add(alpakaEventModel);
                var alpakaEventEmitter = TemplateGenerator.GenerateAlpakaEventEmitter(alpakaEventModel);
                var alpakaEventReceiverBase =
                    TemplateGenerator.GenerateAlpakaEventEventReceiverBase(alpakaEventModel);

                
                context.AddSource(alpakaEventEmitter.FileName, alpakaEventEmitter.SourceCode);
                context.AddSource(alpakaEventReceiverBase.FileName, alpakaEventReceiverBase.SourceCode);
            }
            var assemblyIdentifier =
                TemplateGenerator.GenerateAlpakaAssemblyIdentifier(
                    AlpakaAssemblyIdentifierModel.From(context.Compilation.AssemblyName, alpakaEventModels));
            
            context.AddSource(assemblyIdentifier.FileName, assemblyIdentifier.SourceCode);

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