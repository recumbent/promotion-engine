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

        [Fact]
        public void ShouldApplShouldReturnFalseIfBasketIsEmpty()
        {
            var basket = new List<BasketItem>();
            var result = Multibuy.ShouldApplyPromotion(basket);

            result.Should().BeFalse();
        }
    }
}
