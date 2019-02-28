using System;
using System.Drawing;

namespace FootballBoard
{
    //描画オブジェクトのベース
    [Serializable()]
    public abstract class ObjectBase
    {
        public enum OBJ_STATUS
        {
            NON,        //無し
            ON_CURSOR,  //上に載せた
            SELECT,     //一度選択した
            DRUG,       //掴んでいる

            RIGHT_SET,  //右クリックで掴んだ
        };

        public const int OBJ_POINTS_NUM = 4;

        public ObjectBase()
        {
            for(int i = 0; i < OBJ_POINTS_NUM; i++)
            {
                Points[i].X = 0;
                Points[i].Y = 0;
            }
        }

        //フィールドの回転によって描画座標を変化させる
        protected void TranslatePosition(Point[] org_point_list, ref Point[] def_point_list)
        {
            if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.LEFT)
            {
                int center_x = GUIParam.GetInstance().FiledWidth / 2;
                int center_y = GUIParam.GetInstance().FiledHeight / 2;
                //オブジェクトの位置を回転させる
                for (int i = 0; i < OBJ_POINTS_NUM; i++)
                {
                    int x = org_point_list[i].X - center_x;
                    int y = org_point_list[i].Y - center_y;

                    def_point_list[i].X = (int)(-x) + center_x;
                    def_point_list[i].Y = (int)(-y) + center_y;
                }

            }
            else if (GUIParam.GetInstance().FiledDirection == GUIParam.FILED_DIRECTION.VERTICAL)
            {
                for (int i = 0; i < OBJ_POINTS_NUM; i++)
                {
                    def_point_list[i].X = org_point_list[i].Y;
                    def_point_list[i].Y = GUIParam.GetInstance().FiledWidth - org_point_list[i].X;
                }
            }
            else
            {
                for (int i = 0; i < OBJ_POINTS_NUM; i++)
                {
                    def_point_list[i].X = org_point_list[i].X;
                    def_point_list[i].Y = org_point_list[i].Y;
                }

            }
        }

        //オブジェクトがはみ出さなさいようにする処理
        protected void CheckPointMoveRange( ref Point[] Points)
        {
            int max_x = GUIParam.GetInstance().FiledWidth - 5;
            int max_y = GUIParam.GetInstance().FiledHeight - 5;

            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].X < 0) { Points[i].X = 0; }
                if (Points[i].Y < 0) { Points[i].Y = 0; }
                if (max_x < Points[i].X) { Points[i].X = max_x; }
                if (max_y < Points[i].Y) { Points[i].Y = max_y; }
            }
        }

        //全体を動かすとき
        protected bool CheckWholePointMoveRange(Point[] Points,int move, bool type_x)
        {
            bool ret = true;

            if (type_x)
            {
                int point_max_x = Common.Max(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
                int point_min_x = Common.Min(Points[0].X, Points[1].X, Points[2].X, Points[3].X);
                int max_x = GUIParam.GetInstance().FiledWidth;

                if(move > 0)
                {
                    int tmp_x = point_max_x + move;
                    if (tmp_x > max_x)
                    {
                        ret = false;
                    }
                }
                else 
                {
                    int tmp_x = point_min_x + move;
                    if (tmp_x < 0 )
                    {
                        ret = false;
                    }
                }
            }
            else
            {
                int point_max_y = Common.Max(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
                int point_min_y = Common.Min(Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
                int max_y = GUIParam.GetInstance().FiledHeight;
                if(move > 0)
                {
                    int tmp_y = point_max_y + move;
                    if (tmp_y > max_y)
                    {
                        ret = false;
                    }
                }
                else 
                {
                    int tmp_y = point_min_y + move;
                    if (tmp_y < 0 )
                    {
                        ret = false;
                    }
                }

            }
            

            return ret;
        }


        //ドラッグするときの動き
        public abstract void DrugMove(Point pos);

        //距離のチェック
        public abstract bool CheckDistance(Point pos);

        //描画
        public abstract void DrawObject(Graphics g);

        public Point[] Points = new Point[OBJ_POINTS_NUM];        //矩形や曲線も扱うため
        public OBJ_STATUS ObjStatus = OBJ_STATUS.NON;
        public const int VERTEX_SIZE = 10;      //オブジェクトの頂点の大きさ

        public String DispString = @"";
    }
}
