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

## Design leap

A promotion is defined by a function - `Func<List<BasketItem>,TBA>`

It's a leap because I don't quite see how to test may way there.

But that means that I can start by testing a function `bool ShouldApplyPromotion(List<BasketItem> basket)` for the first promotion type "multibuy"

When that works, I can repeat by testing a function for the second promotion type "PairedBuy" for want of a better term.

## Progressing from can I apply?

If the promotion can be applied then we care about the new price for those items, so we want a function that returns if its been applied and the total for the promoted items. First pass is just return the single promotion value instead of true/false
