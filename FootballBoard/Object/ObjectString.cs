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
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            int min_x, max_x;
            int min_y, max_y;

            //文字列は常に開始点から右に増えていくのでそれを考えた当たり判定になる
            if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.VERTICAL)
            {
                min_x = Points[0].X - this.DispHeight;
                max_x = Points[0].X;
                min_y = Points[0].Y;
                max_y = Points[0].Y + this.DispWidth;
            }
            else if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.LEFT)
            {
                min_x = Points[0].X - this.DispWidth;
                max_x = Points[0].X;
                min_y = Points[0].Y - this.DispHeight;
                max_y = Points[0].Y;
            }
            else
            {
                min_x = Points[0].X;
                max_x = Points[0].X + this.DispWidth;
                min_y = Points[0].Y;
                max_y = Points[0].Y + this.DispHeight;
            }

            if ((min_x <= pos.X) && (pos.X <= max_x) &&
                (min_y <= pos.Y) && (pos.Y <= max_y)
                )
            {
                this.DrugType = DRUG_TYPE.WHOLE;
                this.MoveStartPos = pos;    //全体を動かす基準点
                return true;
            }
            return false;
        }

        private int DispWidth = 0;
        private int DispHeight = 0;

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため

    }
}
