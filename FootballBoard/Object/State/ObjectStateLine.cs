using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //ラインの振る舞いを示すクラス
    public class OStateLine : ObjectState
    {
        //左クリックしたとき
        public override void LeftMouseDown(Point pos)
        {
            //クリックしたところにすでにラインがあるか
            if (CurrentObjIndex >= 0 && GetCurrentObj().CheckDistance(pos))
            {
                GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;
            }
            else
            {
                ObjectLine line = new ObjectLine(pos);
                this.model.ObjectList.Add(line);
                CurrentObjIndex = this.model.ObjectList.Count - 1;
                
                line.DrugType = ObjectLine.DRUG_TYPE.INIT;

                MakeLine(line);
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


        
        //============================================================
        //  private 
        //============================================================
        private void MakeLine(ObjectLine line)
        {
            if(GUIParam.GetInstance().LinStyle.Text == @"Solid") { line.LineStyle = ObjectLine.LINE_STYLE.SOLID;}
            if(GUIParam.GetInstance().LinStyle.Text == @"Jagged"){ line.LineStyle = ObjectLine.LINE_STYLE.JAGGED;}
            if(GUIParam.GetInstance().LinStyle.Text == @"Dotted") { line.LineStyle = ObjectLine.LINE_STYLE.DOTTED; }

            //矢印タイプ
            switch(GUIParam.GetInstance().ArrowStyle.Text)
            {
                case @"Non":
                    line.ArrowStyle = ObjectLine.ARROW_STYLE.NONE;
                    break;
                case @"Start":
                    line.ArrowStyle = ObjectLine.ARROW_STYLE.START;
                    break;
                case @"End":
                    line.ArrowStyle = ObjectLine.ARROW_STYLE.END;
                    break;
                case @"both":
                    line.ArrowStyle = ObjectLine.ARROW_STYLE.BOTH;
                    break;

            }
        }
    }

}
