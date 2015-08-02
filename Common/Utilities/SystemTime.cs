namespace Common.Utilities
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The system time class to use instead of DateTime to get/set time.
    /// </summary>
    public class SystemTime
    {
        /// <summary>
        /// The date.
        /// </summary>
        private static DateTime date;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTime"/> class.
        /// </summary>
        public SystemTime()
        {
            date = DateTime.MinValue;
        }

        /// <summary>
        /// Gets the default date time value.
        /// </summary>
        public static DateTime DateTimeDefault
        {
            get
            {
                return new DateTime(2000, 1, 1);
            }
        }

        /// <summary>
        /// Gets a System.DateTime object that is set to the current date and time on
        /// this computer, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime UtcNow
        {
            get
            {
                if (date != DateTime.MinValue)
                {
                    return date.TrimToSeconds();
                }

                return DateTime.UtcNow.TrimToSeconds();
            }
        }

        /// <summary>
        /// Gets UtcNow expressed as a string using the current culture.
        /// </summary>
        public static string UtcNowString
        {
            get
            {
                return UtcNow.TrimToSeconds().ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Reset date to min value.
        /// </summary>
        public static void Reset()
        {
            date = DateTime.MinValue;
        }

        /// <summary>
        /// Set a custom DateTime value.
        /// </summary>
        public static void Set(DateTime custom)
        {
            date = custom;
        }
    }
}