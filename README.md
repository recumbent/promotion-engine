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

## Stuck

At this point I can detect if I should apply and I can return the amount for a single pass of the promotion.

The question here is how to progress to the next step - i.e. _what_ is the next step.

So another leap, I want to get the value of applied promotion and then to remove the items from the basket - which means I want to return a tuple (appliedTotal, remainingBasket)

That is my promotion function maps from a basket, to a total and a basket. If the promotion does not apply the total is zero and the basket is unchanged.

## Tidying

There is common code between the functions, I've attached that to one of the classes to share. I can go futher though (later) in that we have single named functions to do the job so we only need a single class.

## Close

One more step for the functions, currently they just apply once, but we need to apply the promotion as many times as its valid.

To do that we can extend the function to apply as many times as necessary, summing the promoted price as we go.
