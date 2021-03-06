﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //三角形の振る舞いを示すクラス
    public class OStateTriangle : ObjectState
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
                ObjectTriangle tri = new ObjectTriangle(pos);
                this.model.ObjectList.Add(tri);
                CurrentObjIndex = this.model.ObjectList.Count - 1;
                tri.DrugType = ObjectTriangle.DRUG_TYPE.INIT;
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

    }
}
