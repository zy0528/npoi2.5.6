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
    public class TestDays
    {
        private static DateTime MakeDate(int year, int month, int day)
        {

            //Calendar cal = new GregorianCalendar(year, month-1, day, 0, 0, 0);
            //cal.Set(Calendar.MILLISECOND, 0);
            //return cal.GetTime();
            return new DateTime(year, month, day, 0, 0, 0, 0);
        }
        private static String fmt(DateTime d)
        {
            //Calendar c = new GregorianCalendar();
            //c.SetTimeInMillis(d.GetTime());
            //StringBuilder sb = new StringBuilder();
            //sb.Append(c.Get(Calendar.YEAR));
            //sb.Append("/");
            //sb.Append(c.Get(Calendar.MONTH)+1);
            //sb.Append("/");
            //sb.Append(c.Get(Calendar.DAY_OF_MONTH));
            //return sb.ToString();
            return d.ToString("yyyy/mm/dd");
        }

        [Test]
        public void PreTest()
        {
            DateTime beginTime = new DateTime(2009, 1, 26);
            DateTime endTime = new DateTime(2009, 7, 4);
            double daySpan = (endTime - beginTime).TotalDays;
        }

        [Test]
        public void TestBasic()
        {
            Confirm(120, 2009, 1, 15, 2009, 5, 15);
            Confirm(159, 2009, 1, 26, 2009, 7, 4);

            // same results in leap years
            Confirm(121, 2008, 1, 15, 2008, 5, 15);
            Confirm(158, 2008, 1, 26, 2008, 7, 4);

            // longer time spans
            Confirm(562, 2008, 8, 11, 2010, 3, 3);
            Confirm(916, 2007, 2, 23, 2009, 9, 9);
        }
        private static void Confirm(int expResult, int y1, int m1, int d1, int y2, int m2, int d2)
        {
            Confirm(expResult, MakeDate(y1, m1, d1), MakeDate(y2, m2, d2), false);
            Confirm(-expResult, MakeDate(y2, m2, d2), MakeDate(y1, m1, d1), false);

        }

        private static void Confirm(int expResult, DateTime firstArg, DateTime secondArg, bool method)
        {

            ValueEval ve;
            ve = invokeDays(Convert(firstArg), Convert(secondArg), BoolEval.ValueOf(method));
            if (ve is NumberEval)
            {

                NumberEval numberEval = (NumberEval)ve;
                if (numberEval.NumberValue != expResult)
                {
                    throw new AssertionException(fmt(firstArg) + " " + fmt(secondArg) + " " + method +
                            " wrong result got (" + numberEval.NumberValue
                            + ") but expected (" + expResult + ")");
                }
                //	System.err.println(fmt(firstArg) + " " + fmt(secondArg) + " " + method + " success got (" + expResult + ")");
                return;
            }
            throw new AssertionException("wrong return type (" + ve.GetType().Name + ")");
        }
        private static ValueEval invokeDays(params ValueEval[] args)
        {
            return new Days().Evaluate(args, -1, -1);
        }
        private static NumberEval Convert(DateTime d)
        {
            return new NumberEval(DateUtil.GetExcelDate(d));
        }

    }
}
