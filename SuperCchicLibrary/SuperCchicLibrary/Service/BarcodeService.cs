using IronBarCode;
using IronSoftware.Drawing;

namespace SuperCchicLibrary.Service
{
    public class BarcodeService
    {
        const string COMPANYTPREFIX = "12345";
        private void GenerateBarcodeLabel(Product product)
        {
            var font = new Font("Arial", FontStyle.Regular, 24f);

            GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(product.Code, BarcodeWriterEncoding.UPCA);
            barcode.AddAnnotationTextAboveBarcode(product.Name, font, Color.Black, 10);
            barcode.AddBarcodeValueTextBelowBarcode(10);
            barcode.ResizeTo(400, 120);
            barcode.SetMargins(10);
            barcode.SaveAsImage($"{product.Name}.png");
        }
        public static string GenerateBarcodeDigits(Product product)
        {
            string firstdigit = string.Empty;

            // changer le switch pour la category id

            switch (product.Subcategory.Category.Id)
            {
                case 1:
                    firstdigit = "0";
                    break;
                case 3 or 4:
                    firstdigit = "1";
                    break;
                case 5 or 6:
                    firstdigit = "2";
                    break;
                case 7 or 8:                  
                    firstdigit = "4";
                    break;
                case 9 or 10 or 11:           
                    firstdigit = "5";
                    break;
                case 12 or 13 or 14:           
                    firstdigit = "6";
                    break;
                case 15 or 16:               
                    firstdigit = "7";
                    break;
                default:
                    firstdigit = "0"; 
                    break;
            }

            
            string sequencedigits = product.Id.ToString().PadLeft(5, '0');

            // checksum pour le checkdigit + changer les codes dans la bd pour des 12 chiffres 
            return firstdigit + COMPANYTPREFIX + sequencedigits;
        }
    }
}
