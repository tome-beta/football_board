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
            //フォントオブジェクトの作成
            Font fnt = new Font("MS UI Gothic", 20);
            //文字列を位置(0,0)、青色で表示
            g.DrawString(DispString, fnt, Brushes.White, this.Points[0].X, this.Points[0].Y);

            //矩形全体との当たり判定
            Rectangle rect;
            if( DispString.Length == 0)
            {
                //矩形全体との当たり判定
                int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
                int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
                int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
                int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

                rect = new Rectangle(min_x, min_y, max_x - min_x, max_y - min_y);
            }
            else
            {
                //幅の最大値が1000ピクセルとして、文字列を描画するときの大きさを計測する
                StringFormat sf = new StringFormat();
                SizeF stringSize = g.MeasureString(DispString, fnt, 1000, sf);
                //取得した文字列の大きさを使って四角を描画する
                rect = new Rectangle(Points[0].X, Points[0].Y, (int)stringSize.Width, (int)stringSize.Height);
                this.DispWidth = (int)stringSize.Width;
                this.DispHeight = (int)stringSize.Height;
            }

            if (this.ObjStatus != OBJ_STATUS.NON)
            {
                using (Pen pen = new Pen(Color.Red, 4))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
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
                if ((Points[0].X <= pos.X) && (pos.X <= Points[0].X + this.DispWidth) &&
                    (Points[0].Y <= pos.Y) && (pos.Y <= Points[0].Y + this.DispHeight)
                    )
                {
                    this.DrugType = DRUG_TYPE.WHOLE;
                    this.MoveStartPos = pos;    //全体を動かす基準点
                    return true;
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
