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


namespace WpfSMSApp.View.User
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserList : Page
    {
        public UserList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RdoAll.IsChecked = true;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 UserList Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AddUser());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : BtnAddUser_Click : {ex}");
            }
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new EditUser());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : BtnEditUser_Click : {ex}");
            }
        }

        private void BtnDeactivateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new DeactiveUser());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : BtnDeactiveUser_Click : {ex}");
            }
        }

        private void BtnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF File(*pdf)|*.pdf";
            saveDialog.FileName = "";
            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    //0. PDF 사용 폰트 설정
                    string nanumPath = Path.Combine(Environment.CurrentDirectory, @"NanumGothic.ttf"); // Font 경로;
                    BaseFont nanumBase = BaseFont.CreateFont(nanumPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var nanumTitle = new iTextSharp.text.Font(nanumBase, 20f); // 20 타이틀용 나눔폰트;
                    var nanumContent = new iTextSharp.text.Font(nanumBase, 12f); // 12 컨텐츠용 나눔폰트;
                    
                    string pdfFilePath = saveDialog.FileName;
                    Document pdfDoc = new Document(PageSize.A4);

                    // 1. PDF 생성
                    PdfPTable pdfPTable = new PdfPTable(GrdData.Columns.Count);

                    // 2. PDF 내용 만들기
                    Paragraph title = new Paragraph("재고 관리 시스템(SMS) \n", nanumTitle);
                    Paragraph subtitle = new Paragraph($"사용자 리스트 Export : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n\n", nanumContent);
                    PdfPTable pdfTable = new PdfPTable(GrdData.Columns.Count);
                    pdfTable.WidthPercentage = 100; // 전체 사이즈

                    // 그리드 헤더 작업
                    foreach (DataGridColumn column in GrdData.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString(), nanumContent));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        pdfTable.AddCell(cell);
                    }

                    float[] columnWidth = new float[] { 7f, 15f, 10f, 15f, 28f, 12f, 10f };
                    pdfTable.SetWidths(columnWidth);

                    // 그리드 Row 작업
                    foreach (var item in GrdData.Items)
                    {
                        if (item is Model.User)
                        {
                            var temp = item as Model.User;
                            PdfPCell cell = new PdfPCell(new Phrase(temp.UserID.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            pdfTable.AddCell(cell);

                            cell = new PdfPCell(new Phrase(temp.UserIdentityNumber.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);

                            cell = new PdfPCell(new Phrase(temp.UserSurname.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);

                            cell = new PdfPCell(new Phrase(temp.UserName.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);

                            cell = new PdfPCell(new Phrase(temp.UserEmail.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);

                            cell = new PdfPCell(new Phrase(temp.UserAdmin.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);

                            cell = new PdfPCell(new Phrase(temp.UserActivated.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);
                        }
                    }

                    // 3. PDF 파일생성
                    using (FileStream stream = new FileStream(pdfFilePath, FileMode.OpenOrCreate))
                    {
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        // 2번에서 만들 내용 추가
                        pdfDoc.Add(title);
                        pdfDoc.Add(subtitle);
                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        stream.Close(); //option
                    }

                    Commons.ShowMessageAsync("PDF변환", "PDF Export를 성공했습니다.");
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 BtnExportPdf_Click : {ex}");
                }
            }
        }

        private void RdoAll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();
                if (RdoAll.IsChecked == true)
                {
                    users = Logic.DataAcess.GetUsers();
                }

                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void RdoActive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();

                if (RdoActive.IsChecked == true)
                {
                    users = Logic.DataAcess.GetUsers().Where(u => u.UserActivated == true).ToList();
                }

                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void RdoDeactive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();
                if (RdoDeactive.IsChecked == true)
                {
                    users = Logic.DataAcess.GetUsers().Where(u => u.UserActivated == false).ToList();
                }

                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
        }

    }
}
