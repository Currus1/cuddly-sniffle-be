namespace currus.Models
{
    public static class Extensions
    {
        public static bool IsMoreThanZero(this int[] value, int maxValue)
        {
            return value.Count() <= maxValue && value.Count() > 0;
        }
    }
}