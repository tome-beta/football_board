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
        public void DrawTest(Graphics test_g)
        {
            Pen pen = new Pen(Color.Red, 4);

            test_g.FillRectangle(Brushes.Red, new Rectangle(0, 0, 36, 124));

            pen.Dispose();

        }


    }
}
