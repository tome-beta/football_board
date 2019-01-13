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

            g.FillEllipse(brush, new Rectangle(
            this.Points[0].X - Width / 2,
            this.Points[0].Y - Height / 2,
            Width,
            Height)
            );

            //選択したときの三角をつくる
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                DrawSelectTriangle(g);
            }

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

        private void DrawSelectTriangle(Graphics g)
        {
            Brush brush_tri = new SolidBrush(Color.Black);
            //左
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = this.Points[0].X - Width / 2;
                f_points[0].Y = this.Points[0].Y;

                f_points[1].X = f_points[0].X - (Width / 3);
                f_points[1].Y = f_points[0].Y - (Height / 4);
                f_points[2].X = f_points[0].X - (Width / 3);
                f_points[2].Y = f_points[0].Y + (Height / 4);

                g.FillPolygon(brush_tri, f_points);
            }
            //上
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = this.Points[0].X;
                f_points[0].Y = this.Points[0].Y - Height/ 2;

                f_points[1].X = f_points[0].X - (Width / 4);
                f_points[1].Y = f_points[0].Y - (Height / 3);
                f_points[2].X = f_points[0].X + (Width / 4);
                f_points[2].Y = f_points[0].Y - (Height / 3);

                g.FillPolygon(brush_tri, f_points);
            }

            //右
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = this.Points[0].X + Width / 2;
                f_points[0].Y = this.Points[0].Y;

                f_points[1].X = f_points[0].X + (Width / 3);
                f_points[1].Y = f_points[0].Y - (Height / 4);
                f_points[2].X = f_points[0].X + (Width / 3);
                f_points[2].Y = f_points[0].Y + (Height / 4);

                g.FillPolygon(brush_tri, f_points);
            }
            //下
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = this.Points[0].X;
                f_points[0].Y = this.Points[0].Y + Height / 2;

                f_points[1].X = f_points[0].X - (Width / 4);
                f_points[1].Y = f_points[0].Y + (Height / 3);
                f_points[2].X = f_points[0].X + (Width / 4);
                f_points[2].Y = f_points[0].Y + (Height / 3);

                g.FillPolygon(brush_tri, f_points);
            }
        }


        int TeamType;   //HomeかAwayか

        public int Width = 30;
        public int Height = 30;
    }

}
