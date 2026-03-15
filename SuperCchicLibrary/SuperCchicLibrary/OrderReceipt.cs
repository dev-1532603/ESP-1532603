using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SuperCchicLibrary
{
    public class OrderReceipt
    {
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
       
    }
}
