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
            this.labelTeam = new System.Windows.Forms.Label();
            this.comboBoxTeam = new System.Windows.Forms.ComboBox();
            this.radioButtonName9 = new System.Windows.Forms.RadioButton();
            this.radioButtonName8 = new System.Windows.Forms.RadioButton();
            this.radioButtonName7 = new System.Windows.Forms.RadioButton();
            this.radioButtonName6 = new System.Windows.Forms.RadioButton();
            this.radioButtonName5 = new System.Windows.Forms.RadioButton();
            this.radioButtonName4 = new System.Windows.Forms.RadioButton();
            this.radioButtonName3 = new System.Windows.Forms.RadioButton();
            this.radioButtonName2 = new System.Windows.Forms.RadioButton();
            this.radioButtonName1 = new System.Windows.Forms.RadioButton();
            this.labelName = new System.Windows.Forms.Label();
            this.labelUniformNumber = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxUniformNumber = new System.Windows.Forms.TextBox();
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
            this.buttonColorDialog = new System.Windows.Forms.Button();
            this.groupBoxLine = new System.Windows.Forms.GroupBox();
            this.labelLineStyle = new System.Windows.Forms.Label();
            this.comboBoxLineStyle = new System.Windows.Forms.ComboBox();
            this.labelArrowStyle = new System.Windows.Forms.Label();
            this.comboBoxArrowStyle = new System.Windows.Forms.ComboBox();
            this.groupBoxMarker.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).BeginInit();
            this.groupBoxLine.SuspendLayout();
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
            this.listBoxSelectObject.Location = new System.Drawing.Point(27, 57);
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
            this.textBoxInputString.Location = new System.Drawing.Point(977, 493);
            this.textBoxInputString.Multiline = true;
            this.textBoxInputString.Name = "textBoxInputString";
            this.textBoxInputString.Size = new System.Drawing.Size(177, 76);
            this.textBoxInputString.TabIndex = 4;
            this.textBoxInputString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxInputString_KeyUp);
            // 
            // groupBoxMarker
            // 
            this.groupBoxMarker.Controls.Add(this.labelTeam);
            this.groupBoxMarker.Controls.Add(this.comboBoxTeam);
            this.groupBoxMarker.Controls.Add(this.radioButtonName9);
            this.groupBoxMarker.Controls.Add(this.radioButtonName8);
            this.groupBoxMarker.Controls.Add(this.radioButtonName7);
            this.groupBoxMarker.Controls.Add(this.radioButtonName6);
            this.groupBoxMarker.Controls.Add(this.radioButtonName5);
            this.groupBoxMarker.Controls.Add(this.radioButtonName4);
            this.groupBoxMarker.Controls.Add(this.radioButtonName3);
            this.groupBoxMarker.Controls.Add(this.radioButtonName2);
            this.groupBoxMarker.Controls.Add(this.radioButtonName1);
            this.groupBoxMarker.Controls.Add(this.labelName);
            this.groupBoxMarker.Controls.Add(this.labelUniformNumber);
            this.groupBoxMarker.Controls.Add(this.textBoxName);
            this.groupBoxMarker.Controls.Add(this.textBoxUniformNumber);
            this.groupBoxMarker.Controls.Add(this.checkBoxDirection);
            this.groupBoxMarker.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxMarker.Location = new System.Drawing.Point(965, 40);
            this.groupBoxMarker.Name = "groupBoxMarker";
            this.groupBoxMarker.Size = new System.Drawing.Size(200, 380);
            this.groupBoxMarker.TabIndex = 7;
            this.groupBoxMarker.TabStop = false;
            this.groupBoxMarker.Text = "Marker";
            // 
            // labelTeam
            // 
            this.labelTeam.AutoSize = true;
            this.labelTeam.Location = new System.Drawing.Point(8, 242);
            this.labelTeam.Name = "labelTeam";
            this.labelTeam.Size = new System.Drawing.Size(51, 24);
            this.labelTeam.TabIndex = 19;
            this.labelTeam.Text = "Team";
            // 
            // comboBoxTeam
            // 
            this.comboBoxTeam.FormattingEnabled = true;
            this.comboBoxTeam.Items.AddRange(new object[] {
            "HOME",
            "AWAY"});
            this.comboBoxTeam.Location = new System.Drawing.Point(12, 269);
            this.comboBoxTeam.Name = "comboBoxTeam";
            this.comboBoxTeam.Size = new System.Drawing.Size(121, 32);
            this.comboBoxTeam.TabIndex = 18;
            this.comboBoxTeam.Text = "HOME";
            // 
            // radioButtonName9
            // 
            this.radioButtonName9.AutoSize = true;
            this.radioButtonName9.Location = new System.Drawing.Point(53, 202);
            this.radioButtonName9.Name = "radioButtonName9";
            this.radioButtonName9.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName9.TabIndex = 17;
            this.radioButtonName9.UseVisualStyleBackColor = true;
            // 
            // radioButtonName8
            // 
            this.radioButtonName8.AutoSize = true;
            this.radioButtonName8.Checked = true;
            this.radioButtonName8.Location = new System.Drawing.Point(32, 202);
            this.radioButtonName8.Name = "radioButtonName8";
            this.radioButtonName8.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName8.TabIndex = 16;
            this.radioButtonName8.TabStop = true;
            this.radioButtonName8.UseVisualStyleBackColor = true;
            // 
            // radioButtonName7
            // 
            this.radioButtonName7.AutoSize = true;
            this.radioButtonName7.Location = new System.Drawing.Point(12, 202);
            this.radioButtonName7.Name = "radioButtonName7";
            this.radioButtonName7.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName7.TabIndex = 15;
            this.radioButtonName7.UseVisualStyleBackColor = true;
            // 
            // radioButtonName6
            // 
            this.radioButtonName6.AutoSize = true;
            this.radioButtonName6.Location = new System.Drawing.Point(53, 183);
            this.radioButtonName6.Name = "radioButtonName6";
            this.radioButtonName6.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName6.TabIndex = 14;
            this.radioButtonName6.UseVisualStyleBackColor = true;
            // 
            // radioButtonName5
            // 
            this.radioButtonName5.AutoSize = true;
            this.radioButtonName5.Location = new System.Drawing.Point(32, 183);
            this.radioButtonName5.Name = "radioButtonName5";
            this.radioButtonName5.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName5.TabIndex = 13;
            this.radioButtonName5.UseVisualStyleBackColor = true;
            // 
            // radioButtonName4
            // 
            this.radioButtonName4.AutoSize = true;
            this.radioButtonName4.Location = new System.Drawing.Point(12, 183);
            this.radioButtonName4.Name = "radioButtonName4";
            this.radioButtonName4.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName4.TabIndex = 12;
            this.radioButtonName4.UseVisualStyleBackColor = true;
            // 
            // radioButtonName3
            // 
            this.radioButtonName3.AutoSize = true;
            this.radioButtonName3.Location = new System.Drawing.Point(53, 164);
            this.radioButtonName3.Name = "radioButtonName3";
            this.radioButtonName3.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName3.TabIndex = 11;
            this.radioButtonName3.UseVisualStyleBackColor = true;
            // 
            // radioButtonName2
            // 
            this.radioButtonName2.AutoSize = true;
            this.radioButtonName2.Location = new System.Drawing.Point(32, 164);
            this.radioButtonName2.Name = "radioButtonName2";
            this.radioButtonName2.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName2.TabIndex = 10;
            this.radioButtonName2.UseVisualStyleBackColor = true;
            // 
            // radioButtonName1
            // 
            this.radioButtonName1.AutoSize = true;
            this.radioButtonName1.Location = new System.Drawing.Point(12, 164);
            this.radioButtonName1.Name = "radioButtonName1";
            this.radioButtonName1.Size = new System.Drawing.Size(14, 13);
            this.radioButtonName1.TabIndex = 9;
            this.radioButtonName1.UseVisualStyleBackColor = true;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 99);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 24);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Name";
            // 
            // labelUniformNumber
            // 
            this.labelUniformNumber.AutoSize = true;
            this.labelUniformNumber.Location = new System.Drawing.Point(12, 26);
            this.labelUniformNumber.Name = "labelUniformNumber";
            this.labelUniformNumber.Size = new System.Drawing.Size(73, 24);
            this.labelUniformNumber.TabIndex = 7;
            this.labelUniformNumber.Text = "Number";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(12, 126);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(103, 31);
            this.textBoxName.TabIndex = 6;
            this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
            // 
            // textBoxUniformNumber
            // 
            this.textBoxUniformNumber.Location = new System.Drawing.Point(12, 59);
            this.textBoxUniformNumber.Name = "textBoxUniformNumber";
            this.textBoxUniformNumber.Size = new System.Drawing.Size(103, 31);
            this.textBoxUniformNumber.TabIndex = 5;
            this.textBoxUniformNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxUniformNumber_KeyUp);
            // 
            // checkBoxDirection
            // 
            this.checkBoxDirection.AutoSize = true;
            this.checkBoxDirection.Location = new System.Drawing.Point(6, 346);
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
            this.menuStrip.Size = new System.Drawing.Size(1203, 31);
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
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(105, 27);
            this.ファイルToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // エクスポートToolStripMenuItem
            // 
            this.エクスポートToolStripMenuItem.Name = "エクスポートToolStripMenuItem";
            this.エクスポートToolStripMenuItem.Size = new System.Drawing.Size(185, 28);
            this.エクスポートToolStripMenuItem.Text = "エクスポート...";
            this.エクスポートToolStripMenuItem.Click += new System.EventHandler(this.エクスポートToolStripMenuItem_Click);
            // 
            // インポートToolStripMenuItem
            // 
            this.インポートToolStripMenuItem.Name = "インポートToolStripMenuItem";
            this.インポートToolStripMenuItem.Size = new System.Drawing.Size(185, 28);
            this.インポートToolStripMenuItem.Text = "インポート...";
            this.インポートToolStripMenuItem.Click += new System.EventHandler(this.インポートToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 6);
            // 
            // 画像出力ToolStripMenuItem
            // 
            this.画像出力ToolStripMenuItem.Name = "画像出力ToolStripMenuItem";
            this.画像出力ToolStripMenuItem.Size = new System.Drawing.Size(185, 28);
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
            this.フィールドToolStripMenuItem.Size = new System.Drawing.Size(97, 27);
            this.フィールドToolStripMenuItem.Text = "フィールド";
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(135, 28);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Checked = true;
            this.rightToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(135, 28);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.rightToolStripMenuItem_Click);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(135, 28);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.leftToolStripMenuItem_Click);
            // 
            // pictureBoxGameField
            // 
            this.pictureBoxGameField.BackColor = System.Drawing.Color.White;
            this.pictureBoxGameField.Location = new System.Drawing.Point(225, 57);
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
            // buttonColorDialog
            // 
            this.buttonColorDialog.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonColorDialog.Location = new System.Drawing.Point(981, 657);
            this.buttonColorDialog.Name = "buttonColorDialog";
            this.buttonColorDialog.Size = new System.Drawing.Size(99, 34);
            this.buttonColorDialog.TabIndex = 18;
            this.buttonColorDialog.Text = "Color";
            this.buttonColorDialog.UseVisualStyleBackColor = true;
            this.buttonColorDialog.Click += new System.EventHandler(this.buttonColorDialog_Click);
            // 
            // groupBoxLine
            // 
            this.groupBoxLine.Controls.Add(this.labelArrowStyle);
            this.groupBoxLine.Controls.Add(this.comboBoxArrowStyle);
            this.groupBoxLine.Controls.Add(this.labelLineStyle);
            this.groupBoxLine.Controls.Add(this.comboBoxLineStyle);
            this.groupBoxLine.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxLine.Location = new System.Drawing.Point(965, 40);
            this.groupBoxLine.Name = "groupBoxLine";
            this.groupBoxLine.Size = new System.Drawing.Size(194, 380);
            this.groupBoxLine.TabIndex = 19;
            this.groupBoxLine.TabStop = false;
            this.groupBoxLine.Text = "Line";
            // 
            // labelLineStyle
            // 
            this.labelLineStyle.AutoSize = true;
            this.labelLineStyle.Location = new System.Drawing.Point(12, 27);
            this.labelLineStyle.Name = "labelLineStyle";
            this.labelLineStyle.Size = new System.Drawing.Size(48, 24);
            this.labelLineStyle.TabIndex = 1;
            this.labelLineStyle.Text = "Style";
            // 
            // comboBoxLineStyle
            // 
            this.comboBoxLineStyle.FormattingEnabled = true;
            this.comboBoxLineStyle.Items.AddRange(new object[] {
            "Solid",
            "Jagged",
            "Dotted"});
            this.comboBoxLineStyle.Location = new System.Drawing.Point(12, 53);
            this.comboBoxLineStyle.Name = "comboBoxLineStyle";
            this.comboBoxLineStyle.Size = new System.Drawing.Size(121, 32);
            this.comboBoxLineStyle.TabIndex = 0;
            this.comboBoxLineStyle.Text = "Solid";
            this.comboBoxLineStyle.TextChanged += new System.EventHandler(this.comboBoxLineStyle_TextChanged);
            // 
            // labelArrowStyle
            // 
            this.labelArrowStyle.AutoSize = true;
            this.labelArrowStyle.Location = new System.Drawing.Point(12, 100);
            this.labelArrowStyle.Name = "labelArrowStyle";
            this.labelArrowStyle.Size = new System.Drawing.Size(96, 24);
            this.labelArrowStyle.TabIndex = 3;
            this.labelArrowStyle.Text = "ArrowStyle";
            // 
            // comboBoxArrowStyle
            // 
            this.comboBoxArrowStyle.FormattingEnabled = true;
            this.comboBoxArrowStyle.Items.AddRange(new object[] {
            "Non",
            "Start",
            "End",
            "Both"});
            this.comboBoxArrowStyle.Location = new System.Drawing.Point(12, 126);
            this.comboBoxArrowStyle.Name = "comboBoxArrowStyle";
            this.comboBoxArrowStyle.Size = new System.Drawing.Size(121, 32);
            this.comboBoxArrowStyle.TabIndex = 2;
            this.comboBoxArrowStyle.Text = "Non";
            this.comboBoxArrowStyle.TextChanged += new System.EventHandler(this.comboBoxArrowStyle_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 810);
            this.Controls.Add(this.buttonColorDialog);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.buttonRedo);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.textBoxInputString);
            this.Controls.Add(this.labelCurrentObj);
            this.Controls.Add(this.labelOnCursor);
            this.Controls.Add(this.listBoxSelectObject);
            this.Controls.Add(this.groupBoxLine);
            this.Controls.Add(this.groupBoxMarker);
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
            this.groupBoxLine.ResumeLayout(false);
            this.groupBoxLine.PerformLayout();
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
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxUniformNumber;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelUniformNumber;
        private System.Windows.Forms.RadioButton radioButtonName9;
        private System.Windows.Forms.RadioButton radioButtonName8;
        private System.Windows.Forms.RadioButton radioButtonName7;
        private System.Windows.Forms.RadioButton radioButtonName6;
        private System.Windows.Forms.RadioButton radioButtonName5;
        private System.Windows.Forms.RadioButton radioButtonName4;
        private System.Windows.Forms.RadioButton radioButtonName3;
        private System.Windows.Forms.RadioButton radioButtonName2;
        private System.Windows.Forms.RadioButton radioButtonName1;
        private System.Windows.Forms.Button buttonColorDialog;
        private System.Windows.Forms.ComboBox comboBoxTeam;
        private System.Windows.Forms.Label labelTeam;
        private System.Windows.Forms.GroupBox groupBoxLine;
        private System.Windows.Forms.Label labelLineStyle;
        private System.Windows.Forms.ComboBox comboBoxLineStyle;
        private System.Windows.Forms.Label labelArrowStyle;
        private System.Windows.Forms.ComboBox comboBoxArrowStyle;
    }
}

