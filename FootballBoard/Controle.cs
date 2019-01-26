using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FootballBoard
{
    public class Controle
    {
        //コンストラクタ
        public Controle()
        {
            this.model = new DataModel();

            var list = new List<ObjectBase>(this.model.ObjectList);
            _memento = new TextBoxMemento(list,this.model);
        }

        //オブジェクトリストをファイルとして保存
        public void ExportData()
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            sfd.FileName = "BoardData.csv";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            sfd.Filter = "csvファイル(*.csv)|*.csv";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            //            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = @"保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(sfd.FileName);
            }
        }

        //UNDOLISTを更新
        private void UpdateUndoList()
        {
            //オブジェクトリストの複製を作る
            List<ObjectBase> list = new List<ObjectBase>();
            for (int i = 0; i < this.model.ObjectList.Count(); i++)
            {
                ObjectBase b = DeepCopyHelper.DeepCopy<ObjectBase>(this.model.ObjectList[i]);
                list.Add(b);
            }

            var current = new TextBoxMemento(list, this.model);
            var cmd = new MementoCommand<List<ObjectBase>, DataModel>(_memento, current);
            if (!_cmdManager.Invoke(cmd))
            {
                //                MessageBox.Show("状態の最大保存数を超えました。");
                return;
            }
            _memento = current;
        }

        //================================================================
        //マウス操作の受け渡し
        //================================================================

        public void LeftMouseDown(Point pos)
        {
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

            UpdateUndoList();
        }




        //================================================================
        //GUI操作の受け渡し
        //================================================================
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

        public void DeleteObject()
        {
            this.model.ObjectList.Remove(this.State.CurrentObj);
            this.State.CurrentObj = null;

            UpdateUndoList();
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
