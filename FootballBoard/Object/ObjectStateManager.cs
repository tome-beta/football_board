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
            //オブジェクトを選択状態にしているか
            CurrentObjIndex = OnCursolIndex;

            if( this.OnCursolIndex >= 0)
            {
                ObjectBase obj = this.model.ObjectList[this.OnCursolIndex];

                if (obj != null)
                {
                    //当たり判定チェック
                    if (obj.CheckDistance(pos))
                    {
                        obj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                    }
                }
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
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            if (CurrentObjIndex >= 0)
            {
                //共通にできる
                ObjectBase obj = this.model.ObjectList[this.CurrentObjIndex];
                if (obj != null)
                {
                    obj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
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
        //マウスを動かす（ドラッグも込み）
        public abstract void MouseMove(Point pos);
        //左を離したとき
        public abstract void LeftMouseUp(Point pos);

        //文字列を設定する
        public abstract void SetString(String str);


        public DataModel model;         //データを扱うため
        public bool MouseDrag = false;  //ドラッグしているか

        //操作中のオブジェクトのインデックス
        public int CurrentObjIndex = -1; //操作中のオブジェ
        public int OnCursolIndex = -1;  //カーソルが上にある

    }



}
