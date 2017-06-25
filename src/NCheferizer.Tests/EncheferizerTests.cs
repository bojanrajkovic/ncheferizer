using System;

using Xunit;

namespace NCheferizer.Tests
{
    public class EncheferizerTests
    {
        [Theory]
        [InlineData("Hello, world! #blessed", "Hellu, vurld! #blessed")]
        [InlineData("What are you looking at, @horse_js?", "Vhet ere-a yuoo luukeeng et, @horse_js?")]
        public void Skip_rules_skip_words(string input, string expected)
        {
            var skipRules = new Func<string, bool>[] {
                word => word[0] == '#',
                word => word[0] == '@',
            };

            var output = input.Encheferize(false, skipRules);

            Assert.Equal(expected, output);
        }
    }
}
