using System;
using System.Drawing;

namespace FootballBoard
{
    //文字列の振る舞いを示すクラス
    class OStateString : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            //クリックしたところにすでに矩形があるか
            if (this.CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                ObjectString str = new ObjectString(pos);
                this.model.ObjectList.Add(str);
                this.CurrentObj = str;
                str.DrugType = ObjectString.DRUG_TYPE.INIT;
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
        private ObjectString CurrentObj;
    }

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
            g.DrawString(DispString, fnt, Brushes.Blue, this.Points[0].X, this.Points[0].Y);

            //TODO 文字数で矩形の数を変える
            int char_num = 0;
            int ret_num = 0;
            CheckString(ref ret_num, ref char_num);

            //矩形全体との当たり判定
            int min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            int min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            int max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);

            Rectangle rect = new Rectangle(min_x, min_y, max_x - min_x, max_y - min_y);

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

            return false;
        }

        /// <summary>
        /// 文字列のチェック
        /// </summary>
        /// <param name="ret_num">改行の数</param>
        /// <param name="max_chara">１行の最大文字数位</param>
        private void CheckString(ref int ret_num,ref int max_chara)
        {
            string[] split_array = DispString.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            ret_num = split_array.Length;

            int max = 0;
            for(int i = 0; i < split_array.Length;i++)
            {
                int tmp = split_array[i].Length;
                if(max < tmp)
                {
                    max = tmp;
                }
            }
            max_chara = max;
        }

        String DispString = @"テストです";

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため
    }
}
