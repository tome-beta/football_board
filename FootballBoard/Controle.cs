using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
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
                WriteCsvFile(dialog.FileName);
            }
        }
        //オブジェクトリストをファイルとして保存
        public void ImportData(OpenFileDialog dialog)
        {
            // ダイアログを表示し、戻り値が [OK] の場合は、選択したファイルを表示する
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ReadCsvFile(dialog.FileName);
            }
        }

        //================================================
        // private 
        //================================================
        private void ReadCsvFile(String file_name)
        {
            this.model.ObjectList.Clear();

            using (StreamReader sr = new System.IO.StreamReader(file_name, System.Text.Encoding.GetEncoding("UTF-8")))
            {
                String line = sr.ReadLine();
                //１行目のチェック
                if (line != @"FootBallBoardData")
                {
                    //警告ウインドウ
                    //メッセージボックスを表示する
                    MessageBox.Show("データ形式が違います",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                while (sr.Peek() >-1)
                {
                    line = sr.ReadLine();

                    if (line == @"FootBallBoardData")
                    {
                        continue;
                    }

                    string[] str_array = line.Split(',');
                    //Pointsを取得
                    Point[] tmp_points = new Point[4];
                    for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                    {
                        tmp_points[i].X = int.Parse(str_array[1 + i * 2]);
                        tmp_points[i].Y = int.Parse(str_array[1 + i * 2 + 1]);
                    }

                    //名前で作るオブジェクトを変える
                    if ( str_array[0] == "ObjectMarker")
                    {
                        ObjectMarker obj = new ObjectMarker(tmp_points[0]);
                        for(int i = 0;i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectLine")
                    {
                        ObjectLine obj = new ObjectLine(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectCurve")
                    {
                        ObjectCurve obj = new ObjectCurve(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectCircle")
                    {
                        ObjectCircle obj = new ObjectCircle(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectRect")
                    {
                        ObjectRect obj = new ObjectRect(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectPolygon")
                    {
                        ObjectPolygon obj = new ObjectPolygon(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectTriangle")
                    {
                        ObjectTriangle obj = new ObjectTriangle(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectString")
                    {
                        ObjectString obj = new ObjectString(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        this.model.ObjectList.Add(obj);
                    }
                }


            }
        }
        private void WriteCsvFile(String file_name)
        {
            Encoding sjisEnc = Encoding.GetEncoding("UTF-8");
             using (StreamWriter sw = new StreamWriter(file_name, false, sjisEnc))
            {
                foreach(var obj in this.model.ObjectList)
                {
                    sw.WriteLine(@"FootBallBoardData");

                    String line = @"";
                    //オブジェクトの種類
                    string[] str_array = obj.ToString().Split('.');
                    line += str_array[1] + ",";

                    //座標データ
                    for(int i = 0; i < ObjectBase.OBJ_POINTS_NUM;i++)
                    {
                        line += obj.Points[i].X.ToString() + ",";
                        line += obj.Points[i].Y.ToString() + ",";
                    }

                    sw.WriteLine(line);
                }
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
        }

        //オブジェクトの削除
        public void DeleteObject()
        {
            if (this.State.CurrentObj != null)
            {
                this.model.ObjectList.RemoveAt(this.State.CurrentObjIndex);
                this.State.CurrentObj = null;
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

        public void SetString(String str)
        {
            this.State.SetString(str);
        }

        //フィールドの回転
        public void FieldRotate(GUIParam.FILED_DIRECTION direction)
        {
            //回転のパターンを調べる
            GUIParam.FILED_DIRECTION org_dir = GUIParam.GetInstance().FiledDirection;

            if (org_dir == direction)
            {
                //何もしない
                return;
            }

            double rotation = 0;
            int center_x = 0;
            int center_y = 0;
            if (org_dir == GUIParam.FILED_DIRECTION.RIGHT)
            {
                if (direction == GUIParam.FILED_DIRECTION.VERTICAL)
                {
                    //オブジェクトの位置を回転させる
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                        ObjectBase obj = this.model.ObjectList[i];

                        for (int j = 0; j < ObjectBase.OBJ_POINTS_NUM; j++)
                        {
                            int x = obj.Points[j].X;
                            int y = obj.Points[j].Y;

                            obj.Points[j].X = y;
                            obj.Points[j].Y = 640 - x;
                        }
                    }
                }
                else if (direction == GUIParam.FILED_DIRECTION.LEFT)
                {
                    rotation = 180 * Math.PI / 180d; ;
                    center_x = 640 / 2;
                    center_y = 480 / 2;
                    //オブジェクトの位置を回転させる
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                        ObjectBase obj = this.model.ObjectList[i];

                        for (int j = 0; j < ObjectBase.OBJ_POINTS_NUM; j++)
                        {
                            int x = obj.Points[j].X - center_x;
                            int y = obj.Points[j].Y - center_y;

                            obj.Points[j].X = (int)(x * Math.Cos(rotation) - y * Math.Sin(rotation)) + center_x;
                            obj.Points[j].Y = (int)(x * Math.Sin(rotation) + y * Math.Cos(rotation)) + center_y;
                        }
                    }
                }
            }
            else if(org_dir == GUIParam.FILED_DIRECTION.LEFT)
            {
                if (direction == GUIParam.FILED_DIRECTION.VERTICAL)
                {
                    //オブジェクトの位置を回転させる
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                        ObjectBase obj = this.model.ObjectList[i];

                        for (int j = 0; j < ObjectBase.OBJ_POINTS_NUM; j++)
                        {
                            int x = obj.Points[j].X;
                            int y = obj.Points[j].Y;

                            obj.Points[j].X = 480 - y;
                            obj.Points[j].Y = x;
                        }
                    }
                }
                else if (direction == GUIParam.FILED_DIRECTION.RIGHT)
                {
                    rotation = 180 * Math.PI / 180d;
                    center_x = 640 / 2;
                    center_y = 480 / 2;
                    //オブジェクトの位置を回転させる
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                        ObjectBase obj = this.model.ObjectList[i];

                        for (int j = 0; j < ObjectBase.OBJ_POINTS_NUM; j++)
                        {
                            int x = obj.Points[j].X - center_x;
                            int y = obj.Points[j].Y - center_y;

                            obj.Points[j].X = (int)(x * Math.Cos(rotation) - y * Math.Sin(rotation)) + center_x;
                            obj.Points[j].Y = (int)(x * Math.Sin(rotation) + y * Math.Cos(rotation)) + center_y;
                        }
                    }
                }
            }
            else if (org_dir == GUIParam.FILED_DIRECTION.VERTICAL)
            {
                if (direction == GUIParam.FILED_DIRECTION.RIGHT)
                {
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                        ObjectBase obj = this.model.ObjectList[i];

                        for (int j = 0; j < ObjectBase.OBJ_POINTS_NUM; j++)
                        {
                            int x = obj.Points[j].X;
                            int y = obj.Points[j].Y;

                            obj.Points[j].X = 640 - y;
                            obj.Points[j].Y = x;
                        }
                    }
                }
                else if (direction == GUIParam.FILED_DIRECTION.LEFT)
                {
                    for (int i = 0; i < this.model.ObjectList.Count; i++)
                    {
                        ObjectBase obj = this.model.ObjectList[i];

                        for (int j = 0; j < ObjectBase.OBJ_POINTS_NUM; j++)
                        {
                            int x = obj.Points[j].X;
                            int y = obj.Points[j].Y;

                            obj.Points[j].X = y;
                            obj.Points[j].Y = 480 - x;
                        }
                    }
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


        //オブジェクト毎の振る舞いを管理する
        public ObjectState State = null;

        //データ管理の本体
        DataModel model;

        private CommandManager _cmdManager = new CommandManager(DataModel.UNDO_MAX);
        private Memento<List<ObjectBase>,DataModel> _memento;
    }
}
