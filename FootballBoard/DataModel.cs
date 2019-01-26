using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballBoard
{
    public class DataModel
    {
        public const int UNDO_MAX = 10;

        //コンストラクタ
        public DataModel()
        {
            ObjectList = new List<ObjectBase>();
        }

        public List<ObjectBase> ObjectList;

        //UNDO用の記録リスト
        public Memento<List<ObjectBase>,DataModel> ObjectMement;
    }
}
