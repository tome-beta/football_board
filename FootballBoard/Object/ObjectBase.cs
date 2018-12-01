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
            this.Start = new Point(0, 0);
            this.End = new Point(0, 0);
            this.Selected = false;
        }

        public abstract void DrawObject(Graphics g);  //描画

        public Point Start;    //開始位置
        public Point End;      //終了位置
        public bool Selected;  //選択中
    }

    //ライン
    public class ObjectLine : ObjectBase
    {
        public ObjectLine(Point pos)
        {
            this.Start = pos;
            this.End = pos;
        }

        public void SetEndPoint(Point pos)
        {
            this.End = pos;

        }

        //ラインをを描画
        public override void DrawObject(Graphics g)
        {
            using (Pen pen = new Pen(Color.Red, 4))
            {
                g.DrawLine(pen, this.Start, this.End);
            }
        }
    }

    //マーカー
    public class ObjectMarker : ObjectBase
    {
        public ObjectMarker(Point pos)
        {
            this.Start = pos;
        }

        //マーカーを描画
        public override void DrawObject(Graphics g)
        {
            using (Pen pen = new Pen(Color.Red, 4))
            {
                g.FillEllipse(Brushes.Red, new Rectangle(
                    this.Start.X - Width / 2,
                    this.Start.Y - Height / 2,
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
