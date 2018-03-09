using System;
//by kong
namespace r_interdiction
{
    class r_interdiction
    {
        public double[] hi=new double[Parameter.m];
        public double[,] dij = new double[Parameter.n, Parameter.m];
        int p=Parameter.p, r=Parameter.r, m=Parameter.m;
        public int[] s = new int[Parameter.p];
        public int[] sgood = new int[Parameter.p];
        int[] xj = Parameter.xj;
        public double max = 0;
        public double wfi;
        //int[] xj=new int[5]{ 1, 3, 4, 10, 11 };
        private void f1(int[] xj, double[] hi, double[,] dij, int t, int p, int m)
        {
            double[,] hidijp = new double[12, 5];
            double[] fenpei = new double[12];
            double[] min = new double[12];
            for (int i = 0; i < m; i++)
            {
                //计算需求点到各服务设施设置点的参数hidijp
                for (int j = 0; j < p - t; j++)
                {
                    hidijp[i, j] = hi[i] * dij[i, xj[j]];
                }
                //初始化及用min存储最短距离，用fenpei记录分配方案
                min[i] = hidijp[i, 0];
                fenpei[i] = xj[0];
                for (int j = 0; j < p - t; j++)
                {
                    if (hidijp[i, j] < min[i])
                    {
                        min[i] = hidijp[i, j];
                        fenpei[i] = xj[j];
                    }
                }
            }
        }
        private double f2(int[] f, double[] hi, double[,] dij, int p, int r, int m)
        {
            int i, j;
            double sum = 0;
            double[,] hidijp = new double[12, 5];
            double[] min = new double[12];
            for (i = 0; i < m; i++)
            {
                //计算需求点到各服务设施设置点的参数hidijp
                for (j = 0; j < p - r; j++)
                    hidijp[i, j] = hi[i] * dij[i, f[j]];
                //初始化及用min存储最短距离
                min[i] = hidijp[i, 0];
                for (j = 0; j < p - r; j++)
                    if (hidijp[i, j] < min[i])
                        min[i] = hidijp[i, j];
            }
            for (i = 0; i < m; i++)
                sum = sum + min[i];
            return sum;
        }
        private int f3(int[] ssnow, int[,] xxx, int k, int r)
        {
            int i, j = 0, value = 0;
            int[] xxxx = new int[5];
            for (i = 0; i < k; i++)
            {
                for (j = 0; j < r; j++)
                    xxxx[j] = xxx[i, j];
                if (ssnow == xxxx)
                {
                    value = 1;
                    break;
                }
            }
            return value;
        }
        public int method = 1;//1 for greedy ,2 for neighborhood
        public void greedy_search()//贪婪搜索
        {            
            int jmax, position;
            
            double[,] hidijp = new double[Parameter.m, Parameter.p];
            double[] fenpei = new double[Parameter.m];
            double[] min = new double[Parameter.m];
            double[] w = new double[Parameter.p];            
            /*double[] hi = new double[12] { 15, 10, 12, 18, 5, 24, 11, 16, 13, 22, 19, 20 };
            double[,] dij = new double[12, 12]{ { 0, 15, 37, 55, 24, 60, 18, 33, 48, 40, 58, 67 },
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
            for (int i = 0; i < m; i++)
            {
                //计算需求点到各服务设施设置点的参数hidijp
                for (int j = 0; j < p; j++)
                {
                    hidijp[i, j] = hi[i] * dij[i, xj[j]];
                }
                //初始化及用min存储最短距离，用fenpei记录分配方案
                min[i] = hidijp[i, 0];
                fenpei[i] = xj[0];
                for (int j = 0; j < p; j++)
                {
                    if (hidijp[i, j] < min[i])
                    {
                        min[i] = hidijp[i, j];
                        fenpei[i] = xj[j];
                    }
                }
            }
            for (int t = 1; t <= r; t++)
            {
                //计算每个服务设施设置点的加权
                for (int i = 0; i < p - t; i++)
                {
                    w[i] = 0;
                    for (int j = 0; j < m; j++)
                    {
                        if (fenpei[j] == xj[i])
                        {
                            w[i] = w[i] + min[j];
                        }
                    }
                }
                //找出最大的wj
                max = w[0];
                position = 0;
                jmax = xj[0];
                for (int i = 0; i < p - t + 1; i++)
                {
                    if (w[i] > max)
                    {
                        max = w[i];
                        jmax = xj[i];
                        position = i;
                    }
                }
                //对解集s和选址范围进行重置
                s[t - 1] = jmax;
                for (int i = position; i < p - t; i++)
                {
                    xj[i] = xj[i + 1];
                }
                f1(xj, hi, dij, t, p, m);
            }
            //输出s[i]            
            if(method>1)
                neighborhood_search(s,xj);
        }
        public void neighborhood_search(int[] sood,int[] f)//邻域搜索 
        {
            double wfi = f2(f, hi, dij, p, r, m),wffi;
            //交换，比较并复原
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < p - r; j++)
                {
                    int k;
                    k = s[i];
                    s[i] = f[j];
                    f[j] = k;
                    wffi = f2(f, hi, dij, p, r, m);
                    if (wffi > wfi)
                    {
                        wfi = wffi;
                        for (k = 0; k < r; k++)
                            sgood[k] = s[k];
                    }
                }
            }
            /*输出结果
            for (i = 0; i < r; i++)
                Console.WriteLine(sgood[i]);
            Console.WriteLine(wfi);
            Console.ReadLine();
            */
            if(method>2)
                tabu_search(sgood,xj);
        }
        public void tabu_search(int[] ssgood,int[] f)//未完成的禁忌搜索 
        {
            int[] ssnow = new int[Parameter.p];
            int[,] xxx = new int[10, 5];
            double wssgood = f2(f, hi, dij, p, r, m);
            for (int i = 0; i < r; i++)
                ssnow[i]=s[i];
            double wsmax= 0;
            int u=0;
            int k = 0;
            double wffi;
            for (int t = 0; u < m + p; t++)
            {
                //生成候选解并找到最好的及记录权重
                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < p - r; j++)
                    {
                        k = s[i];
                        s[i] = f[j];
                        f[j] = k;
                        wffi = f2(f, hi, dij, p, r, m);
                        if (wffi > wsmax)
                        {
                            wsmax = wffi;
                            ssnow = s;
                        }
                        k = s[i];
                        s[i] = f[j];
                        f[j] = k;
                    }
                }
                Array.Sort(ssnow);
                int value = f3(ssnow,xxx,k,r);
                if (value == 0)
                {
                    k++;
                    for (int i = 0; i < r; i++)
                        xxx[k, i] = ssnow[i];
                        if (wsmax > wssgood)
                        {
                            u = 0;
                            s = ssnow;
                            ssgood = ssnow;
                            wssgood = wsmax;
                        }
                        else u++;
                }
                if (value == 1)
                {
                    if (wsmax > wssgood)
                    {
                        u = 0;
                        s = ssnow;
                        ssgood = ssnow;
                        wssgood = wsmax;
                    }
                    else u++;
                }
            }
            /*
            for (i = 0; i < r; i++)
                Console.WriteLine(ssgood[i]);哪几个设施中断
            Console.WriteLine(wssgood);中断造成的损失
            Console.ReadLine();        
             */
        }
    }
}