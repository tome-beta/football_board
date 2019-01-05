using System;
using System.Drawing;

namespace FootballBoard
{
    //マーカーの振る舞いを示すクラス
    public class OStateMarker : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            //DRUG状態のマーカーが近くにあるときは動かせる様にする
            if( CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                //全てのオブジェクトを初期状態にする
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    obj.ObjStatus = ObjectBase.OBJ_STATUS.NON;
                }

                //マーカーを追加する
                ObjectMarker marker = new ObjectMarker(pos);
                this.model.ObjectList.Add(marker);

                CurrentObj = marker;
                marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
        }
        //左ドラッグ
        public override void MouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                CurrentObj.Points[0] = pos;
            }
        }
        
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            this.MouseDrag = false;
        }


        //文字列を設定する
        public override void SetString(String str)
        {
        }

        private ObjectMarker CurrentObj;
    }

    //マーカー
    public class ObjectMarker : ObjectBase
    {
        public ObjectMarker(Point pos)
        {
            this.Points[0] = pos;
        }

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            this.Points[0] = pos;
        }
        //マーカーを描画
        public override void DrawObject(Graphics g)
        {
            Brush brush;
            brush = Brushes.Red;

            if(this.ObjStatus == OBJ_STATUS.NON)
            {
                brush = Brushes.Black;
            }

            g.FillEllipse(brush, new Rectangle(
            this.Points[0].X - Width / 2,
            this.Points[0].Y - Height / 2,
            Width,
            Height)
            );
        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            double dist = Common.GetDistance(pos, this.Points[0]);

            if (dist < Width / 2)
            {
                return true;
            }
            return false;
        }

        int TeamType;   //HomeかAwayか

        public int Width = 30;
        public int Height = 30;
    }

}
