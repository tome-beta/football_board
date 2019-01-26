using System.Drawing;
using System;

namespace FootballBoard
{
    //ラインの振る舞いを示すクラス
    public class OStateCurve : ObjectState
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
                ObjectCurve curve = new ObjectCurve(pos);
                this.model.ObjectList.Add(curve);
                this.CurrentObj = curve;
                curve.DrugType = ObjectCurve.DRUG_TYPE.INIT;
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


        //文字列を設定する
        public override void SetString(String str)
        {
        }
    }
    //曲線
    [Serializable()]
    public class ObjectCurve : ObjectBase
    {
        public enum DRUG_TYPE
        {
            START_POINT,    //開始点
            MIDDLE_POINT,   //中間点
            END_POINT,      //終了点
            NON,
            WHOLE,          //全体
            INIT,

        };

        public ObjectCurve(Point pos)
        {
            this.Points[0] = pos;
            this.Points[2] = pos;

            //中間点
            this.Points[1].X = (this.Points[2].X - Points[0].X) / 2;
            this.Points[1].Y = (this.Points[2].Y - Points[0].Y) / 2;

            this.Points[1].X += this.Points[0].X;
            this.Points[1].Y += this.Points[0].Y;
        }

        //オブジェクトを生成したときの動作になる
        public void SetEndPoint(Point pos)
        {
            this.Points[2] = pos;

            //中間点
            this.Points[1].X = (this.Points[2].X - Points[0].X) / 2;
            this.Points[1].Y = (this.Points[2].Y - Points[0].Y) / 2;

            this.Points[1].X += this.Points[0].X;
            this.Points[1].Y += this.Points[0].Y;
        }

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case DRUG_TYPE.START_POINT:
                    {
                        this.Points[0].X = pos.X;
                        this.Points[0].Y = pos.Y;
                    }
                    break;
                case DRUG_TYPE.MIDDLE_POINT:
                    {
                        this.Points[1].X = pos.X;
                        this.Points[1].Y = pos.Y;
                    }
                    break;
                case DRUG_TYPE.END_POINT:
                    {
                        this.Points[2].X = pos.X;
                        this.Points[2].Y = pos.Y;
                    }
                    break;
                case DRUG_TYPE.INIT:
                    {
                        SetEndPoint(pos);
                    }
                    break;
                case DRUG_TYPE.WHOLE:
                    {
                        //全体を動かす
                        int move_x = pos.X - this.MoveStartPos.X;
                        int move_y = pos.Y - this.MoveStartPos.Y;

                        for(int i = 0; i < 3;i++)
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

        //曲線を描画
        public override void DrawObject(Graphics g)
        {
            Console.WriteLine(this.ObjStatus);
            int alpha = 255;

            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                alpha = 255;
            }
            else if (this.ObjStatus == OBJ_STATUS.ON_CURSOR)
            {
                alpha = 128;
            }

            using (Pen pen = new Pen(Color.FromArgb(alpha, GUIParam.GetInstance().ObjectColor), 4))
            {
                Point[] points = new Point[3];
                points[0] = this.Points[0];
                points[1] = this.Points[1];
                points[2] = this.Points[2];

                g.DrawCurve(pen, points);

                //スプライン計算
                MakeSplineFunc();
            }

            //SELECT状態の時には曲線の３点を描画
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                Brush brush = Brushes.Yellow;
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
                //このときは開始点と終了点と中間点を探す
                double point_dist = Common.GetDistance(pos, this.Points[0]);
                if (point_dist < VERTEX_SIZE / 2)
                {
                    this.DrugType = DRUG_TYPE.START_POINT;
                    return true;
                }
                point_dist = Common.GetDistance(pos, this.Points[1]);
                if (point_dist < VERTEX_SIZE / 2)
                {
                    this.DrugType = DRUG_TYPE.MIDDLE_POINT;
                    return true;
                }

                point_dist = Common.GetDistance(pos, this.Points[2]);
                if (point_dist < VERTEX_SIZE / 2)
                {
                    this.DrugType = DRUG_TYPE.END_POINT;
                    return true;
                }
            }

            //曲線が描かれている座標の範囲であることをチェックする
            if (ChkRange(pos))
            {
                //曲線との距離を調べる
                for (double t = 0.0; t < Spline.MAX_SPLINE_SIZE - 1 ; t += 0.01)
                {
                    int X = (int)xs.calc(t);
                    int Y = (int)ys.calc(t);

                    if ((-Spline.HIT_RANGE < pos.X - X) && (pos.X - X < Spline.HIT_RANGE) &&
                        (-Spline.HIT_RANGE < pos.Y - Y) && (pos.Y - Y < Spline.HIT_RANGE)
                        )
                    {
                        this.DrugType = DRUG_TYPE.WHOLE;
                        this.MoveStartPos = pos;    //全体を動かす基準点
                        return true;
                    }
                }
            }

            this.DrugType = DRUG_TYPE.NON;
            return false;
        }

        //スプライン曲線の式をつくる
        private void MakeSplineFunc()
        {
            double[] x_array = new double[] { (double)Points[0].X, (double)Points[1].X, (double)Points[2].X};
            double[] y_array = new double[] { (double)Points[0].Y, (double)Points[1].Y, (double)Points[2].Y};

            xs.Init(x_array, x_array.Length);
            ys.Init(y_array, y_array.Length);

/*
            for (double t = 0.0; t < x_array.Length - 1;t += 0.01)
            {
                p2.X = (int)xs.calc(t);
                p2.Y = (int)ys.calc(t);

                g.DrawLine(pen, p1, p2);
                p1.X = p2.X;
                p1.Y = p2.Y;
            }
*/
        }

        //直線とカーソル位置の範囲チェック
        private bool ChkRange(Point pos)
        {
            int min_x = 0;
            int max_x = 0;
            int min_y = 0;
            int max_y = 0;

            max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X);
            max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y);
            min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X);
            min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y);

            if ((min_x <= pos.X && pos.X <= max_x) &&
                (min_y <= pos.Y && pos.Y <= max_y)
            )
            {
                return true;
            }
            return false;
        }


        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため

        //スプライン曲線の計算のため
        Spline xs = new Spline();
        Spline ys = new Spline();
    }

}
