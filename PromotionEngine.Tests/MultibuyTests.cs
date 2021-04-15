using System;
using System.Collections.Generic;

using PromotionEngine.Types;

using Xunit;
using FluentAssertions;

namespace PromotionEngine.Tests
{
    public class MultibuyTests
    {
        // Create a multibuy promotion to test
        private Func<List<BasketItem>, (decimal, List<BasketItem>)> Multibuy = Promotions.MakeMultibuy("A", 3, 130M);

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketIsEmpty()
        {
            var basket = new List<BasketItem>();
            var (total, newBasket) = this.Multibuy(basket);

            total.Should().Be(0M);
            newBasket.Should().BeEquivalentTo(basket);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasTwoAs()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("A", 2, 50) };
            var (total, newBasket) = this.Multibuy(basket);

            total.Should().Be(0M);
            newBasket.Should().BeEquivalentTo(basket);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasWrongItemTypes()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("B", 2, 30),
                new BasketItem("C", 3, 20), 
                new BasketItem("D", 4, 15) 
            };
            var (total, newBasket) = this.Multibuy(basket);

            total.Should().Be(0M);
            newBasket.Should().BeEquivalentTo(basket);
        }

        [Fact]
        public void ShouldApplyShouldReturn130IfBasketHasExactly3As()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("A", 3, 50) };
            var (total, newBasket) = this.Multibuy(basket);

            total.Should().Be(130M);
            newBasket.Should().BeEmpty();
        }

        [Fact]
        public void ShouldApplyShouldReturn130fBasketContainsMoreThan3As()
        {
            var basket = new List<BasketItem>()
                { new BasketItem("A", 4, 50) };
            var (total, newBasket) = this.Multibuy(basket);

            var expectedBasket = new List<BasketItem>()
                { new BasketItem("A", 1, 50) };

            total.Should().Be(130M);
            newBasket.Should().BeEquivalentTo(expectedBasket);
        }

        [Fact]
        public void ShouldApplyShouldReturn130IfBasketContainsAtLeast3AsAndDiverseOtherItems()
        { 
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 4, 50),
                new BasketItem("B", 3, 30),
                new BasketItem("C", 2, 20), 
                new BasketItem("D", 1, 15) 
            };
            var (total, newBasket) = this.Multibuy(basket);

            var expectedBasket = new List<BasketItem>()
            {
                new BasketItem("A", 1, 50),
                new BasketItem("B", 3, 30),
                new BasketItem("C", 2, 20), 
                new BasketItem("D", 1, 15) 
            };

            total.Should().Be(130M);
            newBasket.Should().BeEquivalentTo(expectedBasket);
        }

        [Fact]
        public void ShouldApplyAsManyTimesAsNecessary()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("A", 10, 50) };
        
            var (total, newBasket) = this.Multibuy(basket);

            var expectedBasket = new List<BasketItem>()
            { new BasketItem("A", 1, 50) };

            total.Should().Be(390M);
            newBasket.Should().BeEquivalentTo(expectedBasket);
        }
    }
}
