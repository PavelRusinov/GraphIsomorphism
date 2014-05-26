using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;
using System.Diagnostics;

namespace GraphIsomorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int vert_num = 100; vert_num < 600; vert_num += 100)
            {
                Random rnd = new Random();
                int average_time = 0;
                for (int count = 0; count < 5; count++)
                {
                    BidirectionalGraph<int, Edge<int>> g1 = new BidirectionalGraph<int, Edge<int>>();
                    BidirectionalGraph<int, Edge<int>> g2 = new BidirectionalGraph<int, Edge<int>>();
                    List<Pair<int, int>> map = new List<Pair<int, int>>();

                    for (int i = 0; i < vert_num; i++)
                    {
                        g1.AddVertex(i);
                        g2.AddVertex(i);
                    }

                    for (int i = 0; i < vert_num; i++)
                    {
                        int numb = rnd.Next(12, 20);
                        for (int j = 0; j < numb; j++)
                        {
                            int end = rnd.Next(0, vert_num-1);
                            if (end != i)
                            {
                                Edge<int> e1 = new Edge<int>(i, end);
                                Edge<int> e2 = new Edge<int>(i, end);
                                g1.AddEdge(e1);
                                g2.AddEdge(e2);
                            }
                        }
                    }

                    GraphComparer gp = new GraphComparer(g1, g2);

                    var watch = Stopwatch.StartNew();
                    map = gp.match(map);
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    average_time += (int)elapsedMs;

                    bool res = true;
                    Pair<int, int>[] arr_map = map.ToArray<Pair<int, int>>();
                    for (int i = 0; i < arr_map.Length - 1; i++)
                    {
                        for (int j = i + 1; j < arr_map.Length; j++)
                        {
                            if (arr_map[i].First == arr_map[j].First
                                || arr_map[i].Second == arr_map[j].Second)
                            {
                                res = false;
                                break;
                            }
                        }
                        if (!res) break;
                    }
                    if (res) { System.Console.WriteLine(elapsedMs); } else { System.Console.WriteLine("fail"); }
                }

                System.Console.Write(vert_num); System.Console.Write(" average: "); System.Console.WriteLine(average_time / 5);
            }
            System.Threading.Thread.Sleep(500000);
            

        }
    }
}
