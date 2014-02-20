#region

using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

#endregion

namespace Validation.Validators
{
    public static class ValidateRegexExtension
    {
        public static ValidateRegexFluentHelper<TModel> ValidateRegex<TModel>(this Validator<TModel> validator, Expression<Func<TModel, object>> property)
        {
            var fluentHelper = new ValidateRegexFluentHelper<TModel>();
            fluentHelper.SetProperty(property);
            fluentHelper.SetValidater(model =>
                {
                    var value = property.GetPropertyValue(model) as string;
                    return string.IsNullOrEmpty(value) || Regex.Match(value, fluentHelper.GetPattern()).Success;
                });

            validator.AddValidation(fluentHelper);

            return fluentHelper;
        }
    }
}