namespace WriteToReadEtl
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
            Console.WriteLine(WorkClass.DoTheWork());

            Console.ReadKey();
        }
    }
}