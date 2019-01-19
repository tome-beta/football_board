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
                circle.DrugType = ObjectCircle.DRUG_TYPE.INIT;
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
    //円オブジェクト
    class ObjectCircle : ObjectBase
    {
        public enum DRUG_TYPE
        {
            POINT_1,        //頂点
            POINT_2,
            POINT_3,
            POINT_4,
            WHOLE,          //全体
            INIT,           //初期
            NON,
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
                case ObjectCircle.DRUG_TYPE.INIT:
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

        //円を描画
        public override void DrawObject(Graphics g)
        {
            //矩形全体との当たり判定
            int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

            Rectangle rect = new Rectangle(min_x, min_y, max_x - min_x, max_y - min_y);

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

            g.FillEllipse(brush, rect);

            //SELECT状態の時には開始点と終了点を表示する
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                for (int i = 0; i < 4; i++)
                {
                    if ( (int)this.DrugType == i)
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
                for (int i = 0; i < 4; i++)
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
            if(CheckCircle(pos))
            {
                this.DrugType = DRUG_TYPE.WHOLE;
                this.MoveStartPos = pos;    //全体を動かす基準点
                return true;
            }

            this.DrugType = DRUG_TYPE.NON;
            return false;
        }

        //楕円と点の当たり判定
        bool CheckCircle(Point pos)
        {
            //考え方として楕円を真円に変換する行列をかける
            //そのとき点も変換行列にかける

            //矩形全体を取得
            int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

            //中心
            int center_x = (max_x - min_x) / 2 + min_x;
            int center_y = (max_y - min_y) / 2 + min_y;

            int length_x = max_x - center_x; // X軸長
            int length_y = max_y - center_y; // X軸長

            // 点に楕円→真円変換行列を適用
            double Ofs_x = (double)(pos.X - center_x);
            double Ofs_y = (double)(pos.Y - center_y);

            double After_x = Ofs_x * Math.Cos(0) + Ofs_y * Math.Sin(0);
            double After_y = length_x / length_y * (-Ofs_x * Math.Sin(0) + Ofs_y * Math.Cos(0));

            // 原点から移動後点までの距離を算出
            if (After_x * After_x + After_y * After_y <= length_x * length_y)
            {
                return true;   // 衝突
            }

            return false;
        }



        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため

    }
}
