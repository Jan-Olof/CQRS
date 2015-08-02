namespace Common.Utilities
{
    using System;

    /// <summary>
    /// The date time extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Trim a DateTime using ticks.
        /// Ex: TimeSpan.TicksPerSecond trims to seconds.
        /// </summary>
        public static DateTime Trim(this DateTime date, long ticks)
        {
            return new DateTime(date.Ticks - (date.Ticks % ticks));
        }

        /// <summary>
        /// Trim a DateTime to milliseconds.
        /// </summary>
        public static DateTime TrimToMilliSeconds(this DateTime date)
        {
            return new DateTime(date.Ticks - (date.Ticks % TimeSpan.TicksPerMillisecond));
        }

        /// <summary>
        /// Trim a DateTime to seconds.
        /// </summary>
        public static DateTime TrimToSeconds(this DateTime date)
        {
            return new DateTime(date.Ticks - (date.Ticks % TimeSpan.TicksPerSecond));
        }
    }
}