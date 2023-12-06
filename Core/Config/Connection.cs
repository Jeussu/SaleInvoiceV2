using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class Connection
    {
        public static SqlConnection ConnectToSQLDataBase()
        {
            string connetionString = @"Data Source=DESKTOP-LOCO2LM;Initial Catalog=SaleInvoice;Integrated Security=True";
            SqlConnection cnnSql = new SqlConnection(connetionString);
            cnnSql.Close();
            return cnnSql;
        }
    }
}


//DESKTOP-LOCO2LM
//SaleInvoice
//Integrated Security=True