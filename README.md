# Promotion Engine

Coding exercise in .NET / C#

.NET 5.0 class library with tests and a console runner

## Tooling

* [xUnit](https://github.com/xunit/xunit)
* [FluentAssertions](https://fluentassertions.com/)

## Starting...

dotnet new sln
dotnet new console --language C# -n PromotionEngine.Console
dotnet sln add .\PromotionEngine.Console\
dotnet new classlib --language C# --name PromotionEngine
dotnet new xunit --language C# --name PromotionEngine.Tests
dotnet sln add .\PromotionEngine
dotnet sln add .\PromotionEngine.Tests