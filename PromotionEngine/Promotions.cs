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

        public static (decimal total, List<BasketItem> basket) Multibuy(List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => (bi.Sku == "A" && bi.Quantity >= 3M));
            }

            var total = 0M;
            while (ShouldApplyPromotion(basket))
            {
                basket = basket.Select(bi => ReduceQuantityForSku(bi, "A", 3)).Where(bi => bi.Quantity > 0).ToList();
                total += 130;
            }

            return (total, basket);
        }

        public static (decimal total, List<BasketItem> basket) PairedBuy(List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => bi.Sku == "C" && bi.Quantity >= 1) &&
                       basket.Exists(bi => bi.Sku == "D" && bi.Quantity >= 1);
            }

            var total = 0;
            while (ShouldApplyPromotion(basket))
            {
                basket = basket
                            .Select(bi => Promotions.ReduceQuantityForSku(bi, "C", 1))
                            .Select(bi => Promotions.ReduceQuantityForSku(bi, "D", 1))
                            .Where(bi => bi.Quantity > 0).ToList();
                total += 30;
            }

            return (total, basket);
        }
    }
}
