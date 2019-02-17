using System.Drawing;
using System.Windows.Forms;

namespace FootballBoard
{
    //GUIの設定を記録するくらす
    public class GUIParam
    {
        private static GUIParam _guiParam = new GUIParam();

        public static GUIParam GetInstance()
        {
            return _guiParam;
        }

        public enum FILED_DIRECTION
        {
            VERTICAL,
            RIGHT,
            LEFT,
        };

        //選択したオブジェクトによって表示GUIを変える
        public void ChangeDispGUI(Common.SELECT_DRAW_OBJECT select)
        {
            //全部一旦消す処理
            this.WriteStringtextBox.Visible = false;
            this.MarkerGroupBox.Visible = false;

            switch (select)
            {
                case Common.SELECT_DRAW_OBJECT.MARKER:
                    {
                        this.MarkerGroupBox.Visible = true;
                    }
                    break;


                case Common.SELECT_DRAW_OBJECT.STRING:
                    {
                        this.WriteStringtextBox.Visible = true;
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }


        //初期化
        private GUIParam()
        {
            this.ObjectColor = Color.Red;
            this.MarkerDirectionOn = false;
            this.FiledDirection = FILED_DIRECTION.RIGHT;
        }

        //GUIの設定
        public Color ObjectColor;               //オブジェクトに設定する色
        public bool MarkerDirectionOn;          //マーカーに方向をつけるか
        public FILED_DIRECTION FiledDirection;  //フィールドの方向

        //パラメータ
        public int FiledWidth = 640;        //フィールドの大きさ
        public int FiledHeight = 480;       //フィールドの幅

        //オブジェクト毎のGUI
        public TextBox WriteStringtextBox;      //文字列を書くため
        public GroupBox MarkerGroupBox;         //マーカー用のグループボックス
        
    }
}
