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
        public override void LeftMouseDown(Point pos) { }
        //左ドラッグ
        public override void LeftMouseDrag(Point pos) { }
        //左を離したとき
        public override void LeftMouseUp(Point pos) { }
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
