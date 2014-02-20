#region

using System;
using System.Linq.Expressions;

#endregion

namespace Validation.Validators
{
    public static class ValidateIpAddressExtension
    {
        public static Validation<TModel> ValidateIpAddress<TModel>(this Validator<TModel> validator, Expression<Func<TModel, object>> property)
        {
            return validator.ValidateRegex(property)
                            .SetPattern(@"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b")
                            .SetErrorMessage(property.GetPropertyName() + " is not a valid IP Address");

        }
    }
}