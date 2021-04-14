using System;
using System.Collections.Generic;

using PromotionEngine.Types;

using Xunit;
using FluentAssertions;

namespace PromotionEngine.Tests
{
    public class PairedBuyTests
    {
        // Assume 3 of A's for 130


        // TODO: Ideally this would be a Theory (data driven) test for "don't apply" cases, need to see where we go first
        [Fact]
        public void ShouldApplyShouldReturnFalseIfBasketIsEmpty()
        {
            var basket = new List<BasketItem>();
            var result = PairedBuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldApplyShouldReturnFalseIfBasketHasCButNotD()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("C", 1, 20) };
            var result = PairedBuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldApplyShouldReturnFalseIfBasketHasDButNotC()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("D", 1, 15) };
            var result = PairedBuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldApplyShouldReturnFalseIfBasketHasWrongItemTypes()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 1, 50),
                new BasketItem("B", 2, 30) 
            };
            var result = PairedBuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }

        // Arguably we should never have a zero quantity, but its would be appropriate to test both cases
        [Fact]
        public void ShouldApplyShouldReturnFalseIfBasketItemQuantityIsZero()
        {
            var basket1 = new List<BasketItem>()
            {
                new BasketItem("C", 0, 20),
                new BasketItem("D", 1, 15) 
            };
            var result1 = PairedBuy.ShouldApplyPromotion(basket1);

            result1.Should().BeFalse();

            var basket2 = new List<BasketItem>()
            {
                new BasketItem("C", 1, 20),
                new BasketItem("D", 0, 15) 
            };
            var result2 = PairedBuy.ShouldApplyPromotion(basket2);

            result2.Should().BeFalse();
        }

        [Fact]
        public void ShouldApplyShouldReturnTrueIfBasketHasExactly1Cand1D()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("C", 1, 20),
                new BasketItem("D", 1, 15)
            };

            var result = PairedBuy.ShouldApplyPromotion(basket);

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldApplyShouldReturnTrueIfBasketContains2And1()
        {
            var basket = new List<BasketItem>()
            { 
                new BasketItem("C", 2, 20),
                new BasketItem("D", 1, 15) 
            };
            var result = PairedBuy.ShouldApplyPromotion(basket);

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldApplyShouldReturnTrueIfBasketContainsAtLeast1ofEachAndDiverseOtherItems()
        { 
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 1, 50),
                new BasketItem("B", 2, 30),
                new BasketItem("C", 3, 20), 
                new BasketItem("D", 4, 15) 
            };
            var result = PairedBuy.ShouldApplyPromotion(basket);

            result.Should().BeTrue();
        }
    }
}
