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
            this.labelOnCursor = new System.Windows.Forms.Label();
            this.labelCurrentObj = new System.Windows.Forms.Label();
            this.textBoxInputString = new System.Windows.Forms.TextBox();
            this.groupBoxMarker = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGameField
            // 
            this.pictureBoxGameField.BackColor = System.Drawing.Color.White;
            this.pictureBoxGameField.Location = new System.Drawing.Point(277, 40);
            this.pictureBoxGameField.Name = "pictureBoxGameField";
            this.pictureBoxGameField.Size = new System.Drawing.Size(640, 480);
            this.pictureBoxGameField.TabIndex = 0;
            this.pictureBoxGameField.TabStop = false;
            this.pictureBoxGameField.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGameField_Paint);
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
            "Line",
            "Curve",
            "Rect",
            "Circle",
            "Polygon",
            "Triangle",
            "String"});
            this.listBoxSelectObject.Location = new System.Drawing.Point(92, 57);
            this.listBoxSelectObject.Name = "listBoxSelectObject";
            this.listBoxSelectObject.Size = new System.Drawing.Size(127, 274);
            this.listBoxSelectObject.TabIndex = 1;
            this.listBoxSelectObject.SelectedIndexChanged += new System.EventHandler(this.listBoxSelectObject_SelectedIndexChanged);
            // 
            // labelOnCursor
            // 
            this.labelOnCursor.AutoSize = true;
            this.labelOnCursor.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelOnCursor.Location = new System.Drawing.Point(22, 378);
            this.labelOnCursor.Name = "labelOnCursor";
            this.labelOnCursor.Size = new System.Drawing.Size(99, 28);
            this.labelOnCursor.TabIndex = 2;
            this.labelOnCursor.Text = "OnCursor";
            // 
            // labelCurrentObj
            // 
            this.labelCurrentObj.AutoSize = true;
            this.labelCurrentObj.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCurrentObj.Location = new System.Drawing.Point(22, 426);
            this.labelCurrentObj.Name = "labelCurrentObj";
            this.labelCurrentObj.Size = new System.Drawing.Size(115, 28);
            this.labelCurrentObj.TabIndex = 3;
            this.labelCurrentObj.Text = "CurrentObj";
            // 
            // textBoxInputString
            // 
            this.textBoxInputString.Location = new System.Drawing.Point(988, 426);
            this.textBoxInputString.Multiline = true;
            this.textBoxInputString.Name = "textBoxInputString";
            this.textBoxInputString.Size = new System.Drawing.Size(177, 76);
            this.textBoxInputString.TabIndex = 4;
            this.textBoxInputString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxInputString_KeyUp);
            // 
            // groupBoxMarker
            // 
            this.groupBoxMarker.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxMarker.Location = new System.Drawing.Point(965, 40);
            this.groupBoxMarker.Name = "groupBoxMarker";
            this.groupBoxMarker.Size = new System.Drawing.Size(200, 100);
            this.groupBoxMarker.TabIndex = 7;
            this.groupBoxMarker.TabStop = false;
            this.groupBoxMarker.Text = "マーカー";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 556);
            this.Controls.Add(this.groupBoxMarker);
            this.Controls.Add(this.textBoxInputString);
            this.Controls.Add(this.labelCurrentObj);
            this.Controls.Add(this.labelOnCursor);
            this.Controls.Add(this.listBoxSelectObject);
            this.Controls.Add(this.pictureBoxGameField);
            this.Name = "MainForm";
            this.Text = "メイン";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGameField;
        private System.Windows.Forms.ListBox listBoxSelectObject;
        private System.Windows.Forms.Label labelOnCursor;
        private System.Windows.Forms.Label labelCurrentObj;
        private System.Windows.Forms.TextBox textBoxInputString;
        private System.Windows.Forms.GroupBox groupBoxMarker;
    }
}

