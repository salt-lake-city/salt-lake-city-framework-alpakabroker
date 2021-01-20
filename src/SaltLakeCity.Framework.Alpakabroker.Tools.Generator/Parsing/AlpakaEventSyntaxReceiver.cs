using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SaltLakeCity.Framework.Alpakabroker.Abstractions;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Extensions;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing
{
    internal class AlpakaEventSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> Events { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classSyntax && classSyntax.HaveAttribute(AlpakaEventAttribute.Name))
            {
                Events.Add(classSyntax);
            }
        }
    }
}
