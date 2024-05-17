using System;

namespace CafeManagement.Helpers
{
    public static class ConsoleHelper
    {
        public static int GetIntInput(string prompt)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một số nguyên.");
                }
            }
            return input;
        }

        public static double GetDoubleInput(string prompt)
        {
            double input;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một số thực.");
                }
            }
            return input;
        }

        public static DateTime GetDateTimeInput(string prompt)
        {
            DateTime input;
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập ngày tháng hợp lệ.");
                }
            }
            return input;
        }

        public static string GetStringInput(string prompt)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng không để trống!");
                }
            }
            return input;
        }

        public static void PrintTitleMenu(string title, Boolean isClearConsole = true)
        {
            if (isClearConsole) Console.Clear();
            Console.WriteLine("===== {0} =====", title);
        }


        public static void PrintInputKeyEnter(string promt)
        {
            Console.WriteLine("Nhập phím [enter] để {0}", promt);
            Console.ReadLine();
        }
    }
}
