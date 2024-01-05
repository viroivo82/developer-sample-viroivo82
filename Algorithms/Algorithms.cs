using System;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Input must be a non-negative integer.");
            }
            else if (n == 0 || n == 1)
            {
                return 1;
            }
            else
            {
                int result = 1;
                for (int i = 2; i <= n; i++)
                {
                    result *= i;
                }
                return result;
            }
        }

        public static string FormatSeparators(params string[] items)
        {
            if (items == null || items.Length == 0)
            {
                return string.Empty;
            }
            else if (items.Length == 1)
            {
                return items[0];
            }
            else
            {
                return string.Join(", ", items, 0, items.Length - 1) + " and " + items[items.Length - 1];
            }
        }
    }
}