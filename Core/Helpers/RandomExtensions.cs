namespace Core.Helpers
{
    /// <summary>
    /// Provides extension methods for generating random long values.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Generates a random long value within the specified range.
        /// </summary>
        /// <param name="random">The <see cref="Random"/> instance used for generating random values.</param>
        /// <param name="min">The inclusive minimum value of the range.</param>
        /// <param name="max">The exclusive maximum value of the range.</param>
        /// <returns>A random long value within the specified range.</returns>
        public static long NextLong(this Random random, long min, long max)
        {
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand % (max - min)) + min;
        }
    }

}
