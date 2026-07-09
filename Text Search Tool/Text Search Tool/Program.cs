using System;
using System.IO;

namespace Text_Search_Tool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Text Search Tool";

            Console.WriteLine("========================================");
            Console.WriteLine("           Text Search Tool");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Text to search: ");
            string searchText = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                Console.WriteLine("Search text cannot be empty.");
                return;
            }

            Console.WriteLine();

            Console.Write("File filter (Press ENTER for *.*, example: *.txt): ");
            string fileFilter = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileFilter))
                fileFilter = "*.*";

            Console.WriteLine();

            Console.Write("Read all matches in the same file? (Y/N, default: N): ");

            string answer = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            bool readAllMatches = answer == "y" || answer == "yes";

            Console.WriteLine();

            Console.Write("Root folder: ");
            string rootFolder = Console.ReadLine();

            if (!Directory.Exists(rootFolder))
            {
                Console.WriteLine("The specified directory does not exist.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Searching...");
            Console.WriteLine();

            int filesWithMatches = 0;

            string[] files = Directory.GetFiles(rootFolder, fileFilter, SearchOption.AllDirectories);

            foreach (string file in files)
            {
                try
                {
                    int lineNumber = 0;
                    bool filePrinted = false;

                    foreach (string line in File.ReadLines(file))
                    {
                        lineNumber++;

                        if (line.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            if (!filePrinted)
                            {
                                filesWithMatches++;
                                Console.WriteLine($"File: {file}");
                                filePrinted = true;
                            }

                            Console.WriteLine($"  Line {lineNumber}: {line.Trim()}");

                            if (!readAllMatches)
                                break;
                        }
                    }

                    if (filePrinted)
                        Console.WriteLine();
                }
                catch
                {
                    // Ignore unreadable files
                }
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Finished. {filesWithMatches} matching file(s) found.");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}