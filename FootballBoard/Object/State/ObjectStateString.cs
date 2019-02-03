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
            if (this.CurrentObj != null && CurrentObj.CheckDistance(pos))
            {
                CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                ObjectString str = new ObjectString(pos);
                this.model.ObjectList.Add(str);
                this.CurrentObj = str;
                str.DrugType = ObjectString.DRUG_TYPE.INIT;
                this.CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
            }
        }
        //左ドラッグ
        public override void LeftMouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                this.CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                //何を掴んでいるかで場合分けしている
                this.CurrentObj.DrugMove(pos);
            }
        }
        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            this.CurrentObj.ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
        }
        //右クリック
        public override void RightMouseDown(Point pos) { }
        public override void RightMouseMove(Point pos) { }
        public override void RightMouseUp(Point pos) { }
        public override void MouseMove(Point pos)
        {

        }
        //文字列を設定する
        public override void SetString(String str)
        {
            this.CurrentObj.DispString = str;
        }
    }

}
