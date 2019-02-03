using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
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
