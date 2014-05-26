using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;

namespace GraphIsomorphism
{
    class GraphComparer
    {
        BidirectionalGraph<int, Edge<int>> g1;
        BidirectionalGraph<int, Edge<int>> g2;

        public GraphComparer(BidirectionalGraph<int, Edge<int>> g1, BidirectionalGraph<int, Edge<int>> g2)
        {
            this.g1 = g1;
            this.g2 = g2;
        }

        public List<Pair<int, int>> match(List<Pair<int, int>> map)
        {
            if (map.Count == g2.VertexCount)
            {
                return map;
            }

            CandidateFinder cf = new CandidateFinder(g1, g2, map);
            List<Pair<int, int>> candidates = cf.candidates;
            FeasibilityTester ft = new FeasibilityTester(g1, g2, cf);
            foreach (Pair<int, int> p in candidates)
            {
                if (ft.feasible(p, map))
                {
                    List<Pair<int, int>> nmap = new List<Pair<int,int>>(map);
                    nmap.Add(p);
                    List<Pair<int, int>> res = match(nmap);
                    if(res != null) return res;
                }
            }
            
            return null;
        }
    }
}
