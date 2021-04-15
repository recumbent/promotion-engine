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
            var result = Promotions.TotalAfterPromotions(emptyBasket, emptyPromotions);

            result.Should().Be(0M);
        }

        [Fact]
        public void Basket_with_single_item_and_no_promotions_should_return_unit_cost_of_that_item()
        {
            var itemPrice = 10M;
            var basket = new List<BasketItem>() { new BasketItem("A", 1, itemPrice) };

            var result = Promotions.TotalAfterPromotions(basket, emptyPromotions);

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

            var result = Promotions.TotalAfterPromotions(basket, emptyPromotions);

            result.Should().Be(total);
        }

        [Fact]
        public void SingleMultibuy()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 3, 50M)
            };

            var promotions = new List<Promotion>() { new Promotion("3A for 130", Promotions.MakeMultibuy("A", 3, 130M)) };
            var result = Promotions.TotalAfterPromotions(basket, promotions);

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

            var promotions = new List<Promotion>() { new Promotion("C + D for 30", Promotions.MakePairedBuy("C", "D", 30M)) };
            var result = Promotions.TotalAfterPromotions(basket, promotions);

            result.Should().Be(30);
        }

        // ==============
        // SCENARIO TESTS
        // ==============

        private readonly List<Promotion> scenarioPromotions = new List<Promotion>()
            {
                new Promotion("3A for 130", Promotions.MakeMultibuy("A", 3, 130M)),
                new Promotion("2B for 45", Promotions.MakeMultibuy("B", 2, 45M)),
                new Promotion("C & D for 30", Promotions.MakePairedBuy("C", "D", 30M))
            };

        [Fact]
        public void ScenarioA()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 1, 50),
                new BasketItem("B", 1, 30),
                new BasketItem("C", 1, 20)
            };

            var total = Promotions.TotalAfterPromotions(basket, scenarioPromotions);

            total.Should().Be(100M);
        }

        [Fact]
        public void ScenarioB()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 5, 50),
                new BasketItem("B", 5, 30),
                new BasketItem("C", 1, 20)
            };

            var total = Promotions.TotalAfterPromotions(basket, scenarioPromotions);

            total.Should().Be(370M);
        }

        [Fact]
        public void ScenarioC()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 3, 50),
                new BasketItem("B", 5, 30),
                new BasketItem("C", 1, 20),
                new BasketItem("D", 1, 15)
            };

            var total = Promotions.TotalAfterPromotions(basket, scenarioPromotions);

            total.Should().Be(280M);
        }
    }
}
