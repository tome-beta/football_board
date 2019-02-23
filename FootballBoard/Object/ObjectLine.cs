using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace FootballBoard
{
    //ライン
    [Serializable()]
    public class ObjectLine : ObjectBase
    {
        public enum DRUG_TYPE
        {
            START_POINT,    //開始点
            END_POINT,       //終了点
            WHOLE,          //全体
            INIT,           //初期
            NON,
        };

        public enum LINE_STYLE
        {
            SOLID,          //直線
            JAGGED,         //波線
            DOTTED,         //点線
        };
        public enum ARROW_STYLE
        {
            NONE,   
            START,  
            END,    
            BOTH,
        };

        //コンストラクタ
        public ObjectLine(Point pos)
        {
            this.Points[0] = pos;
            this.Points[1] = pos;
        }

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case ObjectLine.DRUG_TYPE.START_POINT:
                    {
                        SetStartPoint(pos);
                    }
                    break;
                case ObjectLine.DRUG_TYPE.END_POINT:
                    {
                        SetEndPoint(pos);
                    }
                    break;
                case DRUG_TYPE.INIT:
                    {
                        SetEndPoint(pos);
                    }
                    break;
                case ObjectLine.DRUG_TYPE.WHOLE:
                    {
                        //全体を動かす
                        int move_x = pos.X - this.MoveStartPos.X;
                        int move_y = pos.Y - this.MoveStartPos.Y;

                        for(int i = 0; i < 2; i++)
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
        //開始点を決める
        public void SetStartPoint(Point pos)
        {
            this.Points[0] = pos;
        }

        //終了点を決める
        public void SetEndPoint(Point pos)
        {
            this.Points[1] = pos;
        }

        //ラインをを描画
        public override void DrawObject(Graphics g)
        {
            //描画位置を作る
            Point[] DrawPoints = new Point[ObjectBase.OBJ_POINTS_NUM];
            TranslatePosition(this.Points, ref DrawPoints);

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
                if (this.LineStyle == LINE_STYLE.SOLID)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    g.DrawLine(pen, DrawPoints[0], DrawPoints[1]);
                }
                else if (this.LineStyle == LINE_STYLE.JAGGED)
                {
                    //カーブを重ねる
                    DrawSinLine(g,pen, DrawPoints[0], DrawPoints[1]);
                }
                else if (this.LineStyle == LINE_STYLE.DOTTED)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawLine(pen, DrawPoints[0], DrawPoints[1]);
                }

                //矢印を描く
                DrawArrow(g,pen, DrawPoints);
            }


            //SELECT状態の時には開始点と終了点を表示する
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                Brush brush = Brushes.Yellow;

                for(int i = 0; i < 2;i++)
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
            if(this.ObjStatus == OBJ_STATUS.SELECT)
            {
                //このときは開始点と終了点を探す
                double point_dist = Common.GetDistance(pos, this.Points[0]);
                if (point_dist < VERTEX_SIZE / 2)
                {
                    this.DrugType = DRUG_TYPE.START_POINT;
                    return true;
                }
                point_dist = Common.GetDistance(pos, this.Points[1]);
                if (point_dist < VERTEX_SIZE / 2)
                {
                    this.DrugType = DRUG_TYPE.END_POINT;
                    return true;
                }
            }

            //直線の座標の範囲であることをチェックする
           if(ChkRange(pos))
            {
                //直線の式を導き出す
                double A = 0, B = 0, C = 0;
                FuncLine(Points[0],Points[1],ref A, ref B, ref C);

                //点と直線の距離
                double arpha = Math.Abs(A * pos.X + B * pos.Y + C);
                double beta = Math.Sqrt(A * A + B * B);

                double dist = arpha / beta;

                if (dist < 10)
                {
                    this.DrugType = DRUG_TYPE.WHOLE;
                    this.MoveStartPos = pos;    //全体を動かす基準点
                    return true;
                }
            }

            this.DrugType = DRUG_TYPE.NON;
            return false;
        }

        //======================================================
        //  private 
        //======================================================
        private void DrawSinLine(Graphics g, Pen pen ,Point p1, Point p2)
        {
            //点と点の距離を持っておく
            double distance = Common.GetDistance(p1, p2);
            //２点間の角度
            double radian = Common.GetAngleRadian(p1,p2);
            double degree = Common.RadToDeg(radian);

            double vertical_degree = degree + 90;
            double vertical_radian = Common.DegToRad(vertical_degree);

            //波のための垂直ベクトル
            float vertical_x = (float)Math.Cos(vertical_radian);
            float vertical_y = (float)Math.Sin(vertical_radian);

            //初期位置を決める
            PointF[] _points = new PointF[4];
            _points[0] = p1;

            int add = 5;
            int amplitude = 3; //振幅
            float add_x = (float)(add * Math.Cos(radian));
            float add_y = (float)(add * Math.Sin(radian));
            vertical_y *= amplitude;
            vertical_x *= amplitude;

            while (true)
            {
                //(残りの距離 ＜波の大きさ) 直線を書いて終わり
                if (add * 3> distance)
                {
                    g.DrawLine(pen, _points[0], p2);
                    break;
                }

                _points[1].X = add_x + _points[0].X - vertical_x;
                _points[1].Y = add_y + _points[0].Y - vertical_y;

                _points[2].X = 2 * add_x + _points[0].X + vertical_x;
                _points[2].Y = 2 * add_y + _points[0].Y + vertical_y;

                _points[3].X = 3 * add_x + _points[0].X;
                _points[3].Y = 3 * add_y + _points[0].Y;

                //波を描く
                g.DrawCurve(pen, _points);

                //残り距離を減らす
                distance -= add * 3;
                //次の基準点にする
                _points[0] = _points[3];
            }

        }

        //直線とカーソル位置の範囲チェック
        private bool ChkRange(Point pos)
        {
            int min_x = 0;
            int max_x = 0;
            int min_y = 0;
            int max_y = 0;

            max_x = Common.Max(Points[0].X, Points[1].X);
            max_y = Common.Max(Points[0].Y, Points[1].Y);
            min_x = Common.Min(Points[0].X, Points[1].X);
            min_y = Common.Min(Points[0].Y, Points[1].Y);

            if ( (min_x <= pos.X && pos.X <= max_x) &&
                (min_y <= pos.Y && pos.Y <= max_y)
                )
            {
                return true;
            }



            return false;
        }


        //２点から直線の式を作る
        private void FuncLine(Point p1,Point p2,ref double A, ref double B, ref double C)
        {
            //異なる二点(x1, y1)，(x2, y2) を通る直線の方程式は，
            //(x2−x1)(y−y1)= (y2−y1)(x−x1)
            //tmp1(y−y1)= tmp2(x−x1)
            //(y−y1)=(tmp2(x−x1) / tmp1)
            //y=(tmp2(x−x1) / tmp1) + y1
            //y = tmp3*x - tmpp3*x1 + y1;
            //-tmp3*x + y +tmp3*x1- y1 = 0;

            double tmp1 = p2.X - p1.X;
            double tmp2 = p2.Y - p1.Y;

            if (Math.Abs(tmp1) <= 0.0) tmp1 = 0.00000000001;
            if (Math.Abs(tmp2) <= 0.0) tmp2 = 0.00000000001;

            double tmp3 = (double)tmp2 / (double)tmp1;
            A = -tmp3;
            B = 1;
            C = tmp3 * Points[0].X - Points[0].Y;

        }

        //矢印を描く
        private void DrawArrow(Graphics g,Pen pen, Point[] DrawPoint)
        {
            if (this.ArrowStyle == ARROW_STYLE.NONE) return;

            //２点間の角度
            double radian = Common.GetAngleRadian(DrawPoint[0], DrawPoint[1]);
            double degree = Common.RadToDeg(radian);

            PointF[] _points = new PointF[2];
            int length = 15;

            if (this.ArrowStyle == ARROW_STYLE.START || this.ArrowStyle == ARROW_STYLE.BOTH)
            {
                double arrow_degree1 = degree + 45;
                double arrow_degree2 = degree - 45;
                double arrow_radian1 = Common.DegToRad(arrow_degree1);
                double arrow_radian2 = Common.DegToRad(arrow_degree2);
                _points[0] = DrawPoint[0];
                _points[1].X = _points[0].X + (float)(Math.Cos(arrow_radian1) * length);
                _points[1].Y = _points[0].Y + (float)(Math.Sin(arrow_radian1) * length);

                g.DrawLine(pen, _points[0], _points[1]);

                _points[1].X = _points[0].X + (float)(Math.Cos(arrow_radian2) * length);
                _points[1].Y = _points[0].Y + (float)(Math.Sin(arrow_radian2) * length);

                g.DrawLine(pen, _points[0], _points[1]);
            }
            if (this.ArrowStyle == ARROW_STYLE.END || this.ArrowStyle == ARROW_STYLE.BOTH)
            {
                double arrow_degree1 = degree + 180 + 45;
                double arrow_degree2 = degree + 180 - 45;
                double arrow_radian1 = Common.DegToRad(arrow_degree1);
                double arrow_radian2 = Common.DegToRad(arrow_degree2);
                _points[0] = DrawPoint[1];
                _points[1].X = _points[0].X + (float)(Math.Cos(arrow_radian1) * length);
                _points[1].Y = _points[0].Y + (float)(Math.Sin(arrow_radian1) * length);

                g.DrawLine(pen, _points[0], _points[1]);

                _points[1].X = _points[0].X + (float)(Math.Cos(arrow_radian2) * length);
                _points[1].Y = _points[0].Y + (float)(Math.Sin(arrow_radian2) * length);

                g.DrawLine(pen, _points[0], _points[1]);
            }
        }


        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        public LINE_STYLE LineStyle = LINE_STYLE.SOLID;
        public ARROW_STYLE ArrowStyle = ARROW_STYLE.NONE;

        private Point MoveStartPos = new Point();   //移動量をつくるため
    }
}
