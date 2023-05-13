using System;
using System.Collections.Generic;
//Радиотехническое устройство состоит из m блоков. Надежность устройства (время наработки на отказ) определяется наименее надежным блоком. Блоки можно заказать на любом из n предприятий. 
//При этом известна надежность каждого блока, изготовленного на каждом предприятии Cij, i = 1..m, j = 1..n.
//Каждое предприятие может специализироваться на производстве блоков только одного типа.
//Распределить заказы между предприятиями так, чтобы надежность собираемых из них устройств была наибольшей
namespace GrishinExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Block a = new Block(3);
            Block b = new Block(4);
            Block c = new Block(1);
            Block d = new Block(2);
            //Block e = new Block(5);
            //Block f = new Block(6);
            List<Block> list = new List<Block>();
            list.Add(a);
            list.Add(b);
            //list.Add(c);
            List<Block> list2 = new List<Block>();
            list2.Add(c);
            list2.Add(d);
            Factory factory1 = new Factory("factory1",list);
            Factory factory2 = new Factory("factory2",list2);
            List<Factory> listFactories = new List<Factory>();
            listFactories.Add(factory1);
            listFactories.Add(factory2);
            Problem pr = new Problem(listFactories);
            pr.Solve();
        }
    }
    class Block
    {
        public int _reliability;
        public Block(int t)
        {
            _reliability = t;
        }
        public int Reliability
        {
            get
            {
                return _reliability;
            }
        }
        public void Show()
        {
            Console.WriteLine(_reliability);
        }
    }
    class Radio
    {
        public List<Factory> _factories;
        public Radio()
        {
            _factories = new List<Factory>();
        }
        public Radio(List<Factory> receipt)
        {
            _factories = receipt;
        }
        public void Add(Factory factory)
        {
            _factories.Add(factory);
        }
        public int Count
        {
            get { return _factories.Count; } 
        }
        public int LowestReliability()
        {
            int lowestReliability=int.MaxValue;
            foreach (Factory factory in _factories)
            {
               if( factory.Reliability() < lowestReliability )
                {
                    lowestReliability = factory.Reliability();
                }
            }
            return lowestReliability;
        }
        public void Show()
        {
            foreach (Factory factory in _factories)
            {
                factory.Show();
            }
        }
    }
    class Factory
    {
        string _name;
        public List<Block> _blocks;
        public Factory(string name,List<Block> blocks)
        {
            _name = name;
            _blocks = blocks;
        }
        public int Reliability()
        {
            int reliability = 0;
            foreach (Block block in _blocks)
            {
                reliability += block.Reliability;
            }
            return reliability;
        }
        public int Count
        {
            get { return _blocks.Count; }
        }
        public void Show()
        {
            Console.WriteLine(_name);
        }
    }
    class Problem
    { 
        List<Factory> _factories;
        Radio _radio;
        public Problem(List<Factory> factories)
        {
            _factories = factories;
        } 
        public void Solve()
        {
            int MaxAccordance=0;
            Radio radioAnswer=new Radio();
            int lowestReliability = int.MinValue;
            List<Factory> currentAnswer = new List<Factory>();
            List<Factory> answer = new List<Factory>();
            List<List<int>> comb = SolveFromInet.GetAllVariants(_factories.Count);
            foreach (var item in comb)
            {
                Radio radio = new Radio();
                foreach (var x in item)
                {
                    radio.Add(_factories[x]); 
                }
                if (radio.LowestReliability()>lowestReliability)
                {
                    radioAnswer._factories = radio._factories.ToList();
                    lowestReliability = radio.LowestReliability();
                }
            }
            radioAnswer.Show();
        }
        
    }
    static class SolveFromInet
    {
        static List<List<int>> comb;
        static bool[] used;

        public static List<List<int>> GetAllVariants(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }
            used = new bool[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                used[i] = false;
            }
            comb = new List<List<int>>();
            List<int> c = new List<int>();
            GetComb(arr, 0, c);
            return comb;           
        }
        static void GetComb(int[] arr, int colindex, List<int> c)
        {

            if (colindex >= arr.Length)
            {
                comb.Add(new List<int>(c));
                return;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    c.Add(arr[i]);
                    GetComb(arr, colindex + 1, c);
                    c.RemoveAt(c.Count - 1);
                    used[i] = false;
                }
            }
        }
    }
}
