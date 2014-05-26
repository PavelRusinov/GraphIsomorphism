using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;

namespace GraphIsomorphism
{
    class CandidateFinder
    {
        public CandidateFinder(BidirectionalGraph<int, Edge<int>> g1, BidirectionalGraph<int, Edge<int>> g2, List<Pair<int, int>> map)
        {
            this.g1 = g1;
            this.g2 = g2;
            this.map = map;
            t1_out = new List<int>();
            t2_out = new List<int>();
            t1_in = new List<int>();
            t2_in = new List<int>();
            pd_1 = new List<int>();
            pd_2 = new List<int>();
            findCandidates();
        }
        
        private BidirectionalGraph<int, Edge<int>> g1;
        private BidirectionalGraph<int, Edge<int>> g2;

        public List<int> t1_out { get; set; }
        public List<int> t2_out { get; set; }
        public List<int> t1_in { get; set; }
        public List<int> t2_in { get; set; }
        public List<int> pd_1 { get; set; }
        public List<int> pd_2 { get; set; }

        public List<Pair<int, int>> candidates { get; set; }
        private List<Pair<int, int>> map;

        private void getSets(List<int> n, List<int> l_out, List<int> l_in, List<int> pd, BidirectionalGraph<int, Edge<int>> g)
        {
            List<int> vert = new List<int>(g.Vertices.ToList<int>());
            
            foreach (int v in n)
            {
                vert.Remove(v);
                IEnumerable<Edge<int>> eout = g.OutEdges(v);
                foreach (Edge<int> e in eout)
                {
                    if (!n.Contains(e.Target) && !l_out.Contains(e.Target))
                    {
                        l_out.Add(e.Target);
                        vert.Remove(e.Target);
                    }
                }

                IEnumerable<Edge<int>> ein = g.InEdges(v);
                foreach (Edge<int> e in ein)
                {
                    if (!n.Contains(e.Source) && !l_in.Contains(e.Source))
                    {
                        l_in.Add(e.Source);
                        vert.Remove(e.Source);
                    }
                }
            }

            pd.AddRange(vert);
        }

        private void getPairs(List<int> v1, List<int> v2)
        {
            foreach (int e1 in v1)
            {
                foreach (int e2 in v2)
                {
                    candidates.Add(new Pair<int, int>(e1, e2));
                }
            }
        }

        private void findCandidates()
        {
            candidates = new List<Pair<int, int>>();

            List<int> n1 = new List<int>();
            List<int> n2 = new List<int>();

            foreach (Pair<int, int> p in map)
            {
                n1.Add(p.First);
                n2.Add(p.Second);
            }

            getSets(n1, t1_out, t1_in, pd_1, g1);
            getSets(n2, t2_out, t2_in, pd_2, g2);

            getPairs(t1_out, t2_out);
            getPairs(t1_in, t2_in);
            getPairs(pd_1, pd_2);
        }
    }

}
