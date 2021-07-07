using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DT.Stock
{
    public class StockQuoteSaveDTQ
    {
        public int? Code { get; set; }
        public string Tick { get; set; }
        public string CompanyName { get; set; }
    }
}