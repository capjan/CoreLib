using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extensions.LinqRelated
{
    public static class LinqExpressionExt
    {
        public static MemberInfo GetMember(this LambdaExpression expression)
        {
            var memberExp = RemoveUnary(expression.Body) as MemberExpression;
            return memberExp!.Member;
        }

        private static Expression RemoveUnary(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression expression)
            {
                return expression.Operand;
            }

            return toUnwrap;
        }
    }
}
