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
        }

        private void pictureBoxGameField_MouseDown(object sender, MouseEventArgs e)
        {
            this.DataControle.LeftMouseDown(e.Location);
/*
            //クリックしたときの反応はオブジェクト毎に変わる
            if (this.ObjectSelect == Common.SELECT_DRAW_OBJECT.MARKER)
            {
                //マーカーを置く
                this.DataControle.AddMarker(e.Location);
            }
            if (this.ObjectSelect == Common.SELECT_DRAW_OBJECT.LINE)
            {
                //ラインの開始地点
                this.DataControle.MakeLine(e.Location);
            }
*/
        }
        //マウスドラッグ
        private void pictureBoxGameField_MouseMove(object sender, MouseEventArgs e)
        {
            this.DataControle.LeftMouseDrag(e.Location);
/*
            if (this.ObjectSelect == Common.SELECT_DRAW_OBJECT.LINE)
            {
                if(this.controle.MouseDrag)
                {
                    //ラインを引いてるフラグが必要
                    DataControle.SetLineEndPoint(e.Location);
                }
            }
*/
        }

        //マウスを離したとき
        private void pictureBoxGameField_MouseUp(object sender, MouseEventArgs e)
        {
            this.DataControle.LeftMouseUp(e.Location);
/*
            if (this.ObjectSelect == Common.SELECT_DRAW_OBJECT.LINE)
            {
                //ラインを引いてるフラグが必要
                DataControle.SetLineEndPoint(e.Location);
            }
            this.controle.MouseDrag = false;
*/

        }


        //描画オブジェクトリストをクリックしたとき
        private void listBoxSelectObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.SELECT_DRAW_OBJECT select = (Common.SELECT_DRAW_OBJECT)this.listBoxSelectObject.SelectedIndex;

            ObjectSelect = select;

            //ステートを変える
            this.DataControle.ChangeSelectObject(select);

/*
            switch (select)
            {
                case Common.SELECT_DRAW_OBJECT.MOVE:
                    {
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.MARKER:
                    {
                    }
                    break;

                case Common.SELECT_DRAW_OBJECT.LINE:
                    {
                    }
                    break;
            }
 */
        }

        Controle DataControle = new Controle();

        Common.SELECT_DRAW_OBJECT ObjectSelect = Common.SELECT_DRAW_OBJECT.MOVE;

        //メインフィールド
        Bitmap FieldBitmap;
        Graphics FieldGraphics;

        //        public DataModel model = new DataModel();
        Controle controle = new Controle();

        private void pictureBoxGameField_Paint(object sender, PaintEventArgs e)
        {
            //ピクチャボックスを初期化
            this.FieldGraphics.Clear(Color.White);

            //将来的にはここでサッカーフィールドを描く

            //描画更新
            this.DrawUpdate();
        }
    }
}
