using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleInvoiceV2.Common
{
    public static class UIControl
    {
        public static object GetCurrentDataInGrid(this GridControl grid)
        {
            object result = null;

            // Assuming you are using a framework like DevExpress
            // Access the main view of the GridControl, which is a GridView
            if (grid.MainView is GridView gridView)
            {
                int focusedRowHandle = gridView.FocusedRowHandle;
                if (focusedRowHandle >= 0)
                {
                    result = gridView.GetRow(focusedRowHandle);
                }
            }

            return result;
        }
    }


}
