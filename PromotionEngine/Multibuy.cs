using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Types;

namespace PromotionEngine
{
    public class Multibuy
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

        public static (decimal total, List<BasketItem> basket) AppliedTotal(List<BasketItem> basket)
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
    }
}
