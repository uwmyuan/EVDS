using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dispatch
{
    class dispatch
    {
        static int m, n,num;
        double[] c = new double[n];
        double[,] t=new double[n,m];
        double[] gama=new double[num];
        double[] p = new double[n];
        int[,] index = new int[n,m];
        int A;
        public int flag,level;
        double p_min,T2,T3;
        public dispatch()
        {
            //initialize
            A = 0;
            p_min = 0;
            for (int i = 0; i < n; i++) for (int j = 0; j < m;j++ ) index[i, j] = j;
            sort();
            ini_gama();
            preparation();
        }
        private void preparation()
        {
            for (int i = 0; i < n; i++)
            {
                p[i] = 0;
                for (int j = 0; j < num; j++) 
                {
                    p[i] = p[i] + gama[j] / t[i, j]/c[i];
                }
            }
        }
        private void sort() 
        {
            for (int i = 0; i < n; i++) 
            {
                for (int j = 0; j < m; j++) 
                {
                    for(int k = 0; k < m-j; k++)
                    {
                        if (t[i, k] < t[i,k+1]) 
                        {
                            double temp1;
                            temp1 = t[i, k];
                            t[i, k] = t[i, k + 1];
                            t[i, k + 1] = temp1;
                            int temp2;
                            temp2 = index[i, k];
                            index[i, k] = index[i, k + 1];
                            index[i, k + 1] = temp2;
                        }
                    }
                }
            }
        }
        private double find_p_min() 
        {
            double temp = double.MaxValue;
            for (int i = 0; i < n; i++) 
            {
                temp = Math.Min(temp, p[i]);
            }
            return temp;   
        }
        private void ini_gama() 
        {
            double sum=0;
            for(int j=0;j<m;j++)
            {
                sum = sum + t[flag, j];
            }
            for(int j=0;j<num;j++)
            {
                gama[j] = sum / t[flag, j];
            }
        }
        public int ambulance_dispatch()
        {
            switch (level)
            {
                case 1: A = 1;
                    break;
                case 2:
                    for (int j = 0; j < m; j++)
                    {
                        if (t[flag, j] < T2) gama[j] = 0;
                        preparation();
                        double temp = find_p_min();
                        if (temp > p_min)
                        {
                            p_min = temp;
                            A = j;
                        }
                    }
                    break;
                case 3:
                    for (int j = 0; j < m; j++)
                    {
                        if (t[flag, j] < T3) gama[j] = 0;
                        preparation();
                        double temp = find_p_min();
                        if (temp > p_min)
                        {
                            p_min = temp;
                            A = j;
                        }
                    }
                    break;
            }
            return index[flag, A];
        }
    }
}
