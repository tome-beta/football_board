using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballBoard
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //フォームが作られたあとに呼び出し
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitSystem();
        }
        //起動時の準備
        private void InitSystem()
        {
            //pictureboxに描画する準備
            this.FieldBitmap = new Bitmap(this.pictureBoxGameField.Width, this.pictureBoxGameField.Height);
            this.FieldBitmap_Vertical = new Bitmap(this.pictureBoxGameField.Height, this.pictureBoxGameField.Width);
            this.FieldGraphics = Graphics.FromImage(this.FieldBitmap);
            this.FieldGraphics_Vertical = Graphics.FromImage(this.FieldBitmap_Vertical);

            //フィールド画像を読み込んでおく
            string filed_image_str = @"..\..\..\resource\soccer_field.png";
            if (System.IO.File.Exists(filed_image_str))
            {
                SoccerFieldImage = Image.FromFile(filed_image_str);
                filed_image_str = @"..\..\..\resource\soccer_field_vertical.png";
                SoccerFieldImage_Vertical = Image.FromFile(filed_image_str);
            }
            else
            {
                filed_image_str = @"..\..\resource\soccer_field.png";
                SoccerFieldImage = Image.FromFile(filed_image_str);

                filed_image_str = @"..\..\resource\soccer_field_vertical.png";
                SoccerFieldImage_Vertical = Image.FromFile(filed_image_str);
           
            }

            //GUIを扱えるように登録しておく
            SettingGUI();

            this.listBoxSelectObject.SelectedIndex = 0;

        }


        //マウスクリック
        private void pictureBoxGameField_MouseDown(object sender, MouseEventArgs e)
        {
            //ここで入力ポジションを変換してしまう。RIGHT基準のポジションにする
            Point def = new Point();
            this.DataControle.TranslatePosition(e.Location, ref def);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.DataControle.LeftMouseDown(def);
                    break;
                case MouseButtons.Right:
                    this.DataControle.RightMouseDown(def);
                    break;
            }
        }
        //マウスドラッグ
        private void pictureBoxGameField_MouseMove(object sender, MouseEventArgs e)
        {
            //ここで入力ポジションを変換してしまう。RIGHT基準のポジションにする
            Point def = new Point();
            this.DataControle.TranslatePosition(e.Location, ref def);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.DataControle.LeftMouseDrag(def );
                    break;
                case MouseButtons.Right:
                    this.DataControle.RightMouseDrag(def);
                    break;
                default:
                    this.DataControle.MouseDrag(def);
                    break;
            }

        }
        //マウスを離したとき
        private void pictureBoxGameField_MouseUp(object sender, MouseEventArgs e)
        {
            //ここで入力ポジションを変換してしまう。RIGHT基準のポジションにする
            Point def = new Point();
            this.DataControle.TranslatePosition(e.Location, ref def);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.DataControle.LeftMouseUp(def);
                    break;
                case MouseButtons.Right:
                    this.DataControle.RightMouseUp(def);
                    break;
            }
        }



        //Deleteボタンを押したとき
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            this.DataControle.DeleteObject();
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //deleteキーを押したとき
            if (e.KeyCode == Keys.Delete)
            {
                this.DataControle.DeleteObject();
            }
            else if (e.Control ==true && e.KeyCode == Keys.Z)
            {
                this.DataControle.Undo();
            }
            else if (e.Control == true && e.KeyCode == Keys.Y)
            {
                this.DataControle.Redo();
            }
        }
        //UNDOボタンを押したとき
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            this.DataControle.Undo();
        }

        //REDOボタンを押したとき
        private void buttonRedo_Click(object sender, EventArgs e)
        {
            this.DataControle.Redo();
        }

        //マーカー方向チェックボックス
        private void checkBoxDirection_CheckedChanged(object sender, EventArgs e)
        {
            GUIParam.GetInstance().MarkerDirectionOn = this.checkBoxDirection.Checked;
        }

        //カラーダイアログボタン
        private void buttonColorDialog_Click(object sender, EventArgs e)
        {
    　  }


        //描画オブジェクトリストをクリックしたとき
        private void listBoxSelectObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ObjectSelect = (Common.SELECT_DRAW_OBJECT)this.listBoxSelectObject.SelectedIndex;
            //ステートを変える
            this.DataControle.ChangeSelectObject(this.ObjectSelect);

            //ここでステートによってGUI表示を切り替える
            GUIParam.GetInstance().ChangeDispGUI(this.ObjectSelect,null);
        }

        //メニューからエクスポートを選んだとき
        private void エクスポートToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.DataControle.ExportData(this.saveFileDialogExport);
            this.Visible = true;
            this.Activate();
        }
        //メニューからインポートを選んだとき
        private void インポートToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DataControle.ImportData(this.openFileDialogImport);
        }
        //メニューから画像保存を押したとき
        private void 画像出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.saveFileDialogImage.Filter = "画像ファイル|*.gif;*.jpg;*.png|すべてのファイル|*.*";
            saveFileDialogImage.FileName = "image";
            saveFileDialogImage.Title = @"ファイルを選択してください";
            //ダイアログを表示する
            if (saveFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                string extension = System.IO.Path.GetExtension(saveFileDialogImage.FileName);

                switch (extension.ToUpper())
                {
                    case ".GIF":
                        this.pictureBoxGameField.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case ".JPEG":
                        pictureBoxGameField.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".PNG":
                        pictureBoxGameField.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
            }

            this.Visible = true;
            this.Activate();
        }
        //Verticalを選択
        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIParam.GetInstance().FiledDirection = GUIParam.FILED_DIRECTION.VERTICAL;
            this.verticalToolStripMenuItem.Checked = true;
            this.rightToolStripMenuItem.Checked = false;
            this.leftToolStripMenuItem.Checked = false;
        }

        //Rightを選択
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIParam.GetInstance().FiledDirection = GUIParam.FILED_DIRECTION.RIGHT;
            this.verticalToolStripMenuItem.Checked = false;
            this.rightToolStripMenuItem.Checked = true;
            this.leftToolStripMenuItem.Checked = false;
        }

        //Leftを選択
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIParam.GetInstance().FiledDirection = GUIParam.FILED_DIRECTION.LEFT;
            this.verticalToolStripMenuItem.Checked = false;
            this.rightToolStripMenuItem.Checked = false;
            this.leftToolStripMenuItem.Checked = true;
        }




        private void pictureBoxGameField_Paint(object sender, PaintEventArgs e)
        {
            //ピクチャボックスを初期化
            this.FieldGraphics_Vertical.Clear(Color.White);

            //サッカーフィールドを描く
            if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.VERTICAL)
            {
                GUIParam.GetInstance().FiledHeight = SoccerFieldImage_Vertical.Height;
                GUIParam.GetInstance().FiledWidth = SoccerFieldImage_Vertical.Width;

                this.pictureBoxGameField.Width = GUIParam.GetInstance().FiledWidth;
                this.pictureBoxGameField.Height = GUIParam.GetInstance().FiledHeight;

                this.FieldGraphics_Vertical = Graphics.FromImage(this.FieldBitmap_Vertical);
                this.pictureBoxGameField.Image = this.FieldBitmap_Vertical;

                this.FieldGraphics_Vertical.DrawImage(SoccerFieldImage_Vertical, 0, 0, pictureBoxGameField.Width, pictureBoxGameField.Height);

                //描画更新
                this.DrawUpdate(FieldGraphics_Vertical, this.FieldBitmap_Vertical);
            
            }
            else
            {
                GUIParam.GetInstance().FiledHeight = SoccerFieldImage.Height;
                GUIParam.GetInstance().FiledWidth = SoccerFieldImage.Width;

                this.pictureBoxGameField.Width = GUIParam.GetInstance().FiledWidth;
                this.pictureBoxGameField.Height = GUIParam.GetInstance().FiledHeight;

                this.FieldGraphics = Graphics.FromImage(this.FieldBitmap);
                this.pictureBoxGameField.Image = this.FieldBitmap;

                FieldGraphics.DrawImage(SoccerFieldImage, 0, 0, pictureBoxGameField.Width, pictureBoxGameField.Height);
            
                //描画更新
                this.DrawUpdate(FieldGraphics,this.FieldBitmap);
            }


            this.labelOnCursor.Text = @"OnCursor : " + this.DataControle.State.OnCursolIndex.ToString();
            this.labelCurrentObj.Text = @"CurrentObj : " + this.DataControle.State.CurrentObjIndex;

        }

        Controle DataControle = new Controle();
        Common.SELECT_DRAW_OBJECT ObjectSelect = Common.SELECT_DRAW_OBJECT.MOVE;

        //メインフィールド
        Bitmap FieldBitmap;
        Bitmap FieldBitmap_Vertical;
        Graphics FieldGraphics;
        Graphics FieldGraphics_Vertical;
        private Image SoccerFieldImage;
        private Image SoccerFieldImage_Vertical;


    }
}
