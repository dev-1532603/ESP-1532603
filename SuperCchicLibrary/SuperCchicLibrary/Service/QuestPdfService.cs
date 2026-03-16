using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SuperCchicLibrary.Service
{
    public class QuestPdfService
    {
        // Visuel généré par IA Claude
        public static void PrintReport(MonthlyReportDTO report)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            string filename = $"rapport_mensuel.pdf";

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // En-tête
                    page.Header().Element(ComposeHeader);

                    // Contenu
                    page.Content().Column(col =>
                    {
                        col.Spacing(20);

                        // Section résumé global
                        col.Item().Element(c => ComposeSummary(c, report));

                        // Section par jour de la semaine
                        col.Item().Element(c => ComposeDailyTable(c, report.DailyReports));
                    });

                    // Pied de page
                    page.Footer()
                        .AlignCenter()
                        .Text(t =>
                        {
                            t.Span("Page ");
                            t.CurrentPageNumber();
                            t.Span(" / ");
                            t.TotalPages();
                        });
                });
            });

            document.GeneratePdf(filename);
        }
        static void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item()
                       .Text("Rapport mensuel")
                       .FontSize(20).Bold().FontColor(Colors.Blue.Darken2);

                    col.Item()
                       .Text($"Généré le {DateTime.Now:dd MMMM yyyy}")
                       .FontSize(10).FontColor(Colors.Grey.Darken1);
                });
            });
        }

        static void ComposeSummary(IContainer container, MonthlyReportDTO report)
        {
            container.Column(col =>
            {
                col.Item().Text("Résumé du mois").FontSize(14).Bold().Underline();
                col.Spacing(8);

                col.Item().Row(row =>
                {
                    SummaryCard(row.RelativeItem(), "Ventes totales", $"{report.TotalSales:C}");
                    SummaryCard(row.RelativeItem(), "Nb. transactions", $"{report.TotalOrders}");
                    SummaryCard(row.RelativeItem(), "Valeur moy. / trans.", $"{report.AverageOrderValue:C}");
                });
            });
        }

        static void SummaryCard(IContainer container, string label, string value)
        {
            container
                .Border(1).BorderColor(Colors.Grey.Lighten1)
                .Padding(10)
                .Column(col =>
                {
                    col.Item().Text(label).FontSize(9).FontColor(Colors.Grey.Darken1);
                    col.Item().Text(value).FontSize(15).Bold();
                });
        }

        static void ComposeDailyTable(IContainer container, List<DailyReportDTO> dailyReports)
        {
            var dayNames = new Dictionary<DayOfWeek, string>
            {
                { DayOfWeek.Monday,    "Lundi"    },
                { DayOfWeek.Tuesday,   "Mardi"    },
                { DayOfWeek.Wednesday, "Mercredi" },
                { DayOfWeek.Thursday,  "Jeudi"    },
                { DayOfWeek.Friday,    "Vendredi" },
                { DayOfWeek.Saturday,  "Samedi"   },
                { DayOfWeek.Sunday,    "Dimanche" },
            };

            container.Column(col =>
            {
                col.Item().Text("Détail par jour de la semaine").FontSize(14).Bold().Underline();
                col.Spacing(6);

                col.Item().Table(table =>
                {
                    // Définition des colonnes
                    table.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn(2); // Jour
                        c.RelativeColumn(2); // Ventes totales
                        c.RelativeColumn(2); // Nb transactions
                        c.RelativeColumn(2); // Valeur moyenne
                    });

                    // En-tête du tableau
                    static IContainer HeaderCell(IContainer c) => c
                        .Background(Colors.Blue.Darken2)
                        .Padding(6);

                    table.Header(h =>
                    {
                        h.Cell().Element(HeaderCell).Text("Jour").Bold().FontColor(Colors.White);
                        h.Cell().Element(HeaderCell).Text("Ventes totales").Bold().FontColor(Colors.White);
                        h.Cell().Element(HeaderCell).Text("Nb. transactions").Bold().FontColor(Colors.White);
                        h.Cell().Element(HeaderCell).Text("Valeur moy. / trans.").Bold().FontColor(Colors.White);
                    });

                    // Lignes de données
                    var ordered = dailyReports.OrderBy(d => d.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)d.DayOfWeek);
                    bool alternate = false;

                    foreach (var day in ordered)
                    {
                        var bg = alternate ? Colors.Grey.Lighten3 : Colors.White;
                        alternate = !alternate;

                        IContainer DataCell(IContainer c) => c.Background(bg).Padding(6);

                        table.Cell().Element(DataCell).Text(dayNames[day.DayOfWeek]);
                        table.Cell().Element(DataCell).Text($"{day.TotalSales:C}");
                        table.Cell().Element(DataCell).Text($"{day.TotalOrders}");
                        table.Cell().Element(DataCell).AlignRight().Text($"{day.AverageOrderValue:C}");
                    }
                });
            });
        }

        public static void PrintReceipt(OrderReceipt orderReceipt)
        {
            GenerateReceipt(orderReceipt.items, orderReceipt.comment, orderReceipt.subtotal, orderReceipt.tps, orderReceipt.tvq, orderReceipt.transactiontotal, orderReceipt.date, true);
        }
        // Méthode statique pour générer ET sauvegarder le PDF
        // Visuel généré par IA Claude
        public static OrderReceipt GenerateReceipt(List<OrderDetailDTO> items, string comment, decimal subtotal, decimal tps, decimal tvq, decimal transactiontotal, DateTime date, bool isReprint = false)
        {
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

            string title = isReprint ? "REÇU DE COMMANDE - COPIE" : "REÇU DE COMMANDE";
            string filename = isReprint ? $"receiptcopy{date:yyyyMMddHHmmss}.pdf" : $"receipt{date:yyyyMMddHHmmss}.pdf";

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
                            col.Item().Text(title).Bold().FontSize(16).FontColor(Colors.Blue.Medium);
                            col.Item().Text($"Date: {date:dd/MM/yyyy HH:mm}").FontSize(8);
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
            .GeneratePdf(filename);

            return receipt;
        }
    }
}
