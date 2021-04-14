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
        public void ShouldApplyShouldReturnZeroIfBasketIsEmpty()
        {
            var basket = new List<BasketItem>();
            var result = PairedBuy.AppliedTotal(basket);

            result.Should().Be(0M);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasCButNotD()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("C", 1, 20) };
            var result = PairedBuy.AppliedTotal(basket);

            result.Should().Be(0M);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasDButNotC()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("D", 1, 15) };
            var result = PairedBuy.AppliedTotal(basket);

            result.Should().Be(0M);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasWrongItemTypes()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 1, 50),
                new BasketItem("B", 2, 30) 
            };
            var result = PairedBuy.AppliedTotal(basket);

            result.Should().Be(0M);
        }

        // Arguably we should never have a zero quantity, but its would be appropriate to test both cases
        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketItemQuantityIsZero()
        {
            var basket1 = new List<BasketItem>()
            {
                new BasketItem("C", 0, 20),
                new BasketItem("D", 1, 15) 
            };
            var result1 = PairedBuy.AppliedTotal(basket1);

            result1.Should().Be(0M);

            var basket2 = new List<BasketItem>()
            {
                new BasketItem("C", 1, 20),
                new BasketItem("D", 0, 15) 
            };
            var result2 = PairedBuy.AppliedTotal(basket2);

            result2.Should().Be(0M);
        }

        [Fact]
        public void ShouldApplyShouldReturn30IfBasketHasExactly1Cand1D()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("C", 1, 20),
                new BasketItem("D", 1, 15)
            };

            var result = PairedBuy.AppliedTotal(basket);

            result.Should().Be(30M);
        }

        [Fact]
        public void ShouldApplyShouldReturn30IfBasketContains2And1()
        {
            var basket = new List<BasketItem>()
            { 
                new BasketItem("C", 2, 20),
                new BasketItem("D", 1, 15) 
            };
            var result = PairedBuy.AppliedTotal(basket);

            result.Should().Be(30M);
        }

        [Fact]
        public void ShouldApplyShouldReturn30IfBasketContainsAtLeast1ofEachAndDiverseOtherItems()
        { 
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 1, 50),
                new BasketItem("B", 2, 30),
                new BasketItem("C", 3, 20), 
                new BasketItem("D", 4, 15) 
            };
            var result = PairedBuy.AppliedTotal(basket);

            result.Should().Be(30M);
        }
    }
}
