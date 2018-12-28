using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //ラインの振る舞いを示すクラス
    public class OStateLine : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            ObjectLine line = new ObjectLine(pos);
            this.model.ObjectList.Add(line);
            line.DrugType = ObjectLine.DRUG_TYPE.END_POINT;

            CurrentObjIndex = this.model.ObjectList.Count - 1;
        }
        //左ドラッグ
        public override void MouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                ObjectLine line = (ObjectLine)this.model.ObjectList[this.CurrentObjIndex];
                //何を掴んでいるかで場合分け
                line.SetEndPoint(pos);
            }

        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            ObjectLine line = (ObjectLine)this.model.ObjectList[this.CurrentObjIndex];
            line.SetEndPoint(pos);
        }
    }


    //ライン
    public class ObjectLine : ObjectBase
    {

        public enum DRUG_TYPE
        {
            NON,
            WHOLE,          //全体
            START_POINT,    //開始点
            END_POINT       //終了点
        };
        //コンストラクタ
        public ObjectLine(Point pos)
        {
            this.Points[0] = pos;
            this.Points[1] = pos;
        }

        //ドラッグしているときの動き
        public void DrugMove(Point pos)
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
                case ObjectLine.DRUG_TYPE.WHOLE:
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
            Color col;
            if (this.ObjStatus == OBJ_STATUS.ON_CURSOR)
            {
                col = Color.Blue;
            }
            else
            {
                col = Color.Red;
            }

            //SELECT状態の時には開始点と終了点を表示する
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                Brush brush = Brushes.Yellow;

                for(int i = 0; i < 2;i++)
                {
                    g.FillEllipse(brush, new Rectangle(
                    this.Points[i].X - PointWidth / 2,
                    this.Points[i].Y - PointHeight / 2,
                    PointWidth,
                    PointHeight)
                    );
                }

            }

            using (Pen pen = new Pen(col, 4))
            {
                g.DrawLine(pen, this.Points[0], this.Points[1]);
            }
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            if(this.ObjStatus == OBJ_STATUS.SELECT)
            {
                //このときは開始点と終了点を探す
            }

            //直線の式を導き出す
            double A=0, B=0, C=0;
            FuncLine(ref A, ref B, ref C);

            //点と直線の距離
            double arpha = Math.Abs(A * pos.X + B * pos.Y + C);
            double beta = Math.Sqrt(A*A+B*B);

            double dist = arpha / beta;

            if( dist < 10)
            {
                this.DrugType = DRUG_TYPE.WHOLE;
                this.MoveStartPos = pos;    //全体を動かす基準点
                return true;
            }

            this.DrugType = DRUG_TYPE.NON;
            return false;
        }

        //２点から直線の式を作る
        private void FuncLine(ref double A, ref double B, ref double C)
        {
            //異なる二点(x1, y1)，(x2, y2) を通る直線の方程式は，
            //(x2−x1)(y−y1)= (y2−y1)(x−x1)
            //tmp1(y−y1)= tmp2(x−x1)
            //(y−y1)=(tmp2(x−x1) / tmp1)
            //y=(tmp2(x−x1) / tmp1) + y1
            //y = tmp3*x - tmpp3*x1 + y1;
            //-tmp3*x + y +tmp3*x1- y1 = 0;

            int tmp1 = Points[1].X - Points[0].X;
            int tmp2 = Points[1].Y - Points[0].Y;
            double tmp3 = (double)tmp2 / (double)tmp1;
            A = -tmp3;
            B = 1;
            C = tmp3 * Points[0].X - Points[0].Y;

        }

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;

        private int PointWidth = 20;
        private int PointHeight = 20;

        private Point MoveStartPos = new Point();   //移動量をつくるため
    }
}
