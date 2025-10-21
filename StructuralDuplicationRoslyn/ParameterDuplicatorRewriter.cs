using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StructuralDuplicationRoslyn
{
    /// <summary>
    /// This class visits every node in the syntax tree.
    /// When it finds a method with one parameter, it returns a new
    /// method node with that parameter duplicated.
    /// </summary>
    public class ParameterDuplicatorRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {

            if (node.ParameterList.Parameters.Count == 1)
            {
                // 1. Get the parameter
                var originalParam = node.ParameterList.Parameters.First();

                // 2. Create the new parameter
                var newParamName = SyntaxFactory.Identifier($"new_{originalParam.Identifier.Text}");

                var newParam = SyntaxFactory.Parameter(newParamName)
                                            .WithType(originalParam.Type)
                                            .WithLeadingTrivia(SyntaxFactory.Space);

                // 3. Create a new parameter list by adding our new param
                var newParamList = node.ParameterList.Parameters.Add(newParam);

                // 4. Create a new ParameterList syntax
                var newParameterListSyntax = node.ParameterList.WithParameters(newParamList);

                // 5. Create a new method node, replacing only the parameter list
                var newMethodNode = node.WithParameterList(newParameterListSyntax);

                return newMethodNode;
            }

            return base.VisitMethodDeclaration(node);
        }
    }
}
