using System;

using JetBrains.Annotations;

namespace NCheferizer
{
    [PublicAPI]
    public static class StringExtensions
    {
        /// <summary>
        /// Encheferize the input string.
        /// </summary>
        /// <param name="input">The string to encheferize.</param>
        /// <param name="shouldBork">Should I bork?</param>
        /// <returns>The encheferized string.</returns>
        /// <remarks>This method is thread-safe.</remarks>
        public static string Encheferize(this string input, bool shouldBork)
            => Encheferizer.Instance.Encheferize(input, shouldBork);

        /// <summary>
        /// Encheferize the input string.
        /// </summary>
        /// <param name="input">The string to encheferize.</param>
        /// <param name="shouldBork">Should I bork?</param>
        /// <param name="skipRules">An array of rules by which to skip words.</param>
        /// <returns>The encheferized string.</returns>
        /// <remarks>This method is thread-safe.</remarks>
        public static string Encheferize(this string input, bool shouldBork, params Func<string, bool>[] skipRules)
            => Encheferizer.Instance.Encheferize(input, shouldBork, skipRules);
    }
}
