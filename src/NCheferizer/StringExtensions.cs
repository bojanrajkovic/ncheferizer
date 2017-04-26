namespace NCheferizer
{
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
    }
}
