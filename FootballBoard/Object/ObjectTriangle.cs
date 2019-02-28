using System;
using System.Drawing;

namespace FootballBoard
{
    [Serializable()]
    class ObjectTriangle : ObjectBase
    {
        public ObjectTriangle(Point pos)
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
            NON,
            WHOLE,          //全体
            INIT,
        };
        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case DRUG_TYPE.POINT_1:
                    {
                        this.Points[0] = pos;
                        CheckPointMoveRange(ref this.Points);
                    }
                    break;
                case DRUG_TYPE.POINT_2:
                    {
                        this.Points[1] = pos;
                        CheckPointMoveRange(ref this.Points);
                    }
                    break;
                case DRUG_TYPE.POINT_3:
                    {
                        this.Points[2] = pos;
                        CheckPointMoveRange(ref this.Points);
                    }
                    break;
                case DRUG_TYPE.INIT:
                    {
                        this.Points[2] = pos;

                        this.Points[1].X = this.Points[0].X;
                        this.Points[1].Y = this.Points[2].Y;
                        CheckPointMoveRange(ref this.Points);
                    }
                    break;
                case DRUG_TYPE.WHOLE:
                    {
                        //全体を動かす
                        int move_x = pos.X - this.MoveStartPos.X;
                        int move_y = pos.Y - this.MoveStartPos.Y;

                        //X方向の全体移動チェック
                        if (CheckWholePointMoveRange(this.Points, move_x, true))
                        {
                            for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                            {
                                this.Points[i].X += move_x;
                            }
                            this.MoveStartPos.X = pos.X;
                        }
                        //Y方向の全体移動チェック
                        if (CheckWholePointMoveRange(this.Points, move_y, false))
                        {
                            for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                            {
                                this.Points[i].Y += move_y;
                            }
                            this.MoveStartPos.Y = pos.Y;
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        //三角形を描画
        public override void DrawObject(Graphics g)
        {
            PointF[] f_points = new PointF[3];
            f_points[0] = this.Points[0];
            f_points[1] = this.Points[1];
            f_points[2] = this.Points[2];

            int alpha = 128;

            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                alpha = 128;
            }
            else if (this.ObjStatus == OBJ_STATUS.ON_CURSOR)
            {
                alpha = 64;
            }
            Brush brush = new SolidBrush(Color.FromArgb(alpha, GUIParam.GetInstance().ObjectColor));


            g.FillPolygon(brush, this.Points);
            //SELECT状態の時には３点を描画
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                brush = Brushes.Yellow;
                for (int i = 0; i < 3; i++)
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
                for (int i = 0; i < 3; i++)
                {
                    double point_dist = Common.GetDistance(pos, this.Points[i]);
                    if (point_dist < VERTEX_SIZE / 2)
                    {
                        this.DrugType = (DRUG_TYPE.POINT_1 + i);
                        return true;
                    }
                }
            }

            if (CheckInPolygon(pos))
            {
                this.DrugType = DRUG_TYPE.WHOLE;
                this.MoveStartPos = pos;    //全体を動かす基準点
                return true;
            }
            this.DrugType = DRUG_TYPE.NON;
            return false;
        }
        //ポリゴンとの内外判定
        private bool CheckInPolygon(Point pos)
        {
            //点からレイを伸ばした時に何回交差するか

            int iCountCrossing = 0;

            Point point0 = this.Points[0];
            bool bFlag0x = (pos.X <= point0.X);
            bool bFlag0y = (pos.Y <= point0.Y);

            // レイの方向は、Ｘプラス方向
            for (int ui = 1; ui < Points.Length + 1; ui++)
            {
                Point point1 = this.Points[ui % Points.Length];  // 最後は始点が入る（多角形データの始点と終点が一致していないデータ対応）
                bool bFlag1x = (pos.X <= point1.X);
                bool bFlag1y = (pos.Y <= point1.Y);
                if (bFlag0y != bFlag1y)
                {   // 線分はレイを横切る可能性あり。
                    if (bFlag0x == bFlag1x)
                    {   // 線分の２端点は対象点に対して両方右か両方左にある
                        if (bFlag0x)
                        {   // 完全に右。⇒線分はレイを横切る
                            iCountCrossing += (bFlag0y ? -1 : 1);   // 上から下にレイを横切るときには、交差回数を１引く、下から上は１足す。
                        }
                    }
                    else
                    {   // レイと交差するかどうか、対象点と同じ高さで、対象点の右で交差するか、左で交差するかを求める。
                        if (pos.X <= (point0.X + (point1.X - point0.X) * (pos.Y - point0.Y) / (point1.Y - point0.Y)))
                        {   // 線分は、対象点と同じ高さで、対象点の右で交差する。⇒線分はレイを横切る
                            iCountCrossing += (bFlag0y ? -1 : 1);   // 上から下にレイを横切るときには、交差回数を１引く、下から上は１足す。
                        }
                    }
                }
                // 次の判定のために、
                point0 = point1;
                bFlag0x = bFlag1x;
                bFlag0y = bFlag1y;
            }

            if (iCountCrossing != 0)
            {
                return true;
            }
            return false;
        }
        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため
    }
}
