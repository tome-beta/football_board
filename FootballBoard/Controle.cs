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
            _memento = new TextBoxMemento(this.model.ObjectList,this.model);
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
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.MARKER:
                    {
                        OStateMarker ms = new OStateMarker();
                        this.State = ms;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.LINE:
                    {
                        OStateLine ls = new OStateLine();
                        this.State = ls;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.CURVE:
                    {
                        OStateCurve cs = new OStateCurve();
                        this.State = cs;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.RECT:
                    {
                        OStateRect rs = new OStateRect();
                        this.State = rs;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.CIRCLE:
                    {
                        OStateCircle cs = new OStateCircle();
                        this.State = cs;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.POLYGON:
                    {
                        OStatePolygon ps = new OStatePolygon();
                        this.State = ps;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.TRIANGLE:
                    {
                        OStateTriangle ts = new OStateTriangle();
                        this.State = ts;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
                case Common.SELECT_DRAW_OBJECT.STRING:
                    {
                        OStateString ss = new OStateString();
                        this.State = ss;
                        //モデルを扱えるようにする
                        this.State.model = this.model;
                        this.State.ClearState();
                    }
                    break;
            }

        }

        //================================================================
        //マウス操作の受け渡し
        //================================================================
        public void LeftMouseDown(Point pos)
        {
            //ここでUNDOLISTを更新させる。多分おんなじデータを登録してしまってる
            var list = new List<ObjectBase>(this.model.ObjectList);
            var current = new TextBoxMemento(list, this.model);
            var cmd = new MementoCommand<List<ObjectBase>, DataModel>(_memento, current);
            if (!_cmdManager.Invoke(cmd))
            {
                //                MessageBox.Show("状態の最大保存数を超えました。");
                return;
            }
            _memento = current;
            this.State.MouseDrag = true;
            this.State.LeftMouseDown(pos);
        }
        public void LeftMouseDrag(Point pos)
        {
            this.State.MouseMove(pos);
        }
        public void LeftMouseUp(Point pos)
        {
            this.State.MouseDrag = false;
            this.State.LeftMouseUp(pos);
        }

        //================================================================
        //GUI操作の受け渡し
        //================================================================
        public void DeleteObject()
        {
            this.model.ObjectList.Remove(this.State.CurrentObj);
            this.State.CurrentObj = null;
        }
        //戻る機能
        public void Undo()
        {
            _cmdManager.Undo();
        }

        //進む機能
        public void Redo()
        {
            _cmdManager.Redo();
        }

        public void SetString(String str)
        {
            this.State.SetString(str);
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
        public ObjectState State = null;

        //データ管理の本体
        DataModel model;

        private CommandManager _cmdManager = new CommandManager(DataModel.UNDO_MAX);
        private Memento<List<ObjectBase>,DataModel> _memento;
    }
}
