using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Types;

namespace PromotionEngine
{
    public class Multibuy
    {
        public static (decimal total, List<BasketItem> basket) AppliedTotal(List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => (bi.Sku == "A" && bi.Quantity >= 3M));
            }

            BasketItem ReduceQuantityForSku(BasketItem item, string sku, int quantity)
            {
                if (item.Sku == sku)
                {
                    BasketItem updated = item with { Quantity = item.Quantity - quantity };
                    return new BasketItem(item.Sku, item.Quantity - quantity, item.UnitCost);
                }

                return item;
            }

            if (ShouldApplyPromotion(basket))
            {
                var reducedBasket = basket.Select(bi => ReduceQuantityForSku(bi, "A", 3)).Where(bi => bi.Quantity > 0).ToList();
                return (130, reducedBasket);
            }

            return (0, basket);
        }
    }
}
