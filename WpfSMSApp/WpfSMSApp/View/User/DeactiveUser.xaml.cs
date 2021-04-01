using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfSMSApp.View.User
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeactiveUser : Page
    {
        public DeactiveUser()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
                //콤보박스 초기화
                List<string> comboValues = new List<string>
                {
                    "False", // 0
                    "True" // 1
                };
               
                List<Model.User> users = Logic.DataAcess.GetUsers();
                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 DeactiveUser Loaded : {ex}");
                throw ex;
            }
        }

        private async void BtnDeactive_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;

            if (GrdData.SelectedItem == null)
            {
                await Commons.ShowMessageAsync("오류","비활성화할 사용자를 선택하세요");
                return;
            }

            if (isValid)
            {
                try
                {
                    var user = GrdData.SelectedItem as Model.User;
                    user.UserActivated = false;

                    var result = Logic.DataAcess.SetUser(user);
                    if (result == 0)
                    {
                        await Commons.ShowMessageAsync("오류","사용자 수정에 실패하였습니다.");
                    }
                    else
                    {
                        NavigationService.Navigate(new UserList());
                    }
                }
                catch (Exception ex)
                {
                    // throw ex;
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }

        private Task ShowMessageAsync()
        {
            throw new NotImplementedException();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
            

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var user = GrdData.SelectedItem as Model.User;

            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 GrdData_SelectedCellsChanged : {ex}");
            }
            
        }
    }
}
