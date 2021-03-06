﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using p_median;
namespace p_median
{
    class Parameter
    {
        //location
        public static int m=500, n=500, p;
        public static double[] demand = new double[m];
        public static double[,] weighted_distance = new double[n, m];        
    }
}
namespace dispatch
{
    class Parameter
    {
        //relocation
        public static int m = 12, n = 12, p = 5;
        //public static double[,] T = new double[n, m];
        //public static double[] c = new double[m];
        public static double[] c = { 15, 10, 12, 18, 5, 24, 11, 16, 13, 22, 19, 20 };
        public static double[,] T = new double[,] { { 0, 15, 37, 55, 24, 60, 18, 33, 48, 40, 58, 67 },
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
    }
}
