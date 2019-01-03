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
                bool select_chk = false;

                //オブジェクトを選択状態にしているか
                CurrentObjIndex = OnCursolIndex;
                //ここでON_CURSORのやつを探して
                //DRUG状態に移行する
                ObjectMarker marker = this.model.ObjectList[this.OnCursolIndex] as ObjectMarker;
                if (marker != null)
                {
                    marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                    select_chk = true;
                }

                ObjectLine line = this.model.ObjectList[this.OnCursolIndex] as ObjectLine;
                if (line != null)
                {
                    //ここで直線のとの当たりチェックをする
                    if (line.CheckDistance(pos))
                    {
                        //どこのパーツを掴んでいるかを決める必要がある
                        line.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        select_chk = true;
                    }
                }
                ObjectCurve curve = this.model.ObjectList[this.OnCursolIndex] as ObjectCurve;
                if (curve != null)
                {
                    //ここで直線のとの当たりチェックをする
                    if (curve.CheckDistance(pos))
                    {
                        //どこのパーツを掴んでいるかを決める必要がある
                        curve.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        select_chk = true;
                    }
                }

                //オブジェクトを選択できていたらその他のオブジェクトの状態をリセット
                if (select_chk )
                {
                    //選んだオブジェクト以外をNON状態にする
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                        if (this.model.ObjectList[i].ObjStatus != ObjectBase.OBJ_STATUS.DRUG)
                        {
                            this.model.ObjectList[i].ObjStatus = ObjectBase.OBJ_STATUS.NON;
                        }
                    }
                }
                else
                {
                    //全てNON状態にする
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                            this.model.ObjectList[i].ObjStatus = ObjectBase.OBJ_STATUS.NON;
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
                    //共通にできる
                    ObjectBase obj = this.model.ObjectList[this.CurrentObjIndex];
                    if(obj != null)
                    {
                        obj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        obj.DrugMove(pos);
                    }
                    /*
                    //ここでもオブジェクトによって場合わけ
                    ObjectMarker marker = this.model.ObjectList[this.CurrentObjIndex] as ObjectMarker;
                    if (marker != null)
                    {
                        marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        marker.DrugMove(pos);
                    }

                    ObjectLine line = this.model.ObjectList[this.CurrentObjIndex] as ObjectLine;
                    if (line != null)
                    {
                        line.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        line.DrugMove(pos);
                    }
                    ObjectCurve curve = this.model.ObjectList[this.OnCursolIndex] as ObjectCurve;
                    if (curve != null)
                    {
                        curve.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                        curve.DrugMove(pos);
                    }
*/
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
                    ObjectCurve curve = obj as ObjectCurve;
                    if (curve != null)
                    {
                        if (curve.CheckDistance(pos))
                        {
                            OnCursolIndex = count;
                            if (curve.ObjStatus != ObjectBase.OBJ_STATUS.SELECT)
                            {
                                curve.ObjStatus = ObjectBase.OBJ_STATUS.ON_CURSOR;
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

            ObjectCurve curve = this.model.ObjectList[this.CurrentObjIndex] as ObjectCurve;
            if (curve != null)
            {
                curve.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            }
            //            this.CurrentObjIndex = -1;
            //            this.OnCursolIndex = -1;
        }


    }

    public abstract class ObjectState
    {
        //描画オブジェクトを切り替えたとき
        public void ClearState()
        {
            foreach (var obj in this.model.ObjectList)
            {
                obj.ObjStatus = ObjectBase.OBJ_STATUS.NON;
            }
        }


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
