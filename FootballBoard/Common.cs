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
        };

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
