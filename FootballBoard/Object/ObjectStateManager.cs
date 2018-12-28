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
            if(this.OnCursolIndex < 0)
            {
                //選択状態を一度初期化
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    obj.ObjStatus = ObjectBase.OBJ_STATUS.NON;
                }
                return;
            }

            CurrentObjIndex = -1;
            //ここでON_CURSORのやつを探して
            //DRUG状態に移行する
            ObjectMarker marker = this.model.ObjectList[this.OnCursolIndex] as ObjectMarker;
            if( marker != null)
            {
                marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.CurrentObjIndex = this.OnCursolIndex;
            }

            ObjectLine line = this.model.ObjectList[this.OnCursolIndex] as ObjectLine;
            if (line != null)
            {
                //ここで直線のとの当たりチェックをする
                if(line.CheckDistance(pos) )
                {
                    //どこのパーツを掴んでいるかを決める必要がある
                    line.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                    this.CurrentObjIndex = this.OnCursolIndex;
                }

            }

        }

        //マウスを動かす
        public override void MouseMove(Point pos)
        {
            //選択状態を一度初期化
            foreach (ObjectBase obj in this.model.ObjectList)
            {
                obj.ObjStatus = ObjectBase.OBJ_STATUS.NON;
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
                //オブジェクト毎の距離を調べてON_CURSOR状態にする
                //オブジェクトリストから一番近い場所のオブジェクトを探す
                int count = 0;
                OnCursolIndex = -1;
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    //リストからのオブジェクトの選択方法は考えたほうがいい

                    ObjectMarker marker = obj as ObjectMarker;
                    if (marker != null)
                    {
                        marker.ObjStatus = ObjectBase.OBJ_STATUS.NON;
                        if (marker.CheckDistance(pos))
                        {
                            marker.ObjStatus = ObjectBase.OBJ_STATUS.ON_CURSOR;
                            OnCursolIndex = count;
                            break;
                        }
                    }

                    ObjectLine line = obj as ObjectLine;
                    if (line != null)
                    {
                        line.ObjStatus = ObjectBase.OBJ_STATUS.NON;
                        if (line.CheckDistance(pos))
                        {
                            line.ObjStatus = ObjectBase.OBJ_STATUS.ON_CURSOR;
                            OnCursolIndex = count;
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
            //離した時にDRUG状態のやつはSELECTに移す

            this.CurrentObjIndex = -1;
            this.OnCursolIndex = -1;
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
        public int OnCursolIndex = -1;  //カーソルが上にある

    }



}
