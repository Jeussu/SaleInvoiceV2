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

        }
        //Convert.ToInt32(txtInvoiceNumber.EditValue);
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {   
                    var frDate = dteFromday.DateTime;
                    var toDate = dtetoday.DateTime;
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
    }
}
