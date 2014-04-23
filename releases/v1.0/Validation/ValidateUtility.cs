#region

using System;
using System.Linq.Expressions;

#endregion

namespace Validation
{
    public static class ValidateUtility
    {
        public static object GetPropertyValue<TModel>(this Expression<Func<TModel, object>> property, object model)
        {
            return property.Compile()((TModel) model);
        }

        public static string GetPropertyName<TModel>(this Expression<Func<TModel, object>> property)
        {
            var memberExpression = property.Body as MemberExpression ?? ((UnaryExpression) property.Body).Operand as MemberExpression;
            return memberExpression.Member.Name;
        }
    }
}