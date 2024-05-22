using System.Collections.Generic;
using System.IO;

namespace CafeManagement.Utilities
{
    public static class FileHelper
    {
        public static Models.LinkedList<string> ReadFromFile(string filePath)
        {
            Models.LinkedList<string> lines = new Models.LinkedList<string>();
            if (File.Exists(filePath))
            {
                lines.AddRange(File.ReadAllLines(filePath));
            }
            return lines;
        }

        public static void WriteToFile(string filePath, Models.LinkedList<string> lines)
        {
            File.WriteAllLines(filePath, lines.ToList());
        }
    }
}
