using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballBoard
{
    public class DataModel
    {
        //コンストラクタ
        public DataModel()
        {
            ObjectList = new List<ObjectBase>();
        }

        public List<ObjectBase> ObjectList;

    }
}
