using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Extensions;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing
{
    internal static class AlpakaEventModelFactory
    {
        public static AlpakaEventModel From(ClassDeclarationSyntax classSyntax, Compilation compilation)
        {
            var root = classSyntax.GetCompilationUnit();
            var classSemanticModel = compilation.GetSemanticModel(classSyntax.SyntaxTree);
            var classSymbol = classSemanticModel.GetDeclaredSymbol(classSyntax);

            return new AlpakaEventModel()
            {

                ClassName = $"{classSyntax.GetClassName()}ReceiverBase",
                
                Usings = root.GetUsings(),

                Namespace = root.GetNamespace(),
            };
        }
    }
}
