using System;
using System.Drawing;

namespace FootballBoard
{
    //描画オブジェクトのベース
    public abstract class ObjectBase
    {
        public enum OBJ_STATUS
        {
            NON,        //無し
            ON_CURSOR,  //上に載せた
            SELECT,     //一度選択した
            DRUG,       //掴んでいる
        };

        public ObjectBase()
        {
            for(int i = 0; i < 4; i++)
            {
                Points[i].X = 0;
                Points[i].Y = 0;
            }

//            this.Selected = false;
        }

        protected double GetDistance(Point a, Point b)
        {
            double distance = Math.Sqrt((b.X - a.X) * (b.X - a.X) +
                (b.Y - a.Y) * (b.Y - a.Y));

            return distance;
        }

        public abstract bool CheckDistance(Point pos);

        public abstract void DrawObject(Graphics g);  //描画

        //矩形や曲線も扱うため
        public Point[] Points = new Point[4];

        public OBJ_STATUS ObjStatus = OBJ_STATUS.NON;
    }
}
