using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dispatch
{
    class node 
    {
        public node(int[] arr,int[,] arrr,List<int> b1,List<int> b2)
        {
            for (int i = 0; i < relocation.A; i++) 
            {
                site[i]=arr[i];
            }
            for (int i = 0; i < Parameter.n;i++ )
            {
                for (int j = 0; j < relocation.A; j++)
                {
                    t[i, j] = Parameter.T[i, site[j]];
                    index[i, j] = j;
                    x[i, j] = arrr[i, j];
                }

                for (int j = 0; j < Parameter.m;j++ )
                {
                    t1[i, j] = Parameter.T[i, j];
                    index1[i, j] = j;
                }
            }
            for (int i = 0; i < b1.Count; i++) 
            {
                ban1.Add(b1[i]);
            }
            for (int i = 0; i < b2.Count; i++)
            {
                ban2.Add(b2[i]);
            }
            preparation();
            tree_search();
        }
        double[,] t = new double[Parameter.n, relocation.A];//每次移动后都更新并排序
        int[,] index = new int[Parameter.n, relocation.A];//t的标号，同步排序
        double[,] t1 = new double[Parameter.n, Parameter.m];//在寻找潜在的设施点
        int[,] index1 = new int[Parameter.n, Parameter.m];//t1的标号，同步排序
        double[] p = new double[Parameter.n];//每次移动后更新
        public int[,] x = new int[Parameter.n, relocation.A];//可行解
        public int[] site = new int[relocation.A];//当前分配的设施点的标号
        int sign;
        public List<int> Nk=new List<int>();
        public List<int> Mk=new List<int>();
        public List<int> ban1 = new List<int>();
        public List<int> ban2 = new List<int>();
        private void tree_search()
        {
            if (check_feasibility())
            {
                record_result();
            }
        }
        private void preparation()
        {
            sort_facility();
            for (int i = 0; i < Parameter.n; i++)
            {
                p[i] = 0;
                for (int j = 0; j < relocation.A; j++)
                {
                    if(t[i,j]!=0)
                    p[i] = p[i] + relocation.gama[j] / t[i, j] / Parameter.c[i];
                }
            }
        }
        private void sort_facility()
        {
            for (int i = 0; i < Parameter.n; i++)
            {
                for (int j = 0; j < relocation.A; j++)
                {
                    for (int k = 0; k < relocation.A - j-1; k++)
                    {
                        if (t[i, k] < t[i, k + 1])
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
        }//对t排序
        private void sort_zone() 
        {
            for (int i = 0; i < relocation.A; i++)
            {
                for (int j = 0; j < Parameter.n; j++)
                {
                    for (int k = 0; k < Parameter.n - j-1; k++)
                    {
                        if (t1[k, i] < t1[k + 1,i ])
                        {
                            double temp1;
                            temp1 = t1[k, i];
                            t1[k, i] = t1[k + 1, i];
                            t1[k + 1, i] = temp1;
                            int temp2;
                            temp2 = index1[i, k];
                            index1[i, k] = index1[i, k + 1];
                            index1[i, k + 1] = temp2;
                        }
                    }
                }
            }
        }
        private double find_p_min()
        {
            double temp = double.MaxValue;
            for (int i = 0; i < Parameter.n; i++)
            {
                temp = Math.Min(temp, p[i]);
                sign = i;
            }
            return temp;
        }
        private bool check_feasibility()
        {
            int flag = 0;
            for (int i = 0; i < Parameter.n; i++)
            {
                if (p[i] >= relocation.Pmin) flag++;
            }
            if (flag == Parameter.n) return true;
            else return false;
        }
        private void record_result()
        {
            double max = double.MinValue;
            for (int i = 0; i < Parameter.n; i++)
            {
                for (int j = 0; j < relocation.A; j++)
                {
                    relocation.x1[i, j] = x[i, j];
                    if (x[i, j] == 1) max = Math.Max(max, Parameter.T[i, j]);
                }
            }
            if (max > relocation.z) relocation.z = max;
        }
        public void changesite(int index1,int index2) 
        {
            site[index1] = index2;
            x[index2, index1] = 1; 
        }
        public void find_potential_location(int pool)
        {
            int[] sum = new int[relocation.A];
            bool flag = true;
            for (int i = 0; i < relocation.A; i++)
            {
                for (int j = 0; j < Parameter.n; j++)
                {
                    sum[i] = sum[i] + x[j, i];
                }
                if (!(sum[i] == 1 || sum[i] == 0))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                //对到该设施点的距离排序，取前b个放入Mk
                sort_zone();
                //不应跟已有设施点重复
                for (int i = 0; i < relocation.b; i++)
                {
                    bool flag1 = true;
                    for (int j = 0; j < relocation.A; j++)
                    {
                        if (index1[i, Nk[pool]] == site[j]) flag1 = false;
                    }
                    for (int j = 0; j < ban1.Count; j++) 
                    {
                        if(index1[i,Nk[pool]]==ban1[j])flag1 = false;
                    }                    
                    if (flag1) Mk.Add(index1[i, Nk[pool]]);
                }
            }
        }
        public void find_new_node()
        {
            int s=0;
            for(int i=0;i<relocation.A;i++)
            {
                for(int j=0;j<Parameter.n;j++)
                {
                    s=s+x[j,i];
                }
            }
            if (s < relocation.M)
            {
                find_p_min();
                for (int i = 0; i < relocation.A; i++)
                {
                    if (t[sign, index[sign, i]] < relocation.R && index[sign, i] != sign)
                    {
                        int s_x = 0;
                        bool flag = true;
                        for (int k = 0; k < Parameter.n; k++)
                        {
                            s_x = s_x + x[k, index[sign, i]];
                        }
                        for (int k = 0; k < ban2.Count; k++) 
                        {
                            if (index[sign, i] == ban2[k]) flag = false;
                        }                        
                        if (s_x == 0&&flag)
                        {
                            Nk.Add(index[sign, i]);
                        }
                    }
                }
                while (Nk.Count > relocation.a)
                {
                    Nk.Remove(Nk.Count - 1);
                }
            }
        }
    }
    class relocation
    {
        public static int M = 2, A = 5, a = 3, b = 3;//M可以移动的设施数，A总设施数，a b搜索深度
        public static double[] gama = new double[A];
        public static double R=30, Pmin=0.002;//R是搜索半径，Pmin最小准备度要求
        //public int[] site=new int[A];
        public int[] site = { 0, 5, 7, 9, 11 };
        public static double z;
        public static bool stopflag=false;//为真时停止搜索
        public static int[,] x1 = new int[Parameter.n,A];//最优解
        public List<int> ban1 = new List<int>();
        public List<int> ban2 = new List<int>();
        public relocation()
        {
            ini_gama();
        }
        public void tree_sourch()
        {
            node root = new node(site,x1,ban1,ban2);
            find_solution(ref root);
        }
        private void find_solution(ref node node1)
        {
            if (!stopflag) 
            {
                node1.find_new_node();
                if (node1.Nk.Count != 0)
                {
                    for (int i = 0; i < node1.Nk.Count; i++)
                    {
                        node1.find_potential_location(i);
                        for (int j = 0; j < node1.Mk.Count; j++)
                        { 
                            int[] site2 = new int[A];
                            int[,] x2 =new int[Parameter.n,relocation.A];
                                                            //记录当前
                                for (int ii = 0; ii < relocation.A; ii++)
                                {
                                    site2[ii] = node1.site[ii];
                                    for (int jj = 0; jj < Parameter.n; jj++)
                                    {
                                        x2[jj, ii] = node1.x[jj, ii];
                                    }
                                }
                            if (node1.Mk.Count != 0)
                            {
                                node1.changesite(node1.Nk[i], node1.Mk[j]);
                                node1.ban1.Add(node1.Nk[i]);
                                node1.ban2.Add(node1.Mk[j]);
                                node node2=new node(node1.site, node1.x,ban1,ban2);
                                find_solution(ref node2);
                                //回溯
                                for (int ii = 0; ii < relocation.A; ii++)
                                {
                                    node1.site[ii] = site2[ii];
                                    for (int jj = 0; jj < Parameter.n; jj++)
                                    {
                                        node1.x[jj, ii] = x2[jj, ii];
                                    }
                                }
                                if (node1.ban1.Count != 0) node1.ban1.Remove(node1.Nk[i]);
                                if(node1.ban2.Count != 0) node1.ban2.Remove(node1.Mk[j]);
                            }
                        }
                    }
                }
            }
        }
        private void ini_gama() 
        {
            double s = 1;
            for(int i=0;i<A;i++)
            {
                gama[i] = s;
                s = s / 2;
            }
        }
    }
}
