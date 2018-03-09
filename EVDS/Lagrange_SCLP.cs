using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCLP;

namespace Lagrange_SCLP
{
     class Lagrange
     {
         #region 变量定义
         static int m=500, n=500, p;
         static double cd;//cover distance
        public double totalcost;
        //问题常量
        double[] hi =new double[n];
        double[,] dij = new double[n,m] ;
        double[] costj=new double[m];//每一设施点的费用
        double[] lamdak = new double[n];
        double[] dj = new double[m];
        //记录标号
        int[] jindex = new int[m];
        int iindex;
        //记录下限解
        private int[] xj = new int[m];
        private int[,] yij = new int[n, m];
        //记录下限最优解
        public int[] xj1 = new int[m];
        public int[,] yij1 = new int[n, m];
        //算法系数
        double dertak;
        int flag=0;
        //输出结果
        public string results="";
         #endregion
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
                     costj[j]=Parameter.cost[j];
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
         private void Sum(int[,] arr, int[] arr1, int[] arrr)
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
                     if (arr[i] > arr[i + 1])
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
        private void lowerbound()
        {
          double[] vj = new double[m];
          for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if(dij[i,j]<=cd)
                    {yij[i, j]=1;}
                    vj[j] = vj[j] + yij[i, j] * lamdak[i];
                }
              dj[j]=costj[j] -vj[j];
              if (dj[j]<=0)
              {xj[j]=1;}
              else 
              {xj[j]=0;}
            } 

        }//求下限
        private int  checkresult()
        {
           int[] temp=new int [n];
           for (int i = 0; i < m; i++)
            {
              for (int j = 0; j < m; j++)
              {
                  temp[i] =temp[i]+yij [i,j]*xj[j];
              }
               if (temp[i]<1)
               {iindex =i;
                flag=1;
                break ;}
            } 
          return  flag ;  
        }//判断是否停止计算，flag=0则停止，flag=1则进入下一步
        private void finddertak()
        {    
            Sort(dj, jindex);
            for (int j = 0; j < m; j++)
            {
                if (yij[iindex, jindex[j]] == 1)
                { dertak = dj[jindex[j]]; }
            }
        }//寻找并计算dertak
        private void lamdakUpdate()
        {
            for (int i = 0; i < n; i++)
            {
                if (i == iindex) { lamdak[i] = lamdak[i] + dertak; }
                else { lamdak[i] = lamdak[i]; }
            }
        }//更新拉格朗日乘子
        public void findresult() //求优的主方法
        {
            //计算lamda初值
            double avhi = Average(hi);
            for (int i = 0; i < n; i++) { lamdak[i] = (avhi + (hi[i] - avhi) / 2); }
            do
            {
                lowerbound();//求下限
                checkresult();//判断是否停止计算，flag=0则停止，flag=1则进入下一步
                if (flag == 1)
                {
                    finddertak();
                    lamdakUpdate();
                }
            }
            while (flag == 1);
              for (int j = 0; j < m; j++)
               {
                   totalcost = totalcost + xj[j] * costj[j];//建设总费用
                   xj1[j] = xj[j];//保存最后结果
               }            
        }
     }
}
#endregion