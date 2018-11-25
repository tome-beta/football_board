using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //描画オブジェクトのベース
    class ObjectBase
    {
        Point Start;    //開始位置
        Point End;      //終了位置
        bool Selected;  //選択中
    }
}
