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
        //テキストボックスの種類
        public enum TEXTBOX_TYPE
        {
            STRING,
            MARKER_UNIFORM_NUMBER,
            MARKER_NAME,
        };


        //GUIを扱えるように登録する
        private void SettingGUI()
        {
            //marker
            GUIParam.GetInstance().MarkerGroupBox = this.groupBoxMarker;
            GUIParam.GetInstance().UniformNumberTextBox = this.textBoxUniformNumber;
            GUIParam.GetInstance().NameTextBox = this.textBoxName;
            GUIParam.GetInstance().TeamConboBox = this.comboBoxTeam;
            GUIParam.GetInstance().NamePosButton[0] = this.radioButtonName1;
            GUIParam.GetInstance().NamePosButton[1] = this.radioButtonName2;
            GUIParam.GetInstance().NamePosButton[2] = this.radioButtonName3;
            GUIParam.GetInstance().NamePosButton[3] = this.radioButtonName4;
            GUIParam.GetInstance().NamePosButton[4] = this.radioButtonName5;
            GUIParam.GetInstance().NamePosButton[5] = this.radioButtonName6;
            GUIParam.GetInstance().NamePosButton[6] = this.radioButtonName7;
            GUIParam.GetInstance().NamePosButton[7] = this.radioButtonName8;
            GUIParam.GetInstance().NamePosButton[8] = this.radioButtonName9;

            //Line
            GUIParam.GetInstance().LineGroupBox = this.groupBoxLine;
            GUIParam.GetInstance().LinStyle = this.comboBoxLineStyle;


            //String
            GUIParam.GetInstance().WriteStringtextBox = this.textBoxInputString;

        }


        //テキスト入力で何か文字キー、エンターキーを押した時にはObjectStringに反映させる
        private void textBoxInputString_KeyUp(object sender, KeyEventArgs e)
        {
            //改行は\r\n
            String str = this.textBoxInputString.Text;

            this.DataControle.SetString(str,MainForm.TEXTBOX_TYPE.STRING);
        }

        //マーカーのUniformNumberテキストボックスの入力
        private void textBoxUniformNumber_KeyUp(object sender, KeyEventArgs e)
        {
            //改行は\r\n
            String str = this.textBoxUniformNumber.Text;
            this.DataControle.SetString(str, MainForm.TEXTBOX_TYPE.MARKER_UNIFORM_NUMBER);
        }

        //マーカーのNameテキストボックスの入力
        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            //改行は\r\n
            String str = this.textBoxName.Text;
            this.DataControle.SetString(str, MainForm.TEXTBOX_TYPE.MARKER_NAME);
        }

        //ラインの描画形式を切り替えたとき
        private void comboBoxLineStyle_TextChanged(object sender, EventArgs e)
        {
            //オブジェクトに反映させる
            String str = this.comboBoxLineStyle.Text;
            this.DataControle.SetLineStyle(str);
        }


    }
}
