#region

using System;
using System.Globalization;
using System.Linq.Expressions;

#endregion

namespace Validation.Validators
{
    public static class ValidateRangeExtension
    {
        public static ValidateRangeFluentHelper<TModel> ValidateRange<TModel>(this Validator<TModel> validator, Expression<Func<TModel, object>> property)
        {
            var validateRangeFluentHelper = new ValidateRangeFluentHelper<TModel>();
            validateRangeFluentHelper.SetProperty(property);
            validateRangeFluentHelper.SetValidater(m =>
                {
                    var value = property.GetPropertyValue(m) as string;

                    if (string.IsNullOrEmpty(value))
                        return true;

                    var minText = validateRangeFluentHelper.GetMin().ToString(CultureInfo.InvariantCulture);
                    var maxText = validateRangeFluentHelper.GetMax().ToString(CultureInfo.InvariantCulture);

                    switch (validateRangeFluentHelper.GetDataType())
                    {
                        case ValidationDataType.Integer:

                            int ival;
                            int.TryParse(value, out ival);
                            var imin = int.Parse(minText);
                            var imax = int.Parse(maxText);

                            return (ival >= imin && ival <= imax);

                        case ValidationDataType.Double:
                            
                            double dval;
                            double.TryParse(value, out dval);

                            var dmin = double.Parse(minText);
                            var dmax = double.Parse(maxText);

                            return (dval >= dmin && dval <= dmax);

                        case ValidationDataType.Decimal:

                            decimal cval;
                            decimal.TryParse(value, out cval);

                            var cmin = decimal.Parse(minText);
                            var cmax = decimal.Parse(maxText);

                            return (cval >= cmin && cval <= cmax);

                        case ValidationDataType.Date:

                            DateTime tval;
                            DateTime.TryParse(value, out tval);

                            var tmin = DateTime.Parse(minText);
                            var tmax = DateTime.Parse(maxText);

                            return (tval >= tmin && tval <= tmax);

                        case ValidationDataType.String:

                            var smin = minText;
                            var smax = maxText;

                            var result1 = String.CompareOrdinal(smin, value);
                            var result2 = String.CompareOrdinal(value, smax);

                            return result1 >= 0 && result2 <= 0;
                    }
                    return false;
                });

            validator.AddValidation(validateRangeFluentHelper);

            return validateRangeFluentHelper;
        }
    }
}