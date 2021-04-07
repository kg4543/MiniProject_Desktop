using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfSMSApp.View.Store
{
    
    public partial class EditStore : Page
    {
        public EditStore()
        {
            InitializeComponent();
        }

        public int StoreID { get; set; }
        private Model.Store CurrentStore { get; set; }

        /// <summary>
        /// 추가생성자
        /// StoreList에서 storeId를 받아옴
        /// </summary>
        /// <param name="storeId"></param>
        public EditStore(int storeId) : this()
        {
            StoreID = storeId; 
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
                LblStoreName.Visibility = LblStoreLocation.Visibility = Visibility.Hidden;
                TxtStoreID.Text = TxtStoreName.Text = TxtStoreLocation.Text = "";

            try
            {
                //store테이블에서 내용읽음
                CurrentStore = Logic.DataAcess.GetStores().Where(s => s.StoreID.Equals(StoreID)).FirstOrDefault();
                TxtStoreID.Text = CurrentStore.StoreID.ToString();
                TxtStoreName.Text = CurrentStore.StoreName;
                TxtStoreLocation.Text = CurrentStore.StoreLocation;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"EditStore.xaml.cs Page_Loaded 예외발생 : {ex}");
                Commons.ShowMessageAsync("예외",$"예외발생 : {ex}");
            }
        }
        
        private bool isValid = true; //입력된 값이 모두 만족하는지 판별하는 플래그

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            LblStoreName.Visibility = LblStoreLocation.Visibility = Visibility.Hidden;

            isValid = IsValidInput(); // 유효성 체크

            if (isValid)
            {
                CurrentStore.StoreName = TxtStoreName.Text;
                CurrentStore.StoreLocation = TxtStoreLocation.Text;

                try
                {
                    var result = Logic.DataAcess.SetStore(CurrentStore);
                    if (result == 0)
                    {
                        Commons.LOGGER.Error("AddStore.xaml.cs 창고정보 저장오류 발생");
                        Commons.ShowMessageAsync("오류", "저장 시 오류가 발생했습니다.");
                    }
                    else
                    {
                        NavigationService.Navigate(new StoreList());
                    }
                }
                catch (Exception ex)
                {
                    // throw ex;
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public bool IsValidInput()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(TxtStoreName.Text))
            {
                LblStoreName.Visibility = Visibility.Visible;
                LblStoreName.Text = "창고명을 입력하세요";
                isValid = false;
            }
            /*else
            {
                var cnt = Logic.DataAcess.GetStores().Where(u => u.StoreName.Equals(TxtStoreName.Text)).Count();
                if (cnt > 0)
                {
                    LblStoreName.Visibility = Visibility.Visible;
                    LblStoreName.Text = "이미 등록된 창고명입니다.";
                    isValid = false;
                }
            }*/

            if (string.IsNullOrEmpty(TxtStoreLocation.Text))
            {
                LblStoreLocation.Visibility = Visibility.Visible;
                LblStoreLocation.Text = "창고 위치를 입력하세요";
                isValid = false;
            }

            return isValid;
        }

        private void TxtStoreName_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput();
        }

        private void TxtStoreLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput();
        }
    }
}
