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
            ObjectRect rect = new ObjectRect(pos);
            this.model.ObjectList.Add(rect);
            this.CurrentObj = rect;
            rect.DrugType = ObjectRect.DRUG_TYPE.POINT_4;
            CurrentObjIndex = this.model.ObjectList.Count - 1;
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

    class ObjectRect : ObjectBase
    {
        //座標は左上起点で時計回りに０１２３
        //コンストラクタ
        public ObjectRect(Point pos)
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
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case ObjectRect.DRUG_TYPE.POINT_1:
                    {
                    }
                    break;
                case ObjectRect.DRUG_TYPE.POINT_2:
                    {
                    }
                    break;
                case ObjectRect.DRUG_TYPE.POINT_3:
                    {
                    }
                    break;
                case ObjectRect.DRUG_TYPE.POINT_4:
                    {
                        //３箇所を動かす
                        this.Points[1].X = pos.X;
                        this.Points[2].X = pos.X;
                        this.Points[2].Y = pos.Y;
                        this.Points[3].Y = pos.Y;

                    }
                    break;
                case ObjectRect.DRUG_TYPE.WHOLE:
                    {
                    }
                    break;

                default:
                    break;
            }
        }
        //矩形を描画
        public override void DrawObject(Graphics g)
        {
            //矩形全体との当たり判定
            int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

            Rectangle rect = new Rectangle(min_x, min_y, max_x-min_x, max_y-min_y);

            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                g.FillRectangle(Brushes.Red, rect);
            }
            else
            {
                g.FillRectangle(Brushes.Black, rect);
            }
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {

            //頂点との当たり
            {

            }

            //矩形全体との当たり判定
            int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X,Points[3].X);
            int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

            if (( min_x <= pos.X) && (pos.X <= max_x) &&
                (min_y <= pos.Y) && (pos.Y <= max_y)
                )
            {
                this.DrugType = DRUG_TYPE.WHOLE;
                this.MoveStartPos = pos;    //全体を動かす基準点
                return true;
            }
                


            return false;
        }

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため
    }
}
