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
            //クリックしたところにオブジェクトがあるのか
            if( this.OnCursolIndex >= 0)
            {
                CurrentObj = this.model.ObjectList[this.OnCursolIndex];
                if (CurrentObj != null)
                {
                    //当たり判定チェック
                    if (CurrentObj.CheckDistance(pos))
                    {
                        CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                    }
                }
            }
            else
            {
                this.CurrentObj = null;
            }

            //オブジェクトを選択できていたらその他のオブジェクトの状態をリセット
            for (int i = 0; i < this.model.ObjectList.Count; i++)
            {
                if (this.model.ObjectList[i].ObjStatus != ObjectBase.OBJ_STATUS.DRUG)
                {
                    this.model.ObjectList[i].ObjStatus = ObjectBase.OBJ_STATUS.NON;
                }
            }
        }

        //マウスを動かす
        public override void LeftMouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                //マウスドラッグ中
                if(CurrentObj != null)
                {
                    CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                    CurrentObj.DrugMove(pos);
                }
            }
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            if(this.CurrentObj != null)
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            }

        }
        //右クリック
        public override void RightMouseDown(Point pos)
        {
            //クリックしたところにオブジェクトがあるのか
            if (this.OnCursolIndex >= 0)
            {
                CurrentObj = this.model.ObjectList[this.OnCursolIndex];
                if ((CurrentObj as ObjectMarker) != null)
                {
                    //当たり判定チェック
                    if (CurrentObj.CheckDistance(pos))
                    {
                        CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.RIGHT_SET;
                    }
                }
            }
            else
            {
                this.CurrentObj = null;
            }

            //オブジェクトを選択できていたらその他のオブジェクトの状態をリセット
            for (int i = 0; i < this.model.ObjectList.Count; i++)
            {
                if (this.model.ObjectList[i].ObjStatus != ObjectBase.OBJ_STATUS.RIGHT_SET)
                {
                    this.model.ObjectList[i].ObjStatus = ObjectBase.OBJ_STATUS.NON;
                }
            }

        }
        public override void RightMouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                //マウスドラッグ中
                if ((CurrentObj as ObjectMarker) != null)
                {
                    //回転
                }
            }

        }
        public override void RightMouseUp(Point pos)
        {
            if (this.CurrentObj != null)
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            }
        }
        public override void MouseMove(Point pos)
        {
            {
                //選択状態を一度初期化
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    if (obj.ObjStatus != ObjectBase.OBJ_STATUS.SELECT)
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
                    if (obj.CheckDistance(pos))
                    {
                        OnCursolIndex = count;
                        if (obj.ObjStatus != ObjectBase.OBJ_STATUS.SELECT)
                        {
                            obj.ObjStatus = ObjectBase.OBJ_STATUS.ON_CURSOR;
                        }
                        break;
                    }
                    count++;
                }
            }
        }
        //文字列を設定する
        public override void SetString(String str)
        {
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
        //左マウスを動かす（ドラッグも込み）
        public abstract void LeftMouseMove(Point pos);
        //左を離したとき
        public abstract void LeftMouseUp(Point pos);

        //右クリックしたとき
        public abstract void RightMouseDown(Point pos);
        //右マウスを動かす（ドラッグも込み）
        public abstract void RightMouseMove(Point pos);
        //右を離したとき
        public abstract void RightMouseUp(Point pos);

        //何も押さずに動かしているとき
        public abstract void MouseMove(Point pos);

        //文字列を設定する
        public abstract void SetString(String str);


        public DataModel model;         //データを扱うため
        public bool MouseDrag = false;  //ドラッグしているか

        //操作中のオブジェクトのインデックス
        public int OnCursolIndex = -1;  //カーソルが上にある
        public ObjectBase CurrentObj;   //選択中のオブジェクト
    }



}
