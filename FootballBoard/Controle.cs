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

            CurrentObject = marker;
        }

        //ラインを追加する
        public void AddLine(Point pos)
        {
            ObjectLine line = new ObjectLine(pos);
            this.model.ObjectList.Add(line);

            CurrentObject = line;
        }

        public void DrawAll(Graphics g)
        {
            foreach( ObjectBase obj in this.model.ObjectList)
            {
                obj.DrawObject(g);
            }
        }

        //線を
        public void SetLineStart(Point pos)
        {

        }

        //操作中のオブジェクト
        public ObjectBase CurrentObject;


        DataModel model = new DataModel();

        public bool MouseDrag = false;  //ドラッグ中
    }
}
