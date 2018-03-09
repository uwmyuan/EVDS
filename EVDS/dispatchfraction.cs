using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypercube
{
    class dispatch_fraction
    {
        
        //int[,] ma = new int[10, 3]{{1,2,3},
        //                            {1,2,3},
        //                            {2,1,3},
        //                            {1,3,2},
        //                            {2,1,3},
        //                            {2,1,3},
        //                            {3,1,2},
        //                            {3,1,2},
        //                            {3,1,2},
        //                            {3,1,2}};
        //double[] lamodan = new double[10] { 0.25, 0.25, 0.1, 0.25, 0.15, 0.1, 0.1, 0.1, 0.1, 0.1 };
        public int[,] ma =Parameter.ma;
        public double[] lamodan =Parameter.lamodan;
        double[] pnu = new double[3] ;//{ 0.5626, 0.4717, 0.4657 }
        double[,] f_nj = new double[3, 10];
        public double[,] fnj = new double[3, 10];
        int[,] s=new int[3,10];
        int[,] ss = new int[3, 10];
        int[] numbers = new int[3];
        int[] numberss=new int[3];
        public double[] fin = new double[3];
        public double[] fii = new double[3];
        public double fi=0;
        double lamoda = 1.5, p, pq = 0.2368,lamodad=0.1184;
        public int n = 3, na = 10;
        int miu = 1;
        int i, j,t,k,l;
        public void dispatchfraction()
        {
            for (l = 0; l < n; l++)
                pnu[l] = Hypercube_approximate(ma, lamodan, n, na, miu, lamoda, lamodad, l);
            p = lamoda / (n * miu);
            //n派给j的服务占所有服务的比例
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < na; j++)
                {
                    k=0;
                    for (t = 0; t < n; t++)
                    {
                        if ((i + 1) == ma[j, t])
                        {
                            k = t + 1;
                            break;
                        }
                    }
                    f_nj[i, j] = lamodan[j] / lamoda *class2(n, p, k - 1);
                    for (t = 0; t < k - 1; t++)
                    {
                        f_nj[i, j] = f_nj[i, j] * pnu[ma[j, t] - 1];
                    }
                    f_nj[i, j] = f_nj[i, j] * (1 - pnu[i]);
                    fnj[i,j]=f_nj[i,j]+lamodan[j]*pq/(lamoda*n);
                }
            }
            //确定j是否属于n的响应区
            for(i=0;i<n;i++)
            {
                for (j = 0; j < na; j++)
                {
                    if ((i + 1) == ma[j, 0])
                    {
                        s[i, numbers[i]] = j;
                        numbers[i]++;
                    }
                    else
                    {
                        ss[i, numberss[i]] = j;
                        numberss[i]++;
                    }
                }
            }
            //所有不负责本身区域的频率
            for (i = 0; i < n; i++)
                for (j = 0; j < numberss[i]; j++)
                    fi = fi + fnj[i, ss[i,j]];
            //unit n不负责本身区域的频率
            for (i = 0; i < n; i++)
            {
                fin[i]=class3(fnj,ss,numberss,na,i);
                fii[i]=class4(fnj,s,numbers,n,i);
            }
        }
        public int class1(int n)
        {
            int sum = 1;
            int i;
            for (i = 1; i <= n; i++)
            {
                sum = sum * i;
            }
            return sum;
        }
        public double class2(int n, double p, int j)
        {
            double sum1 = 0.0, sum2 = 0.0, w;
            int i, k;
            for (k = j; k <= n - 1; k++)
            {
                sum1 = sum1 + class1(n - j - 1) * (n - k) / class1(k - j) * Math.Pow(n, k) / class1(n) * Math.Pow(p, k - j);
            }
            for (i = 0; i <= n - 1; i++)
            {
                sum2 = sum2 + Math.Pow(n, i) / class1(i) * Math.Pow(p, i);
            }
            w = sum1 / ((1 - p) * sum2 + Math.Pow(n, n) * Math.Pow(p, n) / class1(n));
            return w;
        }
        public double class3(double[,] fnj, int[,] ss, int[] numberss, int na, int u)
        {
            double sum1 = 0.0, sum2 = 0.0, fin;
            int i;
            for (i = 0; i < numberss[u]; i++)
                sum1 = sum1 + fnj[u, ss[u, i]];
            for (i = 0; i < na; i++)
                sum2 = sum2 + fnj[u, i];
            fin = sum1 / sum2;
            return fin;
        }
        public double class4(double[,] fnj, int[,] s, int[] numbers, int n, int u)
        {
            int i, j;
            double sum1 = 0.0, sum2 = 0.0, fii;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < numbers[u]; j++)
                {
                    sum2 = sum2 + fnj[i, s[u, j]];
                }
            }
            for (i = 0; i < n; i++)
            {
                if (i != u)
                {
                    for (j = 0; j < numbers[u]; j++)
                        sum1 = sum1 + fnj[i, s[u, j]];
                }
            }
            fii = sum1 / sum2;
            return fii;
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
