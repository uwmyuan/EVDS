using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syncfusion.Windows.Forms.Chart;

using p_median;

namespace p_median_neighborhood
{
    class neighborhood
    {
        #region 变量定义
        public static int m=100, n=100, p=100;
        public double[] hi = new double[m];
        public double[,] dij = new double[n, m];
        double[,] hidij = new double[n + 1, m + 2];
        public int[] xj = new int[p];
        public double[] zui = new double[n];
        #endregion
        public neighborhood()
        {
            m = Parameter.m;            
            n = Parameter.n;
            p = Parameter.p;
            for (int i = 0; i < n; i++)
            {
                hi[i] = Parameter.demand[i];
                for (int j = 0; j < m; j++)
                {
                    hidij[i, j] = Parameter.weighted_distance[i, j];
                }
            }
        }
        public void neighborhood_search()
        {            
           
            int i = 0, j = 0, k = 0;
            //double[] hi = new double[12] { 15, 10, 12, 18, 5, 24, 11, 16, 13, 22, 19, 20 };
            /*double[,] dij = new double[12, 12]{ { 0, 15, 37, 55, 24, 60, 18, 33, 48, 40, 58, 67 },
                                              { 15, 0, 22, 40, 38, 52, 33, 48, 42, 55, 61, 61 },
                                              { 37, 22, 0, 18, 16, 30, 41, 28, 20, 58, 39, 39 },
                                              { 55, 40, 18, 0, 34, 12, 59, 46, 24, 62, 43, 34 }, 
                                              { 24, 38, 16, 34, 0, 36, 25, 12, 24, 47, 37, 43 },
                                              { 60, 52, 30, 12, 36, 0, 57, 43, 12, 50, 31, 22 },
                                              { 18, 33, 41, 59, 25, 57, 0, 15, 45, 22, 40, 61 },
                                              { 33, 48, 28, 46, 12, 42, 15, 0, 30, 37, 25, 46 },
                                              { 48, 42, 20, 24, 24, 12, 45, 30, 0, 38, 19, 19 },
                                              { 40, 55, 58, 62, 47, 50, 22, 37, 38, 0, 19, 40 }, 
                                              { 58, 61, 39, 43, 37, 31, 40, 25, 19, 19, 0, 21 },
                                              { 67, 61, 39, 34, 43, 22, 61, 46, 19, 40, 21, 0 } };
            */
            //for (i = 0; i < n; i++)
            //    for (j = 0; j < m; j++)
            //        hidij[i, j] = hi[i] * dij[i, j];
            for (i = 0; i < n; i++)
                hidij[i, m] = 1000000;
            for (k = 0; k < p; k++)
            {
                for (j = 0; j < m; j++)
                {
                    hidij[12,j]=0;
                    for (i = 0; i < n; i++)
                        hidij[12, j] = hidij[12, j] + hidij[i, j];
                }
                hidij[n, m] = hidij[n, 0];
                for (j = 0; j < m; j++)
                {
                    if (hidij[n, j]<=hidij[n, m])
                    {
                        hidij[n, m] = hidij[n, j];
                        xj[k] = j;
                        zui[k] = hidij[n, j];
                    }

                }
                for (i = 0; i < n; i++)
                {
                    if (hidij[i, xj[k]] < hidij[i, m])
                    {
                        hidij[i, m] = hidij[i, xj[k]];
                        hidij[i, m+1] = xj[k];
                    }
                }
                for (i = 0; i < n; i++)
                    for (j = 0; j < m; j++)
                    {
                        if (hidij[i, j] > hidij[i, m])
                            hidij[i, j] = hidij[i, m];
                    }
            }

            /*for (i = 0; i < p; i++)
            {
                Console.WriteLine(xj[i]);
                Console.WriteLine(zui[i]);
            }
            for (i = 0; i < n; i++)
                Console.WriteLine("{0}分配给{1}", i, hidij[i, 13]);
            Console.ReadKey();*/
        }
    }


}

