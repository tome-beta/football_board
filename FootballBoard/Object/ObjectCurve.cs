using System.Drawing;

namespace FootballBoard
{
    //スプライン曲線を作るクラス
    public class Spline
    {
        const int MAX_SPLINE_SIZE = 3;

        //スプライン曲線の係数計算
        public void Init(double[] value, int input_num)
        {
            double tmp;
            double[] w = new double[MAX_SPLINE_SIZE + 1];
            int i;

            num = input_num - 1;

            // ３次多項式の0次係数(a)を設定
            for (i = 0; i <= num; i++)
            {
                a[i] = value[i];
            }

            // ３次多項式の2次係数(c)を計算
            // 連立方程式を解く。
            // 但し、一般解法でなくスプライン計算にチューニングした方法
            c[0] = c[num] = 0.0;
            for (i = 1; i < num; i++)
            {
                c[i] = 3.0 * (a[i - 1] - 2.0 * a[i] + a[i + 1]);
            }
            // 左下を消す
            w[0] = 0.0;
            for (i = 1; i < num; i++)
            {
                tmp = 4.0 - w[i - 1];
                c[i] = (c[i] - c[i - 1]) / tmp;
                w[i] = 1.0 / tmp;
            }
            // 右上を消す
            for (i = num - 1; i > 0; i--)
            {
                c[i] = c[i] - c[i + 1] * w[i];
            }

            // ３次多項式の1次係数(b)と3次係数(b)を計算
            b[num] = d[num] = 0.0;
            for (i = 0; i < num; i++)
            {
                d[i] = (c[i + 1] - c[i]) / 3.0;
                b[i] = a[i + 1] - a[i] - c[i] - d[i];
            }
        }

        //実際の値を取り出す
        public double calc(int t)
        {
            double value = 0.0;

            int j;
            double dt;
            if (t < 0)
            {
                t = 0;
            }
            else if (t >= num)
            {
                t = num - 1; // 丸め誤差を考慮
            }
            

            dt = t - (double)t;
            value =  a[t] + (b[t] + (c[t] + d[t] * dt) * dt) * dt;
            return value;
        }

        private int num = 0;
        double[] a = new double[MAX_SPLINE_SIZE + 1];
        double[] b = new double[MAX_SPLINE_SIZE + 1];
        double[] c = new double[MAX_SPLINE_SIZE + 1];
        double[] d = new double[MAX_SPLINE_SIZE + 1];


    }

    //ラインの振る舞いを示すクラス
    public class OStateCurve : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            ObjectCurve curve = new ObjectCurve(pos);
            this.model.ObjectList.Add(curve);
            this.CurrentObj = curve;
            curve.DrugType = ObjectCurve.DRUG_TYPE.END_POINT;
            CurrentObjIndex = this.model.ObjectList.Count - 1;
        }
        //左ドラッグ
        public override void MouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                this.CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                //何を掴んでいるかで場合分けしている
                this.CurrentObj.SetEndPoint(pos);
            }
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            this.CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
        }

        private ObjectCurve CurrentObj;
    }
    //曲線
    public class ObjectCurve : ObjectBase
    {
        public enum DRUG_TYPE
        {
            NON,
            WHOLE,          //全体
            START_POINT,    //開始点
            END_POINT,      //終了点
            MIDDLE_POINT,   //中間点
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
        public void DrugMove(Point pos)
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
                case DRUG_TYPE.WHOLE:
                    {
                        //全体を動かす
                        int move_x = pos.X - this.MoveStartPos.X;
                        int move_y = pos.Y - this.MoveStartPos.Y;

                        this.Points[0].X += move_x;
                        this.Points[0].Y += move_y;
                        this.Points[1].X += move_x;
                        this.Points[1].Y += move_y;

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
            Color col;
            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                col = Color.Black;
            }
            else
            {
                col = Color.Red;
            }
            using (Pen pen = new Pen(col, 4))
            {
                Point[] points = new Point[3];
                points[0] = this.Points[0];
                points[1] = this.Points[1];
                points[2] = this.Points[2];

                g.DrawCurve(pen, points);

                //テスト
//                MakeSplineFunc(g);
            }

            //曲線の３点を描画
            Brush brush = Brushes.Yellow;
            for (int i = 0; i < 3; i++)
            {
                g.FillEllipse(brush, new Rectangle(
                this.Points[i].X - PointWidth / 2,
                this.Points[i].Y - PointHeight / 2,
                PointWidth,
                PointHeight)
                );
            }
        }

        //スプライン曲線の式をつくる
        private void MakeSplineFunc(Graphics g)
        {
            Spline xs = new Spline();
            Spline ys = new Spline();

            double[] x_array = new double[] { (double)Points[0].X, (double)Points[1].X, (double)Points[2].X};
            double[] y_array = new double[] { (double)Points[0].Y, (double)Points[1].Y, (double)Points[2].Y};

            xs.Init(x_array, x_array.Length);
            ys.Init(y_array, y_array.Length);

            Point p1 = new Point();
            p1.X = Points[0].X;
            p1.Y = Points[0].Y;
            Point p2 = new Point();
            Pen pen = new Pen(Color.Blue, 4);

            for (int x = Points[0].X; x < Points[2].X;x++)
            {
                p2.X = (int)xs.calc(x);
                p2.Y = (int)ys.calc(x);

                g.DrawLine(pen, p1, p2);
                p1.X = p2.X;
                p1.Y = p2.Y;

            }


        }


        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
  //          if (this.ObjStatus == OBJ_STATUS.SELECT)
            {
                //このときは開始点と終了点と中間点を探す
                double point_dist = GetDistance(pos, this.Points[0]);
                if (point_dist < PointWidth / 2)
                {
                    this.DrugType = DRUG_TYPE.START_POINT;
                    return true;
                }
                point_dist = GetDistance(pos, this.Points[1]);
                if (point_dist < PointWidth / 2)
                {
                    this.DrugType = DRUG_TYPE.MIDDLE_POINT;
                    return true;
                }

                point_dist = GetDistance(pos, this.Points[2]);
                if (point_dist < PointWidth / 2)
                {
                    this.DrugType = DRUG_TYPE.END_POINT;
                    return true;
                }
            }
            return false;
        }

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;

        private int PointWidth = 10;
        private int PointHeight = 10;

        private Point MoveStartPos = new Point();   //移動量をつくるため
    }

}
