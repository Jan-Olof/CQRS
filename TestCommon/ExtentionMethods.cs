namespace Tests.TestCommon
{
    using System.Data.Entity;
    using System.Linq;

    using NSubstitute;

    /// <summary>
    /// The extention methods.
    /// </summary>
    public static class ExtentionMethods
    {
        /// <summary>
        /// The initialize.
        /// </summary>
        public static IDbSet<T> Initialize<T>(this IDbSet<T> dbSet, IQueryable<T> data) where T : class
        {
            dbSet.Provider.Returns(data.Provider);
            dbSet.Expression.Returns(data.Expression);
            dbSet.ElementType.Returns(data.ElementType);
            dbSet.GetEnumerator().Returns(data.GetEnumerator());

            return dbSet;
        }
    }
}