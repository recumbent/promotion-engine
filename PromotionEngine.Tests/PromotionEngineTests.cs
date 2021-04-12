using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using PromotionEngine.Types;

namespace PromotionEngine.Tests
{
    public class PromotionEngineTests
    {
        private List<BasketItem> emptyBasket = new List<BasketItem>();
        private List<Promotion> emptyPromotions = new List<Promotion>();

        [Fact]
        public void Empty_basket_should_return_total_of_zero()
        {
            var engine = new PromotionEngine();
            var result = engine.TotalAfterPromotions(emptyBasket, emptyPromotions);

            result.Should().Be(0M);
        }

        [Fact]
        public void Basket_with_single_item_and_no_promotions_should_return_unit_cost_of_that_item()
        {
            var itemPrice = 10M;
            var basket = new List<BasketItem>() { new BasketItem("A", 1, itemPrice) };

            var engine = new PromotionEngine();
            var result = engine.TotalAfterPromotions(basket, emptyPromotions);

            result.Should().Be(itemPrice);
        }
    }
}
