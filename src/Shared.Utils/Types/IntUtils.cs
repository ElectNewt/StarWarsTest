namespace Shared.Utils.Types
{
    public static class IntUtils
    {
        public static bool IsInt(string obj)
        {
            int value;
            if (int.TryParse(obj, out value))
                return true;

            return false;
        }

        public static bool IsInt64(string obj)
        {
            long value;
            if (long.TryParse(obj, out value))
                return true;

            return false;
        }

    }
}
