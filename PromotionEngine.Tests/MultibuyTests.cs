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
        public void ShouldApplyShouldReturnFalseIfBasketIsEmpty()
        {
            var basket = new List<BasketItem>();
            var result = Multibuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldApplyShouldReturnFalseIfBasketHasTwoAs()
        {
            var basket = new List<BasketItem>()
            { new BasketItem("A", 2, 50) };
            var result = Multibuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldApplyShouldReturnFalseIfBasketHasWrongItemTypes()
        {
            var basket = new List<BasketItem>()
            {
                new BasketItem("B", 2, 30),
                new BasketItem("C", 3, 20), 
                new BasketItem("D", 4, 15) 
            };
            var result = Multibuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }
    }
}
