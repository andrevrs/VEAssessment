using System;

namespace VE.OneTimePassword.Utils
{
    public static class TimeUtil
    {
        //public static DateTime now = DateTime.Now;
        private static Func<DateTime> now = () => DateTime.Now;

        public static DateTime Now
        {
            get
            {
                return now();
            }

            set
            {
                now = () => value;
            }
        }

        public static void ResetToNow()
        {
            now = () => DateTime.Now;
        }

        public static void AddSeconds(int seconds)
        {
            Now = Now.AddSeconds(seconds);
        }
    }
}
