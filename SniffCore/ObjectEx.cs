namespace SniffCore
{
    public static class ObjectEx
    {
        public static bool IsNullOrEmpty(this object element)
        {
            if (element == null)
                return true;
            return string.IsNullOrEmpty(element.ToString());
        }

        public static bool IsNullOrWhiteSpace(this object element)
        {
            if (element == null)
                return true;
            return string.IsNullOrWhiteSpace(element.ToString());
        }
    }
}