using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;

namespace TestCases.SS.Formula.Functions
{
    [TestFixture]
    public class TestDatedElta
    {
        [Test]
        public void Test3()
        {
            ValueEval datePara = new StringEval("16:18:44");
            ValueEval numPara = new NumberEval(1);
            ValueEval unitPara = new StringEval("H");
            ValueEval formatPara = new StringEval("HH:mm:ss");
            ValueEval val = invokeDatedElta(datePara, numPara, unitPara, formatPara);
        }


        [Test]
        public void Test2()
        {
            ValueEval datePara = new StringEval("2022-11-17");
            ValueEval numPara = new NumberEval(10);
            ValueEval unitPara = new StringEval("y");
            ValueEval formatPara = new StringEval("");
            ValueEval val = invokeDatedElta(datePara, numPara, unitPara, formatPara);
        }


        [Test]
        public void Test1()
        {
            ValueEval datePara = new StringEval("2022-11-17");
            ValueEval numPara = new NumberEval(10);
            ValueEval unitPara = new StringEval("y");
            ValueEval formatPara = new StringEval("yyyy-MM-dd");
            ValueEval val = invokeDatedElta(datePara, numPara, unitPara, formatPara);
        }


        private static NumberEval Convert(DateTime d)
        {
            return new NumberEval(DateUtil.GetExcelDate(d));
        }
        private static ValueEval invokeDatedElta(params ValueEval[] args)
        {
            return new DatedElta().Evaluate(args, -1, -1);
        }


    }
}
