using ZXing;
using ZXing.Windows.Compatibility;
using System.Drawing;
using System.Drawing.Imaging;

namespace SuperCchicLibrary.Service
{
    public class BarcodeService
    {
        const string COMPANYTPREFIX = "12345";
        //WHEN I USED IRONBARCODE
        //public static void GenerateBarcodeLabel(Product product)
        //{
        //    var font = new Font("Arial", FontStyle.Regular, 24f);
        //    string formattedCode = product.Code.Substring(0, 11);
        //    GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(formattedCode, BarcodeWriterEncoding.UPCA);
        //    barcode.AddAnnotationTextAboveBarcode(product.Name, font, Color.Black, 10);
        //    barcode.AddBarcodeValueTextBelowBarcode(10);
        //    barcode.ResizeTo(400, 120);
        //    barcode.SetMargins(10);
        //    barcode.SaveAsImage($"{product.Name}.png");
        //}
        public static void GenerateBarcodeLabel(Product product)
        {
            string formattedCode = product.Code.Substring(0, 11);

            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.UPC_A
            };

            using Bitmap barcode = barcodeWriter.Write(formattedCode);
            barcode.Save($"{product.Name}.png", ImageFormat.Png);
        }
        public static string GenerateBarcode(Product product)
        {
            string firstdigit = string.Empty;

            switch (product.IdSubcategory)
            {
                default:
                    firstdigit = "0"; 
                    break;
            }

            string sequencedigits = product.Id.ToString().PadLeft(5, '0');

            string barcodeWithoutCheckDigit = firstdigit + COMPANYTPREFIX + sequencedigits;

            string checkDigit = CalculateUPCACheckDigit(barcodeWithoutCheckDigit);

            return barcodeWithoutCheckDigit + checkDigit;
        }
        // ai generated because it is a standard calculation
        private static string CalculateUPCACheckDigit(string code)
        {
            if (code.Length != 11)
            {
                throw new ArgumentException("Code must be exactly 11 digits for UPC-A checksum calculation");
            }

            int sumOdd = 0;
            int sumEven = 0;

            // Add digits at odd positions (1st, 3rd, 5th, 7th, 9th, 11th)
            // Note: positions are 1-based, but array is 0-based
            for (int i = 0; i < code.Length; i++)
            {
                int digit = int.Parse(code[i].ToString());

                if (i % 2 == 0)  // 0-based index: 0, 2, 4, 6, 8, 10 = odd positions (1, 3, 5, 7, 9, 11)
                {
                    sumOdd += digit;
                }
                else  // 0-based index: 1, 3, 5, 7, 9 = even positions (2, 4, 6, 8, 10)
                {
                    sumEven += digit;
                }
            }

            // Multiply odd sum by 3 and add even sum
            int total = (sumOdd * 3) + sumEven;

            // Calculate check digit
            int remainder = total % 10;
            int checkDigit = (remainder == 0) ? 0 : (10 - remainder);

            return checkDigit.ToString();
        }
    }
}
