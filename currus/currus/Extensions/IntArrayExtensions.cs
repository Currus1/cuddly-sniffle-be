namespace currus.Extensions
{
    public static class Extensions
    {
        public static bool IsLengthLessThanOrEqualTo(this int[] value, int maxValue)
        {
            return value.Count() <= maxValue && value.Count() > 0;
        }
    }
}