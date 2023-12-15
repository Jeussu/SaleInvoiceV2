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

        public static List<InvoiceItems> SearchInVoiceItemBySaleInvoice(string invoiceNumber)
        {
            var lstResult = new List<InvoiceItems>();
            string strSql = string.Format(@"SELECT inv.Id, inv.InvoiceNumber, inv.ProductID, inv.ProductName, 
                                           inv.Quantity, inv.UnitPrice, inv.IntoMoney, inv.TotalMoney
                                    FROM InvoiceItems AS inv                                  
                                    WHERE inv.InvoiceNumber = '{0}'", invoiceNumber);

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
                            var invoiceItem = DLHelper.CreatDtoFromDataReader(typeof(InvoiceItems), dr) as InvoiceItems;
                            lstResult.Add(invoiceItem);
                        }
                    }
                }
            }

            return lstResult; // Removed the OrderByDescending as InvoiceItems may not have an InvoiceDate field
        }
        public static string GetLastInvoiceNumber()
        {
            string lastInvoiceNumber = "INV000000"; // Default if no invoices are found
            string query = "SELECT MAX(InvoiceNumber) FROM SalesInvoices";

            using (var connection = Connection.ConnectToSQLDataBase())
            {
                using (var cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            lastInvoiceNumber = Convert.ToString(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine("Error fetching last invoice number: " + ex.Message);
                    }
                }
            }

            return lastInvoiceNumber;
        }



        //public static bool DeleteInvoiceItemsByInvoiceNumber(int invoiceNumber)
        //{
        //    string strSql = "DELETE FROM InvoiceItems WHERE InvoiceNumber = @InvoiceNumber";

        //    using (var connection = Connection.ConnectToSQLDataBase())
        //    {
        //        using (var cmd = new SqlCommand(strSql, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);

        //            if (connection.State != ConnectionState.Open)
        //            {
        //                connection.Open();
        //            }

        //            return cmd.ExecuteNonQuery() > 0;
        //        }
        //    }
        //}

        //public static bool DeleteInvoice(int invoiceId)
        //{
        //    string strSql = "DELETE FROM SalesInvoices WHERE Id = @InvoiceId";

        //    using (var connection = Connection.ConnectToSQLDataBase())
        //    {
        //        using (var cmd = new SqlCommand(strSql, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

        //            if (connection.State != ConnectionState.Open)
        //            {
        //                connection.Open();
        //            }

        //            return cmd.ExecuteNonQuery() > 0;
        //        }
        //    }
        //}

    }
}
