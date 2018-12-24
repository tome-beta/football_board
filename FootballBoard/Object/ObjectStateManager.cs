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
            //何も選択していない
            if(this.SelectObjIndex < 0)
            {
                return;
            }

            CurrentObjIndex = -1;
            //オブジェクトリストから一番近い場所のオブジェクトを探す
            ObjectMarker marker = this.model.ObjectList[this.SelectObjIndex] as ObjectMarker;
            if( marker != null)
            {
                this.CurrentObjIndex = this.SelectObjIndex;
            }
        }

        //左ドラッグ
        public override void MouseMove(Point pos)
        {
            //選択状態を一度初期化
            foreach (ObjectBase obj in this.model.ObjectList)
            {
                obj.Selected = false;
            }

                if (this.MouseDrag)
            {
                //マウスドラッグ中
                if (CurrentObjIndex >= 0)
                {
                    ObjectBase obj = this.model.ObjectList[this.CurrentObjIndex];
                    obj.Points[0] = pos;
                }
            }
            else
            {
                //オブジェクト毎の距離を調べて選択状態にする
                //オブジェクトリストから一番近い場所のオブジェクトを探す
                int count = 0;
                SelectObjIndex = -1;
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    ObjectMarker marker = obj as ObjectMarker;
                    marker.Selected = false;
                    if (marker != null)
                    {
                        if(marker.CheckDistance(pos))
                        {
                            marker.Selected = true;
                            SelectObjIndex = count;
                            break;
                        }
                    }
                    count++;
                }
            }
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            this.CurrentObjIndex = -1;
            this.SelectObjIndex = -1;
        }


    }

    public abstract class ObjectState
    {
        //左クリックしたとき
        public abstract void LeftMouseDown(Point pos);
        //マウスを動かす（ドラッグも込み）
        public abstract void MouseMove(Point pos);
        //左を離したとき
        public abstract void LeftMouseUp(Point pos);

        //カーソルを動かしているとき


        public DataModel model;         //データを扱うため
        public bool MouseDrag = false;  //ドラッグしているか

        //操作中のオブジェクトのインデックス
        public int CurrentObjIndex = -1; //操作中のオブジェ
        public int SelectObjIndex = -1;  //カーソルが近くにある
    }



}
