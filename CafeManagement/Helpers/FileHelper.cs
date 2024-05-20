using System.IO;
using CafeManagement.Models;

namespace CafeManagement.Helpers
{
    public static class FileHelper
    {
        public static LinkedList<string> ReadFromFile(string filePath)
        {
            LinkedList<string> lines = new LinkedList<string>();
            if (File.Exists(filePath))
            {
                lines.AddRange(File.ReadAllLines(filePath));
            }
            return lines;
        }

        public static void WriteToFile(string filePath, LinkedList<string> lines)
        {
            File.WriteAllLines(filePath, lines.ToList());
        }
    }
}
