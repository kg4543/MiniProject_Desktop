﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfSMSApp.View.Store
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddStore : Page
    {
        public AddStore()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LblUserIdentityNumber.Visibility = LblUserSurName.Visibility = Visibility.Hidden;

                //콤보박스 초기화
                List<string> comboValues = new List<string>
                {
                    "False", // 0
                    "True" // 1
                };

                TxtUserID.Text = "";

            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 AddStore Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; //입력된 값이 모두 만족하는지 판별하는 플래그

            LblUserIdentityNumber.Visibility = LblUserSurName.Visibility = Visibility.Hidden;

            var user = new Model.User();

            isValid = IsValidInput();

            if (isValid)
            {
                user.UserIdentityNumber = TxtUserIdentityNumber.Text;
                user.UserSurname = TxtUserSurName.Text;

                try
                {
                    var mdHash = MD5.Create();
                    user.UserPassword = Commons.GetMd5Hash(mdHash, user.UserPassword);

                    var result = Logic.DataAcess.SetUser(user);
                    if (result == 0)
                    {
                        // 수정 안됨
                    }
                    else
                    {
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
            else
            {
                var cnt = Logic.DataAcess.GetUsers().Where(u => u.UserIdentityNumber.Equals(TxtUserIdentityNumber.Text)).Count();
                if (cnt > 0)
                {
                    LblUserIdentityNumber.Visibility = Visibility.Visible;
                    LblUserIdentityNumber.Text = "이미 등록된 사번입니다.";
                    isValid = false;
                }
            }

            if (string.IsNullOrEmpty(TxtUserSurName.Text))
            {
                LblUserSurName.Visibility = Visibility.Visible;
                LblUserSurName.Text = "이름(성)을 입력하세요";
                isValid = false;
            }

            return isValid;
        }
    }
}