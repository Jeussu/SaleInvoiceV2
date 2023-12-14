using Core.Config;
using Core.DL;
using Core.Helper;
using Core.Model;
using DevExpress.Skins;
using SaleInvoiceV2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleInvoiceV2
{
    public partial class frmEdit : Form
    {
        private SalesInvoices _invoiceToEdit; // Private field to hold the invoice data
        private List<InvoiceItems> deletedItems = new List<InvoiceItems>();
        public frmEdit(SalesInvoices invoiceToEdit)
        {
            InitializeComponent();
            _invoiceToEdit = invoiceToEdit;
            _invoiceToEdit.InvoiceNumber.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void frmEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (_invoiceToEdit != null)
                {
                    // Populate form fields with data from _invoiceToEdit
                    txtInvoiceNumber.Text = _invoiceToEdit.InvoiceNumber;
                    dteNgayBaoCao.DateTime = _invoiceToEdit.InvoiceDate;
                    txtCustomerID.EditValue = _invoiceToEdit.CustomerID;
                    txtCustomerName.Text = _invoiceToEdit.CustomerName;
                    txtAddress.Text = _invoiceToEdit.Address;

                    // Load InvoiceItems related to this invoice
                    //LoadInvoiceItems(_invoiceToEdit.InvoiceNumber);
                    lstInvoice = InvoiceDL.SearchInVoiceItemBySaleInvoice(_invoiceToEdit.InvoiceNumber);
                    grcInvoiceDetail.DataSource = lstInvoice;
                    grcInvoiceDetail.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }

        private void LoadInvoiceItems(string invoiceNumber)
        {
            // Fetch InvoiceItems from the database or data source
            var invoiceItems = InvoiceDL.SearchInVoiceItemBySaleInvoice(invoiceNumber);

            // Bind the fetched items to grcInvoiceDetail
            grcInvoiceDetail.DataSource = invoiceItems;
            grcInvoiceDetail.RefreshDataSource();
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




        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateUI())
                {
                    return;
                }
                // Create and populate a new InvoiceItems object
                var dto = new InvoiceItems();//
                dto.ProductID = Convert.ToInt32(txtProductID.Text);
                dto.ProductName = txtProductName.Text;
                dto.Quantity = Convert.ToInt32(txtQuantity.EditValue);
                dto.UnitPrice = Convert.ToDecimal(txtQuantity.EditValue);
                dto.IntoMoney = Convert.ToDecimal(txtIntoMoney.EditValue);
                dto.TotalMoney = Convert.ToDecimal(txtTotalMoney.EditValue);
                // Add the new item to lstInvoice and refresh grcInvoiceDetail
                lstInvoice.Add(dto);

                txtProductID.EditValue = null;
                txtProductName.Text = null;
                txtQuantity.EditValue = null;
                txtUnitPrice.EditValue = null;
                txtIntoMoney.EditValue = null;
                txtTotalMoney.EditValue = null;

                grcInvoiceDetail.DataSource = null;
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

        private SalesInvoices CreateInvoiceFromFormFields()
        {
            var newInvoice = new SalesInvoices
            {
                InvoiceNumber = txtInvoiceNumber.Text,
                InvoiceDate = dteNgayBaoCao.DateTime != DateTime.MinValue ? dteNgayBaoCao.DateTime : DateTime.Now,
                CustomerID = Convert.ToInt32(txtCustomerID.EditValue),
                CustomerName = txtCustomerName.Text,
                Address = txtAddress.Text,
                InsertDate = DateTime.Now,
                InsertTime = DateTime.Now
                // Add other fields as necessary
            };

            return newInvoice;
        }


        private void UpdateInvoiceFromFormFields(SalesInvoices invoice)
        {
            invoice.InvoiceNumber = Convert.ToString(txtInvoiceNumber.Text);
            invoice.InvoiceDate = dteNgayBaoCao.DateTime != DateTime.MinValue ? dteNgayBaoCao.DateTime : DateTime.Now;
            invoice.CustomerID = Convert.ToInt32(txtCustomerID.EditValue);
            invoice.CustomerName = txtCustomerName.Text;
            invoice.Address = txtAddress.Text;
            invoice.InsertDate = DateTime.Now;
            invoice.InsertTime = DateTime.Now;
            // Add other fields as necessary
        }


        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lstAll = grcInvoiceDetail.DataSource as List<InvoiceItems>;
                var dtoSelect = UIControl.GetCurrentDataInGrid(grcInvoiceDetail) as InvoiceItems;
                if (dtoSelect == null)
                {
                    MessageHelper.ShowError("Vui lòng chọn ít nhất một dòng để xóa");
                    return;
                }
                lstAll.Remove(dtoSelect);

                // Refresh the GridControl with the updated list
                grcInvoiceDetail.DataSource = lstAll;
                grcInvoiceDetail.RefreshDataSource();
                deletedItems.Add(dtoSelect);
            }

            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }


        public static bool DeleteInvoiceItem(InvoiceItems item)
        {
            string sqlDelete = $"DELETE FROM InvoiceItems WHERE Id = @Id";

            using (var connection = Connection.ConnectToSQLDataBase())
            {
                using (var cmd = new SqlCommand(sqlDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }





        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (_invoiceToEdit != null)
                {
                    UpdateInvoiceFromFormFields(_invoiceToEdit);
                    DLHelper.Update(_invoiceToEdit); // Update the invoice
                }
                else
                {
                    var newInvoice = CreateInvoiceFromFormFields();
                    DLHelper.Insert(newInvoice); // Insert new invoice
                }

                // Save InvoiceItems
                foreach (var item in lstInvoice)
                {
                    item.InvoiceNumber = Convert.ToString(txtInvoiceNumber.Text);
                    if (item.InsertDate < new DateTime(1753, 1, 1))
                        item.InsertDate = DateTime.Now;
                    if (item.InsertTime < new DateTime(1753, 1, 1))
                        item.InsertTime = DateTime.Now;

                    if (item.Id == 0) // Assuming '0' or 'null' means the item is new
                        DLHelper.Insert(item);
                    else
                        DLHelper.Update(item);
                }

                // Delete removed items
                foreach (var item in deletedItems)
                {
                    DLHelper.DeleteInvoiceItem(item);

                }

                MessageHelper.ShowInfomation("Thực hiện thành công.");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex);
            }
        }


        private void grcInvoiceDetail_Click(object sender, EventArgs e)
        {

        }
    }
}

