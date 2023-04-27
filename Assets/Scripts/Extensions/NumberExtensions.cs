namespace Extensions
{
    /// <summary>
    /// Static class for add methods to different number (float, int, etc) types
    /// </summary>
    public static class NumberExtensions
    {

        /// <summary>
        /// Check to see if a float have a decimal value or not
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool HasDecimalValue(this float number)
        {
            return (number % 1) != 0;
        }
    }
}