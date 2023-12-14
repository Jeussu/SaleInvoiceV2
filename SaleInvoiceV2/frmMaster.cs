using Core.Config;
using Core.DL;
using Core.Helper;
using Core.Model;
using DevExpress.XtraReports.UI;
using SaleInvoiceV2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SaleInvoiceV2
{
    public partial class frmMaster : DevExpress.XtraEditors.XtraForm
    {
        public frmMaster()
        {
            InitializeComponent();
        }

        private void btnThemMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var frm = new frmDetails();
                if (frm.ShowDialog() == DialogResult.OK) // move to new form
                {
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lstAll = grcMaster.DataSource as List<SalesInvoices>;// add grcMaster to datasource as List<>
                var dtoSelect = UIControl.GetCurrentDataInGrid(grcMaster) as SalesInvoices; // declare dto = assign and call GetCurrentDataInGrid method to get data from grcMaster in SalesInvoices 
                if (dtoSelect == null)
                {// if dto null show message
                    MessageHelper.ShowError("Vui lòng chọn ít nhất một dòng để xóa");
                    return;
                }
               
                DeleteSalesInvoices(dtoSelect); // call method to delete data from master
                DeleteInvoiceItem(dtoSelect); // call method to delete data from details
                btnTimKiem_Click(null,null); // after delete refresh data in gridview by call search method
              
            }
            catch (Exception ex)
            {

                MessageHelper.ShowException(ex);
            }
        }
        

        public static bool DeleteSalesInvoices(SalesInvoices item)
        {
            string sqlDelete = $"DELETE FROM SalesInvoices WHERE Id = @Id"; // delete query sql by Id

            using (var connection = Connection.ConnectToSQLDataBase()) // command sql
            {
                using (var cmd = new SqlCommand(sqlDelete, connection)) // opens a connection to the database. 
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id); // adds a parameter to the SQL command
                    connection.Open(); // opens the database connection to execute the command.
                    return cmd.ExecuteNonQuery() > 0; // It returns the number of rows affected.
                }
            }
        }

        public static bool DeleteInvoiceItem(SalesInvoices item)
        {
            string sqlDelete = $"DELETE FROM InvoiceItems WHERE InvoiceNumber = @InvoiceNumber"; // delete query sql by InvoiceNumber 
            using (var connection = Connection.ConnectToSQLDataBase())
            {
                using (var cmd = new SqlCommand(sqlDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@InvoiceNumber", item.InvoiceNumber);
                    connection.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //Convert.ToInt32(txtInvoiceNumber.EditValue);
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                var frDate = dteFromday.DateTime.Date;
                var toDate = dtetoday.DateTime.Date;// these lines retrieve the date values from two date-edit controls('dteFromday' and 'dtetoday')
                var lstSaleInvoice = InvoiceDL.SearchInVoiceNumber(frDate, toDate) as List<SalesInvoices>;
                //var lstInvoiceItems = InvoiceDL.SearchInVoiceNumber(frDate, toDate, invoiceNumber) as List<InvoiceItems>;
                grcMaster.DataSource = lstSaleInvoice;
                grcMaster.RefreshDataSource();
                gridView1_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dtoSelect = UIControl.GetCurrentDataInGrid(grcMaster) as SalesInvoices;
            var lstSaleInvoice = new List<InvoiceItems>();
            if (dtoSelect == null)
            {
                grcDetails.DataSource = lstSaleInvoice;
                grcDetails.RefreshDataSource();
                return;
            }
            lstSaleInvoice = InvoiceDL.SearchInVoiceItemBySaleInvoice(dtoSelect.InvoiceNumber);

            //var lstInvoiceItems = InvoiceDL.SearchInVoiceNumber(frDate, toDate, invoiceNumber) as List<InvoiceItems>;
            grcDetails.DataSource = lstSaleInvoice;
            grcDetails.RefreshDataSource();

        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }
        private void OpenEditForm()
        {
            SalesInvoices selectedInvoice = GetSelectedInvoice(); // Method to get the selected invoice
            if (selectedInvoice != null)
            {
                frmEdit editForm = new frmEdit(selectedInvoice);
                editForm.ShowDialog();
                //grcMaster.RefreshDataSource();
            }
            else
            {
                MessageBox.Show("Please select an invoice to edit.");
            }
        }



        private SalesInvoices GetSelectedInvoice()
        {
            
            if (grcMaster.MainView is DevExpress.XtraGrid.Views.Grid.GridView view && view.SelectedRowsCount > 0)
            {
                // Get the handle of the first selected row
                int selectedRowHandle = view.GetSelectedRows()[0];

                // Fetch the SalesInvoices object from the selected row
                var selectedInvoice = view.GetRow(selectedRowHandle) as SalesInvoices;

                return selectedInvoice;
            }

            return null; // Return null if no row is selected
        }

        private SalesInvoices GetSelectedInvoice1()
        {
            
            if (grcMaster.MainView is DevExpress.XtraGrid.Views.Grid.GridView view && view.SelectedRowsCount > 0)
            {
                
                int selectedRowHandle = view.GetSelectedRows()[0];

                
                var selectedInvoice = view.GetRow(selectedRowHandle) as SalesInvoices;

                
                if (selectedInvoice != null)
                {
                    return selectedInvoice; 
                }
            }

            return null; 
        }


        private void grcMaster_Click(object sender, EventArgs e)
        {

        }

        private void grcDetails_Click(object sender, EventArgs e)
        {

        }

        private void txtCustomerID_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtetoday_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dteFromday_EditValueChanged(object sender, EventArgs e)
        {

        }

        

        private void frmMaster_Load(object sender, EventArgs e)
        {
            dteFromday.DateTime = DateTime.Now;
            dtetoday.DateTime = DateTime.Now;
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenEditForm();
            
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // Retrieve the selected invoice
                SalesInvoices selectedInvoice = GetSelectedInvoice1();
                if (selectedInvoice == null)
                {
                    MessageBox.Show("Please select an invoice to print.");
                    return;
                }

                // Optionally, retrieve the invoice details if needed
                var invoiceDetails = InvoiceDL.SearchInVoiceItemBySaleInvoice(selectedInvoice.InvoiceNumber);

                
                var report = new frmInvoicePrintReport();

                
                report.SetData(selectedInvoice, invoiceDetails); // For the master data
                //report.SetDetailDataSource(invoiceDetails); // For the detail data
                
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error occurred while printing: " + ex.Message);
            }
        }        
    }
}