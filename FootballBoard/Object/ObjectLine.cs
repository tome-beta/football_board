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
            //クリックしたところにすでにラインがあるか
            if(this.CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                ObjectLine line = new ObjectLine(pos);
                this.model.ObjectList.Add(line);
                CurrentObj = line;
                line.DrugType = ObjectLine.DRUG_TYPE.END_POINT;
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

        private ObjectLine CurrentObj;
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
            Console.WriteLine(this.ObjStatus);

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
                g.DrawLine(pen, this.Points[0], this.Points[1]);
            }


            //SELECT状態の時には開始点と終了点を表示する
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                Brush brush = Brushes.Yellow;

                for(int i = 0; i < 2;i++)
                {
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
                FuncLine(ref A, ref B, ref C);

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
                this.DrugType = DRUG_TYPE.NON;
            }



            return false;
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
        private Point MoveStartPos = new Point();   //移動量をつくるため
    }
}
