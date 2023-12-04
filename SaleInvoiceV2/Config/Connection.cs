using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleInvoiceV2.Config
{
    public class Connection
    {
        public static SqlConnection ConnectToSQLDataBase()
        {
            string connetionString = @"Data Source=DESKTOP-LOCO2LM;Initl Catalog=SaleInvoice;Integrated Security=True";
            SqlConnection cnnSql = new SqlConnection(connetionString);
            cnnSql.Open();
            return cnnSql;
        }
    }
}
