using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Model
{
    public partial class SalesInvoices_MasterModel
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime InserTime { get; set; }
        
    }
}
