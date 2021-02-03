using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Templates;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Tools;
using Scriban;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing
{
    internal static class TemplateGenerator
    {
        private static string _alpakaEventReceiverBaseTemplate;
        private static string _alpakaEventEmitterTemplate;
        private static string _alpakaAssemblyIdentifierTemplate;

        static TemplateGenerator()
        {
            _alpakaEventReceiverBaseTemplate = ResourceReader.GetResource("AlpakaEventReceiverBase.sbncs");
            _alpakaEventEmitterTemplate = ResourceReader.GetResource("AlpakaEventEmitter.sbncs");
            _alpakaAssemblyIdentifierTemplate = ResourceReader.GetResource("AlpakaAssemblyIdentifier.sbncs");
        }

        public static GeneratedSource GenerateAlpakaEventEmitter(AlpakaEventModel alpakaEventModel) =>
            new(Execute(_alpakaEventEmitterTemplate, AlpakaEventEmitterModel.From(alpakaEventModel)),
                $"{alpakaEventModel.Namespace}.{alpakaEventModel.EventName}EventEmitter");
        
        public static GeneratedSource GenerateAlpakaEventEventReceiverBase(AlpakaEventModel alpakaEventModel) =>
            new(Execute(_alpakaEventReceiverBaseTemplate, AlpakaEventReceiverBaseModel.From(alpakaEventModel)),
                $"{alpakaEventModel.Namespace}.{alpakaEventModel.EventName}EventReceiverBase");


        private static string Execute(string templateString, object model)
        {
            var template = Template.Parse(templateString);

            if (template.HasErrors)
            {
                var errors = string.Join(" | ", template.Messages.Select(x => x.Message));
                throw new InvalidOperationException($"Template parse error: {errors}");
            }

            var result = template.Render(model, member => member.Name);

            result = SyntaxFactory.ParseCompilationUnit(result)
                .NormalizeWhitespace()
                .GetText()
                .ToString();

            return result;
        }
    }
}