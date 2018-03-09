using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syncfusion.Windows.Forms.Chart;
using MCLP;

namespace MCLP_lagrange
{
    class Lagrange
    {
        #region 变量定义
        static int m = 500, n = 500, p;
        static double cd;//cover distance
        //问题常量
        double[] hi =new double[n];
        double[,] dij = new double[n,m] ;
        double[] lamdak = new double[n];
        //记录标号
        int[] jindex = new int[m];
        //记录下限解
        private int[] xj = new int[m];
        private int[,] yij = new int[n, m];
        private int[] zi = new int[n];
        //记录下限最优解
        public int[] xj1 = new int[m];
        public int[,] yij1 = new int[n, m];
        private int[] zi1 = new int[n];
        //记录上限解
        int[] xj2 = new int[m];
        int[,] yij2 = new int[n, m];
        private int[] zi2 = new int[n];
        //记录上限最优解
        public int[] xj3 = new int[m];
        public int[,] yij3 = new int[n, m];
        private int[] zi3 = new int[n];
        //当前上下限
        double zlk;
        double zuk;
        //最优上下限
        public double upbound;
        public double lowbound;
        public System.Collections.ArrayList upbounds = new System.Collections.ArrayList();
        public System.Collections.ArrayList lowbounds = new System.Collections.ArrayList();
        public System.Collections.ArrayList zuks = new System.Collections.ArrayList();
        public System.Collections.ArrayList zlks = new System.Collections.ArrayList();
        //次梯度系数
        double tk;
        double alfak;
        int flag=0;
        //输出结果
        public string results="";
        #endregion
        //定义方法
        #region 初始化
        public Lagrange()
        {
            m = Parameter.m;
            n = Parameter.n;
            p = Parameter.p;
            cd = Parameter.coverdistance;
            for (int i = 0; i < n;i++) 
            {
                hi[i] = Parameter.demand[i];
                for (int j = 0; j < m; j++)
                {
                    dij[i, j] = Parameter.distance[i, j];
                }
            }
        }
        #endregion
        #region 数学方法
        private void Sum(int[,] arr, int[] arrr, int way)
        {
            int i, j;
            if (way == 1)
            {
                for (i = 0; i < m; i++)
                {
                    arrr[i] = 0;
                    for (j = 0; j < n; j++)
                    {
                        arrr[i] = arrr[i] + arr[i, j];
                    }

                }
            }
            else
            {
                if (way == 2)
                {
                    for (j = 0; j < m; j++)
                    {
                        arrr[j] = 0;
                        for (i = 0; i < n; i++)
                        {
                            arrr[j] = arrr[j] + arr[i, j];
                        }
                    }
                }
            }
        }
        private void Sum(int[,] arr, double[] arrr, int way)
        {
            int i, j;
            if (way == 1)
            {
                for (i = 0; i < m; i++)
                {
                    arrr[i] = 0;
                    for (j = 0; j < n; j++)
                    {
                        arrr[i] = arrr[i] + arr[i, j];
                    }

                }
            }
            else
            {
                if (way == 2)
                {
                    for (j = 0; j < m; j++)
                    {
                        arrr[j] = 0;
                        for (i = 0; i < n; i++)
                        {
                            arrr[j] = arrr[j] + arr[i, j];
                        }
                    }
                }
            }
        }
        private void Sum(double[] arr, double sum)
        {
            int i;
            sum = 0;
            for (i = 0; i <= arr.Length; i++)
            {
                sum = sum + arr[i];
            }
        }
        private double Sum(double[] arr)
        {
            int i;
            double sum = 0;
            for (i = 0; i < arr.Length; i++)
            {
                sum = sum + arr[i];
            }
            return sum;
        }
        private void Sum(int[,] arr, int[] arr1, double [] arrr)
        {
            int i, j;
            for (j = 0; j < m; j++)
            {
                arrr[j] = 0;
                for (i = 0; i < n; i++)
                {
                    arrr[j] = arrr[j] + arr[i, j] * arr1[j];
                }

            }
        }
        private void Sort(double[] arr, int[] index)
        {

            int i, j;
            double temp1;
            int temp2;
            bool done = false;
            j = 1;
            while ((j < arr.Length) && (!done))//判断长度　
            {
                done = true;
                for (i = 0; i < arr.Length - j; i++)
                {
                    if (arr[i] < arr[i + 1])
                    {
                        done = false;
                        temp1 = arr[i];
                        arr[i] = arr[i + 1];//交换数据　
                        arr[i + 1] = temp1;
                        temp2 = index[i];
                        index[i] = index[i + 1];//交换标号
                        index[i + 1] = temp2;
                    }
                }
                j++;
            }
        }
        private double Average(double[,] arr)
        {
            double sum = 0, aver;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    sum = sum + arr[i, j];
            }
            aver = sum / m / n;
            return aver;
        }
        private double Average(double[] arr)
        {
            double sum = 0, aver;
            for (int i = 0; i < arr.Length; i++)
            {
                sum = sum + arr[i];
            }
            aver = sum / arr.Length;
            return aver;
        }
        private double Max(double[] arr)
        {
            double max = 0;
            int i;
            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            return max;

        }
        private double Min(double[] arr)
        {
            double min = double.MaxValue;
            int i;
            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                    min = arr[i];
            }
            return min;

        }
        #endregion
        #region 寻优方法
        private double lowerbound()
        {
            double zlk = 0;
            for (int i = 0; i < n; i++)
            {
                zlk = zlk + zi[i]*hi[i];
            }
            return zlk;

        }//求下限
        private double upperbound()
        {
            double zuk = 0;
            double zuk1 = 0;
            double zuk2 = 0;
            double[] vvj = new double[m];
            //初始化
           
            for (int j = 0; j < m; j++)
            {
                xj2[j] = xj[j];
                for (int i = 0; i < n; i++)
                {
                    yij2[i, j] = yij[i,j];
                }
            }
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    vvj[j] = vvj[j] + yij2[i, j] * lamdak[i];
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (hi[i] > lamdak[i])
                {
                    zi2[i] = 1;
                }
                else {zi2[i]=0;}
                zuk1 = zuk1 + (hi[i] - lamdak[i]) * zi2[i];
                
                for (int j = 0; j < p; j++)
                {
                    zuk2 = zuk2 + vvj[j] * xj2[j];
                }
                zuk = zuk1 + zuk2;
            }
            return zuk;
        }//求上限
        private void findsolution()
        {
            double[] vj = new double[m];
            //初始化
            for (int j = 0; j < m; j++)
            {
                xj[j] = 0;
                vj[j] = 0;
                jindex[j] = j;
                for (int i = 0; i < n; i++)
                {
                    yij[i, j] = 0;
                }
            }
            //寻找可行解
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if(dij[i,j]<=cd)
                    {yij[i, j]=1;}
                    vj[j] = vj[j] + yij[i, j] * lamdak[i];
                }
            }
            Sort(vj, jindex);
            for (int j = 0; j < m; j++)
            {
                for (int k = 0; k < p; k++)
                {
                    if (j == jindex[k])
                    { xj[j] = 1; }
                }

            }
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if ((xj[j] == 1) && ( yij[i, j] == 1))
                    {zi[i] = 1; }
                    else {zi[i] = 0; }

                }
            }
        }//寻找可行解
        public void findresult() //求优的主方法
        {
            //定义局部变量
            int k = 1;
            //初值
            tk = double.MaxValue;
            for (int j = 0; j < m; j++)
            {
                xj1[j] = 0;
                jindex[j] = j;
                for (int i = 0; i < n; i++)
                {
                    yij1[i, j] = 0;
                }
            }
            alfak = 2;
            //上下限初值
            upbound = double.MaxValue;
            lowbound = double.MinValue;
            upbounds.Clear();
            lowbounds.Clear();
            zuks.Clear();
            zlks.Clear();
            //计算lamda初值
            double avhi = Average(hi);
            for (int i = 0; i < n; i++) { lamdak[i] = (avhi + (hi[i] - avhi) / 2); }
            while (k <= 1000)
            {
                //求可能的最优解
                findsolution();
                //求上下限
                zuk = upperbound();
                zlk = lowerbound();
                if (lowbound <= zlk)flag++;
                //更新上下限
                upbound = Math.Min(upbound, zuk);
                lowbound = Math.Max(lowbound, zlk);
                upbounds.Add(upbound);
                lowbounds.Add(lowbound);
                zuks.Add(zuk);
                zlks.Add(zlk);
                //判断当前结果是否更优
                if (lowbound == zlk)
                {
                    for (int j = 0; j < m; j++)
                    {
                        xj1[j] = xj[j];
                        for (int i = 0; i < n; i++)
                        {
                            yij1[i, j] = yij[i, j];
                            zi1[i] = zi[i];
                        }
                    }

                }
                if (upbound == zuk)
                {
                    for (int j = 0; j < m; j++)
                    {
                        xj3[j] = xj2[j];
                        for (int i = 0; i < n; i++)
                        {
                            yij3[i, j] = yij2[i, j];
                            zi3[i] = zi2[i];
                        }
                    }
                }
                //记录过程

                //输出结果
                //判断是否最优解
                checkresult();
                if (k == 2000)
                {
                    results = "a satisfied feasible solution iteration迭代次数足够多";
                }
                if (results != "") break;
                //更新lamdak
                lamdakUpdate();
                k++;
            }
        }
        private void checkresult() 
        {
            int[] bbb = new int[n];
            Sum(yij1, bbb, 1);
            int flag = 0;
            for (int i = 0; i < n; i++) if (bbb[i] == 1) flag++;
            if (flag == n)
            {
                results = "a optimal solution最优解";
            }
            //判断是否满意解
            else if ((upbound - lowbound) <= 0.03)
            {
                results = "an approximate solution近似解";
            }
            //判断步长是否足够小
            else if (tk <= 0.0001)
            {
                results = "a satisfied feasible solution tk满意解";
            }
            //判断循环次数是否足够多
        }//检查结果
        private void lamdakUpdate()
        {
            if (flag >= 200) { flag = 0; alfak = alfak / 2; }
            double[] tkki = new double[n];
            double[] tki = new double[n];
            Sum(yij2, xj2, tkki);
            for (int i = 0; i < n; i++)
            {
                tkki[i] = tkki[i] - zi2[i];
                tki[i] = tkki[i] * tkki[i];
            }
            tk = (alfak * (zuk - lowbound)) / (Sum(tki));
            for (int i = 0; i < m; i++)
            {
                lamdak[i] = Math.Max(0, (lamdak[i] - tk *tkki[i]));
            }
        }//更新拉格朗日乘子
        #endregion
    }
}
