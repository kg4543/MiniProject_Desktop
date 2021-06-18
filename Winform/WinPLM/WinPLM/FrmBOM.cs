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
    public partial class FrmBOM : Form
    {
        string connString = "Data Source=127.0.0.1;Initial Catalog=ERP;User ID=sa;Password=mssql_p@ssw0rd!;";
        internal int itemId;

        public FrmBOM()
        {
            InitializeComponent();
        }

        private void FrmBOM_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string pQuery = $@"SELECT PartId
                                          ,PartName
                                          ,PartLevel
                                          ,UpPart
                                      FROM dbo.Part
                                    where ItemId = {itemId}";
                    SqlDataAdapter pAdapter = new SqlDataAdapter(pQuery, conn);
                    DataSet pDs = new DataSet();
                    pAdapter.Fill(pDs);
                    var tar = pDs.Tables[0].Rows; //Part 테이블 열

                    string mQuery = $@"SELECT MaterialId
                                          ,MaterialName
                                          ,Price
                                          ,Unit
                                          ,Quantity
                                          ,ItemId
                                          ,PartId
                                      FROM dbo.Material
                                    where ItemId = {itemId}";
                    SqlDataAdapter mAdapter = new SqlDataAdapter(mQuery, conn);
                    DataSet mDs = new DataSet();
                    mAdapter.Fill(mDs);
                    var tar2 = mDs.Tables[0].Rows; //Part 테이블 열

                    // Part Node 생성
                    TreeNode[] part = new TreeNode[20];
                    for (int i = 0; i < tar.Count; i++)
                    {
                        part[i] = new TreeNode(tar[i].ItemArray[1].ToString()); //name
                        part[i].Tag = tar[i].ItemArray[0].ToString(); //id
                    }

                    // Material Node 생성
                    int tp = 0; //total price
                    TreeNode[] material = new TreeNode[20];
                    for (int i = 0; i < tar2.Count; i++)
                    {
                        material[i] = new TreeNode($"{tar2[i].ItemArray[1]}" + $"{tar2[i].ItemArray[4]}" + $"{tar2[i].ItemArray[3]}"); //name
                        material[i].Tag = tar2[i].ItemArray[0].ToString(); //id
                        tp += (int)(tar2[i].ItemArray[2]); 
                    }

                    // (고유아이디, 이름, 레벨, 부모아이디)
                    TrvBom.Nodes.Add(part[0]);
                    part[0].Text = $"{tar[0].ItemArray[1]}" +"$"+ $"{tp}";
                    part[0].Tag = 0;
                    part[0].Nodes.Add(part[1]);
                    part[0].Nodes.Add(part[2]);
                    part[1].Nodes.Add(part[3]);
                    part[1].Nodes.Add(part[4]);
                    part[1].Nodes.Add(part[5]);
                    part[3].Nodes.Add(material[0]);
                    part[4].Nodes.Add(material[1]);
                    part[5].Nodes.Add(material[2]);
                    part[2].Nodes.Add(material[3]);
                    part[2].Nodes.Add(material[4]);
                    part[2].Nodes.Add(material[5]);

                    /*for (int i = 1; i < tar.Count; i++)
                    {
                        CreateNode((int)tar[i].ItemArray[0]
                                    , tar[i].ItemArray[1].ToString()
                                    , (int)tar[i].ItemArray[2]
                                    , (int)tar[i].ItemArray[3]);
                    }*/
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
                throw;
            }
        }

        /*void CreateNode(int id,string name,int level, int parent)
        {
            TreeNode node = new TreeNode(name);
            node.Tag = id;
            if (level == null)
            {
                TrvBom.Nodes.Add(node);
            }
        }*/
    }
}
