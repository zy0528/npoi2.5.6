using System;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.Util;

namespace NPOI.SS.Formula.Functions
{
    public class Days360 : Var2or3ArgFunction
    {
        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
        {
            double result;
            try
            {
                DateTime startingDate = GetStartingDate(TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg0));
                DateTime endDate = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg1);
                result = Evaluate(startingDate, endDate);
            }
            catch (EvaluationException e)
            {
                return e.GetErrorEval();
            }
            return new NumberEval(result);
        }

        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
        {
            double result;
            try
            {
                DateTime startingDate = GetStartingDate(TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg0));
                DateTime endDate = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg1);
                ValueEval ve = OperandResolver.GetSingleValue(arg2, srcRowIndex, srcColumnIndex);
                bool? method = OperandResolver.CoerceValueToBoolean(ve, false);
                result = Evaluate(startingDate, endDate);
            }
            catch (EvaluationException e)
            {
                return e.GetErrorEval();
            }
            return new NumberEval(result);
        }
        private double Evaluate(DateTime startingDate, DateTime endingDatePara)
        {
            DateTime endingDate = GetEndingDateAccordingToStartingDate(endingDatePara, startingDate);
            long startingDay = startingDate.Month * 30 + startingDate.Day;
            long endingDay = (endingDate.Year - startingDate.Year) * 360
                    + endingDate.Month * 30 + endingDate.Day;
            return endingDay - startingDay;
        }
        private DateTime GetDate(double date)
        {
            return DateUtil.GetJavaDate(date);
        }
        private DateTime GetStartingDate(DateTime startingDate)
        {
            if (IsLastDayOfMonth(startingDate))
            {
                startingDate = new DateTime(startingDate.Year, startingDate.Month, 30, startingDate.Hour, startingDate.Minute, startingDate.Second);
            }
            return startingDate;
        }
        private DateTime GetEndingDateAccordingToStartingDate(DateTime endingDate, DateTime startingDate)
        {
            if (IsLastDayOfMonth(endingDate))
            {
                if (startingDate.Day < 30)
                {
                    endingDate = GetFirstDayOfNextMonth(endingDate);
                }
            }
            return endingDate;
        }
        private bool IsLastDayOfMonth(DateTime date)
        {
            return date.AddDays(1).Month != date.Month;
        }
        private DateTime GetFirstDayOfNextMonth(DateTime date)
        {
            DateTime newDate;
            if (date.Month < 12)
            {
                newDate = new DateTime(date.Year, date.Month + 1, 1, date.Hour, date.Minute, date.Second);
            }
            else
            {
                newDate = new DateTime(date.Year + 1, 1, 1, date.Hour, date.Minute, date.Second);
            }
            return newDate;
        }
    }
}