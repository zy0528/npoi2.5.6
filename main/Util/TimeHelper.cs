using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.Util
{
    public class TimeHelper
    {
        /// <summary>
        /// 获取参数中的时间
        /// </summary>
        /// <param name="srcRowIndex"></param>
        /// <param name="srcColumnIndex"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(int srcRowIndex, int srcColumnIndex, ValueEval arg)
        {
            DateTime dateTimeTmp = DateTime.MinValue;
            string argString = TextFunction.EvaluateStringArg(arg, srcRowIndex, srcColumnIndex);
            double dTemp;
            if (double.TryParse(argString, out dTemp))
            {
                double dValue = NumericFunction.SingleOperandEvaluate(arg, srcRowIndex, srcColumnIndex);
                dateTimeTmp = DateUtil.GetJavaDate(dValue);
            }
            else
            {
                dateTimeTmp = DateTime.Parse(argString);
            }
            return dateTimeTmp;
        }
    }
}
