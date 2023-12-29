using NPOI.SS.Formula.Eval;
using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.Util;

namespace NPOI.SS.Formula.Functions
{
    public class Days : Var2or3ArgFunction
    {
        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
        {
            double result;
            try
            {
                DateTime t0 = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg0);
                DateTime t1 = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg1);
                TimeSpan timeSpan = t0 - t1;
                result = timeSpan.TotalDays;
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
                //double d0 = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
                //double d1 = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
                //result = Evaluate(d0, d1);

                ValueEval ve = OperandResolver.GetSingleValue(arg2, srcRowIndex, srcColumnIndex);
                bool? method = OperandResolver.CoerceValueToBoolean(ve, false);
                DateTime t0 = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg0);
                DateTime t1 = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, arg1);
                TimeSpan timeSpan = t0 - t1;
                result = timeSpan.TotalDays;
            }
            catch (EvaluationException e)
            {
                return e.GetErrorEval();
            }
            return new NumberEval(result);
        }
    }
}
