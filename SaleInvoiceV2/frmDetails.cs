using Core.Helper;
using Core.Model;
using DevExpress.Skins;
using SaleInvoiceV2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleInvoiceV2
{
    public partial class frmDetails : Form
    {
        public frmDetails()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }

        private void frmDetails_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {

                MessageHelper.ShowException(ex);
            }
        }
        List<InvoiceItems> lstInvoice = new List<InvoiceItems>();
        private bool ValidateUI()
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageHelper.ShowError("Vui lòng nhập mã sản phẩm.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageHelper.ShowError("Vui lòng nhập tên sản phẩm.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageHelper.ShowError("Vui lòng nhập số lượng sản phẩm.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtUnitPrice.Text))
            {
                MessageHelper.ShowError("Vui lòng nhập đơn giá.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtIntoMoney.Text))
            {
                MessageHelper.ShowError("Vui lòng nhập thành tiền.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTotalMoney.Text))
            {
                MessageHelper.ShowError("Vui lòng nhập tổng tiền.");
                return false;
            }
            return true;
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lstAll = grcInvoiceDetail.DataSource as List<InvoiceItems>;
                var dtoSelect = UIControl.GetCurrentDataInGrid(grcInvoiceDetail) as InvoiceItems;
                if (dtoSelect == null)
                {
                    MessageHelper.ShowError("Vui lòng chọn ít nhất một dòng để xóa.");
                    return;
                }
                lstAll.Remove(dtoSelect);
                grcInvoiceDetail.DataSource = lstAll;
                grcInvoiceDetail.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateUI())
                {
                    return;
                }
                var dto = new InvoiceItems();//
                dto.ProductID = Convert.ToInt32(txtProductID.Text);
                dto.ProductName = txtProductName.Text;
                dto.Quantity = Convert.ToInt32(txtQuantity.EditValue);
                dto.UnitPrice = Convert.ToDecimal(txtQuantity.EditValue);
                dto.IntoMoney = Convert.ToDecimal(txtIntoMoney.EditValue);
                dto.TotalMoney = Convert.ToDecimal(txtTotalMoney.EditValue);

                lstInvoice.Add(dto);

                txtProductID.EditValue = null;
                txtProductName.Text = null;
                txtQuantity.EditValue = null;
                txtUnitPrice.EditValue = null;
                txtIntoMoney.EditValue = null;
                txtTotalMoney.EditValue = null;

                grcInvoiceDetail.DataSource = lstInvoice;
                grcInvoiceDetail.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }

        private void txtUnitPrice_Validated(object sender, EventArgs e)
        {
            try
            {
                txtTotalMoney.EditValue = 0;
                txtIntoMoney.EditValue = 0;
                if (string.IsNullOrWhiteSpace(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                {
                    return;
                }
                txtIntoMoney.EditValue = Convert.ToInt32(txtQuantity.EditValue) * Convert.ToInt32(txtUnitPrice.EditValue);
                txtTotalMoney.EditValue = txtIntoMoney.EditValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lstInvoice.IsNullOrEmpty())
                {
                    MessageHelper.ShowError("Vui lòng nhập hóa đơn để lưu");
                    return;
                }

                // Save Master Invoice Information

                var masterItem = new SalesInvoices();
                masterItem.InvoiceNumber = Convert.ToInt32(txtInvoiceNumber.EditValue);
                masterItem.InvoiceDate = dteNgayBaoCao.DateTime.Date;
                masterItem.CustomerId = Convert.ToInt32(txtCustomerID.EditValue);
                masterItem.CustomerName = txtCustomerName.Text.Trim();
                masterItem.Address = txtAddress.Text.Trim();
                masterItem.InsertDate = DateTime.Now;
                masterItem.InsertTime = DateTime.Now;

                DLHelper.Insert(masterItem);

                // Save Invoice Details
                foreach (var item in lstInvoice)
                {
                    item.InvoiceNumber = Convert.ToInt32(txtInvoiceNumber.EditValue);

                    item.InsertDate = DateTime.Now;
                    item.InsertTime = DateTime.Now;

                    DLHelper.Insert(item);
                }


                MessageHelper.ShowInfomation("Thực hiện thành công.");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }

    }
}
