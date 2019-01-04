using System;
using System;
using System.Drawing;

namespace FootballBoard
{
    class OStatePolygon : ObjectState
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
                ObjectPolygon poly = new ObjectPolygon(pos);
                this.model.ObjectList.Add(poly);
                this.CurrentObj = poly;
                poly.DrugType = ObjectPolygon.DRUG_TYPE.INIT;
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
        private ObjectPolygon CurrentObj;
    }
    class ObjectPolygon : ObjectBase
    {
        //座標は左上起点で時計回りに０１２３
        //コンストラクタ
        public ObjectPolygon(Point pos)
        {
            this.Points[0] = pos;
            this.Points[1] = pos;
            this.Points[2] = pos;
            this.Points[3] = pos;
        }

        public enum DRUG_TYPE
        {
            NON,
            WHOLE,          //全体
            POINT_1,        //頂点
            POINT_2,
            POINT_3,
            POINT_4,
            INIT,           //新しく作ったときの動き
        };
        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            //何を掴んでいるかで場合分け
            switch (this.DrugType)
            {
                case DRUG_TYPE.POINT_1:
                    {
                        this.Points[0] = pos;
                    }
                    break;
                case DRUG_TYPE.POINT_2:
                    {
                        this.Points[1] = pos;
                    }
                    break;
                case DRUG_TYPE.POINT_3:
                    {
                        this.Points[2] = pos;
                    }
                    break;
                case DRUG_TYPE.POINT_4:
                    {
                        this.Points[3] = pos;
                    }
                    break;
                case DRUG_TYPE.INIT:
                    {
                        //新しく作ったときの動き
                        this.Points[2] = pos;

                        int w = Points[2].X - Points[0].X;

                        this.Points[1].X = this.Points[0].X + (w/2);
                        this.Points[1].Y = this.Points[0].Y;

                        this.Points[3].X = this.Points[0].X + (w / 2);
                        this.Points[3].Y = this.Points[2].Y;


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
        //ポリゴンを描画
        public override void DrawObject(Graphics g)
        {
            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                g.FillPolygon(Brushes.Black, this.Points);
            }
            else
            {
                g.FillPolygon(Brushes.Red, this.Points);
            }
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            return false;
        }

        public DRUG_TYPE DrugType = DRUG_TYPE.NON;
        private Point MoveStartPos = new Point();   //移動量をつくるため
    }
}
