using System;
using System.Collections.Generic;

using PromotionEngine.Types;

using Xunit;
using FluentAssertions;

namespace PromotionEngine.Tests
{
    public class MultibuyTests
    {
        // Assume 3 of A's for 130


        // TODO: Ideally this would be a Theory (data driven) test for "don't apply" cases, need to see where we go first
        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketIsEmpty()
        {
            var basket = new List<BasketItem>();
            var result = Multibuy.AppliedTotal(basket);

            result.Should().Be(0M);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasTwoAs()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("A", 2, 50) };
            var result = Multibuy.AppliedTotal(basket);

            result.Should().Be(0M);
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
            var result = Multibuy.AppliedTotal(basket);

            result.Should().Be(0M);
        }

        [Fact]
        public void ShouldApplyShouldReturn130IfBasketHasExactly3As()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("A", 3, 50) };
            var result = Multibuy.AppliedTotal(basket);

            result.Should().Be(130M);
        }

        [Fact]
        public void ShouldApplyShouldReturn130fBasketContainsMoreThan3As()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("A", 4, 50) };
            var result = Multibuy.AppliedTotal(basket);

            result.Should().Be(130M);
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
            var result = Multibuy.AppliedTotal(basket);

            result.Should().Be(130M);
        }
    }
}
