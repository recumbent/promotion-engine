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
            var total = 0M;

            foreach(var promo in promotions)
            {
                var (promotionTotal, newBasket) = promo.Applicator(basket);
                total += promotionTotal;
                basket = newBasket;
            }
            
            total += this.Total(basket);
            return total;
        }
    }
}
