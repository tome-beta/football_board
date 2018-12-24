using System.Drawing;

namespace FootballBoard
{
    //ラインの振る舞いを示すクラス
    public class OStateCurve : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            ObjectCurve curve = new ObjectCurve(pos);
            this.model.ObjectList.Add(curve);

            CurrentObjIndex = this.model.ObjectList.Count - 1;
        }
        //左ドラッグ
        public override void MouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                ObjectCurve curve = (ObjectCurve)this.model.ObjectList[this.CurrentObjIndex];
                curve.SetEndPoint(pos);
            }

        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            ObjectCurve curve = (ObjectCurve)this.model.ObjectList[this.CurrentObjIndex];
            curve.SetEndPoint(pos);
        }
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

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            return false;
        }

    }

}
