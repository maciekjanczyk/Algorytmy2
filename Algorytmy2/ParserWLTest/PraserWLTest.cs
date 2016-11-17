using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserWLTest
{
    using ParserWyrazenLogicznych;
    using System.Collections.Generic;

    [TestClass]
    public class ParserWLTest
    {
        [TestMethod]
        public void BezSpacji2CNF()
        {
            string expr = "(x0|x2)&(x0|!x3)&(x1|!x3)&(x1|!x4)&(x2|!x4)&(x0|!x5)&(x1|!x5)&(x2|!x5)&(x3|x6)&(x4|x6)&(x5|x6)";

            List<Tuple<string, string>> wyrlog = ParserWL.CNF2(expr);
            List<Tuple<string, string>> spr = new List<Tuple<string, string>>();
            spr.Add(Tuple.Create("x0", "x2"));
            spr.Add(Tuple.Create("x0", "!x3"));
            spr.Add(Tuple.Create("x1", "!x3"));
            spr.Add(Tuple.Create("x1", "!x4"));
            spr.Add(Tuple.Create("x2", "!x4"));
            spr.Add(Tuple.Create("x0", "!x5"));
            spr.Add(Tuple.Create("x1", "!x5"));
            spr.Add(Tuple.Create("x2", "!x5"));
            spr.Add(Tuple.Create("x3", "x6"));
            spr.Add(Tuple.Create("x4", "x6"));
            spr.Add(Tuple.Create("x5", "x6"));

            for (int i = 0; i < spr.Count; i++)
            {
                Assert.AreEqual(spr[i].Item1, wyrlog[i].Item1);
                Assert.AreEqual(spr[i].Item2, wyrlog[i].Item2);
            }
        }

        [TestMethod]
        public void ZeSpacjami2CNF()
        {
            string expr = "(x0|x2) & (x0|!x3) & (x1|!x3) & (x1|!x4) & (x2|!x4) & (x0|!x5) & (x1|!x5) & (x2|!x5) & (x3|x6) & (x4|x6) & (x5|x6)";

            List<Tuple<string, string>> wyrlog = ParserWL.CNF2(expr);
            List<Tuple<string, string>> spr = new List<Tuple<string, string>>();
            spr.Add(Tuple.Create("x0", "x2"));
            spr.Add(Tuple.Create("x0", "!x3"));
            spr.Add(Tuple.Create("x1", "!x3"));
            spr.Add(Tuple.Create("x1", "!x4"));
            spr.Add(Tuple.Create("x2", "!x4"));
            spr.Add(Tuple.Create("x0", "!x5"));
            spr.Add(Tuple.Create("x1", "!x5"));
            spr.Add(Tuple.Create("x2", "!x5"));
            spr.Add(Tuple.Create("x3", "x6"));
            spr.Add(Tuple.Create("x4", "x6"));
            spr.Add(Tuple.Create("x5", "x6"));

            for (int i = 0; i < spr.Count; i++)
            {
                Assert.AreEqual(spr[i].Item1, wyrlog[i].Item1);
                Assert.AreEqual(spr[i].Item2, wyrlog[i].Item2);
            }
        }
    }
}
