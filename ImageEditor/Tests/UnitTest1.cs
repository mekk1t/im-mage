using System;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Cast_as_float_allows_for_a_precision_level_calculcation()
        {
            var input = 153;
            var threshold = 100;

            var result = (float)input / (float)threshold;

            Assert.Equal("1.53", Math.Round(result, 2).ToString());
        }
    }
}
