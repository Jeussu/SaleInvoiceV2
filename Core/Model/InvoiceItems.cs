﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public partial class InvoiceItems
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal IntoMoney { get; set; }
        public decimal TotalMoney { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime InsertTime { get; set; }
        
    }
}
