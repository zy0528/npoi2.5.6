using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
    public class DatedIf : Fixed3ArgFunction
    {
        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
        {
            double result;
            try
            {
                DateTime sTime = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg0);
                DateTime eTime = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg1);
                string s0 = TextFunction.EvaluateStringArg(arg2, srcRowIndex, srcColumnIndex);
                result = Evaluate(sTime, eTime, s0);
            }
            catch (EvaluationException e)
            {
                return e.GetErrorEval();
            }
            return new NumberEval(result);
        }
        private double Evaluate(DateTime startingDate, DateTime endingDate, string s0)
        {
            TimeSpan timeSpan = endingDate - startingDate;
            s0 = s0.Trim();
            if ("y".Equals(s0) || "Y".Equals(s0))
            {
                int year = endingDate.Year - startingDate.Year;
                if (year > 0)
                {
                    if (endingDate.Month < startingDate.Month)
                    {
                        year--;
                    }
                    else if (startingDate.Month == endingDate.Month)
                    {
                        if (endingDate.Day < startingDate.Day)
                        {
                            year--;
                        }
                    }
                }
                if (year < 0)
                {
                    if (endingDate.Month > startingDate.Month)
                    {
                        year++;
                    }
                    if (endingDate.Month == startingDate.Month)
                    {
                        if (endingDate.Day > startingDate.Day)
                        {
                            year++;
                        }
                    }
                }
                return year;
            }
            if ("M".Equals(s0))
            {

                int month = (endingDate.Year - startingDate.Year) * 12 + (endingDate.Month - startingDate.Month);
                if (endingDate.Month < startingDate.Month && endingDate.Day > startingDate.Day)
                {
                    month++;
                }
                if (endingDate.Month > startingDate.Month && endingDate.Day < startingDate.Day)
                {
                    month--;
                }
                return month;
            }
            if ("w".Equals(s0) || "W".Equals(s0))
            {
                return timeSpan.TotalDays / 7;
            }
            if ("d".Equals(s0) || "D".Equals(s0))
            {
                return timeSpan.TotalDays;
            }
            if ("h".Equals(s0) || "H".Equals(s0))
            {
                return timeSpan.TotalMinutes / 60;
            }
            if ("m".Equals(s0))
            {
                return timeSpan.TotalMinutes;
            }
            if ("s".Equals(s0) || "S".Equals(s0))
            {
                return timeSpan.TotalSeconds;
            }
            return -1;
        }
    }
}
