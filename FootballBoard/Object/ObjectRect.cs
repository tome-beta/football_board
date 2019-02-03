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
            //クリックしたところにすでに矩形があるか
            if (this.CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                ObjectRect rect = new ObjectRect(pos);
                this.model.ObjectList.Add(rect);
                this.CurrentObj = rect;
                rect.DrugType = ObjectRect.DRUG_TYPE.INIT;
            }
        }
        //左ドラッグ
        public override void LeftMouseMove(Point pos)
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
        //右クリック
        public override void RightMouseDown(Point pos) { }
        public override void RightMouseMove(Point pos) { }
        public override void RightMouseUp(Point pos) { }

        //文字列を設定する
        public override void SetString(String str)
        {
        }
    }

    [Serializable()]
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
            POINT_1,        //頂点
            POINT_2,
            POINT_3,
            POINT_4,
            NON,
            WHOLE,          //全体
            INIT,           //新しく作ったときの動き
        };

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case ObjectRect.DRUG_TYPE.POINT_1:
                    {
                        //３箇所を動かす
                        this.Points[0] = pos;

                        this.Points[1].Y = pos.Y;
                        this.Points[3].X = pos.X;
                    }
                    break;
                case ObjectRect.DRUG_TYPE.POINT_2:
                    {
                        this.Points[1] = pos;

                        this.Points[0].Y = pos.Y;
                        this.Points[2].X = pos.X;
                    }
                    break;
                case ObjectRect.DRUG_TYPE.POINT_3:
                    {
                        this.Points[2] = pos;

                        this.Points[3].Y = pos.Y;
                        this.Points[1].X = pos.X;
                    }
                    break;
                case ObjectRect.DRUG_TYPE.POINT_4:
                    {
                        //３箇所を動かす
                        this.Points[3] = pos;

                        this.Points[0].X = pos.X;
                        this.Points[2].Y = pos.Y;
                    }
                    break;
                case ObjectRect.DRUG_TYPE.WHOLE:
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
                case ObjectRect.DRUG_TYPE.INIT:
                    {
                        this.Points[2] = pos;

                        this.Points[3].Y = pos.Y;
                        this.Points[1].X = pos.X;
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

            Brush brush;
            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                brush = new SolidBrush(Color.FromArgb(128, GUIParam.GetInstance().ObjectColor));
            }
            else
            {
                brush = new SolidBrush(Color.FromArgb(64, GUIParam.GetInstance().ObjectColor));
            }

            g.FillRectangle(brush, rect);

            //SELECT状態の時には開始点と終了点を表示する
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                brush = Brushes.Yellow;

                for (int i = 0; i < 4; i++)
                {
                    if ((int)this.DrugType == i)
                    {
                        brush = Brushes.Red;
                    }
                    else
                    {
                        brush = Brushes.Yellow;
                    }
                    g.FillEllipse(brush, new Rectangle(
                    this.Points[i].X - VERTEX_SIZE / 2,
                    this.Points[i].Y - VERTEX_SIZE / 2,
                    VERTEX_SIZE,
                    VERTEX_SIZE)
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
                for(int i = 0; i < 4;i++)
                {
                    double point_dist = Common.GetDistance(pos, this.Points[i]);
                    if (point_dist < VERTEX_SIZE / 2)
                    {
                        this.DrugType = (DRUG_TYPE.POINT_1 + i);
                        return true;
                    }
                }
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


            this.DrugType = DRUG_TYPE.NON;
            return false;
        }

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため

    }
}
