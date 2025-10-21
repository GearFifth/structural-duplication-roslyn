using System;

public class TestClass
{
    // This method has one parameter, it should be modified.
    public void SayHello(string name)
    {
        Console.WriteLine($"Hello, {name}!");
    }

    // This method has two parameters, it should be ignored.
    public void SayGoodbye(string name, int age)
    {
        Console.WriteLine($"Goodbye, {name} of {age} years.");
    }

    // This method has no parameters, it should be ignored.
    public void DoNothing()
    {

    }

    // This static method has one parameter, it should be modified.
    public static string FormatMessage(string message)
    {
        return $"[{DateTime.Now}] {message}";
    }

    // Test with custom class type, it should be modified.
    private void ProcessUser(User user)
    {
        Console.WriteLine(user.Name);
    }
}

public class User
{
    public string Name { get; set; } = "Default";
}