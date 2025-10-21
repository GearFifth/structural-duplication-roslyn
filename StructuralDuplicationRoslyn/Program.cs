// Program.cs
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace StructuralDuplicationRoslyn
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: Please provide a C# file path as an argument.");
                Console.WriteLine("Usage: dotnet run YourSourceFile.cs");
                return;
            }

            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found: {filePath}");
                return;
            }

            // 1. Read the source code file
            string sourceCode = await File.ReadAllTextAsync(filePath);

            // 2. Parse the code into a syntax tree
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = await tree.GetRootAsync();

            // 3. Apply custom rewriter
            var rewriter = new ParameterDuplicatorRewriter();
            SyntaxNode newRoot = rewriter.Visit(root);

            // 4. Define a new output file path
            string? directory = Path.GetDirectoryName(filePath);
            string effectiveDirectory = directory ?? ".";
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string newFilePath = Path.Combine(effectiveDirectory, $"{fileName}.modified.cs");

            if (newRoot != null)
            {
                // 5. Save the modified source code
                string newSourceCode = newRoot.ToFullString();
                await File.WriteAllTextAsync(newFilePath, newSourceCode);
            }
            else
            {
                await File.WriteAllTextAsync(newFilePath, "// Code analysis resulted in an empty file.");
            }

            Console.WriteLine("Source code analysis and transformation complete.");
            Console.WriteLine($"Modified file saved to: {newFilePath}");
        }
    }
}