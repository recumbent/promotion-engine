using System;
using System.Collections.Generic;

using PromotionEngine.Types;

namespace PromotionEngine
{
    public class Multibuy
    {
        public static object AppliedTotal(List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => (bi.Sku == "A" && bi.Quantity >= 3M));
            }

            return ShouldApplyPromotion(basket) ? 130 : 0;
        }
    }
}
