using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QuickGraph;
using QuickGraph.Algorithms;

namespace GraphIsomorphism
{
    [TestFixture]
    class GraphComparerTest
    {
        [Test]
        public void isomorphicEmpty()
        {
            BidirectionalGraph<int, Edge<int>> g1 = new BidirectionalGraph<int, Edge<int>>();
            BidirectionalGraph<int, Edge<int>> g2 = new BidirectionalGraph<int, Edge<int>>();
            List<Pair<int, int>> map = new List<Pair<int, int>>();

            GraphComparer gp = new GraphComparer(g1, g2);
            map = gp.match(map);
            Assert.IsTrue(map != null && map.Count == 0);
        }

        [Test]
        public void isomorphicVertex()
        {
            BidirectionalGraph<int, Edge<int>> g1 = new BidirectionalGraph<int, Edge<int>>();
            BidirectionalGraph<int, Edge<int>> g2 = new BidirectionalGraph<int, Edge<int>>();
            List<Pair<int, int>> map = new List<Pair<int, int>>();

            g1.AddVertex(1);
            g1.AddVertex(2);
            g1.AddVertex(3);
            g1.AddVertex(4);

            g2.AddVertex(5);
            g2.AddVertex(6);
            g2.AddVertex(7);
            g2.AddVertex(8);

            GraphComparer gp = new GraphComparer(g1, g2);
            map = gp.match(map);

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

            Assert.IsTrue(map != null && res);
        }
        
        [Test]
        public void isomorphic()
        {
            BidirectionalGraph<int, Edge<int>> g1 = new BidirectionalGraph<int,Edge<int>>();
            BidirectionalGraph<int, Edge<int>> g2 = new BidirectionalGraph<int,Edge<int>>();
            List<Pair<int, int>> map = new List<Pair<int, int>>();

            g1.AddVertex(1);
            g1.AddVertex(2);
            g1.AddVertex(3);
            g1.AddVertex(4);

            g2.AddVertex(5);
            g2.AddVertex(6);
            g2.AddVertex(7);
            g2.AddVertex(8);

            Edge<int> e_1_2 = new Edge<int>(1, 2);
            Edge<int> e_2_3 = new Edge<int>(2, 3);
            Edge<int> e_3_4 = new Edge<int>(3, 4);
            Edge<int> e_4_1 = new Edge<int>(4, 1);

            Edge<int> e_5_6 = new Edge<int>(5, 6);
            Edge<int> e_6_7 = new Edge<int>(6, 7);
            Edge<int> e_7_8 = new Edge<int>(7, 8);
            Edge<int> e_8_5 = new Edge<int>(8, 5);

            g1.AddEdge(e_1_2);
            g1.AddEdge(e_2_3);
            g1.AddEdge(e_3_4);
            g1.AddEdge(e_4_1);

            g2.AddEdge(e_5_6);
            g2.AddEdge(e_6_7);
            g2.AddEdge(e_7_8);
            g2.AddEdge(e_8_5);
            

            GraphComparer gp = new GraphComparer(g1, g2);
            map = gp.match(map);

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

            Assert.IsTrue(map != null && res);
        }

        [Test]
        public void notIsomorphic()
        {
            BidirectionalGraph<int, Edge<int>> g1 = new BidirectionalGraph<int, Edge<int>>();
            BidirectionalGraph<int, Edge<int>> g2 = new BidirectionalGraph<int, Edge<int>>();
            List<Pair<int, int>> map = new List<Pair<int, int>>();

            g1.AddVertex(1);
            g1.AddVertex(2);
            g1.AddVertex(3);
            g1.AddVertex(4);

            g2.AddVertex(5);
            g2.AddVertex(6);
            g2.AddVertex(7);
            g2.AddVertex(8);

            Edge<int> e_1_2 = new Edge<int>(1, 2);
            Edge<int> e_2_3 = new Edge<int>(2, 3);
            Edge<int> e_3_4 = new Edge<int>(3, 4);
            Edge<int> e_4_1 = new Edge<int>(4, 1);

            Edge<int> e_5_6 = new Edge<int>(5, 6);
            Edge<int> e_6_7 = new Edge<int>(6, 7);
            Edge<int> e_7_8 = new Edge<int>(7, 8);
            Edge<int> e_5_8 = new Edge<int>(5, 8);

            g1.AddEdge(e_1_2);
            g1.AddEdge(e_2_3);
            g1.AddEdge(e_3_4);
            g1.AddEdge(e_4_1);

            g2.AddEdge(e_5_6);
            g2.AddEdge(e_6_7);
            g2.AddEdge(e_7_8);
            g2.AddEdge(e_5_8);


            GraphComparer gp = new GraphComparer(g1, g2);
            map = gp.match(map);
            Assert.IsTrue(map == null);
        }

        [Test]
        public void isomorphicBig()
        {
            BidirectionalGraph<int, Edge<int>> g1 = new BidirectionalGraph<int, Edge<int>>();
            BidirectionalGraph<int, Edge<int>> g2 = new BidirectionalGraph<int, Edge<int>>();
            List<Pair<int, int>> map = new List<Pair<int, int>>();

            for (int i = 0; i < 150; i++)
            {
                g1.AddVertex(i);
                g2.AddVertex(i);
            }

            for (int i = 0; i < 150; i++)
            {
                for (int j = 0; j < 150; j++)
                {
                    Edge<int> e1 = new Edge<int>(i, j);
                    Edge<int> e2 = new Edge<int>(i, j);
                    g1.AddEdge(e1);
                    g2.AddEdge(e2);
                }
            }

            GraphComparer gp = new GraphComparer(g1, g2);
            map = gp.match(map);

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

            Assert.IsTrue(map != null && res);
        }

        [Test]
        public void notIsomorphic2()
        {
            BidirectionalGraph<int, Edge<int>> g1 = new BidirectionalGraph<int, Edge<int>>();
            BidirectionalGraph<int, Edge<int>> g2 = new BidirectionalGraph<int, Edge<int>>();
            List<Pair<int, int>> map = new List<Pair<int, int>>();

            for (int i = 0; i < 5; i++)
            {
                g1.AddVertex(i);
                g2.AddVertex(i);
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Edge<int> e1 = new Edge<int>(i, j);
                    Edge<int> e2 = new Edge<int>(i, j);
                    g1.AddEdge(e1);
                    g2.AddEdge(e2);
                }
            }

            g1.AddEdge(new Edge<int>(4, 1));
            g2.AddEdge(new Edge<int>(1, 4));

            GraphComparer gp = new GraphComparer(g1, g2);
            map = gp.match(map);
            Assert.IsTrue(map == null);
        }
    }
}
