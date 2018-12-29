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
            //カーソルをオブジェクトに合わせていない
            if( this.OnCursolIndex < 0)
            {
                //全てのオブジェクトを初期状態にする
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    obj.ObjStatus = ObjectBase.OBJ_STATUS.NON;
                }
                this.CurrentObjIndex = -1;
            }
            else
            {
                //選んだオブジェクト以外をNON状態にする
                for(int i = 0; i < this.model.ObjectList.Count;i++)
                {
                    this.model.ObjectList[i].ObjStatus = ObjectBase.OBJ_STATUS.NON;
                }

                //オブジェクトを選択状態にしているか
                CurrentObjIndex = OnCursolIndex;
                //ここでON_CURSORのやつを探して
                //DRUG状態に移行する
                ObjectMarker marker = this.model.ObjectList[this.OnCursolIndex] as ObjectMarker;
                if (marker != null)
                {
                    marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                    this.CurrentObjIndex = this.OnCursolIndex;
                }

                ObjectLine line = this.model.ObjectList[this.OnCursolIndex] as ObjectLine;
                if (line != null)
                {
                    //ここで直線のとの当たりチェックをする
                    if (line.CheckDistance(pos))
                    {
                        //どこのパーツを掴んでいるかを決める必要がある
                        line.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        this.CurrentObjIndex = this.OnCursolIndex;
                    }

                }
            }

        }

        //マウスを動かす
        public override void MouseMove(Point pos)
        {
            if (this.MouseDrag)
            {

                //マウスドラッグ中
                if (CurrentObjIndex >= 0)
                {
                    //ここでもオブジェクトによって場合わけ
                    ObjectMarker marker = this.model.ObjectList[this.CurrentObjIndex] as ObjectMarker;
                    if (marker != null)
                    {
                        marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        marker.Points[0] = pos;
                    }

                    ObjectLine line = this.model.ObjectList[this.CurrentObjIndex] as ObjectLine;
                    if (line != null)
                    {
                        line.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        line.DrugMove(pos);
                    }
                }
            }
            else
            {
                //選択状態を一度初期化
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    if(obj.ObjStatus != ObjectBase.OBJ_STATUS.SELECT)
                    {
                        obj.ObjStatus = ObjectBase.OBJ_STATUS.NON;
                    }
                }

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
                        if (marker.CheckDistance(pos))
                        {
                            OnCursolIndex = count;
                            if (marker.ObjStatus != ObjectBase.OBJ_STATUS.SELECT)
                            {
                                marker.ObjStatus = ObjectBase.OBJ_STATUS.ON_CURSOR;
                            }
                            break;
                        }
                    }

                    ObjectLine line = obj as ObjectLine;
                    if (line != null)
                    {
                        if (line.CheckDistance(pos))
                        {
                            OnCursolIndex = count;
                            if (line.ObjStatus != ObjectBase.OBJ_STATUS.SELECT)
                            {
                                line.ObjStatus = ObjectBase.OBJ_STATUS.ON_CURSOR;
                                break;
                            }
                        }
                    }

                    count++;
                }
            }
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            if (this.CurrentObjIndex == -1)
            {
                return;
            }

            //離した時にDRUG状態のやつはSELECTに移す
            ObjectMarker marker = this.model.ObjectList[this.CurrentObjIndex] as ObjectMarker;
            if (marker != null)
            {
                marker.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            }

            ObjectLine line = this.model.ObjectList[this.CurrentObjIndex] as ObjectLine;
            if (line != null)
            {
                line.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            }

//            this.CurrentObjIndex = -1;
//            this.OnCursolIndex = -1;
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
