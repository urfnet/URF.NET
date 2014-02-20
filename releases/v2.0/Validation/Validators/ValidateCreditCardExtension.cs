#region

using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

#endregion

namespace Validation.Validators
{
    public static class ValidateCreditCardExtension
    {
        public static Validation<TModel> ValidateCreditCard<TModel>(this Validator<TModel> validator, Expression<Func<TModel, object>> property)
        {
            return validator.ValidateRegex(property)
                     .SetPattern(@"^((\d{4}[- ]?){3}\d{4})$")
                     .SetErrorMessage(property.GetPropertyName() + " is not a valid credit card number");
        }
    }
}