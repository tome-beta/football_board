using System;
using System.Drawing;

namespace FootballBoard
{
    //矩形の振る舞いを示すクラス
    public class OStateRect : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
        }
        //左ドラッグ
        public override void MouseMove(Point pos)
        {
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
        }
    }

    class ObjectRect : ObjectBase
    {
        public enum DRUG_TYPE
        {
            NON,
            WHOLE,          //全体
            LEFT_UP,        //頂点
            LEFT_DOWN,
            RIGHT_UP,
            RIGHT_DOWN,
        };

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
        }

        //ラインをを描画
        public override void DrawObject(Graphics g)
        {
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            return false;
        }
    }
}
