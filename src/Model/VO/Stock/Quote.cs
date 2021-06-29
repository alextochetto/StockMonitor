using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.VO.Stock
{
    public class Quote
    {
        public int Code { get; set; }
        public string Tick { get; set; }
        public string CompanyName { get; set; }
        public decimal Value { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string TickUrl
        {
            get
            {
                return $"{this.Tick}.SA";
            }
        }
        public string GetTickForUrl()
        {
            return $"{this.Tick}.SA";
        }
    }
}