using System.Collections.Generic;
using System.IO;

namespace CafeManagement.Helpers
{
    public static class FileHelper
    {
        public static List<string> ReadFromFile(string filePath)
        {
            List<string> lines = new List<string>();
            if (File.Exists(filePath))
            {
                lines.AddRange(File.ReadAllLines(filePath));
            }
            return lines;
        }

        public static void WriteToFile(string filePath, List<string> lines)
        {
            File.WriteAllLines(filePath, lines);
        }
    }
}
