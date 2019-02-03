using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //マーカーの振る舞いを示すクラス
    public class OStateMarker : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            //DRUG状態のマーカーが近くにあるときは動かせる様にする
            if (CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                //全てのオブジェクトを初期状態にする
                foreach (ObjectBase obj in this.model.ObjectList)
                {
                    obj.ObjStatus = ObjectBase.OBJ_STATUS.NON;
                }

                //マーカーを追加する
                ObjectMarker marker = new ObjectMarker(pos);
                this.model.ObjectList.Add(marker);

                CurrentObj = marker;
                marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
        }
        //左ドラッグ
        public override void LeftMouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                CurrentObj.Points[0] = pos;
            }
        }

        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            this.MouseDrag = false;
        }

        //右クリック
        public override void RightMouseDown(Point pos)
        {
            //DRUG状態のマーカーが近くにあるときは動かせる様にする
            if (CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.RIGHT_SET;
                this.MouseDrag = true;
            }
        }
        public override void RightMouseMove(Point pos)
        {
            if (this.MouseDrag && GUIParam.GetInstance().MarkerDirectionOn)
            {
                //角度を変える
            }
        }

        public override void RightMouseUp(Point pos)
        {
            CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            this.MouseDrag = false;
        }

        public override void MouseMove(Point pos)
        {

        }

        //文字列を設定する
        public override void SetString(String str)
        {
        }
    }

}
