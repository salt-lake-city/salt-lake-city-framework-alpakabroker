using System.Diagnostics;
using Microsoft.CodeAnalysis;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is AlpakaEventSyntaxReceiver actorSyntaxReciver)
            {
                foreach (var @event in actorSyntaxReciver.Events)
                {
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
    }
}