using System;
using System.Drawing;

namespace FootballBoard
{

    //マーカー
    [Serializable()]
    public class ObjectMarker : ObjectBase
    {
        public enum StringType
        {
            UniformNumver,
            Name
        };


        public ObjectMarker(Point pos)
        {
            this.Points[0] = pos;
        }

        //ドラッグしているときの動き
        public override void DrugMove(Point pos)
        {
            this.Points[0] = pos;
        }
        //マーカーを描画
        public override void DrawObject(Graphics g)
        {
            int alpha = 255;
            if (this.ObjStatus == OBJ_STATUS.NON)
            {
                alpha = 255;
            }
            else if (this.ObjStatus == OBJ_STATUS.ON_CURSOR)
            {
                alpha = 128;
            }
            Brush brush = new SolidBrush(Color.FromArgb(alpha, GUIParam.GetInstance().ObjectColor));

            //描画位置を作る
            Point[] DrawPoints = new Point[ObjectBase.OBJ_POINTS_NUM];
            TranslatePosition(this.Points, ref DrawPoints);


            //方向を示す
            if(GUIParam.GetInstance().MarkerDirectionOn)
            {
                Brush b = new SolidBrush(Color.FromArgb(alpha, Color.Blue));

                Pen pen = new Pen(b,5);
                DrawMarkerDirection(g,pen,DrawPoints);
            }

            //円を描く
            g.FillEllipse(brush, new Rectangle(
            DrawPoints[0].X - Width / 2,
            DrawPoints[0].Y - Height / 2,
            Width,
            Height)
            );

            //背番号を表示
            if (this.UniformNumber.Length != 0)
            {
                Font fnt = new Font("MS UI Gothic", 20);
                StringFormat sf = new StringFormat();
                SizeF stringSize = g.MeasureString(UniformNumber, fnt, 1000, sf);
                g.DrawString(this.UniformNumber,
                    fnt,
                    Brushes.White,
                    DrawPoints[0].X - stringSize.Width / 2,
                    DrawPoints[0].Y - stringSize.Height / 2);
            }

            //名前を表示 TODO 表示位置のオフセットがあれば
            if (this.Name.Length != 0)
            {
                DrawName(g, DrawPoints[0]);
            }


            //選択したときの三角をつくる
            if (this.ObjStatus == OBJ_STATUS.SELECT ||
                this.ObjStatus == OBJ_STATUS.DRUG)
            {
                DrawSelectTriangle(g,DrawPoints);
            }

        }

        //オブジェクトとの距離をチェックする
        public override bool CheckDistance(Point pos)
        {
            double dist = Common.GetDistance(pos, this.Points[0]);

            if (dist < Width / 2)
            {
                return true;
            }
            return false;
        }

        //方向を決める
        public void RotateDirection(Point pos)
        {
            int MakerCenter_x = this.Points[0].X;
            int MakerCenter_y = this.Points[0].Y;

            double radian = Math.Atan2(pos.Y - MakerCenter_y, pos.X - MakerCenter_x);
            direction = radian * 180d / Math.PI;
        }


        //外部から文字列を設定する
        public void SetString(String str, ObjectMarker.StringType type)
        {
            switch (type)
            {
                case ObjectMarker.StringType.Name:
                    {
                        Name = str;
                    }
                    break;
                case ObjectMarker.StringType.UniformNumver:
                    {
                        UniformNumber = str;
                    }
                    break;
                default:
                    break;
            }
        }

        //============================================================
        //  private 
        //============================================================


        //選択しているときの三角形を描画
        private void DrawSelectTriangle(Graphics g,Point[] points)
        {
            Brush brush_tri = new SolidBrush(Color.Black);
            //左
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = points[0].X - Width / 2;
                f_points[0].Y = points[0].Y;

                f_points[1].X = f_points[0].X - (Width / 3);
                f_points[1].Y = f_points[0].Y - (Height / 4);
                f_points[2].X = f_points[0].X - (Width / 3);
                f_points[2].Y = f_points[0].Y + (Height / 4);

                g.FillPolygon(brush_tri, f_points);
            }
            //上
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = points[0].X;
                f_points[0].Y = points[0].Y - Height / 2;

                f_points[1].X = f_points[0].X - (Width / 4);
                f_points[1].Y = f_points[0].Y - (Height / 3);
                f_points[2].X = f_points[0].X + (Width / 4);
                f_points[2].Y = f_points[0].Y - (Height / 3);

                g.FillPolygon(brush_tri, f_points);
            }

            //右
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = points[0].X + Width / 2;
                f_points[0].Y = points[0].Y;

                f_points[1].X = f_points[0].X + (Width / 3);
                f_points[1].Y = f_points[0].Y - (Height / 4);
                f_points[2].X = f_points[0].X + (Width / 3);
                f_points[2].Y = f_points[0].Y + (Height / 4);

                g.FillPolygon(brush_tri, f_points);
            }
            //下
            {
                PointF[] f_points = new PointF[3];
                f_points[0].X = points[0].X;
                f_points[0].Y = points[0].Y + Height / 2;

                f_points[1].X = f_points[0].X - (Width / 4);
                f_points[1].Y = f_points[0].Y + (Height / 3);
                f_points[2].X = f_points[0].X + (Width / 4);
                f_points[2].Y = f_points[0].Y + (Height / 3);

                g.FillPolygon(brush_tri, f_points);
            }
        }

        //マーカーの方向を描画
        private void DrawMarkerDirection(Graphics g,Pen pen,Point[] points)
        {
            int MakerCenter_x = points[0].X;
            int MakerCenter_y = points[0].Y;

            int offset_x_90  = (int)(20 * Math.Cos((direction + 90) * (Math.PI / 180)) );
            int offset_y_90  = (int)(20 * Math.Sin((direction + 90) * (Math.PI / 180)) );
            int offset_x_180 = (int)(20 * Math.Cos((direction + 180) * (Math.PI / 180)));
            int offset_y_180 = (int)(20 * Math.Sin((direction + 180) * (Math.PI / 180)));
            int offset_x_270 = (int)(20 * Math.Cos((direction + 270) * (Math.PI / 180)));
            int offset_y_270 = (int)(20 * Math.Sin((direction + 270) * (Math.PI / 180)));

            offset_x_90 = (int)(offset_x_90 * 1.2);
            offset_y_90 = (int)(offset_y_90 * 1.2);
            offset_x_180 = (int)(offset_x_180 * 0.5);
            offset_y_180 = (int)(offset_y_180 * 0.5);
            offset_x_270 = (int)(offset_x_270 * 1.2);
            offset_y_270 = (int)(offset_y_270 * 1.2);


            Point[] ps = new Point[3];
            ps[0].X = MakerCenter_x + offset_x_90;
            ps[0].Y = MakerCenter_y + offset_y_90;
            ps[1].X = MakerCenter_x + offset_x_180;
            ps[1].Y = MakerCenter_y + offset_y_180;
            ps[2].X = MakerCenter_x + offset_x_270;
            ps[2].Y = MakerCenter_y + offset_y_270;


            g.DrawCurve(pen, ps,1);

        }

        //名前の描画
        private void DrawName(Graphics g,Point DrawPoint)
        {
            //オフセットの位置を決める
            int offset_x = 0;
            int offset_y = 0;
            int OFFSET = 30; //TODO これはマーカーのサイズによる
            for (int i = 0; i < 9; i++)
            {
                if (GUIParam.GetInstance().NamePosButton[i].Checked)
                {
                    offset_x = -OFFSET + (i % 3) * OFFSET;
                    offset_y = -OFFSET + (i / 3) * OFFSET;
                    this.NamePosition = i;
                    break;
                }
            }

            //文字列を位置(0,0)、青色で表示
            Font fnt = new Font("MS UI Gothic", 20);
            StringFormat sf = new StringFormat();
            SizeF stringSize = g.MeasureString(Name, fnt, 1000, sf);
            g.DrawString(this.Name,
                fnt,
                Brushes.White,
                DrawPoint.X - stringSize.Width / 2 + offset_x,
                DrawPoint.Y - stringSize.Height / 2 + offset_y);

        }


        public int TeamType;   //HomeかAwayか
        public int NamePosition;
        public int Width = 30;
        public int Height = 30;

        double direction = 0;

        //表示文字列
        public String UniformNumber = @"";
        public String Name = @"";

    }

}
