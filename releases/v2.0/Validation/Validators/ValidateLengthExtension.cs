#region

using System;
using System.Linq.Expressions;

#endregion

namespace Validation.Validators
{
    public static class ValidateLengthExtension
    {
        public static ValidateLengthFluentHelper<TModel> ValidateLength<TModel>(this Validator<TModel> validator, Expression<Func<TModel, object>> property)
        {
            var fluentHelper = new ValidateLengthFluentHelper<TModel>();

            fluentHelper.SetProperty(property);
            fluentHelper.SetValidater(model =>
                {
                    var value = property.GetPropertyValue(model) as string;

                    return string.IsNullOrEmpty(value) || value.Length >= fluentHelper.GetMin() && value.Length <= fluentHelper.GetMax();
                });

            validator.AddValidation(fluentHelper);

            return fluentHelper;
        }
    }
}