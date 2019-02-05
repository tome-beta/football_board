using System.Drawing;
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


        //初期化
        private GUIParam()
        {
            this.ObjectColor = Color.Red;
            this.MarkerDirectionOn = false;
            this.FiledDirection = FILED_DIRECTION.RIGHT;
        }

        public Color ObjectColor;               //オブジェクトに設定する色
        public bool MarkerDirectionOn;          //マーカーに方向をつけるか

        public FILED_DIRECTION FiledDirection;  //フィールドの方向

    }
}
