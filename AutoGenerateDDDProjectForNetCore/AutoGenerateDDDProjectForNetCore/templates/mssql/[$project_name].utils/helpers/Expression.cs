using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace [$project_name].utils.helpers
{
    public class ExpressionObjectValue
    {
        public string MemberName { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
    }

    public static class ObjectValue
    {
        public static string ToStringForQuery(this object value)
        {
            if (value == null) return "NULL";
            else if (value.GetType() == typeof(string) || value.GetType() == typeof(String)) return $"N'{value}'";
            else if (value.GetType() == typeof(DateTime)) return $"'{(DateTime.Parse(value.ToString())).ToString("YYYY-MM-DD hh:mm:ss")}'";
            else return value.ToString();
        }

        /// <summary>
        /// Get data for math operator of expression
        /// </summary>
        /// <param name="memberSelector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static ExpressionObjectValue GetMemberName(LambdaExpression memberSelector)
        {
            BinaryExpression? member = memberSelector.Body as BinaryExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    memberSelector.ToString()));

            Func<Expression, string>? nameSelector = null;  //recursive func
            nameSelector = e => //or move the entire thing to a separate recursive method
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Parameter:
                        return ((ParameterExpression)e).Name;

                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)e).Member.Name;

                    case ExpressionType.Call:
                        return ((MethodCallExpression)e).Method.Name;

                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        return nameSelector(((UnaryExpression)e).Operand);

                    case ExpressionType.Invoke:
                        return nameSelector(((InvocationExpression)e).Expression);

                    case ExpressionType.ArrayLength:
                        return "Length";

                    default:
                        throw new Exception("not a proper member selector");
                }
            };

            Func<Expression, string>? operatorSelector = null;
            operatorSelector = e =>
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Equal:
                        return " = ";

                    case ExpressionType.GreaterThan:
                        return " > ";

                    case ExpressionType.GreaterThanOrEqual:
                        return " >= ";

                    case ExpressionType.LessThan:
                        return " < ";

                    case ExpressionType.LessThanOrEqual:
                        return " <= ";

                    default:
                        return " = ";
                }
            };

            ExpressionObjectValue data = new ExpressionObjectValue
            {
                MemberName = nameSelector(member.Left),
                Operator = operatorSelector(member),
                Value = Expression.Lambda(member.Right).Compile().DynamicInvoke()
            };
            return data;
        }

        public static ExpressionObjectValue GetMemberNameOfContain(LambdaExpression memberSelector)
        {
            MethodCallExpression? member = memberSelector.Body as MethodCallExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property, not a method.",
                    memberSelector.ToString()));
            string value = Expression.Lambda(member.Arguments[0]).Compile().DynamicInvoke().ToString();
            ExpressionObjectValue data = new ExpressionObjectValue
            {
                MemberName = (member.Object as MemberExpression).Member.Name.ToString(),
                Operator = " LIKE ",
                Value = $"%{Regex.Replace(value, "\"+", "")}%"
            };
            return data;
        }
    }
}