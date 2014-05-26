using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;

namespace GraphIsomorphism
{
    class FeasibilityTester
    {
        public FeasibilityTester(BidirectionalGraph<int, Edge<int>> g1, BidirectionalGraph<int, Edge<int>> g2, CandidateFinder cf)
        {
            this.g1 = g1;
            this.g2 = g2;
            t1_out = cf.t1_out;
            t1_in = cf.t1_in;
            t2_out = cf.t2_out;
            t2_in = cf.t2_in;
            pd_1 = cf.pd_1;
            pd_2 = cf.pd_2;
        }

        private BidirectionalGraph<int, Edge<int>> g1;
        private BidirectionalGraph<int, Edge<int>> g2;
        
        List<int> t1_out { get; set; }
        List<int> t2_out { get; set; }
        List<int> t1_in { get; set; }
        List<int> t2_in { get; set; }
        List<int> pd_1 { get; set; }
        List<int> pd_2 { get; set; }

        List<int> pred(BidirectionalGraph<int, Edge<int>> g, int n)
        {
            IEnumerable<Edge<int>> e_in = g.InEdges(n);
            List<int> res = new List<int>();
            foreach (Edge<int> e in e_in)
            {
                res.Add(e.Source);
            }
            return res;
        }

        List<int> succ(BidirectionalGraph<int, Edge<int>> g, int n)
        {
            IEnumerable<Edge<int>> e_out = g.OutEdges(n);
            List<int> res = new List<int>();
            foreach (Edge<int> e in e_out)
            {
                res.Add(e.Target);
            }
            return res;
        }

        List<int> intersection(List<int> l1, List<int> l2)
        {
            List<int> res = new List<int>();
            foreach (int v1 in l1)
            {
                if (l2.Contains(v1))
                {
                    res.Add(v1);
                }
            }
            return res;
        }

        List<int> mapfst(List<Pair<int, int>> map)
        {
            List<int> res = new List<int>();
            foreach (Pair<int, int> p in map)
            {
                res.Add(p.First);
            }
            return res;
        }

        List<int> mapsnd(List<Pair<int, int>> map)
        {
            List<int> res = new List<int>();
            foreach (Pair<int, int> p in map)
            {
                res.Add(p.Second);
            }
            return res;
        }

        List<int> m_concat(List<int> l1, List<int> l2)
        {
            List<int> res = new List<int>(l1);
            foreach (int v in l2)
            {
                res.Add(v);
            }
            return res;
        }

        List<int> get_N(IEnumerable<int> N, List<int> T, List<int> M)
        {
            List<int> res = new List<int>();
            foreach (int v in N)
            {
                if (!T.Contains(v) && !M.Contains(v))
                {
                    res.Add(v);
                }
            }
            return res;
        }

        bool contains(List<Pair<int, int>> map, Pair<int, int> p)
        {
            bool res = false;
            foreach (Pair<int, int> p1 in map)
            {
                if (p1.First == p.First && p1.Second == p.Second)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        bool hasAllCorrespond(List<int> neighb, List<int> ints, List<Pair<int, int>> map, bool map_dir) 
        {
            bool res = true;
            foreach (int n1 in ints)
            {
                bool h = false;
                foreach (int m1 in neighb)
                {
                    Pair<int, int> m;
                    if (map_dir) { m = new Pair<int, int>(n1, m1); } else { m = new Pair<int, int>(m1, n1); }
                    if (contains(map, m))
                    {
                        h = true;
                        break;
                    }
                }
                if (!h)
                {
                    res = false;
                    break;
                }
            }
            return res;
        }

        private bool r_pred(List<int> predn, List<int> predm, List<int> map_first_el, List<int> map_second_el, List<Pair<int, int>> map)
        {
            List<int> ints_m1_predn = intersection(map_first_el, predn);
            List<int> ints_m2_predm = intersection(map_second_el, predm);
            bool c1 = hasAllCorrespond(predm, ints_m1_predn, map, true);
            bool c2 = true; 
            if (c1) 
            { 
                c2 = hasAllCorrespond(predn, ints_m2_predm, map, false); 
            }
            
            return c1 && c2;
        }

        private bool r_succ(List<int> succn, List<int> succm, List<int> map_first_el, List<int> map_second_el, List<Pair<int, int>> map)
        {
            List<int> ints_m1_succn = intersection(map_first_el, succn);
            List<int> ints_m2_succm = intersection(map_second_el, succm);
            bool c1 = hasAllCorrespond(succm, ints_m1_succn, map, true);
            bool c2 = true; 
            if (c1) 
            { 
                c2 = hasAllCorrespond(succn, ints_m2_succm, map, false); 
            }

            return c1 && c2;
        }

        private bool r_in(List<int> succn, List<int> succm, List<int> predn, List<int> predm)
        {
            List<int> ints_succn_tin = intersection(t1_in, succn);
            List<int> ints_succm_tin = intersection(t2_in, succm);

            List<int> ints_predn_tin = intersection(t1_in, predn);
            List<int> ints_predm_tin = intersection(t2_in, predm);
            return (ints_predm_tin.Count == ints_predn_tin.Count 
                 && ints_succm_tin.Count == ints_succn_tin.Count);
        }

        private bool r_out(List<int> succn, List<int> succm, List<int> predn, List<int> predm)
        {
            List<int> ints_succn_tout = intersection(t1_out, succn);
            List<int> ints_succm_tout = intersection(t2_out, succm);

            List<int> ints_predn_tout = intersection(t1_out, predn);
            List<int> ints_predm_tout = intersection(t2_out, predm);
            return (ints_predm_tout.Count == ints_predn_tout.Count 
                 && ints_succm_tout.Count == ints_succn_tout.Count);
        }

        private bool r_new(List<int> succn, List<int> succm, List<int> predn, List<int> predm
            , List<int> map_first_el, List<int> map_second_el)
        {

            List<int> T1 = m_concat(t1_in, t1_out);
            List<int> T2 = m_concat(t2_in, t2_out);

            List<int> N1 = get_N(g1.Vertices, T1, map_first_el);
            List<int> N2 = get_N(g2.Vertices, T2, map_second_el);

            List<int> ints_succn_N1 = intersection(N1, succn);
            List<int> ints_succm_N2 = intersection(N2, succm);

            List<int> ints_predn_N1 = intersection(N1, predn);
            List<int> ints_predm_N2 = intersection(N2, predm);
            return (ints_predm_N2.Count == ints_predn_N1.Count 
                 && ints_succm_N2.Count == ints_succn_N1.Count);
        }

        public bool feasible(Pair<int, int> p, List<Pair<int, int>> map)
        {
            int n = p.First;
            int m = p.Second;
            List<int> succn = succ(g1, n);
            List<int> succm = succ(g2, m);
            List<int> predn = pred(g1, n);
            List<int> predm = pred(g2, m);

            List<int> map_first_el = mapfst(map);
            List<int> map_second_el = mapsnd(map);

            return r_pred(predn, predm, map_first_el, map_second_el, map)
                && r_succ(succn, succm, map_first_el, map_second_el, map)
                && r_in(succn, succm, predn, predm)
                && r_out(succn, succm, predn, predm) 
                && r_new(succn, succm, predn, predm, map_first_el, map_second_el);
        }
    }
}
