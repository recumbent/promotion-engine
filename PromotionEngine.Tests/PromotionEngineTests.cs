using System;
using Xunit;

namespace PromotionEngine.Tests
{
    public class PromotionEngineTests
    {
        [Fact]
        public void Should_be_able_call_TotalAfterPromotions()
        {
            var engine = new PromotionEngine();
            engine.TotalAfterPromotions();
        }
    }
}
