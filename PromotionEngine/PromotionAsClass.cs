using PromotionEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public interface IPromotion
    {
        (decimal total, List<BasketItem> basket) ApplyPromotion(List<BasketItem> basket);
    }

    public class MultiExample : IPromotion
    {
        private readonly string sku;
        private readonly int quantityRequired;
        private readonly decimal discountedCost;

        public MultiExample(string sku, int quantityRequired, decimal discountedCost)
        {
            this.sku = sku;
            this.quantityRequired = quantityRequired;
            this.discountedCost = discountedCost;
        }

        public (decimal total, List<BasketItem> basket) ApplyPromotion(List<BasketItem> basket)
        {
            bool ShouldApplyPromotion(List<BasketItem> basket)
            {
                return basket.Exists(bi => (bi.Sku == this.sku && bi.Quantity >= this.quantityRequired));
            }

            var total = 0M;
            while (ShouldApplyPromotion(basket))
            {
                basket = basket.Select(bi => Promotions.ReduceQuantityForSku(bi, this.sku, this.quantityRequired)).Where(bi => bi.Quantity > 0).ToList();
                total += this.discountedCost;
            }

            return (total, basket);
        }
    }
}
