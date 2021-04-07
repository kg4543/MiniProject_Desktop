using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;
using WpfSMSApp.View;
using WpfSMSApp.View.Account;
using WpfSMSApp.View.Store;
using WpfSMSApp.View.User;

namespace WpfSMSApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            ShowLoginView();
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            if (Commons.LOGINED_USER != null)
                BtnLoginedID.Content = $"{Commons.LOGINED_USER.UserEmail} ({Commons.LOGINED_USER.UserName})";
        }

        private async void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            //todo: 로그아웃 시 모든화면 해제

            var result = await this.ShowMessageAsync("로그아웃","로그아웃하시겠습니까?",
                                                      MessageDialogStyle.AffirmativeAndNegative,null);
            
            if (result == MessageDialogResult.Affirmative)
            {
                Commons.LOGINED_USER = null; //로그인했던 사용자정보 초기화
                ShowLoginView();
            }
        }

        private void ShowLoginView()
        {
            LoginView view = new LoginView();
            view.Owner = this;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.ShowDialog();
        }

        private async void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new MyAccount();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAccount_Click : {ex}");
                var result = await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new UserList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAccount_Click : {ex}");
                var result = await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void BtnStore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new StoreList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAccount_Click : {ex}");
                var result = await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                e.Cancel = true;
                var result = await this.ShowMessageAsync("종료", "종료하시겠습니까?"
                                                     , MessageDialogStyle.AffirmativeAndNegative, null);

                if (result == MessageDialogResult.Affirmative)
                {
                    Commons.LOGGER.Info("프로그램 종료");
                    Application.Current.Shutdown(); //프로그램 종료
                }
        }
    }
}
