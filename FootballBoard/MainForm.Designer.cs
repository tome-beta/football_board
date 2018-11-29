namespace FootballBoard
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxGameField = new System.Windows.Forms.PictureBox();
            this.listBoxSelectObject = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGameField
            // 
            this.pictureBoxGameField.BackColor = System.Drawing.Color.White;
            this.pictureBoxGameField.Location = new System.Drawing.Point(277, 40);
            this.pictureBoxGameField.Name = "pictureBoxGameField";
            this.pictureBoxGameField.Size = new System.Drawing.Size(524, 329);
            this.pictureBoxGameField.TabIndex = 0;
            this.pictureBoxGameField.TabStop = false;
            this.pictureBoxGameField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGameField_MouseDown);
            this.pictureBoxGameField.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGameField_MouseMove);
            this.pictureBoxGameField.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGameField_MouseUp);
            // 
            // listBoxSelectObject
            // 
            this.listBoxSelectObject.Font = new System.Drawing.Font("Ricty Diminished", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBoxSelectObject.FormattingEnabled = true;
            this.listBoxSelectObject.ItemHeight = 30;
            this.listBoxSelectObject.Items.AddRange(new object[] {
            "Move",
            "Marker",
            "Line"});
            this.listBoxSelectObject.Location = new System.Drawing.Point(90, 61);
            this.listBoxSelectObject.Name = "listBoxSelectObject";
            this.listBoxSelectObject.Size = new System.Drawing.Size(127, 184);
            this.listBoxSelectObject.TabIndex = 1;
            this.listBoxSelectObject.SelectedIndexChanged += new System.EventHandler(this.listBoxSelectObject_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 529);
            this.Controls.Add(this.listBoxSelectObject);
            this.Controls.Add(this.pictureBoxGameField);
            this.Name = "MainForm";
            this.Text = "メイン";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGameField;
        private System.Windows.Forms.ListBox listBoxSelectObject;
    }
}

