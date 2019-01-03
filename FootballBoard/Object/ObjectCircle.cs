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
            //クリックしたところにすでにラインがあるか
            if (this.CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                ObjectCircle circle = new ObjectCircle(pos);
                this.model.ObjectList.Add(circle);
                this.CurrentObj = circle;
                circle.DrugType = ObjectCircle.DRUG_TYPE.POINT_3;
                CurrentObjIndex = this.model.ObjectList.Count - 1;
            }
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
        private ObjectCircle CurrentObj;
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
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case ObjectCircle.DRUG_TYPE.POINT_1:
                    {
                        //３箇所を動かす
                        this.Points[0] = pos;

                        this.Points[1].Y = pos.Y;
                        this.Points[3].X = pos.X;
                    }
                    break;
                case ObjectCircle.DRUG_TYPE.POINT_2:
                    {
                        this.Points[1] = pos;

                        this.Points[0].Y = pos.Y;
                        this.Points[2].X = pos.X;
                    }
                    break;
                case ObjectCircle.DRUG_TYPE.POINT_3:
                    {
                        this.Points[2] = pos;

                        this.Points[3].Y = pos.Y;
                        this.Points[1].X = pos.X;
                    }
                    break;
                case ObjectCircle.DRUG_TYPE.POINT_4:
                    {
                        //３箇所を動かす
                        this.Points[3] = pos;

                        this.Points[0].X = pos.X;
                        this.Points[2].Y = pos.Y;
                    }
                    break;
                case ObjectCircle.DRUG_TYPE.WHOLE:
                    {
                        //全体を動かす
                        int move_x = pos.X - this.MoveStartPos.X;
                        int move_y = pos.Y - this.MoveStartPos.Y;

                        for (int i = 0; i < 4; i++)
                        {
                            this.Points[i].X += move_x;
                            this.Points[i].Y += move_y;
                        }

                        this.MoveStartPos = pos;
                    }
                    break;

                default:
                    break;
            }
        }

        //円を描画
        public override void DrawObject(Graphics g)
        {
            //矩形全体との当たり判定
            int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

            Rectangle rect = new Rectangle(min_x, min_y, max_x - min_x, max_y - min_y);

            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                g.FillEllipse(Brushes.Black, rect);
            }
            else
            {
                g.FillEllipse(Brushes.Red, rect);
            }

            //SELECT状態の時には開始点と終了点を表示する
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                Brush brush = Brushes.Yellow;

                for (int i = 0; i < 4; i++)
                {
                    g.FillEllipse(brush, new Rectangle(
                    this.Points[i].X - PointWidth / 2,
                    this.Points[i].Y - PointHeight / 2,
                    PointWidth,
                    PointHeight)
                    );
                }
            }

        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            if (this.ObjStatus == OBJ_STATUS.SELECT)
            {
                //頂点との当たり
                for (int i = 0; i < 4; i++)
                {
                    double point_dist = Common.GetDistance(pos, this.Points[i]);
                    if (point_dist < PointWidth / 2)
                    {
                        this.DrugType = (DRUG_TYPE.POINT_1 + i);
                        return true;
                    }
                }
            }
            //矩形全体との当たり判定
            int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

            if ((min_x <= pos.X) && (pos.X <= max_x) &&
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


        private int PointWidth = 10;
        private int PointHeight = 10;
    }
}
