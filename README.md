# Promotion Engine

Coding exercise in .NET / C#

.NET 5.0 class library with tests and a console runner

## Notes

Running the console application will demonstrate the 3 scenarios in an example that its slightly beyond the scope of the exercise.

The reason for the extended example is that the solution to the stated problem is very functional - the problem maps neatly to a functional solution. The `PricingService` is a more OO example, tending towards something that might exists as a serverless function.

The scenarios are also covered by the last 3 tests in PromotionEngineTests.cs.

### Of tests

The tests, at this point, are more indicative than complete. I would use xUnit theory tests to run through a more comprehensive set of examples to better check and to look for edge cases. I would also usually use values other than those for the scenarios in general testing

If you go back through the commits there are some interesting points where I've let the tooling do its thing and not picked up on issues - missing the `M` from decimals for example and returning an obj rather than the "right" values for a while. The tests always pass though...

### Issues

Using functions addresses the specific problem neatly, and the system as implemented would allow for easy configuration - one could parse json definining a list of promotions into a list of `Promotions` without too much effort. However this just calculates totals, there's no breakdown of how we get to the total, no list of promotions applied. The functions could be extended to provide that as part of the return value.

In applying promotions I've used a loop and mutation where I might use a fold (a Linq Aggregate) the foreach loop is easier to read and being able to read the code is important

Fun, but I spent too long...

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

## Putting it all together

I've got functions that apply a single promotion, so can I apply a list of promotions to a basket?

First I'll tidy up a bit, fewer files needed to achieve the same result.

## Totals

At this point we can define a "promotion" as a `Func<List<BasketItem>,(decimal,List<BasketItem>)>` and good things should happen.

...

And indeed they do, I have a working solution to the problem (almost, I haven't yet address the second multibuy case).

## Extensibility and utility

The solution I have - a function per promotion - is easily extensible in that there is a clear pattern to add new promotions, i.e. write a new function - a simple pattern, easily tested.

But practically we don't want to have to write a function for 3 x A = 130 and then another for 2 x B = 45.

To solve this I could introduce classes (one per promotion type) as follows (include as `PromotionAsClass.cs`):

```csharp
    public interface IPromotion
    {
        (decimal total, List<BasketItem> basket) ApplyPromotion(List<BasketItem> basket);
    }

    public class MultiExample : IPromotion
    {
        private readonly string sku;
        private readonly int quantityRequired;
        private readonly decimal discountedCost;

        public MultiExample(string sku, int quantityRequired, decimal discountedCost)
        {
            this.sku = sku;
            this.quantityRequired = quantityRequired;
            this.discountedCost = discountedCost;
        }

        public (decimal total, List<BasketItem> basket) ApplyPromotion(List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => (bi.Sku == this.sku && bi.Quantity >= this.quantityRequired));
            }

            var total = 0M;
            while (ShouldApplyPromotion(basket))
            {
                basket = basket.Select(bi => Promotions.ReduceQuantityForSku(bi, this.sku, this.quantityRequired)).Where(bi => bi.Quantity > 0).ToList();
                total += this.discountedCost;
            }

            return (total, basket);
        }
    }
```

The interface isn't strictly necessary, but it helps define the requirement.

Then it could be used as follows:

```csharp
   var promoA = new MultiExample("A", 3, 130);
   var promoB = new MultiExample("B", 2, 45);

   var promotions = new List<Promotion>()
   {
       new Promotion("3 of A for 130", promoA.ApplyPromotion),
       new Promotion("2 of b for 45", promoA.ApplyPromotion)
   }
```

But that's still more than is needed to solve the problem as described - what's needed here is a function that returns a function which C# lets us do with lambdas

So make the functions private and add `MakePromotion` functions to the promotions class - that gives us everything needed to write the scenario tests in a neat, concise fashion.

## Why do we instantiate the promotion engine

At this point there's no need for a promotion engine at all given the implementation, so take it out.

## Show me the objects...

The code now solves the problem in a neat, concise, fashion - but its very functional (its a functional problem), but there is also a requirement to show OO code 🤔

Lets assume we need a pricing service, and build something that has dependencies. I'm going to do this without tests first as its outside the original scope, but it will use interfaces to allow for testing etc.
