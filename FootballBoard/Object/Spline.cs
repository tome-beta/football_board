using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballBoard
{
    //スプライン曲線を作るクラス
    [Serializable()]
    public class Spline
    {
        public const int MAX_SPLINE_SIZE = 3;   //スプライン曲線の頂点
        public const int HIT_RANGE = 10;        //当たり判定チェック用
        //スプライン曲線の係数計算
        public void Init(double[] value, int input_num)
        {
            double tmp;
            double[] w = new double[MAX_SPLINE_SIZE + 1];
            int i;

            num = input_num - 1;

            // ３次多項式の0次係数(a)を設定
            for (i = 0; i <= num; i++)
            {
                a[i] = value[i];
            }

            // ３次多項式の2次係数(c)を計算
            // 連立方程式を解く。
            // 但し、一般解法でなくスプライン計算にチューニングした方法
            c[0] = c[num] = 0.0;
            for (i = 1; i < num; i++)
            {
                c[i] = 3.0 * (a[i - 1] - 2.0 * a[i] + a[i + 1]);
            }
            // 左下を消す
            w[0] = 0.0;
            for (i = 1; i < num; i++)
            {
                tmp = 4.0 - w[i - 1];
                c[i] = (c[i] - c[i - 1]) / tmp;
                w[i] = 1.0 / tmp;
            }
            // 右上を消す
            for (i = num - 1; i > 0; i--)
            {
                c[i] = c[i] - c[i + 1] * w[i];
            }

            // ３次多項式の1次係数(b)と3次係数(b)を計算
            b[num] = d[num] = 0.0;
            for (i = 0; i < num; i++)
            {
                d[i] = (c[i + 1] - c[i]) / 3.0;
                b[i] = a[i + 1] - a[i] - c[i] - d[i];
            }
        }

        //実際の値を取り出す
        public double calc(double t)
        {
            double value = 0.0;

            int j;
            double dt;
            j = (int)(t); // 小数点以下切捨て
            if (j < 0)
            {
                j = 0;
            }
            else if (j >= num)
            {
                j = num - 1; // 丸め誤差を考慮
            }


            dt = t - (double)j;
            value = a[j] + (b[j] + (c[j] + d[j] * dt) * dt) * dt;
            return value;
        }

        private int num = 0;
        double[] a = new double[MAX_SPLINE_SIZE + 1];
        double[] b = new double[MAX_SPLINE_SIZE + 1];
        double[] c = new double[MAX_SPLINE_SIZE + 1];
        double[] d = new double[MAX_SPLINE_SIZE + 1];


    }
}
