using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypercube
{
    class Hypercube
    {
        public double[] pnu = new double[3];
        public int u;
        //public int[,] ma = new int[10, 3]{{1,2,3},
        //                                   {1,2,3},
        //                                   {2,1,3},
        //                                   {1,3,2},
        //                                   {2,1,3},
        //                                   {2,1,3},
        //                                   {3,1,2},
        //                                   {3,1,2},
        //                                   {3,1,2},
        //                                   {3,1,2}};
        //public double[] lamodan = new double[10] { 0.25, 0.25, 0.1, 0.25, 0.15, 0.1, 0.1, 0.1, 0.1, 0.1 };
        public int[,] ma = Parameter.ma;
        public double[] lamodan = Parameter.lamodan;
        public double e = 0.001;
        public void Hypercube_approximate()
        {   
            double[] pnl = new double[3];
            double[] rf = new double[3];            
            double[] pn = new double[3];
            int i, j, n = 3, na = 10, miu = 1;
            double p, lamoda = 1.5, lamodad = 0.1184, sum = 0.0, t, max;
            p = lamoda / (n * miu);
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
        int jiecheng(int n)
        {
            int sum = 1;
            int i;
            for (i = 1; i <= n; i++)
            {
                sum = sum * i;
            }
            return sum;
        }
        double Q(int n, double p, int j)
        {
            double sum1 = 0.0, sum2 = 0.0, w;
            int i, k;
            for (k = j; k <= n - 1; k++)
            {
                sum1 = sum1 + jiecheng(n - j - 1) * (n - k) /jiecheng(k - j) * Math.Pow(n, k) / jiecheng(n) * Math.Pow(p, k - j);
            }
            for (i = 0; i <= n - 1; i++)
            {
                sum2 = sum2 + Math.Pow(n, i) / jiecheng(i) * Math.Pow(p, i);
            }
            w = sum1 / ((1 - p) * sum2 + Math.Pow(n, n) * Math.Pow(p, n) / jiecheng(n));
            return w;
        }
        int Gnj(int[,] ma, int x, int y, int m, int k)
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
        double Rpart2(int x, int y, int na, int n, double p, double[] lamodan, int[,] ma, double[] pp)
        {
            int[] gxy = new int[na];
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
        double Max(double[] a, int n)
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
