using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SuperCchicLibrary.Service
{
    public class OrderReceipt
    {
        // Propriétés pour stocker les données du reçu
        public List<OrderDetailDTO> items { get; set; }
        public string comment { get; set; }
        public decimal subtotal { get; set; }
        public decimal transactiontotal { get; set; }
        public decimal tps { get; set; }
        public decimal tvq { get; set; }
        public DateTime date {  get; set; }

        public OrderReceipt() { }
        public OrderReceipt(List<OrderDetailDTO> items, string comment, decimal subtotal, decimal transactiontotal, decimal tps, decimal tvq, DateTime date)
        {
            this.items = items;
            this.comment = comment;
            this.subtotal = subtotal;
            this.transactiontotal = transactiontotal;
            this.tps = tps;
            this.tvq = tvq;
            this.date = date;
        }
        public static void PrintReceipt(OrderReceipt orderReceipt)
        {
            GenerateReceipt(orderReceipt.items, orderReceipt.comment, orderReceipt.subtotal, orderReceipt.tps, orderReceipt.tvq, orderReceipt.transactiontotal, orderReceipt.date);
        }
        // Méthode statique pour générer ET sauvegarder le PDF
        public static OrderReceipt GenerateReceipt(List<OrderDetailDTO> items, string comment, decimal subtotal, decimal tps, decimal tvq, decimal transactiontotal, DateTime date)
        {
            // Définir la licence AVANT de créer le document
            QuestPDF.Settings.License = LicenseType.Community;

            var receipt = new OrderReceipt
            {
                items = items,
                comment = comment,
                subtotal = subtotal,
                tps = tps,
                transactiontotal = transactiontotal,
                date = date
            };

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A7);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // En-tête du reçu
                    page.Header()
                        .Column(col =>
                        {
                            col.Item().Text("REÇU DE COMMANDE")
                                .Bold().FontSize(16).FontColor(Colors.Blue.Medium);
                            col.Item().Text($"Date: {date:dd/MM/yyyy HH:mm}")
                                .FontSize(8);
                        });

                    // Contenu principal
                    page.Content()
                        .PaddingVertical(0.5f, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(10);

                            // Tableau des articles
                            x.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {   // Produit
                                    columns.RelativeColumn(3);
                                    // Qté
                                    columns.RelativeColumn(1);
                                    // Prix
                                    columns.RelativeColumn(2); 
                                });

                                // En-tête
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Produit").Bold();
                                    header.Cell().Element(CellStyle).Text("Qté").Bold();
                                    header.Cell().Element(CellStyle).Text("Prix").Bold();

                                    static IContainer CellStyle(IContainer container) =>
                                        container.BorderBottom(1)
                                            .BorderColor(Colors.Grey.Lighten2)
                                            .PaddingVertical(3);
                                });

                                // Produits
                                foreach (var item in items)
                                {
                                    table.Cell().Element(CellStyle).Text(item.ProductName);
                                    table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                                    table.Cell().Element(CellStyle).Text($"{item.TotalPrice:C2}");

                                    static IContainer CellStyle(IContainer container) =>
                                        container.BorderBottom(1)
                                            .BorderColor(Colors.Grey.Lighten3)
                                            .PaddingVertical(3);
                                }

                                // Totaux
                                table.Cell().ColumnSpan(2).PaddingVertical(5).Text("SUBTOTAL").SemiBold();
                                table.Cell().PaddingVertical(5).Text($"{subtotal:C2}").SemiBold();

                                table.Cell().ColumnSpan(2).PaddingVertical(2).Text("TPS");
                                table.Cell().PaddingVertical(2).Text($"{tps:C2}");

                                table.Cell().ColumnSpan(2).PaddingVertical(2).Text("TVQ");
                                table.Cell().PaddingVertical(2).Text($"{tvq:C2}");

                                table.Cell().ColumnSpan(2).PaddingTop(5).Text("TOTAL").Bold();
                                table.Cell().PaddingTop(5).Text($"{transactiontotal:C2}").Bold();
                            });

                            // Commentaire
                            if (!string.IsNullOrWhiteSpace(comment))
                            {
                                x.Item().PaddingTop(10).Column(col =>
                                {
                                    col.Item().Text("Commentaire:").Bold().FontSize(9);
                                    col.Item().Text(comment).FontSize(8).Italic();
                                });
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text("Merci de votre visite!")
                        .FontSize(8);
                });
            })
            .GeneratePdf($"receipt{date:yyyyMMddHHmmss}.pdf");

            return receipt;
        }
    }
}
