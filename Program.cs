using AuthConsole;
using Microsoft.Extensions.DependencyInjection;

var host = HostBuilder.BuildHost(args);

var userService = ActivatorUtilities.CreateInstance<UserService>(host.Services);

string? email = null;
while (email is null)
{
    Console.WriteLine("Email:");
    email = Console.ReadLine();
}
string? password = null;
while (password is null)
{
    Console.WriteLine("Passwort:");
    password = Console.ReadLine();
}

var success = await userService.CreateUser(email, password);
string resultMessage = success ? "Erfolg!" : "Fehlgeschlagen!";
Console.WriteLine(resultMessage);
Console.ReadLine();

