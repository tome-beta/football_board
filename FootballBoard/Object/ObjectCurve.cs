﻿using System.Drawing;
using System;

namespace FootballBoard
{
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
            this.Points[3] = pos;

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
                        CheckPointMoveRange(ref this.Points);
                    }
                    break;
                case DRUG_TYPE.MIDDLE_POINT:
                    {
                        this.Points[1].X = pos.X;
                        this.Points[1].Y = pos.Y;
                        CheckPointMoveRange(ref this.Points);
                    }
                    break;
                case DRUG_TYPE.END_POINT:
                    {
                        this.Points[2].X = pos.X;
                        this.Points[2].Y = pos.Y;
                        CheckPointMoveRange(ref this.Points);
                    }
                    break;
                case DRUG_TYPE.INIT:
                    {
                        SetEndPoint(pos);
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

        //曲線を描画
        public override void DrawObject(Graphics g)
        {
            int alpha = 255;

            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                alpha = 255;
            }
            else if (this.ObjStatus == OBJ_STATUS.ON_CURSOR)
            {
                alpha = 128;
            }


            //描画位置を作る
            Point[] DrawPoints = new Point[ObjectBase.OBJ_POINTS_NUM];
            TranslatePosition(this.Points, ref DrawPoints);
            //表示領域の調整
            {
                int offset_x = 0;
                int offset_y = 0;
                double rate = 1.0;
                Common.MakeFieldPositionOffset(ref offset_x, ref offset_y, ref rate);

                for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                {
                    DrawPoints[i].X -= offset_x;
                    DrawPoints[i].Y -= offset_y;
                    double tmp_x = (double)DrawPoints[i].X * rate;
                    double tmp_y = (double)DrawPoints[i].Y * rate;
                    DrawPoints[i].X = (int)(tmp_x);
                    DrawPoints[i].Y = (int)(tmp_y);
                }
            }
            using (Pen pen = new Pen(Color.FromArgb(alpha, GUIParam.GetInstance().ObjectColor), 4))
            {
                Point[] points = new Point[3];
                points[0] = DrawPoints[0];
                points[1] = DrawPoints[1];
                points[2] = DrawPoints[2];

                g.DrawCurve(pen, points);

                //スプライン計算
                MakeSplineFunc(this.Points);
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
                    DrawPoints[i].X - VERTEX_SIZE / 2,
                    DrawPoints[i].Y - VERTEX_SIZE / 2,
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
        private void MakeSplineFunc(Point[] DrawPoints)
        {
            double[] x_array = new double[] { (double)DrawPoints[0].X, (double)DrawPoints[1].X, (double)DrawPoints[2].X };
            double[] y_array = new double[] { (double)DrawPoints[0].Y, (double)DrawPoints[1].Y, (double)DrawPoints[2].Y };

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
