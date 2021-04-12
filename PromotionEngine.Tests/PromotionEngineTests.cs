using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using PromotionEngine.Types;

namespace PromotionEngine.Tests
{
    public class PromotionEngineTests
    {
        private List<BasketItem> emptyBasket = new List<BasketItem>();

        [Fact]
        public void Empty_basket_should_return_total_of_zero()
        {
            var engine = new PromotionEngine();
            var result = engine.TotalAfterPromotions(emptyBasket);

            result.Should().Be(0M);
        }
    }
}
