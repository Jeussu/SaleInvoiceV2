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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                throw new Exception();
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }
    }
}
