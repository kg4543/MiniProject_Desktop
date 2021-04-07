using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace WpfSMSApp.View.Store
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StoreList : Page
    {
        public StoreList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Model.Store> stores = new List<Model.Store>();
                List<Model.StockStore> stockStores = new List<Model.StockStore>();
                stores = Logic.DataAcess.GetStores(); //수영1

                //stores 데이터를 Stockstores로 옮김
                foreach (Model.Store item in stores)
                {
                    var store = new Model.StockStore()
                    {
                        StoreID = item.StoreID,
                        StoreName = item.StoreName,
                        StoreLocation = item.StoreLocation,
                        ItemStatus = item.ItemStatus,
                        TagID = item.TagID,
                        BarcodeID = item.BarcodeID,
                        StockQuantity = 0
                    };

                    store.StockQuantity = Logic.DataAcess.GetStocks().Where(t => t.StoreID.Equals(store.StoreID)).Count();

                    stockStores.Add(store);
                }
                
                this.DataContext = stockStores;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnAddStore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AddStore());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : BtnAddStore_Click : {ex}");
            }
        }

        private void BtnEditStore_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("창고수정", "수정할 창고를 선택하세요");
                return;
            }

            try
            {
                var storeId = (GrdData.SelectedItem as Model.Store).StoreID;
                NavigationService.Navigate(new EditStore(storeId));
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : BtnEditStore_Click : {ex}");
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel File (*xlsx)|*.xlsx";
            dialog.FileName = "";
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    IWorkbook workbook = new XSSFWorkbook(); //new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("StoreList"); //sheet이름 설정
                    IRow rowHeader = sheet.CreateRow(0);
                    ICell cell = rowHeader.CreateCell(0);
                    cell.SetCellValue("순번");
                    cell = rowHeader.CreateCell(1);
                    cell.SetCellValue("창고명");
                    cell = rowHeader.CreateCell(2);
                    cell.SetCellValue("창고위치");
                    cell = rowHeader.CreateCell(3);
                    cell.SetCellValue("재고 수량");

                    for (int i = 0; i < GrdData.Items.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i + 1);
                        if (GrdData.Items[i] is Model.StockStore)
                        {
                            var stockStore = GrdData.Items[i] as Model.StockStore;
                            ICell dataCell = row.CreateCell(0);
                            dataCell.SetCellValue(stockStore.StoreID);
                            dataCell = row.CreateCell(1);
                            dataCell.SetCellValue(stockStore.StoreName);
                            dataCell = row.CreateCell(2);
                            dataCell.SetCellValue(stockStore.StoreLocation);
                            dataCell = row.CreateCell(3);
                            dataCell.SetCellValue(stockStore.StockQuantity);
                        }
                    }

                    //파일저장
                    using (var fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }
                    Commons.ShowMessageAsync("Excel 저장", $"Export 성공");
                }
                catch (Exception ex)
                {
                    Commons.ShowMessageAsync("예외",$"예외처리 : {ex}");
                }
            }
        }
    }
}
