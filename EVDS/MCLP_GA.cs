using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MCLP;
namespace MCLP_genetic_algorithm
{
    #region 染色体类
    public class GAChromosome : List<int>
    {
        public void FitnessCalculate() { }
        public void FitnessCalculate(double[] hi,double[,] dij,int m,int n,int[] parent,double r)
        {
            int[,] fugaibiao=new int[m,n];
            for(int i=0;i<n;i++)
            {
                if(parent[i]==1)
                {
                    for(int j=0;j<m;j++)
                    {
                        if(dij[j,i]<=r)
                        {
                            fugaibiao[j,i]=1;
                        }
                    }
                }
            }
            double Fitness=0.0;
            for(int i=0;i<m;i++)
            {
                for(int j=0;j<n;j++)
                {
                    if(fugaibiao[i,j]==1)
                    {
                        Fitness=Fitness+hi[i];
                        continue;
                    }
                }
            }
        }
        public double Fitness;
    }
    #endregion
    #region 遗传算法类
    public class GA
    {
        #region 变量定义
        int n, p;
        public GA()
        {
            n = Parameter.n;
            p = Parameter.p;
        }
        public int PopulationSize;
        private void PopulationSizeCalculate()
        {
            int d = (int)Math.Round((double)n / p);
            double s = 1;
            int i, j;
            for (i = n; i >= n - p; i--)
            {
                s = s * i;
            }
            for (j = p; j >= 1; j--)
            {
                s = s / j;
            }
            this.PopulationSize = Math.Max(2, (int)Math.Round(n / 100 * (Math.Log(s) / d))) * d;
        }
        public enum SelectionType
        {
            Tournment,
            Roullette,
        };
        public SelectionType Selection = SelectionType.Roullette;
        public double MutationRate;//变异率
        public double CrossRate;//交叉率
        public System.Random m_Random = new System.Random();
        public List<GAChromosome> m_thisGeneration = new List<GAChromosome>();//这一代种群
        public List<GAChromosome> m_nextGeneration = new List<GAChromosome>();//下一代种群
        public Boolean ApplyElitism = false;
        public int GenerationNum = 0;//迭代次数
        public int Crosstype = 1;
        public System.Collections.ArrayList TotalFitness = new System.Collections.ArrayList();
        #endregion
        #region 方法定义
        public void Initialize()
        {
            PopulationSizeCalculate();
            if (PopulationSize >= 50) ApplyElitism = true;
            for (int i = 0; i < PopulationSize; i++)//PopulationSize为染色体数    
            {
                GAChromosome newParent = new GAChromosome();
                this.Initializer(ref newParent);
                newParent.FitnessCalculate();
                m_thisGeneration.Add(newParent);
            }
            TotalFitness.Clear();
            RankPopulation(m_thisGeneration);
            TotalFitnessRecord();
        }//初始化种群
        public class GAComparer : IComparer<GAChromosome>
        {
            public int Compare(GAChromosome x, GAChromosome y)
            {
                if (x.Fitness > y.Fitness) return -1;
                else if (x.Fitness == y.Fitness) return 0;
                else return 1;
            }
        }//染色体比较器
        public void RankPopulation(List<GAChromosome> GASet)
        {
            GAComparer GAComp = new GAComparer();
            GASet.Sort(GAComp);
        }//对染色体按适应度降序排序
        public void CreateNextGeneration()
        {
            m_nextGeneration.Clear();
            GAChromosome bestChromo = null;
            if (this.ApplyElitism) //最优秀适应度最好是否直接选择加入到新一代群体
            {
                bestChromo = m_thisGeneration[0];
            }//取出最优染色体(已排序)
            for (int i = 0; i < this.PopulationSize; i += 2)
            {
                //Step1选择
                int iDadParent = 0;
                int iMumParent = 0;
                if (this.Selection == SelectionType.Tournment)//竞争法
                {
                    iDadParent = TournamentSelection();
                    iMumParent = TournamentSelection();
                }
                else if (this.Selection == SelectionType.Roullette)//轮赌法
                {
                    iDadParent = RoulletteSelection();
                    iMumParent = RoulletteSelection();
                }
                GAChromosome Dad = (GAChromosome)m_thisGeneration[iDadParent];
                GAChromosome Mum = (GAChromosome)m_thisGeneration[iMumParent];
                GAChromosome child1 = new GAChromosome();
                GAChromosome child2 = new GAChromosome();
                //Step2交叉
                if (m_Random.NextDouble() < this.CrossRate)
                {
                    CrossOver(Dad, Mum, ref child1, ref child2);
                }
                else
                {
                    child1 = Dad;
                    child2 = Mum;
                }
                //Step3变异
                if (m_Random.NextDouble() < this.MutationRate)
                {
                    this.Mutation(ref child1);
                    this.Mutation(ref child2);
                }
                //Step4计算适应度
                child1.FitnessCalculate();
                child2.FitnessCalculate();
                m_nextGeneration.Add(Dad);
                m_nextGeneration.Add(Mum);
                m_nextGeneration.Add(child1);
                m_nextGeneration.Add(child2);
            }
            if (null != bestChromo)
                m_nextGeneration.Add(bestChromo);//最优的染色体插入子代群体中
            RankPopulation(m_nextGeneration);//按照适应度对染色体排序
            m_thisGeneration.Clear();//用新一代替换当前群体
            for (int j = 0; j < this.PopulationSize; j++)
            {
                m_thisGeneration.Add(m_nextGeneration[j]);
            }
            TotalFitnessRecord();
            this.GenerationNum++;//进化代计数
        }//产生下一代种群
        private int RoulletteSelection()
        {            
            double randomFitness = m_Random.NextDouble() * (double)TotalFitness[GenerationNum];
            //在当代群体中找到适应度与randomFitness相接近的染色体
            int index = -1;
            int i = 0;
            double sumofpart = 0;
            while (i < PopulationSize)
            {
                sumofpart = sumofpart + this.m_thisGeneration[i].Fitness;
                if (randomFitness <= sumofpart)
                {
                    index = i;
                    break;
                }
                i++;
            }
            if (index == -1)
            {
                index = PopulationSize - 1;
            }
            return index;
        }//轮赌法
        private int TournamentSelection()
        {
            int Count = 1;
            if (this.PopulationSize >= 50)
                Count = 8;
            else if (this.PopulationSize >= 30)
                Count = 6;
            else if (this.PopulationSize >= 10)
                Count = 3;
            else if (this.PopulationSize >= 2)
                Count = 2;
            int finalindex = 0;
            double dMaxFit = 0;
            for (int i = 0; i < Count; i++)
            {
                int sellndex = m_Random.Next(0, this.PopulationSize);
                double fitness = m_thisGeneration[sellndex].Fitness;
                if (fitness > dMaxFit)
                {
                    finalindex = i;
                    dMaxFit = fitness;
                }
            }
            return finalindex;

        }//竞赛法
        private void TotalFitnessRecord() 
        {
            double m_dTotalFitness = 0;
            for (int j = 0; j < PopulationSize; j++)
            {
                m_dTotalFitness += m_thisGeneration[j].Fitness;
            }
            TotalFitness.Add(m_dTotalFitness);
        }
        public GAChromosome GetBestChromosome()
        {
            GAChromosome bestChromosome = m_thisGeneration[0];
            return bestChromosome;
        }//获得当前种群的最优适应度
        public void Initializer(ref GAChromosome GAChro)
        {
            int flag1 = 0;
            int flag2 = 1;
            double flag3;
            while (flag2 == 1)
            {
                for (int i = 0; i < n; i++)
                {
                    flag3 = m_Random.NextDouble();
                    if (flag3 > 0.5 && flag1 != p)
                    {
                        GAChro.Add(1);
                        flag1++;
                    }
                    else GAChro.Add(0);

                }
                if (flag1 == p) flag2 = 0;
                else
                {
                    GAChro.Clear();
                    flag1 = 0;
                }
            }
        }//染色体初始化
        public void Initializer1(ref GAChromosome GAChro) 
        {
            List<int> ones=new List<int>();
            ones.Clear();            
            while (ones.Count<p)
            {
                int temp=m_Random.Next(0, n);
                bool flag = true;
                for (int i = 0; i < ones.Count; i++)
                {
                    if (temp == ones[i]) 
                    {
                        flag = false;
                        continue;
                    }
                }
                if (flag) ones.Add(temp);
            }
            GAChro.Clear();
            for(int i=0;i<n;i++)
            {
                bool flag = true;
                for(int j=0;j<ones.Count;j++)
                {
                    if (ones[j] == i) 
                    {
                        GAChro.Add(1);
                        ones.Remove(i);
                        flag = false;
                    }
                }
                if (flag)
                    GAChro.Add(0);
            }
        }
        public void CrossOver(GAChromosome Dad, GAChromosome Mum, ref GAChromosome child1, ref GAChromosome child2)
        {   
            int countDad = 0, countMum = 0;
            List<int> crosspoint = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (Dad[i] == 1) countDad++;
                if (Mum[i] == 1) countMum++;
                if (countDad == countMum) { crosspoint.Add(i); };
            }
            switch (Crosstype = 1)
            {
                //一点交叉
                case 1:
                    {
                        int count = crosspoint.Count();
                        int j = ((int)m_Random.Next(0, count));
                        int index = (int)crosspoint[j];
                        for (int k = 0; k < n; k++)
                        {
                            if (k <= index)
                            {
                                child1.Add(Dad[k]);
                                child2.Add(Mum[k]);
                            }
                            else
                            {
                                child1.Add(Mum[k]);
                                child2.Add(Dad[k]);
                            }
                        }
                        break;
                    }
                //多点交叉
                case 2:
                    {
                        for (int i = 0; i < crosspoint.Count; i++)
                        {
                            if (m_Random.NextDouble() > 0.5)
                            {
                                for (int k = crosspoint[i]; k < crosspoint[i + 1]; k++)
                                {
                                    child1.Add(Dad[k]);
                                    child2.Add(Mum[k]);
                                }
                            }
                            else
                                for (int k = crosspoint[i]; k < crosspoint[i + 1]; k++)
                                {
                                child1.Add(Mum[k]);
                                child2.Add(Dad[k]);
                                }
                        }
                        break;
                    }
            }
        }//交叉
        public void Mutation(ref GAChromosome chromose)
        {
            int index = ((int)m_Random.Next(0,n));
            int count = ((int)m_Random.Next(0,n - index));
            chromose.Reverse(index, count);
        }//变异
        #endregion
    }
    #endregion
}
    