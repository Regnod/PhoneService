namespace MVCPhoneServiceWeb.Utils
{
    public static class Parse
    {
        /// <summary>
        /// Return -1 if conversion was not possible
        /// </summary>
        /// <returns></returns>
        public static int IntTryParse(string s)
        {
            if (!int.TryParse(s,out var ans))
            {
                ans = -1;
            }
            return ans;
        }
        public static float FloatTryParse(string s)
        {
            if (!float.TryParse(s, out var ans))
            {
                ans = -1;
            }
            return ans;
        }
        public static string AdminRole = "Admin";
        public static string VisitorRole = "Admin";
    }
}