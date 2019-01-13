using System.Drawing;
namespace FootballBoard
{
    //GUIの設定を記録するくらす
    class GUIParam
    {
        private static GUIParam _guiParam = new GUIParam();

        public static GUIParam GetInstance()
        {
            return _guiParam;
        }

        //初期化
        private GUIParam()
        {
            this.ObjectColor = Color.Red;
        }

        public Color ObjectColor;
    }
}
