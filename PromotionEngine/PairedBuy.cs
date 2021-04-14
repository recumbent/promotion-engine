﻿using System.Collections.Generic;
using PromotionEngine.Types;

namespace PromotionEngine
{
    public class PairedBuy
    {
        public static bool ShouldApplyPromotion(List<BasketItem> basket)
        {
            return basket.Exists(bi => bi.Sku == "C" && bi.Quantity >= 1) &&
                   basket.Exists(bi => bi.Sku == "D" && bi.Quantity >= 1);
        }
    }
}
