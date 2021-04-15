using PromotionEngine.Types;
using System;

namespace PromotionEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Promotions using pricing");
            System.Console.WriteLine("--------------------");
            System.Console.WriteLine("Scenario A");
            var pricingA = new PricingService(new GetPrices(), new GetPromotions());
            pricingA.AddItem(new UnpricedItem("A", 1));
            pricingA.AddItem(new UnpricedItem("B", 1));
            pricingA.AddItem(new UnpricedItem("C", 1));

            System.Console.WriteLine($"Basket A:");
            foreach(var item in pricingA.Basket())
            {
                System.Console.WriteLine(item);
            }

            System.Console.WriteLine($"Total  A: {pricingA.BasketTotal()}");

            System.Console.WriteLine("--------------------");

            System.Console.WriteLine("Scenario B");
            var pricingB = new PricingService(new GetPrices(), new GetPromotions());
            pricingB.AddItem(new UnpricedItem("A", 5));
            pricingB.AddItem(new UnpricedItem("B", 5));
            pricingB.AddItem(new UnpricedItem("C", 1));

            System.Console.WriteLine($"Basket B:");
            foreach(var item in pricingB.Basket())
            {
                System.Console.WriteLine(item);
            }

            System.Console.WriteLine($"Total  B: {pricingB.BasketTotal()}");

            System.Console.WriteLine("--------------------");

            System.Console.WriteLine("Scenario B");
            var pricingC = new PricingService(new GetPrices(), new GetPromotions());
            pricingC.AddItem(new UnpricedItem("A", 3));
            pricingC.AddItem(new UnpricedItem("B", 2));
            pricingC.AddItem(new UnpricedItem("C", 1));
            pricingC.AddItem(new UnpricedItem("D", 1));
            pricingC.AddItem(new UnpricedItem("B", 3));

            System.Console.WriteLine($"Basket C:");
            foreach(var item in pricingC.Basket())
            {
                System.Console.WriteLine(item);
            }

            System.Console.WriteLine($"Total  C: {pricingC.BasketTotal()}");

            System.Console.WriteLine("--------------------");
            System.Console.WriteLine("Finis!");
            System.Console.ReadKey();
        }
    }
}
