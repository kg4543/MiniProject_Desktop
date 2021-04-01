using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfSMSApp.View.User
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditUser : Page
    {
        public EditUser()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LblUserIdentityNumber.Visibility = LblUserSurName.Visibility =
                LblUserName.Visibility = LblUserEmail.Visibility =
                LblUserPassword.Visibility = LblUserAdmin.Visibility =
                LblUserActivated.Visibility = Visibility.Hidden;

                //콤보박스 초기화
                List<string> comboValues = new List<string>
                {
                    "False", // 0
                    "True" // 1
                };
                CboUserAdmin.ItemsSource = comboValues;
                CboUserActivated.ItemsSource = comboValues;

                TxtUserID.Text = "";

                List<Model.User> users = Logic.DataAcess.GetUsers();
                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 EditUser Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; //입력된 값이 모두 만족하는지 판별하는 플래그

            LblUserIdentityNumber.Visibility = LblUserSurName.Visibility =
                LblUserName.Visibility = LblUserEmail.Visibility =
                LblUserPassword.Visibility = LblUserAdmin.Visibility =
                LblUserActivated.Visibility = Visibility.Hidden;

            var user = GrdData.SelectedItem as Model.User;

            isValid = IsValidInput();

            if (isValid)
            {
                user.UserIdentityNumber = TxtUserIdentityNumber.Text;
                user.UserSurname = TxtUserSurName.Text;
                user.UserName = TxtUserName.Text;
                user.UserEmail = TxtUserEmail.Text;
                user.UserPassword = TxtUserPassword.Password;
                user.UserAdmin = bool.Parse(CboUserAdmin.SelectedValue.ToString());
                user.UserActivated = bool.Parse(CboUserActivated.SelectedValue.ToString());

                try
                {
                    var mdHash = MD5.Create();
                    user.UserPassword = Commons.GetMd5Hash(mdHash, user.UserPassword);

                    var result = Logic.DataAcess.SetUser(user);
                    if (result == 0)
                    {
                        // 수정 안됨
                        LblResult.Text = "계정 수정에 문제가 발생했습니다. 관리자에게 문의바랍니다.";
                        LblResult.Foreground = Brushes.OrangeRed;
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public bool IsValidInput()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(TxtUserIdentityNumber.Text))
            {
                LblUserIdentityNumber.Visibility = Visibility.Visible;
                LblUserIdentityNumber.Text = "사번을 입력하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(TxtUserSurName.Text))
            {
                LblUserSurName.Visibility = Visibility.Visible;
                LblUserSurName.Text = "이름(성)을 입력하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(TxtUserName.Text))
            {
                LblUserName.Visibility = Visibility.Visible;
                LblUserName.Text = "이름을 입력하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(TxtUserEmail.Text))
            {
                LblUserEmail.Visibility = Visibility.Visible;
                LblUserEmail.Text = "이메일을 입력하세요";
                isValid = false;
            }

            else if (!Commons.IsValidEmail(TxtUserEmail.Text))
            {
                LblUserEmail.Visibility = Visibility.Visible;
                LblUserEmail.Text = "이메일 형식이 올바르지 않습니다.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(TxtUserPassword.Password))
            {
                LblUserPassword.Visibility = Visibility.Visible;
                LblUserPassword.Text = "패스워드를 입력하세요";
                isValid = false;
            }

            if (CboUserAdmin.SelectedIndex < 0)
            {
                LblUserAdmin.Visibility = Visibility.Visible;
                LblUserAdmin.Text = "관리자 권한 여부를 선택하세요";
                isValid = false;
            }

            if (CboUserActivated.SelectedIndex < 0)
            {
                LblUserActivated.Visibility = Visibility.Visible;
                LblUserActivated.Text = "활성 권한 여부를 선택하세요";
                isValid = false;
            }

            return isValid;
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var user = GrdData.SelectedItem as Model.User;

                TxtUserID.Text = user.UserID.ToString();
                TxtUserIdentityNumber.Text = user.UserIdentityNumber.ToString();
                TxtUserSurName.Text = user.UserSurname.ToString();
                TxtUserName.Text = user.UserName.ToString();
                TxtUserEmail.Text = user.UserEmail.ToString();
                //TxtUserPassword.Password = user.UserPassword.ToString();
                CboUserAdmin.SelectedIndex = user.UserAdmin == false ? 0 : 1;
                CboUserActivated.SelectedIndex = user.UserActivated == false ? 0 : 1;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 GrdData_SelectedCellsChanged : {ex}");
            }
            
        }
    }
}
