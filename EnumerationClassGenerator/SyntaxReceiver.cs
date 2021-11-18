using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace EnumerationClassGenerator
{
    internal class SyntaxReceiver : ISyntaxReceiver
    {
        internal List<EnumDeclarationSyntax> Enums { get; } = new List<EnumDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is EnumDeclarationSyntax enumDeclaration
                && enumDeclaration.AttributeLists.Count > 0)
            {
                Enums.Add(enumDeclaration);
            }
        }
    }
}