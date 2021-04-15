using PromotionEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Console
{
    public interface IGetPrice
    {
        decimal PriceFor(string sku);
    }

    public interface IGetPromotions
    {
        List<Promotion> FetchPromotions();
    }

    public class GetPrices : IGetPrice
    {
        public decimal PriceFor(string sku)
        {
            return sku switch
            {
                "A" => 50,
                "B" => 30,
                "C" => 20,
                "D" => 15,
                _   => throw new ArgumentOutOfRangeException($"Unknown SKU: {sku}")
            };
        }
    }

    public class GetPromotions : IGetPromotions
    {
        public List<Promotion> FetchPromotions()
        {
            return new List<Promotion>()
            {
                new Promotion("3A for 130", Promotions.MakeMultibuy("A", 3, 130M)),
                new Promotion("2B for 45", Promotions.MakeMultibuy("B", 2, 45M)),
                new Promotion("C & D for 30", Promotions.MakePairedBuy("C", "D", 30M))
            };
        }
    }

    public class PricingService
    {
        private readonly IGetPrice pricing;
        private readonly IGetPromotions promotions;
        private readonly Dictionary<string, BasketItem> basket;

        public PricingService(IGetPrice pricing, IGetPromotions promotions)
        {
            this.pricing = pricing;
            this.promotions = promotions;
            this.basket = new Dictionary<string, BasketItem>();
        }

        public void AddItem(UnpricedItem item)
        {
            if (basket.ContainsKey(item.Sku))
            {
                var basketItem = basket[item.Sku];
                basket[item.Sku] = basketItem with { Quantity = basketItem.Quantity + item.Quantity };
            }
            else
            {
                var price = pricing.PriceFor(item.Sku);
                basket.Add(item.Sku, new BasketItem(item.Sku, item.Quantity, price));
            }
        }

        public List<BasketItem> Basket()
        {
            return this.basket.Values.ToList();
        }

        public decimal BasketTotal()
        {
            var promosToApply = this.promotions.FetchPromotions();
            return Promotions.TotalAfterPromotions(this.Basket(), promosToApply);
        }
    }
}
