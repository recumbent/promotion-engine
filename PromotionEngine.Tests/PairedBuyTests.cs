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
            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            total.Should().Be(0M);
            newBasket.Should().BeEquivalentTo(basket);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasCButNotD()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("C", 1, 20) };
            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            total.Should().Be(0M);
            newBasket.Should().BeEquivalentTo(basket);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasDButNotC()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("D", 1, 15) };
            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            total.Should().Be(0M);
            newBasket.Should().BeEquivalentTo(basket);
        }

        [Fact]
        public void ShouldApplyShouldReturnZeroIfBasketHasWrongItemTypes()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 1, 50),
                new BasketItem("B", 2, 30) 
            };
            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            total.Should().Be(0M);
            newBasket.Should().BeEquivalentTo(basket);
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
            var (total1, newBasket1) = PairedBuy.AppliedTotal(basket1);

            total1.Should().Be(0M);
            newBasket1.Should().BeEquivalentTo(basket1);

            var basket2 = new List<BasketItem>()
            {
                new BasketItem("C", 1, 20),
                new BasketItem("D", 0, 15) 
            };
            var (total2, newBasket2) = PairedBuy.AppliedTotal(basket2);

            total2.Should().Be(0M);
            newBasket2.Should().BeEquivalentTo(basket2);
        }

        [Fact]
        public void ShouldApplyShouldReturn30IfBasketHasExactly1Cand1D()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("C", 1, 20),
                new BasketItem("D", 1, 15)
            };

            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            total.Should().Be(30M);
            newBasket.Should().BeEmpty();
        }

        [Fact]
        public void ShouldApplyShouldReturn30IfBasketContains2And1()
        {
            var basket = new List<BasketItem>()
            { 
                new BasketItem("C", 2, 20),
                new BasketItem("D", 1, 15) 
            };
            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            var expectedBasket = new List<BasketItem>()
            { 
                new BasketItem("C", 1, 20),
            };
            total.Should().Be(30M);
            newBasket.Should().BeEquivalentTo(expectedBasket);
        }

        [Fact]
        public void ShouldApplyShouldReturn30IfBasketContainsAtLeast1ofEachAndDiverseOtherItems()
        { 
            var basket = new List<BasketItem>()
            {
                new BasketItem("A", 3, 50),
                new BasketItem("B", 2, 30),
                new BasketItem("C", 1, 20), 
                new BasketItem("D", 4, 15) 
            };
            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            var expectedBasket = new List<BasketItem>()
            {
                new BasketItem("A", 3, 50),
                new BasketItem("B", 2, 30),
                new BasketItem("D", 3, 15) 
            };
            total.Should().Be(30M);
            newBasket.Should().BeEquivalentTo(expectedBasket);
        }

        [Fact]
        public void ShouldApplyAsManyTimesAsNeccessary()
        {
            var basket = new List<BasketItem>()
            { 
                new BasketItem("C", 4, 20),
                new BasketItem("D", 3, 15) 
            };
            var (total, newBasket) = PairedBuy.AppliedTotal(basket);

            var expectedBasket = new List<BasketItem>()
            { 
                new BasketItem("C", 1, 20),
            };
            total.Should().Be(90M);
            newBasket.Should().BeEquivalentTo(expectedBasket);
        }
    }
}
