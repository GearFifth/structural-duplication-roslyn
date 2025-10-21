# Roslyn SDK - Structural Duplication Task

This project is a small C# console application built for **Task #3** of the JetBrains internship application. It uses the **Microsoft.CodeAnalysis (Roslyn) SDK** to analyze and transform C# source code.

## Project Goal

The program accepts a C# source file as input, finds all method declarations that have **exactly one parameter**, duplicates that parameter, suggests a new name based on the original, and saves the transformed code to a new file.
## Features

* **Roslyn Syntax Parsing**: Parses C# source code into an immutable syntax tree.
* **Structural Transformation**: Uses a `CSharpSyntaxRewriter` to traverse and modify the syntax tree.
* **Intelligent Duplication**:
    * Detects methods with a single parameter.
    * Duplicates the parameter while preserving its type.
    * Generates a suggested name using the format `new_{original_parameter_name}`.
- **Safe File Output** – Saves the modified code to `{original_name}.modified.cs` without changing the original.

## Requirements

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (or newer)
* The `Microsoft.CodeAnalysis.CSharp` NuGet package

## How to Run

1.  **Clone or download** this repository.
2.  **Install dependencies** by running `dotnet restore` in the project's root directory.
3.  **Create an input file** or use the predefined one (`Input.cs`) in the project's root directory:
4.  **Run the application**, passing your input file as an argument:

    ```bash
    dotnet run Input.cs
    ```

5.  **Check the output**. The program will create a new file named `Input.modified.cs` in the same directory.

## Example: Before vs. After

#### `Input.cs` (Before)

```csharp
public class MyClass
{
    // This method has one parameter and will be modified.
    public void Greet(string name)
    {
        Console.WriteLine($"Hello, {name}!");
    }

    // This method has two parameters and will be ignored.
    public void Add(int a, int b)
    {
        Console.WriteLine(a + b);
    }
}
```

#### `Input.modified.cs` (After)
```csharp
public class MyClass
{
    // This method has one parameter and will be modified.
    public void Greet(string name, string new_name)
    {
        Console.WriteLine($"Hello, {name}!");
    }

    // This method has two parameters and will be ignored.
    public void Add(int a, int b)
    {
        Console.WriteLine(a + b);
    }
}
```
