using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //描画オブジェクトのベース
    public abstract class ObjectBase
    {
        public ObjectBase()
        {
            for(int i = 0; i < 4; i++)
            {
                Points[i].X = 0;
                Points[i].Y = 0;
            }

            this.Selected = false;
        }

        public abstract void DrawObject(Graphics g);  //描画

        //矩形や曲線も扱うため
        public Point[] Points = new Point[4];

        public bool Selected;  //選択中
    }

    //曲線
    public class ObjectCurve : ObjectBase
    {
        public ObjectCurve(Point pos)
        {
            this.Points[0] = pos;
            this.Points[2] = pos;

            //中間点
            this.Points[1].X = (this.Points[2].X - Points[0].X) / 2;
            this.Points[1].Y = (this.Points[2].Y - Points[0].Y) / 2;

            this.Points[1].X += this.Points[0].X;
            this.Points[1].Y += this.Points[0].Y;
        }
        public void SetEndPoint(Point pos)
        {
            this.Points[2] = pos;

            //中間点
            this.Points[1].X = (this.Points[2].X - Points[0].X) / 2;
            this.Points[1].Y = (this.Points[2].Y - Points[0].Y) / 2;

            this.Points[1].X += this.Points[0].X;
            this.Points[1].Y += this.Points[0].Y;
        }

        //曲線を描画
        public override void DrawObject(Graphics g)
        {
            using (Pen pen = new Pen(Color.Red, 4))
            {
                Point[] points = new Point[3];
                points[0] = this.Points[0];
                points[1] = this.Points[1];
                points[2] = this.Points[2];

                g.DrawCurve(pen, points);
            }
        }
    }


    //ライン
    public class ObjectLine : ObjectBase
    {
        public ObjectLine(Point pos)
        {
            this.Points[0] = pos;
            this.Points[1] = pos;
        }

        public void SetEndPoint(Point pos)
        {
            this.Points[1] = pos;

        }

        //ラインをを描画
        public override void DrawObject(Graphics g)
        {
            using (Pen pen = new Pen(Color.Red, 4))
            {
                g.DrawLine(pen, this.Points[0],this.Points[1]);
            }
        }
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
            using (Pen pen = new Pen(Color.Red, 4))
            {
                g.FillEllipse(Brushes.Red, new Rectangle(
                    this.Points[0].X - Width / 2,
                    this.Points[0].Y - Height / 2,
                    Width,
                    Height)
                    );
            }
        }

        int TeamType;   //HomeかAwayか

        public int Width = 30;
        public int Height = 30;
    }
}
