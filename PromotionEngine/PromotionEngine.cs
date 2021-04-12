using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Types;

namespace PromotionEngine
{
    public class PromotionEngine
    {
        private decimal Total(List<BasketItem> basket)
        {
            return basket.Sum(item => item.Quantity * item.UnitCost);
        }

        public decimal TotalAfterPromotions(List<BasketItem> basket, List<Promotion> promotions)
        {
            return this.Total(basket);
        }
    }
}
