namespace MVCPhoneServiceWeb.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Return -1 if conversion was not possible
        /// </summary>
        /// <returns></returns>
        public static int IntTryParse(string s)
        {
            int ans;
            if (!int.TryParse(s,out ans))
            {
                ans = -1;
            }
            return ans;
        }
        public static string AdminRole = "Admin";
        public static string VisitorRole = "Admin";

    }
}