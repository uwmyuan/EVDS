using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypercube
{
    class traveltime
    {
        //public double[,] tij = new double[10, 10]{{0,3,5,2,5,7,5,5,7,7},
        //                                       {3,0,2,5,2,4,8,6,10,8},
        //                                       {5,2,0,7,4,2,10,8,12,10},
        //                                       {2,5,7,0,3,5,3,3,5,5},
        //                                       {5,2,4,3,0,2,6,4,8,6},
        //                                       {7,4,2,5,2,0,8,6,10,8},
        //                                       {5,8,10,3,6,8,0,2,2,4},
        //                                       {5,6,8,3,4,6,2,0,4,2},
        //                                       {7,10,12,5,8,10,2,4,0,2},
        //                                       {7,8,10,5,6,8,4,2,2,0}};
        //public double[,] pnj = new double[3, 10] {{0,0,0,1,0,0,0,0,0,0},
        //                                       {0,0,0,0,1,0,0,0,0,0},
        //                                       {0,0,0,0,0,0,0.5,0.5,0,0}};
        //int[,] ma = new int[10, 3]{{1,2,3},
        //                               {1,2,3},
        //                               {2,1,3},
        //                               {1,3,2},
        //                               {2,1,3},
        //                               {2,1,3},
        //                               {3,1,2},
        //                               {3,1,2},
        //                               {3,1,2},
        //                               {3,1,2}};
        //double[] lamodan = new double[10] { 0.25, 0.25, 0.1, 0.25, 0.15, 0.1, 0.1, 0.1, 0.1, 0.1 };
        public double[,] tij = Parameter.tij;
        public int[,] ma = Parameter.ma;
        public double[] lamodan = Parameter.lamodan;
        public double[,] pnj = Parameter.pnj;
        double[] pnu = new double[3];
        public double[,] tnj = new double[3, 10];
        double[,] f_nk = new double[3, 10];
        public double[] tj = new double[10];
        public double[] tran = new double[3];
        public double[] tun = new double[3];
        double lamoda = 1.5, p,pq = 0.2368, lamodad = 0.1184;
        public double tq, _t = 0;
        public int n = 3, na = 10;
        int miu = 1;
        int i, j, t, k = 0, l;
        public void traveltime_calculate()
        {
            p = lamoda / (n * miu);
            for (l = 0; l < n; l++)
                pnu[l] = Hypercube_approximate(ma, lamodan, n, na, miu, lamoda, lamodad, l);
            //unit n到atom j的时间
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < na; j++)
                {
                    tnj[i, j] = c1(i + 1, j + 1, tij, pnj, na);
                }
            }
            //有排队需求的平均出行时间
            tq = c2(na, lamoda, tij, lamodan);
            //全区域无限制平均出行时间
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < na; j++)
                {
                    for (t = 0; t < n; t++)
                    {
                        if ((i + 1) == ma[j, t])
                        {
                            k = t + 1;
                            break;
                        }
                    }
                    f_nk[i, j] = lamodan[j] / lamoda * c3(n, p, k - 1);
                    for (t = 0; t < k - 1; t++)
                    {
                        f_nk[i, j] = f_nk[i, j] * pnu[ma[j, t] - 1];
                    }
                    f_nk[i, j] = f_nk[i, j] * (1 - pnu[i]);
                }
            }
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < na; j++)
                {
                    _t = _t + f_nk[i, j] * tnj[i, j];
                }
            }
            _t = _t + pq * tq;
            //到atom j点的平均出行时间
            for (j = 0; j < na; j++)
            {
                tj[j] = c5(f_nk, tnj, pq, lamodan, tij, lamoda, n, na, j);
            }
            //对unit n区域需求的平均出行时间
            for (i = 0; i < n; i++)
            {
                tran[i] = c6(ma, f_nk, tnj, lamodan, tij, lamoda, pq, n, na, i);
            }
            //unit n的平均出行时间
            for (i = 0; i < n; i++)
            {
                tun[i] = c7(f_nk, tnj, tq, pq, n, na, i);
            }
        }

        public static double c1(int n, int j, double[,] tij, double[,] pnj, int na)
        {
            double tnj = 0;
            int k;
            for (k = 1; k <= na; k++)
            {
                tnj = tnj + pnj[n - 1, k - 1] * tij[k - 1, j - 1];
            }
            return tnj;
        }
        public static double c2(int na, double lamoda, double[,] tij, double[] lamodan)
        {
            int i, j;
            double sum = 0.0;
            for (i = 0; i < na; i++)
            {
                for (j = 0; j < na; j++)
                {
                    sum = sum + lamodan[i] * lamodan[j] / (lamoda * lamoda) * tij[i, j];
                }
            }
            return sum;
        }
        public static double c3(int n, double p, int j)
        {
            double sum1 = 0.0, sum2 = 0.0, w;
            int i, k;
            for (k = j; k <= n - 1; k++)
            {
                sum1 = sum1 + c4(n - j - 1) * (n - k) / c4(k - j) * Math.Pow(n, k) / c4(n) * Math.Pow(p, k - j);
            }
            for (i = 0; i <= n - 1; i++)
            {
                sum2 = sum2 + Math.Pow(n, i) / c4(i) * Math.Pow(p, i);
            }
            w = sum1 / ((1 - p) * sum2 + Math.Pow(n, n) * Math.Pow(p, n) / c4(n));
            return w;
        }
        public static int c4(int n)
        {
            int sum = 1;
            int i;
            for (i = 1; i <= n; i++)
            {
                sum = sum * i;
            }
            return sum;
        }
        public static double c5(double[,] f_nj, double[,] tnj, double pq, double[] lamodan, double[,] tij, double lamoda, int n, int na, int u)
        {
            double sum1 = 0.0, sum2 = 0.0, sum3 = 0.0;
            int i;
            double tj;
            for (i = 0; i < n; i++)
            {
                sum1 = sum1 + f_nj[i, u] * tnj[i, u];
                sum2 = sum2 + f_nj[i, u];
            }
            for (i = 0; i < na; i++)
            {
                sum3 = sum3 + lamodan[i] / lamoda * tij[i, u];
            }
            tj = sum1 / sum2 * (1 - pq) + sum3 * pq;
            return tj;
        }
        public static double c6(int[,] ma, double[,] f_nj, double[,] tnj, double[] lamodan, double[,] tij, double lamoda, double pq, int n, int na, int u)
        {
            int i, j, k = 0;
            double sum1 = 0.0, sum2 = 0.0, sum3 = 0.0, sum4 = 0.0, tran;
            int[] gu1 = new int[10];
            for (i = 0; i < na; i++)
            {
                if ((u + 1) == ma[i, 0])
                {
                    gu1[k] = i;
                    k++;
                }
            }
            for (i = 0; i < k; i++)
            {
                for (j = 0; j < n; j++)
                {
                    sum1 = sum1 + f_nj[j, gu1[i]] * tnj[j, gu1[i]];
                    sum2 = sum2 + f_nj[j, gu1[i]];
                }
            }
            for (i = 0; i < k; i++)
            {
                for (j = 0; j < na; j++)
                {
                    sum3 = sum3 + lamodan[j] * lamodan[gu1[i]] * tij[j, gu1[i]] / (lamoda * lamoda);
                }
            }
            for (i = 0; i < k; i++)
            {
                sum4 = sum4 + lamodan[gu1[i]] / lamoda;
            }
            tran = sum1 / sum2 * (1 - pq) + sum3 / sum4 * pq;
            return tran;
        }
        public static double c7(double[,] f_nj, double[,] tnj, double tq, double pq, int n, int na, int u)
        {
            int j;
            double sum1 = 0.0, sum2 = 0.0, tun;
            for (j = 0; j < na; j++)
            {
                sum1 = sum1 + f_nj[u, j] * tnj[u, j];
                sum2 = sum2 + f_nj[u, j];
            }
            tun = (sum1 + tq * pq / n) / (sum2 + pq / n);
            return tun;
        }
        public double Hypercube_approximate(int[,] ma, double[] lamodan, int n, int na, int miu, double lamoda, double lamodad, int l)
        {
            double[] pnl = new double[3];
            double[] rf = new double[3];
            double[] pn = new double[3];
            double[] pnu = new double[3];
            int i, j, u;
            double p, sum = 0.0, t, max, e = 0.001;
            p = lamoda / (n * miu);
            //lamodad = lamodads(na, n, miu, lamoda);
            for (i = 0; i < n; i++)
                pnl[i] = p;
            for (u = 1; ; u++)
            {
                for (i = 0; i < n; i++)
                {
                    rf[i] = 0.0;
                    for (j = 0; j < n; j++)
                    {
                        rf[i] = rf[i] + Rpart2(i + 1, j + 1, na, n, p, lamodan, ma, pnl);
                    }
                }
                for (i = 0; i < n; i++)
                {
                    pnu[i] = (rf[i] + lamodad) / (1 + rf[i]);
                }
                sum = 0.0;
                for (i = 0; i < n; i++)
                {
                    sum = sum + pnu[i];
                }
                t = n * p / sum;
                for (i = 0; i < n; i++)
                {
                    pnu[i] = t * pnu[i];
                }
                for (i = 0; i < n; i++)
                {
                    pn[i] = pnu[i] - pnl[i];
                    pn[i] = Math.Abs(pn[i]);
                }
                max = Max(pn, n);
                if (max > e)
                {
                    for (i = 0; i < n; i++)
                    {
                        pnl[i] = pnu[i];
                    }
                }
                else break;
            }
            return pnu[l];
            /*
            for (i = 0; i < n; i++)
            {
                Console.WriteLine(pnu[i]);每个设施的繁忙率
            }
            //Console.WriteLine(u);
            //Console.WriteLine(max);
            Console.ReadLine();
             */
        }
        public int jiecheng(int n)
        {
            int sum = 1;
            int i;
            for (i = 1; i <= n; i++)
            {
                sum = sum * i;
            }
            return sum;
        }
        public double Q(int n, double p, int j)
        {
            double sum1 = 0.0, sum2 = 0.0, w;
            int i, k;
            for (k = j; k <= n - 1; k++)
            {
                sum1 = sum1 + jiecheng(n - j - 1) * (n - k) / jiecheng(k - j) * Math.Pow(n, k) / jiecheng(n) * Math.Pow(p, k - j);
            }
            for (i = 0; i <= n - 1; i++)
            {
                sum2 = sum2 + Math.Pow(n, i) / jiecheng(i) * Math.Pow(p, i);
            }
            w = sum1 / ((1 - p) * sum2 + Math.Pow(n, n) * Math.Pow(p, n) / jiecheng(n));
            return w;
        }
        public int Gnj(int[,] ma, int x, int y, int m, int k)
        {
            int[] gxy = new int[10];
            int i, j = 0;
            for (i = 0; i < m; i++)
            {
                if (x == ma[i, y - 1])
                {
                    gxy[j] = i + 1;
                    j++;
                }
            }
            return gxy[k];
        }
        public double Rpart2(int x, int y, int na, int n, double p, double[] lamodan, int[,] ma, double[] pp)
        {
            int[] gxy = new int[10];
            int i, j, k;
            double sum = 0.0, sum1 = 0.0;
            for (k = 0; k < na; k++)
            {
                gxy[k] = Gnj(ma, x, y, na, k);
            }
            for (i = 0; ; i++)
            {
                if (gxy[i] != 0)
                {
                    sum = lamodan[gxy[i] - 1] * Q(n, p, y - 1);
                    for (j = 0; j < y - 1; j++)
                    {
                        sum = sum * pp[ma[gxy[i] - 1, j] - 1];
                    }
                    sum1 = sum1 + sum;
                }
                else break;
            }
            return sum1;
        }
        public double Max(double[] a, int n)
        {
            int i;
            double max = a[0];
            for (i = 1; i < n; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max;
        }
    }
}
