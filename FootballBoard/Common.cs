using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballBoard
{
    //共通でつかうデータとか定義
    public class Common
    {
        public enum SELECT_DRAW_OBJECT
        {
            MOVE,
            MARKER,
            LINE,
            CURVE,
            RECT,
            CIRCLE,
            POLYGON,
            TRIANGLE,
            STRING,
        };

        //２点間の距離
        public static double GetDistance(Point a, Point b)
        {
            double distance = Math.Sqrt((b.X - a.X) * (b.X - a.X) +
                (b.Y - a.Y) * (b.Y - a.Y));

            return distance;
        }

        //２点間の角度（ラジアン）
        public static double GetAngleRadian(Point p1, Point p2)
        {
            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
        }

        //ラジアンから角度への変換
        public static double RadToDeg(double radian)
        {
            return radian * 180d / Math.PI;
        }

        //角度からラジアンへの変換
        public static double DegToRad(double deg)
        {
            return deg * Math.PI / 180d;
        }


        //配列の最大値を取得
        public static T Max<T>(params T[] nums) where T : IComparable
        {
            if (nums.Length == 0) return default(T);

            T max = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                max = max.CompareTo(nums[i]) > 0 ? max : nums[i];
                // Minの場合は不等号を逆にすればOK
            }
            return max;
        }

        //配列の最小値を取得
        public static T Min<T>(params T[] nums) where T : IComparable
        {
            if (nums.Length == 0) return default(T);

            T max = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                max = max.CompareTo(nums[i]) < 0 ? max : nums[i];
            }
            return max;
        }


        //フィールドの１部分表示しているときにマウスカーソルに対して下駄を履かせる
        public static void MakeFieldPositionOffset(ref int offset_x, ref int offset_y,ref double rate)
        {
            GUIParam.FILED_DISP_SIZE size = GUIParam.GetInstance().FiledDispSize;
            GUIParam.FILED_DIRECTION dir = GUIParam.GetInstance().FiledDirection;

            if (dir == GUIParam.FILED_DIRECTION.RIGHT)
            {
                switch (size)
                {
                    case GUIParam.FILED_DISP_SIZE.FULL:
                        offset_x = 0;
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_BOTTOM:
                        offset_x = 0;
                        rate = 1.2;
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_MIDDLE:
                        offset_x = 160;
                        rate = 1.2;
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_TOP:
                        offset_x = 320;
                        rate = 1.2;
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_BOTTOM:
                        offset_x = 0;
                        rate = 1.2;
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_MIDDLE:
                        offset_x = 200;
                        rate = 1.2;
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_TOP:
                        offset_x = 420;
                        rate = 1.2;
                        break;
                }

            }
            else if (dir == GUIParam.FILED_DIRECTION.LEFT)
            {
                switch (size)
                {
                    case GUIParam.FILED_DISP_SIZE.FULL:
                        {
                            offset_x = 0;
                            offset_y = 0;
                        }
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_BOTTOM:
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_MIDDLE:
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_TOP:
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_BOTTOM:
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_MIDDLE:
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_TOP:
                        break;
                }
            }
            else
            {
                switch (size)
                {
                    case GUIParam.FILED_DISP_SIZE.FULL:
                        offset_x = 0;
                        offset_y = 0;
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_BOTTOM:
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_MIDDLE:
                        break;
                    case GUIParam.FILED_DISP_SIZE.HALF_TOP:
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_BOTTOM:
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_MIDDLE:
                        break;
                    case GUIParam.FILED_DISP_SIZE.THIRD_TOP:
                        break;
                }
            }

        }

    }
}
