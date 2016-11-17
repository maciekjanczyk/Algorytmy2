using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserWyrazenLogicznych
{
    public static class ParserWL
    {
        public static List<Tuple<string, string>> CNF2(string expr, char andChar = '&', char orChar = '|')
        {
            List<Tuple<string, string>> ret = new List<Tuple<string, string>>();

            expr = expr.Replace(" ", string.Empty);
            List<string> pary = expr.Split(andChar).ToList();

            foreach (string para in pary)
            {
                string tmp_para = para.Replace("(", string.Empty).Replace(")", string.Empty);
                var wyrazenia = tmp_para.Split(orChar);
                ret.Add(Tuple.Create(wyrazenia[0], wyrazenia[1]));
            }

            return ret;
        }
    }
}
