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
            this.listBoxSelectObject = new System.Windows.Forms.ListBox();
            this.labelOnCursor = new System.Windows.Forms.Label();
            this.labelCurrentObj = new System.Windows.Forms.Label();
            this.textBoxInputString = new System.Windows.Forms.TextBox();
            this.groupBoxMarker = new System.Windows.Forms.GroupBox();
            this.checkBoxDirection = new System.Windows.Forms.CheckBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonRedo = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.エクスポートToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.インポートToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.画像出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.フィールドToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxGameField = new System.Windows.Forms.PictureBox();
            this.openFileDialogImport = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogExport = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialogImage = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxMarker.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).BeginInit();
            this.SuspendLayout();
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
            this.groupBoxMarker.Controls.Add(this.checkBoxDirection);
            this.groupBoxMarker.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxMarker.Location = new System.Drawing.Point(965, 40);
            this.groupBoxMarker.Name = "groupBoxMarker";
            this.groupBoxMarker.Size = new System.Drawing.Size(200, 218);
            this.groupBoxMarker.TabIndex = 7;
            this.groupBoxMarker.TabStop = false;
            this.groupBoxMarker.Text = "マーカー";
            // 
            // checkBoxDirection
            // 
            this.checkBoxDirection.AutoSize = true;
            this.checkBoxDirection.Location = new System.Drawing.Point(7, 31);
            this.checkBoxDirection.Name = "checkBoxDirection";
            this.checkBoxDirection.Size = new System.Drawing.Size(97, 28);
            this.checkBoxDirection.TabIndex = 0;
            this.checkBoxDirection.Text = "direction";
            this.checkBoxDirection.UseVisualStyleBackColor = true;
            this.checkBoxDirection.CheckedChanged += new System.EventHandler(this.checkBoxDirection_CheckedChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonDelete.Location = new System.Drawing.Point(12, 457);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(142, 54);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonUndo.Location = new System.Drawing.Point(12, 517);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(166, 61);
            this.buttonUndo.TabIndex = 9;
            this.buttonUndo.Text = "UNDO";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonRedo
            // 
            this.buttonRedo.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonRedo.Location = new System.Drawing.Point(12, 584);
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.Size = new System.Drawing.Size(166, 61);
            this.buttonRedo.TabIndex = 10;
            this.buttonRedo.Text = "REDO";
            this.buttonRedo.UseVisualStyleBackColor = true;
            this.buttonRedo.Click += new System.EventHandler(this.buttonRedo_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.フィールドToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1203, 26);
            this.menuStrip.TabIndex = 11;
            this.menuStrip.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.エクスポートToolStripMenuItem,
            this.インポートToolStripMenuItem,
            this.toolStripMenuItem1,
            this.画像出力ToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.ファイルToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // エクスポートToolStripMenuItem
            // 
            this.エクスポートToolStripMenuItem.Name = "エクスポートToolStripMenuItem";
            this.エクスポートToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.エクスポートToolStripMenuItem.Text = "エクスポート...";
            this.エクスポートToolStripMenuItem.Click += new System.EventHandler(this.エクスポートToolStripMenuItem_Click);
            // 
            // インポートToolStripMenuItem
            // 
            this.インポートToolStripMenuItem.Name = "インポートToolStripMenuItem";
            this.インポートToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.インポートToolStripMenuItem.Text = "インポート...";
            this.インポートToolStripMenuItem.Click += new System.EventHandler(this.インポートToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // 画像出力ToolStripMenuItem
            // 
            this.画像出力ToolStripMenuItem.Name = "画像出力ToolStripMenuItem";
            this.画像出力ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.画像出力ToolStripMenuItem.Text = "画像出力";
            this.画像出力ToolStripMenuItem.Click += new System.EventHandler(this.画像出力ToolStripMenuItem_Click);
            // 
            // フィールドToolStripMenuItem
            // 
            this.フィールドToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verticalToolStripMenuItem,
            this.rightToolStripMenuItem,
            this.leftToolStripMenuItem});
            this.フィールドToolStripMenuItem.Name = "フィールドToolStripMenuItem";
            this.フィールドToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.フィールドToolStripMenuItem.Text = "フィールド";
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Checked = true;
            this.rightToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.rightToolStripMenuItem_Click);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.leftToolStripMenuItem_Click);
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
            // openFileDialogImport
            // 
            this.openFileDialogImport.AddExtension = false;
            this.openFileDialogImport.FileName = "openFileDialogImport";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 810);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.buttonRedo);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.groupBoxMarker);
            this.Controls.Add(this.textBoxInputString);
            this.Controls.Add(this.labelCurrentObj);
            this.Controls.Add(this.labelOnCursor);
            this.Controls.Add(this.listBoxSelectObject);
            this.Controls.Add(this.pictureBoxGameField);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "メイン";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.groupBoxMarker.ResumeLayout(false);
            this.groupBoxMarker.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
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
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonRedo;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem エクスポートToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem インポートToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialogExport;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 画像出力ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogImage;
        private System.Windows.Forms.CheckBox checkBoxDirection;
        private System.Windows.Forms.ToolStripMenuItem フィールドToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
    }
}

