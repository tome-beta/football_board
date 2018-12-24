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
            //マーカーを追加する
            ObjectMarker marker = new ObjectMarker(pos);
            this.model.ObjectList.Add(marker);
        }
        //左ドラッグ
        public override void MouseMove(Point pos) { }
        //左を離したとき
        public override void LeftMouseUp(Point pos) { }
    }

    //マーカー
    public class ObjectMarker : ObjectBase
    {
        public ObjectMarker(Point pos)
        {
            this.Points[0] = pos;
        }

        //マーカーを描画
        public override void DrawObject(Graphics g)
        {
            Brush brush;
            brush = Brushes.Red;

            if(Selected)
            {
                brush = Brushes.Blue;
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
            double dist = GetDistance(pos, this.Points[0]);

            if (dist < Width / 2)
            {
                return true;
            }
            return false;
        }

        private double GetDistance(Point a, Point b)
        {
            double distance = Math.Sqrt((b.X - a.X) * (b.X - a.X) +
                (b.Y - a.Y) * (b.Y - a.Y));

            return distance;
        }

        int TeamType;   //HomeかAwayか

        public int Width = 30;
        public int Height = 30;
    }

}
