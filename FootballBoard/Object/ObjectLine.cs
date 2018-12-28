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

            CurrentObjIndex = this.model.ObjectList.Count - 1;
        }
        //左ドラッグ
        public override void MouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                ObjectLine line = (ObjectLine)this.model.ObjectList[this.CurrentObjIndex];
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
        public ObjectLine(Point pos)
        {
            this.Points[0] = pos;
            this.Points[1] = pos;
        }

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
                return true;
            }



            return false;
        }

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


    }
}
