using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Types;

namespace PromotionEngine
{
    public class Promotions
    {
        public static BasketItem ReduceQuantityForSku(BasketItem item, string sku, int quantity)
        {
            if (item.Sku == sku)
            {
                BasketItem updated = item with { Quantity = item.Quantity - quantity };
                return new BasketItem(item.Sku, item.Quantity - quantity, item.UnitCost);
            }

            return item;
        }

        private static decimal Total(List<BasketItem> basket)
        {
            return basket.Sum(item => item.Quantity * item.UnitCost);
        }

        public static decimal TotalAfterPromotions(List<BasketItem> basket, List<Promotion> promotions)
        {
            var total = 0M;

            foreach(var promo in promotions)
            {
                var (promotionTotal, newBasket) = promo.Applicator(basket);
                total += promotionTotal;
                basket = newBasket;
            }
            
            total += Total(basket);
            return total;
        }

        private static (decimal total, List<BasketItem> basket) Multibuy(string sku, int quantity, decimal discountedPrice, List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => (bi.Sku == sku && bi.Quantity >= quantity));
            }

            var total = 0M;
            while (ShouldApplyPromotion(basket))
            {
                basket = basket.Select(bi => ReduceQuantityForSku(bi, sku, quantity)).Where(bi => bi.Quantity > 0).ToList();
                total += discountedPrice;
            }

            return (total, basket);
        }

        public static Func<List<BasketItem>, (decimal, List<BasketItem>)> MakeMultibuy(string sku, int quantity, decimal discountedPrice)
        {
            return basket => Multibuy(sku, quantity, discountedPrice, basket); 
        }

        private static (decimal total, List<BasketItem> basket) PairedBuy(string sku1, string sku2, decimal discountedPrice, List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => bi.Sku == sku1 && bi.Quantity >= 1) &&
                       basket.Exists(bi => bi.Sku == sku2 && bi.Quantity >= 1);
            }

            var total = 0M;
            while (ShouldApplyPromotion(basket))
            {
                basket = basket
                            .Select(bi => Promotions.ReduceQuantityForSku(bi, sku1, 1))
                            .Select(bi => Promotions.ReduceQuantityForSku(bi, sku2, 1))
                            .Where(bi => bi.Quantity > 0).ToList();
                total += discountedPrice;
            }

            return (total, basket);
        }
        public static Func<List<BasketItem>, (decimal, List<BasketItem>)> MakePairedBuy(string sku1, string sku2, decimal discountedPrice)
        {
            return basket => PairedBuy(sku1, sku2, discountedPrice, basket); 
        }
    }
}
