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
            STRING,
        };

        //２点間の距離
        public static double GetDistance(Point a, Point b)
        {
            double distance = Math.Sqrt((b.X - a.X) * (b.X - a.X) +
                (b.Y - a.Y) * (b.Y - a.Y));

            return distance;
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
    }
}
