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
        //コンストラクタ
        public Controle()
        {
            this.model = new DataModel();
        }

        /// <summary>
        ///オブジェクトリストを選択したとき 
        /// </summary>
        /// <param name="select"></param>
        public void ChangeSelectObject(Common.SELECT_DRAW_OBJECT select)
        {
            //ここで条件分岐させてオブジェクトを設定
            switch (select)
            {
                case Common.SELECT_DRAW_OBJECT.MOVE:
                    {
                        MoveState ms = new MoveState();
                        this.State = ms;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.MARKER:
                    {
                        MarkerState ms = new MarkerState();
                        this.State = ms;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.LINE:
                    {
                        LineState ls = new LineState();
                        this.State = ls;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                    }
                    break;
            }

        }

        //マウス操作の受け渡し
        public void LeftMouseDown(Point pos)
        {
            this.State.MouseDrag = true;
            this.State.LeftMouseDown(pos);
        }
        public void LeftMouseDrag(Point pos)
        {
            this.State.LeftMouseDrag(pos);
        }
        public void LeftMouseUp(Point pos)
        {
            this.State.MouseDrag = false;
            this.State.LeftMouseUp(pos);
        }

        //登録されているオブジェクトの描画
        public void DrawAll(Graphics g)
        {
            foreach( ObjectBase obj in this.model.ObjectList)
            {
                obj.DrawObject(g);
            }
        }


        //オブジェクト毎の振る舞いを管理する
        private ObjectState State = null;

        //データ管理の本体
        DataModel model;

    }
}
