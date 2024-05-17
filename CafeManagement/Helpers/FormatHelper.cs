using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CafeManagement.Helpers
{
    public static class FormatHelper
    {
        public static string FormatNumberEuropeanStyle(double value)
        {
            // Sử dụng định dạng văn hóa của Đức để định dạng số theo kiểu châu Âu
            CultureInfo culture = new CultureInfo("de-DE");
            return value.ToString("N2", culture);
        }

        public static string FormatNumberEuropeanStyle(int value)
        {
            // Sử dụng định dạng văn hóa của Đức để định dạng số theo kiểu châu Âu
            CultureInfo culture = new CultureInfo("de-DE");
            return value.ToString("N2", culture);
        }

        public static string FormatNumberEuropeanStyle(float value)
        {
            // Sử dụng định dạng văn hóa của Đức để định dạng số theo kiểu châu Âu
            CultureInfo culture = new CultureInfo("de-DE");
            return value.ToString("N2", culture);
        }

        public static string FormatNumberEuropeanStyle(decimal value)
        {
            // Sử dụng định dạng văn hóa của Đức để định dạng số theo kiểu châu Âu
            CultureInfo culture = new CultureInfo("de-DE");
            return value.ToString("N2", culture);
        }
    }
}
