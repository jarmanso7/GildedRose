using System.Runtime.CompilerServices;

namespace GildedRose.Core.Helper
{
    public static class IntExtensions
    {
        public static int AddUpToMax(this int a, int addend, int max)
        {
            return a + addend > max ? max : a + addend;
        }

        public static int SubstractDownToMin(this int a, int subtrahend, int min)
        {
            return a - subtrahend < min ? min : a - subtrahend;
        }
    }
}