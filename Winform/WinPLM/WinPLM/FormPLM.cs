using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPLM
{
    public partial class FormPLM : Form
    {
        string connString = "Data Source=127.0.0.1;Initial Catalog=ERP;User ID=sa;Password=mssql_p@ssw0rd!;";
        int i = 0; // 0 = item, 1 = Category, 2 = Brand, 3 = Part, 4 = Material;
        int itemId = 0;

        public FormPLM()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshData(i);
        }

        private void BtnItem_Click(object sender, EventArgs e)
        {
            i = 0;
            RefreshData(i);
        }

        private void BtnCate_Click(object sender, EventArgs e)
        {
            i = 1;
            RefreshData(i);
        }

        private void BtnBrand_Click(object sender, EventArgs e)
        {
            i = 2;
            RefreshData(i);
        }

        private void BtnPart_Click(object sender, EventArgs e)
        {
            i = 3;
            RefreshData(i);
        }

        private void BtnMaterial_Click(object sender, EventArgs e)
        {
            i = 4;
            RefreshData(i);
        }
        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selData = DgvData.Rows[e.RowIndex].Cells;
            itemId = (int)selData[0].Value;
        }

        // 데이터 로드
        private void RefreshData(int i)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "";

                switch (i)
                {
                    case 0: // item
                        query = @"SELECT ItemId
                                        ,ItemName
                                        ,ItemDescription
                                        ,ItemImage
                                        ,BrandId
                                        ,CartegoryId
                                        FROM dbo.Item";
                        break;
                    case 1: // category
                        query = @"SELECT CategoryID
                                        ,CategoryName
                                    FROM dbo.ICategory";
                        break;
                    case 2: // brand
                        query = @"SELECT BrandId
                                        ,BrandName
                                         FROM dbo.Brand";
                        break;
                    case 3: //part
                        query = @"SELECT PartId
                                          ,PartName
                                          ,PartLevel
                                          ,UpPart
                                          ,ItemId
                                          FROM dbo.Part";
                        break;
                    case 4: //Material
                        query = @"SELECT MaterialId
                                          ,MaterialName
                                          ,Price
                                          ,Unit
                                          ,Quantity
                                          ,ItemId
                                          ,PartId
                                          ,Total_Price = Price * Quantity
                                      FROM dbo.Material";
                        break;
                }


                /*SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();*/

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DgvData.DataSource = ds.Tables[0];
            }
        }

        private void BtnBom_Click(object sender, EventArgs e)
        {

            if (i == 0 && itemId != 0)
            {
                FrmBOM frm = new FrmBOM();
                frm.itemId = itemId;
                frm.Show();
            }
            else
            {
                MessageBox.Show("ITEM을 선택하세요!!");
            }
            
        }
    }
}
