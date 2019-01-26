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
        };

        public ObjectBase()
        {
            for(int i = 0; i < 4; i++)
            {
                Points[i].X = 0;
                Points[i].Y = 0;
            }
        }

        //ドラッグするときの動き
        public abstract void DrugMove(Point pos);

        //距離のチェック
        public abstract bool CheckDistance(Point pos);

        //描画
        public abstract void DrawObject(Graphics g);

        public Point[] Points = new Point[4];        //矩形や曲線も扱うため
        public OBJ_STATUS ObjStatus = OBJ_STATUS.NON;
        public const int VERTEX_SIZE = 10;      //オブジェクトの頂点の大きさ

        public String DispString = @"";
    }
}
