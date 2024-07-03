namespace AutoOpen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string directoryPath = AppDomain.CurrentDomain.BaseDirectory; // Get the current directory
            string batFileName = "AutoProcces.bat";
            string batFilePath = Path.Combine(directoryPath, batFileName);

            System.Diagnostics.Process.Start(batFilePath);
        }
    }
}