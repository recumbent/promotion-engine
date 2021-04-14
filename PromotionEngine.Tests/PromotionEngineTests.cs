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

        private List<BasketItem> testBasket = new List<BasketItem>()
        {
            new BasketItem("A", 5, 50M),
            new BasketItem("B", 5, 30M),
            new BasketItem("C", 2, 20M),
            new BasketItem("D", 1, 15M)
        };

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

        [Fact]
        public void Complex_basket_with_no_promotins_returns_total()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 2, 20M),
                new BasketItem("B", 3, 30M),
                new BasketItem("C", 4, 40M)
            };

            var total = 2 * 20M + 3 * 30M + 4 * 40M;

            var engine = new PromotionEngine();
            var result = engine.TotalAfterPromotions(basket, emptyPromotions);

            result.Should().Be(total);
        }

        [Fact]
        public void SingleMultibuy()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 3, 50M)
            };

            var promotions = new List<Promotion>() { new Promotion("3A for 130", Promotions.Multibuy) };
            var engine = new PromotionEngine();
            var result = engine.TotalAfterPromotions(basket, promotions);

            result.Should().Be(130);
        }

        [Fact]
        public void SinglePairedBuy()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("C", 1, 20M),
                new BasketItem("D", 1, 15M)
            };

            var promotions = new List<Promotion>() { new Promotion("C + D for 30", Promotions.PairedBuy) };
            var engine = new PromotionEngine();
            var result = engine.TotalAfterPromotions(basket, promotions);

            result.Should().Be(30);
        }
    }
}
