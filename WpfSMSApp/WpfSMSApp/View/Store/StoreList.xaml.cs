using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
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
                List<Model.Stock> stocks = new List<Model.Stock>();
                List<Model.StockStore> stockStores = new List<Model.StockStore>();
                stores = Logic.DataAcess.GetStores(); //수영1
                stocks = Logic.DataAcess.GetStocks();

                //stores 데이터를 Stockstores로 옮김
                foreach (var item in stores)
                {
                    stockStores.Add(new Model.StockStore()
                    {
                        StoreID = item.StoreID,
                        StoreName = item.StoreName,
                        StoreLocation = item.StoreLocation,
                        ItemStatus = item.ItemStatus,
                        TagID = item.TagID,
                        BarcodeID = item.BarcodeID,
                        StockQuantity = 0
                    });
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
            try
            {
                //NavigationService.Navigate(new EditStore());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : BtnEditStore_Click : {ex}");
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
