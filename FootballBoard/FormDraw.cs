using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FootballBoard
{
    //描画関数はここにまとめる
    public partial class MainForm : Form
    {
        //描画更新はここでやる
        public void DrawUpdate()
        {
            //オブジェクトを描画
            this.DataControle.DrawAll(this.FieldGraphics);
            //反映
            this.pictureBoxGameField.Image = this.FieldBitmap;

        }

    }
}
