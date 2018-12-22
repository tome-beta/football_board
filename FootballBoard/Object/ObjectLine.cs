using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //ラインの振る舞いを示すクラス
    public class OStateLine : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            ObjectLine line = new ObjectLine(pos);
            this.model.ObjectList.Add(line);

            CurrentObjIndex = this.model.ObjectList.Count - 1;
        }
        //左ドラッグ
        public override void LeftMouseDrag(Point pos)
        {
            if (this.MouseDrag)
            {
                ObjectLine line = (ObjectLine)this.model.ObjectList[this.CurrentObjIndex];
                line.SetEndPoint(pos);
            }

        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            ObjectLine line = (ObjectLine)this.model.ObjectList[this.CurrentObjIndex];
            line.SetEndPoint(pos);
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
                g.DrawLine(pen, this.Points[0], this.Points[1]);
            }
        }
    }
}
