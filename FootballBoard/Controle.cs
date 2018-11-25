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

        }

        DataModel model = new DataModel();
    }
}
