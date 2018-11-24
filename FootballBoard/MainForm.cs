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
        }

        //メインフィールド
        Bitmap FieldBitmap;
        Graphics FieldGraphics;

        //        public DataModel model = new DataModel();
        Controle controle = new Controle();

        private void pictureBoxGameField_Click(object sender, EventArgs e)
        {
            DrawTest(this.FieldGraphics);

            //反映
            this.pictureBoxGameField.Image = this.FieldBitmap;

        }


    }
}
