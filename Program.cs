using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Упражнение_21
{
    internal class Program
    {
        static void Planting(Site s, Worker w, int option)
        {
            switch (option) 
            {
                case 0:
                    for (int i = 0; i < s.Width; i++)
                    {
                        for (int j = 0; j < s.Length; j++)
                        {
                            if (s[i, j].Role.Name == "NoName") 
                            {
                                s[i, j].SetWorker(w);
                                Thread.Sleep(1);
                            }
                        }
                    }
                    break;
                default:
                    for (int i = s.Length - 1; i >= 0; i--)
                    {
                        for (int j = s.Width - 1; j >= 0; j--)
                        {
                            if (s[j, i].Role.Name == "NoName") 
                            {
                                s[j, i].SetWorker(w);
                                Thread.Sleep(1);
                            }
                        }
                    }
                    break;
            }
        }
        static void Main(string[] args)
        {
            
            Worker designer = new Worker(); 
            Worker worker1 = new Worker(ConsoleColor.Green, "Володя"); 
            Worker worker2 = new Worker(ConsoleColor.Red, "Валера"); 

            
            Site s = new Site(10, 10, designer); 
            s.AddPlannedPlant(100); 

            
            Thread myThread = new Thread(() => Planting(s, worker1, 0));
            myThread.Start();
            Planting(s, worker2, 1);

            
            s.ShowMap();

            
            Console.WriteLine("\nНажмите любую кнопку");
            Console.ReadKey();
        }
    }

    public class Site
    {
        
        public Tree[,] Map { get; private set; }
        
        public Tree this[int lIndex, int wIndex]
        {
            get => Map[lIndex, wIndex];
            set => Map[lIndex, wIndex] = value;
        }
        
        public int Length { get; }
        public int Width { get; }
        
        public Site(int l, int w, Worker wr)
        {
            Length = l;
            Width = w;
            Map = new Tree[w, l];
            
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Map[i, j] = new Tree(wr);
                }
            }
        }
        
        public void AddPlannedPlant(int plantRate)
        {
            Random random = new Random();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Map[i, j].Symbol = ((int)Math.Round(random.NextDouble() + plantRate / 100)).ToString();
                }
            }
        }
        
        public void ShowMap()
        {
            Console.Clear();
            Dictionary<Worker, int> planted = new Dictionary<Worker, int>(); 
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (!planted.ContainsKey(Map[i, j].Role)) 
                    {
                        planted.Add(Map[i, j].Role, 1);
                    }
                    else
                    {
                        planted[Map[i, j].Role]++;
                    }
                    Console.ForegroundColor = Map[i, j].Role.Color; 
                    Console.Write(Map[i, j].Symbol + "  ");
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            foreach (KeyValuePair<Worker, int> item in planted) 
            {
                Console.WriteLine($"{item.Key.Name}[{item.Key.Color}] plant {item.Value} trees");
            }
        }
    }
        
    public class Tree : Object
    {
        public string Symbol { get; set; } 
        public Worker Role { get; set; } 
        public Tree(Worker w) 
        {
            Role = w;
            Symbol = "0";
        }
        
        public void SetWorker(Worker w)
        {
            if (Symbol != "0")
            {
                Role = w;
            }
        }
        
        public override string ToString()
        {
            return Symbol;
        }
    }
        
    public class Worker
    {
        public ConsoleColor Color { get; } 
        public string Name { get; set; }
        
        public Worker() 
        {
            Color = ConsoleColor.Gray;
            Name = "NoName";
        }
        public Worker(ConsoleColor c, string n) 
        {
            Color = c;
            Name = n;
        }
    }
}
