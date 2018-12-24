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

        public abstract bool CheckDistance(Point pos);

        public abstract void DrawObject(Graphics g);  //描画

        //矩形や曲線も扱うため
        public Point[] Points = new Point[4];

        public bool Selected;  //選択中
    }
}
