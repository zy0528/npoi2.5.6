using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
    public class DatedElta : Var3or4ArgFunction
    {
        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval datePara, ValueEval numPara, ValueEval unitPara, ValueEval formatPara)
        {
            string formalFormat = "yyyy-MM-dd HH:mm:ss";
            string result = string.Empty;
            try
            {
                string timeStr = TextFunction.EvaluateStringArg(datePara, srcRowIndex, srcColumnIndex);
                string format = TextFunction.EvaluateStringArg(formatPara, srcRowIndex, srcColumnIndex);
                string unit = TextFunction.EvaluateStringArg(unitPara, srcRowIndex, srcColumnIndex);
                DateTime time0;
                if (!string.IsNullOrWhiteSpace(format) && !format.Contains("D") && !format.Contains("Y") && !format.Contains("M"))
                {
                    time0 = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + timeStr.Trim());
                    format = formalFormat;
                }
                else
                {
                    time0 = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, datePara);
                }

                if (string.IsNullOrWhiteSpace(format))
                {
                    format = formalFormat;
                }
                double d1 = NumericFunction.SingleOperandEvaluate(numPara, srcRowIndex, srcColumnIndex);
                result = Evaluate(time0, d1, format, unit);
            }
            catch (EvaluationException e)
            {
                return e.GetErrorEval();
            }
            return new StringEval(result);
        }

        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval datePara, ValueEval numPara, ValueEval unitPara)
        {
            string result = string.Empty;
            try
            {
                DateTime time0 = TimeHelper.GetDateTime(srcRowIndex, srcColumnIndex, datePara);
                double d1 = NumericFunction.SingleOperandEvaluate(numPara, srcRowIndex, srcColumnIndex);
                string unit = TextFunction.EvaluateStringArg(unitPara, srcRowIndex, srcColumnIndex);
                result = Evaluate(time0, d1, string.Empty, unit);
            }
            catch (EvaluationException e)
            {
                return e.GetErrorEval();
            }
            return new StringEval(result);
        }

        private string Evaluate(DateTime dateTime, double d1, string format, string unit)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                dateTime = dateTime.AddDays(d1);
            }
            else if (unit == "y" || unit == "Y")
            {
                dateTime = dateTime.AddYears((int)d1);
            }
            else if (unit == "M")
            {
                dateTime = dateTime.AddMonths((int)d1);
            }
            else if (unit == "D" || unit == "d")
            {
                dateTime = dateTime.AddDays((int)d1);
            }
            else if (unit == "h" || unit == "H")
            {
                dateTime = dateTime.AddHours(d1);
            }
            else if (unit == "m")
            {
                dateTime = dateTime.AddMinutes(d1);
            }
            else if (unit == "s" || unit == "S")
            {
                dateTime = dateTime.AddSeconds(d1);
            }

            if (string.IsNullOrEmpty(format))
            {
                return dateTime.ToShortDateString();
            }
            return dateTime.ToString(format);
        }
    }
}
