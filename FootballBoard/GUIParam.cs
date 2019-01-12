using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

    }
}
