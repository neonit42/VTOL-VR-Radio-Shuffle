using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;


//Desine sperare qui hic intras

namespace VTOL_VR_Radio_Shuffle
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

        public static void FormatExceptionCatchMessage(int timer)
        {
            while (timer != -1)
            {
                Console.Clear();
                TitleShow();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input.\n\n\n\n\n\n");
                Console.ResetColor();
                Console.WriteLine("You'll be redirected to the main menu in: " + timer);
                timer--;
                Thread.Sleep(1000);
            }
        }

        public static void NoTxtFileFound(int timer, string newFolderPath)
        {
            while (timer != -1)
            {
                Console.Clear();
                TitleShow();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR]");
                Console.WriteLine("File at path: " + Path.Combine(newFolderPath, "Original MP3 File Names.txt") + " does not exist. Please save file names with the third option in the main menu.\n\n\n\n\n\n");
                Console.ResetColor();
                Console.WriteLine("You'll be redirected to the main menu in: " + timer);
                timer--;
                Thread.Sleep(1000);
            }
            
        }
        //maybe not needed
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

        public static void resetFileNames(List<string> mp3Files)
        {
            Regex pattern = new Regex(@"^\[\d+\]");

            for (int i = 0; i < mp3Files.Count; i++)
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
            int CaseUserIn = 0;
            int lastIndex = 0;
            int mp3FileCount = 0;

            string folderPath = "";
            string MusicFolderName = "";
            string result = "";
            string newFolderPath = "";
            string filePath = "";
            string gitHubURL = "https://github.com/neonit42/VTOL-VR-Radio-Shuffle";

            ConsoleKeyInfo keyInfo;

            bool wasRightKeyPressed = false;

            List<string> mp3Files = new List<string>();
            List<int> numbers = new List<int>();

            folderPath = Environment.CurrentDirectory;
            MusicFolderName = Path.GetFileName(Path.GetDirectoryName(folderPath));

            while (true)
            {
                switch (CaseUserIn)
                {
                    case 0:
                        {
                            Console.Clear();
                            TitleShow();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Welcome to VTOL VR Radio Shuffle V1.0, type appropriate numbers for choosing options:\n\n");
                            Console.ResetColor();

                            Console.WriteLine("1 - Shuffle Radio");
                            Console.WriteLine("2 - Reset Radio");
                            Console.WriteLine("3 - Save Radio Names");
                            Console.WriteLine("4 - Automate Process");
                            Console.Write("\n\n5 -");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Quit\n\n");
                            Console.ResetColor();

                            
                            Console.Write("For information go to the GitHub page: ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(gitHubURL + "\n");
                            Console.ResetColor();

                            try
                            {
                                CaseUserIn = int.Parse(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                FormatExceptionCatchMessage(5);
                                CaseUserIn = 0;
                            }


                        }
                        break;
                    case 1:
                        {
                            Console.Clear();
                            TitleShow();

                            WrongPathMessage(folderPath, MusicFolderName);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nShuffle radio?");
                            Console.ResetColor();
                            Console.WriteLine("[Y/N]");

                            keyInfo = Console.ReadKey();
                            while (wasRightKeyPressed == false)
                            {
                                if (keyInfo.KeyChar == 'y' || keyInfo.KeyChar == 'Y')
                                {
                                    wasRightKeyPressed = true;
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nGetting file names...\n");
                                    Console.ResetColor();

                                    lastIndex = folderPath.LastIndexOf('\\');
                                    if (lastIndex >= 0)
                                    {
                                        result = folderPath.Substring(0, lastIndex);
                                    }
                                    mp3Files = new List<string>(Directory.GetFiles(result, "*.mp3"));


                                    numbers = Numbers(mp3Files);
                                    renameFiles(mp3Files, numbers);

                                }
                                else if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N')
                                {
                                    wasRightKeyPressed = true;
                                }
                                else
                                {
                                    keyInfo = Console.ReadKey();
                                }
                            }
                            wasRightKeyPressed = false;
                            Console.WriteLine("\nDone. Press any key to return to menu.");
                            Console.ReadKey();
                            CaseUserIn = 0;
                        }
                        break;
                    case 2:
                        {
                            while (wasRightKeyPressed == false)
                            {
                                Console.Clear();
                                TitleShow();

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Reset radio?");
                                Console.ResetColor();
                                Console.WriteLine("[Y/N]");

                                keyInfo = Console.ReadKey();
                                if (keyInfo.KeyChar == 'y' || keyInfo.KeyChar == 'Y')
                                {
                                    wasRightKeyPressed = true;
                                    lastIndex = folderPath.LastIndexOf('\\');
                                    if (lastIndex >= 0)
                                    {
                                        result = folderPath.Substring(0, lastIndex);
                                    }
                                    mp3Files = new List<string>(Directory.GetFiles(result, "*.mp3"));
                                    resetFileNames(mp3Files);
                                }
                                else if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N')
                                {
                                    wasRightKeyPressed = true;
                                }
                                else
                                {
                                    keyInfo = Console.ReadKey();
                                }
                            }
                            wasRightKeyPressed = false;
                            Console.WriteLine("\nDone. Press any key to return to menu.");
                            Console.ReadKey();
                            CaseUserIn = 0;
                        }
                        break;
                    case 3:
                        {
                            Console.Clear();
                            TitleShow();
                            WrongPathMessage(folderPath, MusicFolderName);

                            lastIndex = folderPath.LastIndexOf('\\');
                            if (lastIndex >= 0)
                            {
                                result = folderPath.Substring(0, lastIndex);
                            }

                            Console.Write("\nThe next process will create a new folder with a text file containing all the current names of the songs.");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Continue?");
                            Console.ResetColor();
                            Console.WriteLine("[Y/N]");



                            keyInfo = Console.ReadKey();

                            //NEED FIX (fixed. I think)
                            while (wasRightKeyPressed == false)
                            {
                                if (keyInfo.KeyChar == 'y' || keyInfo.KeyChar == 'Y')
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nCopying music names...");
                                    Console.ResetColor();
                                    wasRightKeyPressed = true;

                                    Console.WriteLine("Radio folder path: " + result + "\n");

                                    // Get all MP3 files in the directory
                                    foreach (string file in Directory.GetFiles(result, "*.mp3"))
                                    {
                                        string fileName = Path.GetFileName(file);
                                        mp3Files.Add(fileName);
                                    }

                                    // Print out all MP3 file names
                                    Console.WriteLine("MP3 File Names:");
                                    foreach (string fileName in mp3Files)
                                    {
                                        Console.WriteLine(fileName);
                                    }


                                    newFolderPath = Path.Combine(folderPath, "MP3 File Names");

                                    if (!Directory.Exists(newFolderPath))
                                    {
                                        Directory.CreateDirectory(newFolderPath);
                                        filePath = Path.Combine(newFolderPath, "Original MP3 File Names.txt");
                                        File.WriteAllLines(filePath, mp3Files);
                                    }
                                    else
                                    {
                                        filePath = Path.Combine(newFolderPath, "Original MP3 File Names.txt");
                                        File.WriteAllLines(filePath, mp3Files);
                                    }

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Copy complete. Press any key to return to menu.");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    CaseUserIn = 0;
                                }
                                else if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N')
                                {
                                    wasRightKeyPressed = true;
                                    CaseUserIn = 0;
                                }
                                else
                                {
                                    keyInfo = Console.ReadKey();
                                }
                            }

                            wasRightKeyPressed = false;
                        }
                        break;
                    case 4:
                        {
                            Console.Clear();
                            TitleShow();

                            string vtolVrPath = "";
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("The next process will create a .bat file. In order to automate the shuffle, you'll need to add a side game in your steam and link it to the AutoOpen.exe file.\n\n Continue?");
                            Console.ResetColor();

                            Console.WriteLine("[Y/N]");

                            keyInfo = Console.ReadKey();

                            while (wasRightKeyPressed == false)
                            {
                                if (keyInfo.KeyChar == 'y' || keyInfo.KeyChar == 'Y')
                                {
                                    wasRightKeyPressed = true;

                                    Console.WriteLine("\nEnter VTOL VR's directory path:");
                                    vtolVrPath = Console.ReadLine();


                                    string directoryPath = AppDomain.CurrentDomain.BaseDirectory; // Get the current directory
                                    string txtFileName = "AutoProcces.txt";
                                    string batFileName = "AutoProcces.bat";
                                    string txtFilePath = Path.Combine(directoryPath, txtFileName);
                                    string batFilePath = Path.Combine(directoryPath, batFileName);

                                    // Text to be written to the file
                                    string textToWrite = "@echo off\n" +
                                                         "start \"\" \"" + folderPath + "\\VTOL-VR Auto Radio Shuffle.exe\"" + "\n" +
                                                         "timeout /t 1 /nobreak > nul\n" +
                                                         "start \"\" \"" + vtolVrPath + "\\VTOLVR.exe\"";

                                    try
                                    {
                                        // Step 1: Create the .txt file and write text to it
                                        File.WriteAllText(txtFilePath, textToWrite);
                                        Console.WriteLine($"Text file created at: {txtFilePath}");

                                        // Step 2: Rename the .txt file to .bat
                                        if (File.Exists(batFilePath))
                                        {
                                            File.Delete(batFilePath); // Delete the existing .bat file if it exists
                                        }
                                        File.Move(txtFilePath, batFilePath);
                                        Console.WriteLine($"Text file renamed to: {batFilePath}");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"An error occurred: {ex.Message}");
                                    }

                                    Console.WriteLine("\nDone. Press any key to return to main menu.");
                                    Console.ReadKey();

                                }
                                else if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N')
                                {
                                    wasRightKeyPressed = true;
                                }
                                else
                                {
                                    keyInfo = Console.ReadKey();
                                }
                            }

                            wasRightKeyPressed = false;
                            CaseUserIn = 0;
                        }
                            break;
                    case 5:
                        {
                            Console.Clear();
                            TitleShow();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You sure you want to quit?\n\n");
                            Console.ResetColor();

                            Console.WriteLine("1 - Yes");
                            Console.WriteLine("2 - No\n\n");

                            try
                            {
                                CaseUserIn = int.Parse(Console.ReadLine());

                                if (CaseUserIn == 1)
                                {
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    CaseUserIn = 0;
                                }
                            }
                            catch (FormatException)
                            {
                                FormatExceptionCatchMessage(5);
                                CaseUserIn = 0;
                            }
                        }
                        break;
                    default:
                        {
                            CaseUserIn = 0;
                        }
                        break;
                }
            }
            

            //end of MAIN
        }
    }
}

//I'm a self proclaimed c# programmer, but I don't think people would agree with me.
//fun fact: when I was writing this code, only I and god knew what it does. now only god knows...