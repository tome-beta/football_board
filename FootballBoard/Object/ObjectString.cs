using System;
using System.Drawing;

namespace FootballBoard
{

    [Serializable()]
    class ObjectString : ObjectBase
    {
        public enum DRUG_TYPE
        {
            NON,
            INIT,           //最初
            WHOLE,          //全体
        };

        //座標は左上起点で時計回りに０１２３
        //コンストラクタ
        public ObjectString(Point pos)
        {
            //３箇所を動かす
            this.Points[0] = pos;
            this.Points[1] = pos;
            this.Points[2] = pos;
            this.Points[3] = pos;

            int offset = 5;
            //調整
            this.Points[0].X -= offset;
            this.Points[0].Y -= offset;


            this.Points[1].X += 10 + offset;
            this.Points[1].Y += 10 - offset;

            this.Points[2].X += 10 + offset;
            this.Points[2].Y += 20 + offset;

            this.Points[3].X += 20 - offset;
            this.Points[3].Y += 20 + offset;

            this.DrugType = ObjectString.DRUG_TYPE.INIT;
            this.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
        }

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case DRUG_TYPE.INIT:
                    {
                        //３箇所を動かす
                        this.Points[0] = pos;
                        this.Points[1] = pos;
                        this.Points[2] = pos;
                        this.Points[3] = pos;

                        int offset = 5;
                        //調整
                        this.Points[0].X -= offset;
                        this.Points[0].Y -= offset;


                        this.Points[1].X += 10 + offset;
                        this.Points[1].Y += 10 - offset;

                        this.Points[2].X += 10 + offset;
                        this.Points[2].Y += 20 + offset;

                        this.Points[3].X += 20 - offset;
                        this.Points[3].Y += 20 + offset;

                    }
                    break;
                case DRUG_TYPE.WHOLE:
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
                default:
                    break;

            }
        }

        //矩形を描画
        public override void DrawObject(Graphics g)
        {
            //描画位置を作る
            Point[] DrawPoints = new Point[ObjectBase.OBJ_POINTS_NUM];
            TranslatePosition(this.Points, ref DrawPoints);

            //フォントオブジェクトの作成
            Font fnt = new Font("MS UI Gothic", 20);
            //文字列を位置(0,0)、青色で表示
            g.DrawString(DispString, fnt, Brushes.White, DrawPoints[0].X, DrawPoints[0].Y);

            //矩形全体との当たり判定
            Rectangle rect;
            if( DispString.Length == 0)
            {
                //矩形全体との当たり判定
                int min_x = Common.Min(DrawPoints[0].X, DrawPoints[1].X, DrawPoints[2].X, DrawPoints[3].X);
                int max_x = Common.Max(DrawPoints[0].X, DrawPoints[1].X, DrawPoints[2].X, DrawPoints[3].X);
                int min_y = Common.Min(DrawPoints[0].Y, DrawPoints[1].Y, DrawPoints[2].Y, DrawPoints[3].Y);
                int max_y = Common.Max(DrawPoints[0].Y, DrawPoints[1].Y, DrawPoints[2].Y, DrawPoints[3].Y);

                rect = new Rectangle(min_x, min_y, max_x - min_x, max_y - min_y);
            }
            else
            {
                //幅の最大値が1000ピクセルとして、文字列を描画するときの大きさを計測する
                StringFormat sf = new StringFormat();
                SizeF stringSize = g.MeasureString(DispString, fnt, 1000, sf);
                //取得した文字列の大きさを使って四角を描画する
                rect = new Rectangle(DrawPoints[0].X, DrawPoints[0].Y, (int)stringSize.Width, (int)stringSize.Height);
                this.DispWidth = (int)stringSize.Width;
                this.DispHeight = (int)stringSize.Height;
/*
                //向きによって変える
                if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.VERTICAL)
                {
                    this.DispWidth = (int)stringSize.Height;
                    this.DispHeight = (int)stringSize.Width;
                }
                else if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.LEFT)
                {
                    this.DispWidth = (int)stringSize.Height;
                    this.DispHeight = (int)stringSize.Width;
                }
*/

            }

            int alpha = 255;
            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                alpha = 255;
            }
            else if (this.ObjStatus == OBJ_STATUS.ON_CURSOR)
            {
                alpha = 128;
            }
            Brush brush = new SolidBrush(Color.FromArgb(alpha, GUIParam.GetInstance().ObjectColor));


            if (this.ObjStatus != OBJ_STATUS.NON)
            {
                using (Pen pen = new Pen(brush, 4))
                {
                    g.DrawRectangle(pen, rect);
                }


            }
/*            using (Pen pen = new Pen(Color.Blue, 4))
            {
                g.DrawRectangle(pen, DrawPoints[0].X, DrawPoints[0].Y, DispWidth, DispHeight);
            }
*/        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            //文字列は常に開始点から右に増えていくのでそれを考えた当たり判定になる
            if(this.DispString.Length == 0)
            {
                //幅の最大値が1000ピクセルとして、文字列を描画するときの大きさを計測する
                //フォントオブジェクトの作成
                //矩形全体との当たり判定
                int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
                int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
                int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
                int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

                if ((min_x <= pos.X) && (pos.X <= max_x) &&
                    (min_y <= pos.Y) && (pos.Y <= max_y)
                    )
                {
                    this.DrugType = DRUG_TYPE.WHOLE;
                    this.MoveStartPos = pos;    //全体を動かす基準点
                    return true;
                }
            }
            else
            {
                if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.VERTICAL)
                {
                    if ((Points[0].X - this.DispHeight <= pos.X) && (pos.X <= Points[0].X) &&
                        (Points[0].Y <= pos.Y) && (pos.Y <= Points[0].Y + this.DispWidth)
                        )
                    {
                        this.DrugType = DRUG_TYPE.WHOLE;
                        this.MoveStartPos = pos;    //全体を動かす基準点
                        return true;
                    }
                }
                else if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.LEFT)
                {
                    if ((Points[0].X - this.DispWidth <= pos.X) && (pos.X <= Points[0].X) &&
                        (Points[0].Y - this.DispHeight<= pos.Y) && (pos.Y <= Points[0].Y)
                        )
                    {
                        this.DrugType = DRUG_TYPE.WHOLE;
                        this.MoveStartPos = pos;    //全体を動かす基準点
                        return true;
                    }
                }
                else
                {
                    if ((Points[0].X <= pos.X) && (pos.X <= Points[0].X + this.DispWidth) &&
                        (Points[0].Y <= pos.Y) && (pos.Y <= Points[0].Y + this.DispHeight)
                        )
                    {
                        this.DrugType = DRUG_TYPE.WHOLE;
                        this.MoveStartPos = pos;    //全体を動かす基準点
                        return true;
                    }
                }

            }

            return false;
        }

        private int DispWidth = 0;
        private int DispHeight = 0;

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため

    }
}
