using Core.DL;
using Core.Helper;
using Core.Model;
using SaleInvoiceV2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                if (frm.ShowDialog() == DialogResult.OK)
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
                    var lstAll = grcMaster.DataSource as List<SalesInvoices>;
                    var dtoSelect = UIControl.GetCurrentDataInGrid(grcMaster) as SalesInvoices;
                    if (dtoSelect == null)
                    {
                        MessageHelper.ShowError("Vui lòng chọn ít nhất một dòng để xóa.");
                        return;
                    }
                    lstAll.Remove(dtoSelect);
                    grcMaster.DataSource = lstAll;
                    grcMaster.RefreshDataSource();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex);
                }
            
        }
        //Convert.ToInt32(txtInvoiceNumber.EditValue);
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {   
                    var frDate = dteFromday.DateTime.Date;
                    var toDate = dtetoday.DateTime.Date;
                    var lstSaleInvoice = InvoiceDL.SearchInVoiceNumber(frDate, toDate) as List<SalesInvoices>;
                    //var lstInvoiceItems = InvoiceDL.SearchInVoiceNumber(frDate, toDate, invoiceNumber) as List<InvoiceItems>;
                    grcMaster.DataSource = lstSaleInvoice;
            
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
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
                
            }
            else
            {
                MessageBox.Show("Please select an invoice to edit.");
            }
        }



        private SalesInvoices GetSelectedInvoice()
        {
            // Check if there is any row selected in the grcMaster grid
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dtoSelect = UIControl.GetCurrentDataInGrid(grcMaster) as SalesInvoices;
            if (dtoSelect == null)
            {
                return;
            }
            var lstSaleInvoice = InvoiceDL.SearchInVoiceItemBySaleInvoice(dtoSelect.InvoiceNumber);
            
            //var lstInvoiceItems = InvoiceDL.SearchInVoiceNumber(frDate, toDate, invoiceNumber) as List<InvoiceItems>;
            grcDetails.DataSource = lstSaleInvoice;

        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            dteFromday.DateTime = DateTime.Now;
            dtetoday.DateTime = DateTime.Now;
        }
        
        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenEditForm();
            //RefreshData(); // Refresh the data in frmMaster to reflect changes
        }
    }
} 
