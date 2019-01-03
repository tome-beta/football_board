using System;
using System.Drawing;

namespace FootballBoard
{
    //円の振る舞いを示すクラス
    public class OStateCircle : ObjectState
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
    //円オブジェクト
    class ObjectCircle : ObjectBase
    {
        public enum DRUG_TYPE
        {
            NON,
            WHOLE,          //全体
            POINT_1,        //頂点
            POINT_2,
            POINT_3,
            POINT_4,
        };

        public ObjectCircle(Point pos)
        {
            this.Points[0] = pos;
            this.Points[1] = pos;
            this.Points[2] = pos;
            this.Points[3] = pos;
        }

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
        }

        //円を描画
        public override void DrawObject(Graphics g)
        {
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            return false;
        }

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため


        private int PointWidth = 10;
        private int PointHeight = 10;
    }
}
