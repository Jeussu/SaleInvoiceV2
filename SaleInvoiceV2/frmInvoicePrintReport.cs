using Core.Model;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace SaleInvoiceV2
{
    public partial class frmInvoicePrintReport : DevExpress.XtraReports.UI.XtraReport
    {
        public frmInvoicePrintReport()
        {
            InitializeComponent();            
        }

        //public void SetDataSource(IEnumerable<SalesInvoices> dataSource)
        //{
        //    this.DataSource = dataSource;
        //}

        //public void SetDetailDataSource(IEnumerable<InvoiceItems> detailData)
        //{
        //    this.DataSource = detailData;
        //}

        public void SetData(SalesInvoices master, List<InvoiceItems> details) 
        {
            var dataSource = new MauInHoaDon();
            dataSource.master = master;
            dataSource.Details = details;
            var lstSourece = new List<MauInHoaDon>();
            lstSourece.Add(dataSource);
            bsShowHoaDon.DataSource = lstSourece;
        }
    }

    public class MauInHoaDon
    {
        public SalesInvoices master;
        public List<InvoiceItems> Details;
        public MauInHoaDon()
        {
            master = new SalesInvoices();
            Details = new List<InvoiceItems>();
        }
    }
}

