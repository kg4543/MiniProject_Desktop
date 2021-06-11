
namespace WinPLM
{
    partial class FormPLM
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.DgvData = new System.Windows.Forms.DataGridView();
            this.BtnItem = new System.Windows.Forms.Button();
            this.BtnCate = new System.Windows.Forms.Button();
            this.BtnBrand = new System.Windows.Forms.Button();
            this.BtnPart = new System.Windows.Forms.Button();
            this.BtnMaterial = new System.Windows.Forms.Button();
            this.BtnBom = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvData
            // 
            this.DgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvData.Location = new System.Drawing.Point(12, 60);
            this.DgvData.Name = "DgvData";
            this.DgvData.RowTemplate.Height = 23;
            this.DgvData.Size = new System.Drawing.Size(505, 215);
            this.DgvData.TabIndex = 0;
            this.DgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellContentClick);
            // 
            // BtnItem
            // 
            this.BtnItem.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnItem.Location = new System.Drawing.Point(13, 13);
            this.BtnItem.Name = "BtnItem";
            this.BtnItem.Size = new System.Drawing.Size(79, 30);
            this.BtnItem.TabIndex = 1;
            this.BtnItem.Text = "Item";
            this.BtnItem.UseVisualStyleBackColor = true;
            this.BtnItem.Click += new System.EventHandler(this.BtnItem_Click);
            // 
            // BtnCate
            // 
            this.BtnCate.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnCate.Location = new System.Drawing.Point(98, 13);
            this.BtnCate.Name = "BtnCate";
            this.BtnCate.Size = new System.Drawing.Size(79, 30);
            this.BtnCate.TabIndex = 2;
            this.BtnCate.Text = "Categry";
            this.BtnCate.UseVisualStyleBackColor = true;
            this.BtnCate.Click += new System.EventHandler(this.BtnCate_Click);
            // 
            // BtnBrand
            // 
            this.BtnBrand.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnBrand.Location = new System.Drawing.Point(183, 13);
            this.BtnBrand.Name = "BtnBrand";
            this.BtnBrand.Size = new System.Drawing.Size(79, 30);
            this.BtnBrand.TabIndex = 3;
            this.BtnBrand.Text = "Brand";
            this.BtnBrand.UseVisualStyleBackColor = true;
            this.BtnBrand.Click += new System.EventHandler(this.BtnBrand_Click);
            // 
            // BtnPart
            // 
            this.BtnPart.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPart.Location = new System.Drawing.Point(268, 13);
            this.BtnPart.Name = "BtnPart";
            this.BtnPart.Size = new System.Drawing.Size(79, 30);
            this.BtnPart.TabIndex = 4;
            this.BtnPart.Text = "Part";
            this.BtnPart.UseVisualStyleBackColor = true;
            this.BtnPart.Click += new System.EventHandler(this.BtnPart_Click);
            // 
            // BtnMaterial
            // 
            this.BtnMaterial.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnMaterial.Location = new System.Drawing.Point(353, 13);
            this.BtnMaterial.Name = "BtnMaterial";
            this.BtnMaterial.Size = new System.Drawing.Size(79, 30);
            this.BtnMaterial.TabIndex = 5;
            this.BtnMaterial.Text = "Material";
            this.BtnMaterial.UseVisualStyleBackColor = true;
            this.BtnMaterial.Click += new System.EventHandler(this.BtnMaterial_Click);
            // 
            // BtnBom
            // 
            this.BtnBom.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnBom.Location = new System.Drawing.Point(438, 12);
            this.BtnBom.Name = "BtnBom";
            this.BtnBom.Size = new System.Drawing.Size(79, 30);
            this.BtnBom.TabIndex = 6;
            this.BtnBom.Text = "BOM";
            this.BtnBom.UseVisualStyleBackColor = true;
            // 
            // FormPLM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 440);
            this.Controls.Add(this.BtnBom);
            this.Controls.Add(this.BtnMaterial);
            this.Controls.Add(this.BtnPart);
            this.Controls.Add(this.BtnBrand);
            this.Controls.Add(this.BtnCate);
            this.Controls.Add(this.BtnItem);
            this.Controls.Add(this.DgvData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPLM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLM";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvData;
        private System.Windows.Forms.Button BtnItem;
        private System.Windows.Forms.Button BtnCate;
        private System.Windows.Forms.Button BtnBrand;
        private System.Windows.Forms.Button BtnPart;
        private System.Windows.Forms.Button BtnMaterial;
        private System.Windows.Forms.Button BtnBom;
    }
}

