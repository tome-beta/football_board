using System;
using System;
using System.Drawing;

namespace FootballBoard
{
    class OStatePolygon : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
        }
        //左ドラッグ
        public override void MouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                this.CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                //何を掴んでいるかで場合分けしている
                this.CurrentObj.DrugMove(pos);
            }
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            this.CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
        }
        private ObjectRect CurrentObj;
    }
    class ObjectPolygon : ObjectBase
    {
        //座標は左上起点で時計回りに０１２３
        //コンストラクタ
        public ObjectPolygon(Point pos)
        {
            this.Points[0] = pos;
            this.Points[1] = pos;
            this.Points[2] = pos;
            this.Points[3] = pos;
        }

        public enum DRUG_TYPE
        {
            NON,
            WHOLE,          //全体
            POINT_1,        //頂点
            POINT_2,
            POINT_3,
            POINT_4,
        };
        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
        }

        //ポリゴンを描画
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
    }
}
