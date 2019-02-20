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
            if (CurrentObjIndex >= 0 && GetCurrentObj().CheckDistance(pos))
            {
                GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
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
                CurrentObjIndex = this.model.ObjectList.Count - 1;

                marker.ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                this.MouseDrag = true;

                //GUIの反映
                MakeMarker(marker);

            }
        }
        //左ドラッグ
        public override void LeftMouseMove(Point pos)
        {
            if (this.MouseDrag)
            {
                GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.DRUG;
                GetCurrentObj().Points[0] = pos;
            }
        }

        //左を離したとき
        public override void LeftMouseUp(Point pos)
        {
            GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
        }

        //右クリック
        public override void RightMouseDown(Point pos)
        {
            //DRUG状態のマーカーが近くにあるときは動かせる様にする
            if (GetCurrentObj() != null && GetCurrentObj().CheckDistance(pos))
            {
                GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.RIGHT_SET;
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
            GetCurrentObj().ObjStatus = ObjectBase.OBJ_STATUS.SELECT;
            this.MouseDrag = false;
        }

        public override void MouseMove(Point pos)
        {

        }

        //オブジェクトの持つ文字列をテキストボックスに設定する
        public void TransitionString(String str,ObjectMarker.StringType type)
        {
            if (type == ObjectMarker.StringType.UniformNumver)
            {
                GUIParam.GetInstance().UniformNumberTextBox.Text = str;
            }
            else if (type == ObjectMarker.StringType.Name)
            {
                GUIParam.GetInstance().NameTextBox.Text = str;
            }
        }

        //============================================================
        //  private 
        //============================================================
        private void MakeMarker(ObjectMarker marker)
        {
            //文字列があれば
            marker.Name = GUIParam.GetInstance().NameTextBox.Text;
            marker.UniformNumber = GUIParam.GetInstance().UniformNumberTextBox.Text;

            //名前ポジション
            for (int i = 0; i < 9; i++)
            {
                if (GUIParam.GetInstance().NamePosButton[i].Checked)
                {
                    marker.NamePosition = i;
                    break;
                }
            }

            //チーム設定
            if (GUIParam.GetInstance().TeamConboBox.Text == @"HOME")
            {
                marker.TeamType = 0;
            }
            else
            {
                marker.TeamType = 1;
            }
        }
    }

}
