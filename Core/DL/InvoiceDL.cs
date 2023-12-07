using Core.Config;
using Core.Helper;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DL
{
    public class InvoiceDL
    {

        public static List<SalesInvoices> SearchInVoiceNumber(DateTime tungay, DateTime denngay)
        {
            var lstResult = new List<SalesInvoices>();
            string strSql = string.Format(@"SELECT inv.*
                                    FROM SalesInvoices AS inv                                  
                                    WHERE 
                                     inv.InvoiceDate >= '{0}'
                                    AND inv.InvoiceDate <= '{1}'",
                                             tungay.ToString("yyyy-MM-dd"), denngay.ToString("yyyy-MM-dd"));

            using (var connection = Connection.ConnectToSQLDataBase())
            {
                using (var cmd = new SqlCommand(strSql, connection))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (var dr = cmd.ExecuteReader())
                    {
                        

                        while (dr.Read())
                        {
                            var invoice = DLHelper.CreatDtoFromDataReader(typeof(SalesInvoices), dr) as SalesInvoices;
                           

                            lstResult.Add(invoice);
                            
                        }
                    }
                }
            }

            return lstResult.OrderByDescending(s => s.InvoiceDate).ToList();
        }

        public static List<SalesInvoices> SearchInVoiceItemBySaleInvoice(int invoiceNumber)
        {
            var lstResult = new List<SalesInvoices>();
            string strSql = string.Format(@"SELECT inv.*
                                    FROM InvoiceItems AS inv                                  
                                    WHERE 
                                     inv.InvoiceNumber = {0}", invoiceNumber);


            using (var connection = Connection.ConnectToSQLDataBase())
            {
                using (var cmd = new SqlCommand(strSql, connection))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (var dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            var invoice = DLHelper.CreatDtoFromDataReader(typeof(SalesInvoices), dr) as SalesInvoices;


                            lstResult.Add(invoice);

                        }
                    }
                }
            }

            return lstResult.OrderByDescending(s => s.InvoiceDate).ToList();
        }

    }
}
