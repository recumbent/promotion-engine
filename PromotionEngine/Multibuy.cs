using System.Collections.Generic;

using PromotionEngine.Types;

namespace PromotionEngine
{
    public class Multibuy
    {
        public static bool ShouldApplyPromotion(List<BasketItem> basket)
        {
            return basket.Exists(bi => (bi.Sku == "A" && bi.Quantity >= 3M));
        }
    }
}
