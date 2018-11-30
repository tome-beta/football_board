using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    public class Controle
    {
        //マーカーを追加する
        public void AddMarker(Point pos)
        {
            ObjectMarker marker = new ObjectMarker(pos);
            this.model.ObjectList.Add(marker);

//            CurrentObject = marker;
        }

        //ラインを追加する
        public void MakeLine(Point pos)
        {
            ObjectLine line = new ObjectLine(pos);
            this.model.ObjectList.Add(line);

            CurrentObjIndex = this.model.ObjectList.Count - 1;
        }

        public void SetLineEndPoint(Point pos)
        {
            ObjectLine line = (ObjectLine)this.model.ObjectList[this.CurrentObjIndex];
            line.SetEndPoint(pos);

        }

        public void DrawAll(Graphics g)
        {
            foreach( ObjectBase obj in this.model.ObjectList)
            {
                obj.DrawObject(g);
            }
        }


        //操作中のライン
        public int CurrentObjIndex = 0;


        DataModel model = new DataModel();

        public bool MouseDrag = false;  //ドラッグ中
    }
}
