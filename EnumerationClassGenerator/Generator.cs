using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace EnumerationClassGenerator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource(nameof(GeneratorConstants.EnumerationClass), SourceText.From(GeneratorConstants.EnumerationClass, Encoding.UTF8));
            context.AddSource(nameof(GeneratorConstants.EnumerationClassAttribute), SourceText.From(GeneratorConstants.EnumerationClassAttribute, Encoding.UTF8));

            if (context.SyntaxReceiver is not SyntaxReceiver receiver)
                return;

            CSharpParseOptions options = (context.Compilation as CSharpCompilation).SyntaxTrees[0].Options as CSharpParseOptions;
            Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(GeneratorConstants.EnumerationClassAttribute, Encoding.UTF8), options));
            INamedTypeSymbol attributeSymbol = compilation.GetTypeByMetadataName($"System.{nameof(GeneratorConstants.EnumerationClassAttribute)}");

            var sources = new StringBuilder();
            var assemblyName = "";
            foreach (var @enum in receiver.Enums)
            {
                SemanticModel model = compilation.GetSemanticModel(@enum.SyntaxTree);
                var enumSymbol = model.GetDeclaredSymbol(@enum);

                var attr = enumSymbol.GetAttributes().FirstOrDefault(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));

                if (attr == null) continue;
                var args = attr?.ConstructorArguments;

                var cls = (string)args.Value[0].Value ?? enumSymbol.Name + "Enumeration";
                var nsp = (string)args.Value[1].Value ?? enumSymbol.ContainingNamespace.ToString();
                assemblyName = enumSymbol.ContainingAssembly.Identity.Name;

                var codeBlock = new StringBuilder();
                codeBlock.AppendLine($"namespace {nsp} {{");

                codeBlock.AppendLine($"\tpublic partial class {cls} : Enumeration {{");

                codeBlock.AppendLine($"\t\tpublic {cls}(int id, string name) : base(id, name) {{}}");

                var members = enumSymbol.GetMembers().Where(x => x.Kind == SymbolKind.Field).Select(y => y.Name);

                var i = 0;
                foreach (var member in members)
                {
                    codeBlock.AppendLine($"\t\tpublic static readonly {cls} {member} = new {cls}({++i}, nameof({member}));");
                }

                codeBlock.AppendLine("\t}");
                codeBlock.AppendLine("}");

                sources.AppendLine(codeBlock.ToString());

            }

            var code = new StringBuilder();
            code.AppendLine("using System;");
            code.AppendLine(sources.ToString());
            var result = code.ToString();
            context.AddSource($"{assemblyName}EnumerationGenerated", SourceText.From(result, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // System.Diagnostics.Debugger.Launch();
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }


    }
}
