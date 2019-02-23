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
        public void ChangeDispGUI(Common.SELECT_DRAW_OBJECT select,ObjectBase obj)
        {
            //全部一旦消す処理
            this.WriteStringtextBox.Visible = false;
            this.MarkerGroupBox.Visible = false;
            this.LineGroupBox.Visible = false;

            switch (select)
            {
                case Common.SELECT_DRAW_OBJECT.MARKER:
                    {
                        this.MarkerGroupBox.Visible = true;

                        //保持している値を設定
                        ObjectMarker marker = obj as ObjectMarker;
                        if (marker != null)
                        {
                            //チームタイプ
                            if (marker.TeamType == 0)
                            {
                                this.TeamConboBox.Text = @"HOME";
                            }
                            else
                            {
                                this.TeamConboBox.Text = @"AWAY";
                            }

                            //番号
                            this.UniformNumberTextBox.Text = marker.UniformNumber;

                            //名前
                            this.NameTextBox.Text = marker.Name;

                            //名前位置
                            this.NamePosButton[marker.NamePosition].Checked = true;
                        }
                    }
                    break;

                case Common.SELECT_DRAW_OBJECT.LINE:
                    {
                        this.LineGroupBox.Visible = true;
                        //保持している値を設定
                        ObjectLine line = obj as ObjectLine;
                        if (line != null)
                        {
                            //ラインの種類
                            if (line.LineStyle == ObjectLine.LINE_STYLE.SOLID)
                            {
                                this.LinStyle.Text = @"Solid";
                            }
                            if (line.LineStyle == ObjectLine.LINE_STYLE.JAGGED)
                            {
                                this.LinStyle.Text = @"Jagged";
                            }
                            if (line.LineStyle == ObjectLine.LINE_STYLE.DOTTED)
                            {
                                this.LinStyle.Text = @"Dotted";
                            }

                            //矢印の種類
                            switch (line.ArrowStyle)
                            {
                                case ObjectLine.ARROW_STYLE.NONE:
                                    this.ArrowStyle.Text = @"None";
                                    break;
                                case ObjectLine.ARROW_STYLE.START:
                                    this.ArrowStyle.Text = @"Start";
                                    break;
                                case ObjectLine.ARROW_STYLE.END:
                                    this.ArrowStyle.Text = @"End";
                                    break;
                                case ObjectLine.ARROW_STYLE.BOTH:
                                    this.ArrowStyle.Text = @"Both";
                                    break;
                            }
                        }

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
        //marker
        public GroupBox MarkerGroupBox;         //マーカー用のグループボックス
        public TextBox UniformNumberTextBox;    //背番号
        public TextBox NameTextBox;             //名前
        public RadioButton[] NamePosButton = new RadioButton[9];    //名前の表示位置ボタン
        public ComboBox TeamConboBox;           //チーム切り替え

        //Line
        public GroupBox LineGroupBox;           //ライン用のグループボックス
        public ComboBox LinStyle;               //ラインの描画形式
        public ComboBox ArrowStyle;             //矢印の描画形式

        //String
        public TextBox WriteStringtextBox;      //文字列を書くため

    }
}
