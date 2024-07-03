using System.Text.RegularExpressions;

namespace VTOL_VR_Auto_Radio_Shuffle
{
    internal class Program
    {
        public static void WrongPathMessage(string folderPath, string MusicFolderName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Path check:");
            if (MusicFolderName != "RadioMusic")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR]");
                Console.WriteLine("Path of this app: " + folderPath);
                Console.WriteLine("Radio folder name: " + MusicFolderName);

                Console.WriteLine("\nPath mismatch. Please put the app folder in \"RadioMusic\" folder (can be found in VTOL VR directory).\n");
                Console.ResetColor();

                Console.WriteLine("Terminating process. Press any key to close.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[SUCCESS]");
                Console.WriteLine("Path of this app: " + folderPath);
                Console.WriteLine("Radio folder name: " + MusicFolderName);

                Console.WriteLine("\nPath is correct (unless you created the folder \"RadioMusic\" and put the app's folder in it. This is considered as intentional error and will not be checked).");
                Console.ResetColor();
            }
        }

        public static void TitleShow()
        {

            Console.WriteLine(" ___      ___ _________  ________  ___               ___      ___ ________       tm\r\n" +
                "|\\  \\    /  /|\\___   ___\\\\   __  \\|\\  \\             |\\  \\    /  /|\\   __  \\    \r\n" +
                "\\ \\  \\  /  / ||___ \\  \\_\\ \\  \\|\\  \\ \\  \\            \\ \\  \\  /  / | \\  \\|\\  \\   \r\n" +
                " \\ \\  \\/  / /     \\ \\  \\ \\ \\  \\\\\\  \\ \\  \\            \\ \\  \\/  / / \\ \\   _  _\\  \r\n" +
                "  \\ \\    / /       \\ \\  \\ \\ \\  \\\\\\  \\ \\  \\____        \\ \\    / /   \\ \\  \\\\  \\| \r\n" +
                "   \\ \\__/ /         \\ \\__\\ \\ \\_______\\ \\_______\\       \\ \\__/ /     \\ \\__\\\\ _\\ \r\n" +
                "    \\|__|/           \\|__|  \\|_______|\\|_______|        \\|__|/       \\|__|\\|__|\r\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   ___          ___        ______        ________   \r\n" +
                "  / _ \\___ ____/ (_)__    / __/ /  __ __/ _/ _/ /__ \r\n" +
                " / , _/ _ `/ _  / / _ \\  _\\ \\/ _ \\/ // / _/ _/ / -_)\r\n" +
                "/_/|_|\\_,_/\\_,_/_/\\___/ /___/_//_/\\_,_/_//_//_/\\__/ V1.0\r\n");
            Console.ResetColor();
            Console.WriteLine("===================================================================================");
        }

        public static void renameFiles(List<string> mp3Files, List<int> numbers)
        {
            Regex pattern = new Regex(@"^\[\d+\]");

            for (int i = 0; i < mp3Files.Count - 1; i++)
            {
                string filePath = mp3Files[i];
                string fileName = Path.GetFileName(filePath);

                if (pattern.IsMatch(fileName))
                {
                    // Remove the pattern from the file name
                    string newFileName = pattern.Replace(fileName, "");
                    string directoryPath = Path.GetDirectoryName(filePath);
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    // Rename the file
                    File.Move(filePath, newFilePath);

                    // Update the list with the new file path
                    mp3Files[i] = newFilePath;
                }
            }
            for (int j = 0; j <= mp3Files.Count - 1; j++)
            {
                string filePath = mp3Files[j];
                string fileName = Path.GetFileName(filePath);

                if (!pattern.IsMatch(fileName))
                {
                    string directoryPath = Path.GetDirectoryName(filePath);
                    string newFileName = $"[{numbers[j]}]{fileName}";
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    // Rename the file
                    File.Move(filePath, newFilePath);

                    // Update the list with the new file path
                    mp3Files[j] = newFilePath;
                }
            }
            Console.WriteLine("\nFile names have been updated.");
        }

        //I literally have no idea how this thing works. I'm not that smart lol
        public static List<int> Numbers(List<string> mp3Files)
        {
            // Generate a list of numbers from 1 to file count
            List<int> numbers = new List<int>();
            for (int i = 1; i <= mp3Files.Count; i++)
            {
                numbers.Add(i);
            }

            // Shuffle the list of numbers
            Random rng = new Random();
            int n = numbers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = numbers[k];
                numbers[k] = numbers[n];
                numbers[n] = value;
            }
            return numbers;
        }
        static void Main(string[] args)
        {
            string folderPath = Environment.CurrentDirectory;
            string MusicFolderName = Path.GetFileName(Path.GetDirectoryName(folderPath));

            int lastIndex = 0;
            int mp3FileCount = 0;
            int timer = 5;

            string result = "";
            string newFolderPath = "";
            string filePath = "";

            ConsoleKeyInfo keyInfo;

            List<string> mp3Files = new List<string>();
            List<int> numbers = new List<int>();

            TitleShow();
            WrongPathMessage(folderPath, MusicFolderName);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nGetting the files...");
            Console.ResetColor();
            lastIndex = folderPath.LastIndexOf('\\');
            if (lastIndex >= 0)
            {
                result = folderPath.Substring(0, lastIndex);
            }
            mp3Files = new List<string>(Directory.GetFiles(result, "*.mp3"));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done.\n");
            Console.ResetColor();

            numbers = Numbers(mp3Files);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nRenaming the files...");
            Console.ResetColor();
            renameFiles(mp3Files, numbers);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done.\n");
            Console.ResetColor();

            while (timer != -1)
            {
                Console.WriteLine("Task finished, terminating process in: " + timer);
                timer--;
                Thread.Sleep(1000);
            }
            Environment.Exit(0);
        }
    }
}