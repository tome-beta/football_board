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
        public override void LeftMouseDrag(Point pos) { }
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
