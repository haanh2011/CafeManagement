using System.Globalization;


namespace CafeManagement.Utilities
{
    public static class FormatHelper
    {
        public static string FormatToVND(double number)
        {
            // Định dạng số theo tiền VNĐ
            return number.ToString("#,##0") + " đ";
        }

        public static string FormatToVND(int number)
        {
            // Định dạng số theo tiền VNĐ
            return number.ToString("#,##0") + " đ";
        }

        public static string FormatToVND(float number)
        {
            // Định dạng số theo tiền VNĐ
            return number.ToString("#,##0") + " đ";
        }

        public static string FormatToVND(decimal number)
        {
            // Định dạng số theo tiền VNĐ
            return number.ToString("#,##0") + " đ";
        }

        public static string ToTitleCase(string input)
        {
            // Tạo một đối tượng TextInfo từ CultureInfo hiện tại để sử dụng phương thức ToTitleCase
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            // Sử dụng phương thức ToTitleCase để chuyển đổi chuỗi vào chuỗi với chữ cái đầu tiên của mỗi từ viết hoa
            return textInfo.ToTitleCase(input);
        }
    }
}
