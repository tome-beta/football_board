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
            this.FieldGraphics = Graphics.FromImage(this.FieldBitmap);
            this.pictureBoxGameField.Image = this.FieldBitmap;

            this.listBoxSelectObject.SelectedIndex = 0;

            //フィールド画像を読み込んでおく
            string filed_image_str = @"..\..\..\resource\soccer_field.png";
            if (System.IO.File.Exists(filed_image_str))
            {
                SoccerFieldImage = Image.FromFile(filed_image_str);
            }
            else
            {
                filed_image_str = @"..\..\resource\soccer_field.png";
                SoccerFieldImage = Image.FromFile(filed_image_str);
            }
        }

        //左マウスクリック
        private void pictureBoxGameField_MouseDown(object sender, MouseEventArgs e)
        {
            this.DataControle.LeftMouseDown(e.Location);
        }
        //マウスドラッグ
        private void pictureBoxGameField_MouseMove(object sender, MouseEventArgs e)
        {
            this.DataControle.LeftMouseDrag(e.Location);
        }

        //マウスを離したとき
        private void pictureBoxGameField_MouseUp(object sender, MouseEventArgs e)
        {
            this.DataControle.LeftMouseUp(e.Location);
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

        //描画オブジェクトリストをクリックしたとき
        private void listBoxSelectObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ObjectSelect = (Common.SELECT_DRAW_OBJECT)this.listBoxSelectObject.SelectedIndex;
            //ステートを変える
            this.DataControle.ChangeSelectObject(this.ObjectSelect);

            //ここでステートによってGUI表示を切り替える
            ChangeGUI(this.ObjectSelect);
        }

        private void pictureBoxGameField_Paint(object sender, PaintEventArgs e)
        {
            //ピクチャボックスを初期化
            this.FieldGraphics.Clear(Color.White);

            //サッカーフィールドを描く
            FieldGraphics.DrawImage(SoccerFieldImage,0,0,pictureBoxGameField.Width,pictureBoxGameField.Height);

            //描画更新
            this.DrawUpdate();

            this.labelOnCursor.Text = @"OnCursor : " + this.DataControle.State.OnCursolIndex.ToString();
        }

        Controle DataControle = new Controle();

        Common.SELECT_DRAW_OBJECT ObjectSelect = Common.SELECT_DRAW_OBJECT.MOVE;

        //メインフィールド
        Bitmap FieldBitmap;
        Graphics FieldGraphics;

        private Image SoccerFieldImage;


    }
}
