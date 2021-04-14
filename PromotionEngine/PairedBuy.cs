using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Types;

namespace PromotionEngine
{
    public class PairedBuy
    {
        public static (decimal total, List<BasketItem> basket) AppliedTotal(List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => bi.Sku == "C" && bi.Quantity >= 1) &&
                       basket.Exists(bi => bi.Sku == "D" && bi.Quantity >= 1);
            }

            if (ShouldApplyPromotion(basket))
            {
                var reducedBasket = basket
                                        .Select(bi => Multibuy.ReduceQuantityForSku(bi, "C", 1))
                                        .Select(bi => Multibuy.ReduceQuantityForSku(bi, "D", 1))
                                        .Where(bi => bi.Quantity > 0).ToList();
                return (30, reducedBasket);
            }

            return (0, basket);
        }
    }
}
