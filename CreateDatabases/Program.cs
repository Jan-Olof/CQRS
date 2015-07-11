namespace Utilities.CreateDatabases
{
    using System;

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
            Console.WriteLine("Do you want to recreate the databases?");
            var keyInfo = Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine();

            if (keyInfo.Key != ConsoleKey.Y)
            {
                return;
            }

            var dropAndCreateDatabases = new DropAndCreateDatabases();

            bool writeResult = dropAndCreateDatabases.DropAndCreateWriteDatabase();

            Console.WriteLine(writeResult ? "Write database successfully recreated." : "Write database recreation failed.");

            bool readResult = dropAndCreateDatabases.DropAndCreateReadDatabase();

            Console.WriteLine(readResult ? "Read database successfully recreated." : "Read database recreation failed.");

            Console.ReadKey();
        }
    }
}
