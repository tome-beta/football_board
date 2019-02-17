using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//GUI操作はここにまとめる
namespace FootballBoard
{
    public partial class MainForm : Form
    {
        //カラーダイアログを開く
        private void buttonColorDialog_Click(object sender, EventArgs e)
        {
        }

        //テキスト入力で何か文字キー、エンターキーを押した時にはObjectStringに反映させる
        private void textBoxInputString_KeyUp(object sender, KeyEventArgs e)
        {
            //改行は\r\n
            String str = this.textBoxInputString.Text;

            this.DataControle.SetString(str);
        }


    }
}
