using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //MOVEの動作を示すクラス
    public class MoveState : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            CurrentObjIndex = -1;
            //オブジェクトリストから一番近い場所のオブジェクトを探す
            int count = 0;
            foreach(ObjectBase obj in this.model.ObjectList )
            {
                ObjectMarker marker = obj as ObjectMarker;
                if( marker != null)
                {
                    double dist = GetDistance(pos, marker.Points[0]);
                   
                    if( dist < marker.Width / 2)
                    {
                        CurrentObjIndex = count;
                        break;
                    }

                }
                count++;
            }


        }
        //左ドラッグ
        public override void LeftMouseDrag(Point pos)
        {
            if (this.MouseDrag && CurrentObjIndex >= 0)
            {
                ObjectBase obj = this.model.ObjectList[this.CurrentObjIndex];
                obj.Points[0] = pos;
            }
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos) { }

        private double GetDistance(Point a, Point b)
        {
            double distance = Math.Sqrt((b.X - a.X) * (b.X - a.X) +
                (b.Y - a.Y) * (b.Y - a.Y));

            return distance;
        }
    }

    //マーカーの振る舞いを示すクラス
    public class MarkerState : ObjectState
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

    //ラインの振る舞いを示すクラス
    public class LineState : ObjectState
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
            if(this.MouseDrag)
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

    //ラインの振る舞いを示すクラス
    public class CurveState : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            ObjectCurve curve = new ObjectCurve(pos);
            this.model.ObjectList.Add(curve);

            CurrentObjIndex = this.model.ObjectList.Count - 1;
        }
        //左ドラッグ
        public override void LeftMouseDrag(Point pos)
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

    public abstract class ObjectState
    {
        //左クリックしたとき
        public abstract void LeftMouseDown(Point pos);
        //左ドラッグ
        public abstract void LeftMouseDrag(Point pos);
        //左を離したとき
        public abstract void LeftMouseUp(Point pos);

        public DataModel model;         //データを扱うため
        public bool MouseDrag = false;  //ドラッグしているか

        //操作中のオブジェクトのインデックス
        public int CurrentObjIndex = 0;
    }



}
