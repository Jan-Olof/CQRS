namespace WriteToReadEtl
{
    using System;

    using Common.DataAccess;
    using Common.Utilities;

    using DataAccess.Read.Dal.CodeFirst.DbContext;

    using Domain.Write.Entities;

    using NLog;

    using WriteToRead;
    using WriteToRead.FromWriteDb;
    using WriteToRead.ToReadDb;

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine(WorkClass.DoTheWork());

            Console.ReadKey();
        }
    }
}
