using CafeManagement.Constants;
using CafeManagement.Models;
using System;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

namespace CafeManagement.Utilities
{
    public static class ConsoleHelper
    {
        public static int GetIntInput(string prompt)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out input) && input >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một số nguyên lớn hơn bằng 0.");
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
                if (double.TryParse(Console.ReadLine(), out input) && input >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một số thực lớn hơn bằng 0.");
                }
            }
            return input;
        }

        public static DateTime GetDateTimeInput(string prompt)
        {
            string input;
            DateTime date = DateTime.MinValue;

            while (date == DateTime.MinValue)
            {
                try
                {
                    Console.Write(prompt);
                    input = Console.ReadLine();

                    // Chuyển đổi chuỗi đầu vào thành đối tượng DateTime
                    date = DateTime.ParseExact(input, StringConstants.FORMAT_DATE, CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Định dạng ngày không hợp lệ. Vui lòng nhập ngày theo định dạng {StringConstants.FORMAT_DATE}.");
                }
            }
            return date;
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
            return input.TrimStart().TrimEnd();
        }
        public static string GetYNInput(string prompt)
        {
            string input;
            Console.Write(prompt);
            while (true)
            {
                string userInput = Console.ReadLine().Trim().ToUpper();
                if (userInput == "Y" || userInput == "N")
                {
                    input = userInput;
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập 'Y' hoặc 'N'.");
                }
            }
            return input;
        }

        public static void PrintTitleMenu(string title, Boolean isClearConsole = true)
        {
            if (isClearConsole) ClearConsole();
            Console.WriteLine("===== {0} =====", title);
        }

        public static void ClearConsole()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");
        }
        /// <summary>
        /// Table header hiển thị column của table X.
        /// </summary>
        /// <returns>Chuỗi biểu diễn columns header.</returns>
        public static void PrintHeaderTable(string title)
        {
            PrintHorizontalLineOfTable(title);
            switch (title)
            {
                case StringConstants.CATEGORY:
                    Console.WriteLine($"| {"ID",-5} | {"Tên loại sản phẩm",-25} |");
                    break;
                case StringConstants.PRODUCT:
                    Console.WriteLine($"| {"ID",-5} | {"Tên sản phẩm",-25} | {"Giá",-15} |");
                    break;
                case StringConstants.ORDER:
                    Console.WriteLine($"| {"STT",-5} | {"ID",-5} | {"Tên sản phẩm",-25} | {"Giá",15} | {"Số lượng",10} | {"Thành tiền",-15} |");
                    break;
                case StringConstants.CUSTOMER:
                    Console.WriteLine($"| {"Mã",5} | {"Tên",-25} | {"Ngày sinh",-10} | {"SĐT",-10} | {"Email",-25} | {"Điểm",10} |");
                    break;
                case StringConstants.INVOICE:
                    Console.WriteLine($"| {"Mã",-5} | {"Mã đơn hàng",-15} | {"Ngày lập hoá đơn",-20} |");
                    break;
                default:
                    break;
            }
            PrintHorizontalLineOfTable(title);
        }

        public static void PrintMenuDetails(string name)
        {
            PrintTitleMenu(string.Format(StringConstants.MANAGE_X, name).ToUpper());
            Console.WriteLine("1. " + FormatHelper.ToTitleCase(string.Format(StringConstants.DISPLAY_LIST_X, name)));
            Console.WriteLine("2. " + FormatHelper.ToTitleCase(string.Format(StringConstants.ADD_X, name)));
            Console.WriteLine("3. " + FormatHelper.ToTitleCase(string.Format(StringConstants.UPDATE_X, name)));
            Console.WriteLine("4. " + FormatHelper.ToTitleCase(string.Format(StringConstants.DELETE_X, name)));
            Console.WriteLine("0. " + StringConstants.BACK);
        }

        /// <summary>
        /// In dòng kẻ ngang của bảng
        /// </summary>
        /// <param name="len"></param>
        public static void PrintHorizontalLineOfTable(string title)
        {
            switch (title)
            {
                case StringConstants.CATEGORY:
                    Console.WriteLine(new string('-', 37));
                    break;
                case StringConstants.PRODUCT:
                    Console.WriteLine(new string('-', 50));
                    break;
                case StringConstants.ORDER:
                    Console.WriteLine(new string('-', 94));
                    break;
                case StringConstants.CUSTOMER:
                    Console.WriteLine(new string('-', 104));
                    break;
                case StringConstants.INVOICE:
                    Console.WriteLine(new string('-', 45));
                    break;
                default:
                    break;

            }
        }

    }
}
