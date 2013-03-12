// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelper.cs" company="Developer In The Flow">
//   © 2013 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LinqContrib
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ExpressionExtensions
    {
        #region Public Methods and Operators

        [DebuggerStepThrough]
        public static string GetName<TProperty>(this Expression<Func<TProperty>> property)
        {
            var memberInfo = GetMemberExpression(property.Body).Member;
            if (memberInfo.MemberType != MemberTypes.Property)
            {
                throw new ArgumentException(@"This method can only be invoked on properties!", "property");
            }

            return memberInfo.Name;
        }

        #endregion

        #region Methods

        [DebuggerStepThrough]
        private static MemberExpression GetMemberExpression(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression as UnaryExpression;
                if (unaryExpression != null)
                {
                    memberExpression = GetMemberExpression(unaryExpression.Operand);
                }
            }

            return memberExpression;
        }

        #endregion
    }
}