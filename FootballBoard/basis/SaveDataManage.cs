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
    //データのセーブ・ロード
    class CSVDataManage
    {
        //csvファイルの読み込み
        public void ReadCsvFile(String file_name, ref DataModel model)
        {
            model.ObjectList.Clear();
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

                while (sr.Peek() > -1)
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
                    if (str_array[0] == "ObjectMarker")
                    {
                        ObjectMarker obj = new ObjectMarker(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectLine")
                    {
                        ObjectLine obj = new ObjectLine(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectCurve")
                    {
                        ObjectCurve obj = new ObjectCurve(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectCircle")
                    {
                        ObjectCircle obj = new ObjectCircle(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectRect")
                    {
                        ObjectRect obj = new ObjectRect(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectPolygon")
                    {
                        ObjectPolygon obj = new ObjectPolygon(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectTriangle")
                    {
                        ObjectTriangle obj = new ObjectTriangle(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                    if (str_array[0] == "ObjectString")
                    {
                        ObjectString obj = new ObjectString(tmp_points[0]);
                        for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                        {
                            obj.Points[i] = tmp_points[i];
                        }
                        model.ObjectList.Add(obj);
                    }
                }
            }

         }

        //csvファイルの書き出し
        public void WriteCsvFile(String file_name, ref DataModel model)
        {
            Encoding sjisEnc = Encoding.GetEncoding("UTF-8");
            using (StreamWriter sw = new StreamWriter(file_name, false, sjisEnc))
            {
                foreach (var obj in model.ObjectList)
                {
                    sw.WriteLine(@"FootBallBoardData");

                    String line = @"";
                    //オブジェクトの種類
                    string[] str_array = obj.ToString().Split('.');
                    line += str_array[1] + ",";

                    //座標データ
                    for (int i = 0; i < ObjectBase.OBJ_POINTS_NUM; i++)
                    {
                        line += obj.Points[i].X.ToString() + ",";
                        line += obj.Points[i].Y.ToString() + ",";
                    }

                    sw.WriteLine(line);
                }
            }

        }
    }
}
