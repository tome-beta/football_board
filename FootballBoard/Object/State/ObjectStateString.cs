using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //文字列の振る舞いを示すクラス
    class OStateString : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            //クリックしたところにすでに矩形があるか
            if (CurrentObjIndex >= 0 && GetCurrentObj().CheckDistance(pos))
            {
                GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else 
            {
                ObjectString obj = new ObjectString(pos);
                this.model.ObjectList.Add(obj);
                CurrentObjIndex = this.model.ObjectList.Count - 1;

                SetString(GUIParam.GetInstance().WriteStringtextBox.Text);

            }
        }
        //左ドラッグ
        public override void LeftMouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                //何を掴んでいるかで場合分けしている
                GetCurrentObj().DrugMove(pos);
            }

        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
        }

        //右クリック
        public override void RightMouseDown(Point pos) { }
        public override void RightMouseMove(Point pos) { }
        public override void RightMouseUp(Point pos) { }
        public override void MouseMove(Point pos)
        {

        }


        //オブジェクトの持つ文字列をテキストボックスに設定する
        public void TransitionString(String str)
        {
            GUIParam.GetInstance().WriteStringtextBox.Text = str;
        }

        //外部から文字列を設定する
        public void SetString(String str)
        {
            if (CurrentObjIndex >= 0)
            {
                GetCurrentObj().DispString = str;
            }
        }
    }

}
