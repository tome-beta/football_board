using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
        public void ExportData(SaveFileDialog dialog)
        {
            dialog.Filter = "csvファイル(*.csv)|*.csv";
            dialog.FileName = "BoardData.csv";
            dialog.Title = @"保存先のファイルを選択してください";
            //ダイアログを表示する
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                SaveManage.WriteCsvFile(dialog.FileName, ref this.model);
            }
        }
        //オブジェクトリストをファイルとして保存
        public void ImportData(OpenFileDialog dialog)
        {
            // ダイアログを表示し、戻り値が [OK] の場合は、選択したファイルを表示する
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveManage.ReadCsvFile(dialog.FileName, ref this.model);
            }
        }

        //フィールドの回転によって描画座標を変化させる
        public void TranslatePosition(Point org_point, ref Point def_point)
        {
            //RIGHTであるのを基準にした位置に変換する

            if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.LEFT)
            {
                double rotation = 180 * Math.PI / 180d;
                int center_x = GUIParam.GetInstance().FiledWidth / 2;
                int center_y = GUIParam.GetInstance().FiledHeight / 2;

                int x = org_point.X - center_x;
                int y = org_point.Y - center_y;

                def_point.X = (int)(x * Math.Cos(rotation) - y * Math.Sin(rotation)) + center_x;
                def_point.Y = (int)(x * Math.Sin(rotation) + y * Math.Cos(rotation)) + center_y;
            }
            else if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.VERTICAL)
            {
                def_point.X = GUIParam.GetInstance().FiledWidth - org_point.Y;
                def_point.Y = org_point.X;

            }
            else
            {
                def_point = org_point;
            }

        }
        //================================================
        // private 
        //================================================
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
            this.State.LeftMouseMove(pos);
        }
        public void LeftMouseUp(Point pos)
        {
            this.State.MouseDrag = false;
            this.State.LeftMouseUp(pos);

            UpdateUndoList();
        }

        public void RightMouseDown(Point pos)
        {
            this.State.MouseDrag = true;
            this.State.RightMouseDown(pos);
        }
        public void RightMouseDrag(Point pos)
        {
            this.State.RightMouseMove(pos);
        }
        public void RightMouseUp(Point pos)
        {
            this.State.MouseDrag = false;
            this.State.RightMouseUp(pos);

            UpdateUndoList();
        }

        public void MouseDrag(Point pos)
        {
            this.State.MouseMove(pos);
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

            //無効なオブジェクトを消すチェック
            CheckInvalidObject();
        }

        //オブジェクトの削除
        public void DeleteObject()
        {
            if (this.State.GetCurrentObj() != null)
            {
                this.model.ObjectList.RemoveAt(this.State.CurrentObjIndex);
                this.State.CurrentObjIndex = -1;
                UpdateUndoList();
            }
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


        //テキストボックスの入力を反映
        public void SetString(String str,MainForm.TEXTBOX_TYPE type)
        {
            if (type == MainForm.TEXTBOX_TYPE.STRING)
            {
                OStateString st = this.State as OStateString;
                if (st != null)
                {
                    st.SetString(str);
                }
            }
            else if (type == MainForm.TEXTBOX_TYPE.MARKER_UNIFORM_NUMBER)
            {
                ObjectMarker mark = this.State.GetCurrentObj() as ObjectMarker;
                if (mark != null)
                {
                    mark.SetString(str, ObjectMarker.StringType.UniformNumver);
                }

            }
            else if (type == MainForm.TEXTBOX_TYPE.MARKER_NAME)
            {
                ObjectMarker mark = this.State.GetCurrentObj() as ObjectMarker;
                if (mark != null)
                {
                    mark.SetString(str, ObjectMarker.StringType.Name);
                }
            }
        }

        //ライン描画形式の変更
        public void SetLineStyle(String style)
        {
            ObjectLine line =  this.State.GetCurrentObj() as ObjectLine;
            if(line != null)
            {
                if (style == @"Solid")
                {
                    line.LineStyle = ObjectLine.LINE_STYLE.SOLID;
                }
                if (style == @"Jagged")
                {
                    line.LineStyle = ObjectLine.LINE_STYLE.JAGGED;
                }
                if (style == @"Dotted")
                {
                    line.LineStyle = ObjectLine.LINE_STYLE.DOTTED;
                }
            }
        }

 
        //登録されているオブジェクトの描画
        public void DrawAll(Graphics g)
        {
            foreach( ObjectBase obj in this.model.ObjectList)
            {
                obj.DrawObject(g);
            }
        }

        //無効なオブジェクトをチェックして消す
        private void CheckInvalidObject()
        {
            //文字列が入力されていないString
            for(int i = 0; i < this.model.ObjectList.Count;i++)
            {
                ObjectString obj = model.ObjectList[i] as ObjectString;

                if(obj != null)
                {
                    if(obj.DispString.Length == 0)
                    {
                        this.model.ObjectList.RemoveAt(i);
                        break;
                    }
                }

            }
        }


        //オブジェクト毎の振る舞いを管理する
        public ObjectState State = null;

        //データ管理の本体
        DataModel model;

        CSVDataManage SaveManage = new CSVDataManage();

        private CommandManager _cmdManager = new CommandManager(DataModel.UNDO_MAX);
        private Memento<List<ObjectBase>,DataModel> _memento;
    }
}
