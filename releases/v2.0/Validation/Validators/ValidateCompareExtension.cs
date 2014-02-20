#region

using System;
using System.Linq.Expressions;

#endregion

namespace Validation.Validators
{
    public static class ValidateCompareExtension
    {
        public static ValidateCompareFluentHelper<TModel> ValidateCompare<TModel>(this Validator<TModel> validator, Expression<Func<TModel, object>> property, Expression<Func<TModel, object>> otherProperty)
        {
            var validationFluentHelper = new ValidateCompareFluentHelper<TModel>();

            Func<TModel, bool> validateComparer = model =>
            {
                var propValue1 = property.GetPropertyValue(model) as string;
                var propValue2 = otherProperty.GetPropertyValue(model) as string;
                var @operator = validationFluentHelper.GetOperator();
                var dataType = validationFluentHelper.GetDataType();

                switch (dataType)
                {
                    case ValidationDataType.Integer:

                        int ival1;
                        int.TryParse(propValue1, out ival1);

                        int ival2;
                        int.TryParse(propValue2, out ival2);

                        switch (validationFluentHelper.GetOperator())
                        {
                            case ValidationOperator.Equal:
                                return ival1 == ival2;
                            case ValidationOperator.NotEqual:
                                return ival1 != ival2;
                            case ValidationOperator.GreaterThan:
                                return ival1 > ival2;
                            case ValidationOperator.GreaterThanEqual:
                                return ival1 >= ival2;
                            case ValidationOperator.LessThan:
                                return ival1 < ival2;
                            case ValidationOperator.LessThanEqual:
                                return ival1 <= ival2;
                        }
                        break;

                    case ValidationDataType.Double:

                        double dval1; 
                        double.TryParse(propValue1, out dval1);

                        double dval2;
                        double.TryParse(propValue2, out dval2);

                        switch (@operator)
                        {
                            case ValidationOperator.Equal:
                                return dval1 == dval2;
                            case ValidationOperator.NotEqual:
                                return dval1 != dval2;
                            case ValidationOperator.GreaterThan:
                                return dval1 > dval2;
                            case ValidationOperator.GreaterThanEqual:
                                return dval1 >= dval2;
                            case ValidationOperator.LessThan:
                                return dval1 < dval2;
                            case ValidationOperator.LessThanEqual:
                                return dval1 <= dval2;
                        }
                        break;

                    case ValidationDataType.Decimal:

                        decimal cval1;
                        decimal.TryParse(propValue1, out cval1);

                        decimal cval2;
                        decimal.TryParse(propValue2, out cval2);

                        switch (@operator)
                        {
                            case ValidationOperator.Equal:
                                return cval1 == cval2;
                            case ValidationOperator.NotEqual:
                                return cval1 != cval2;
                            case ValidationOperator.GreaterThan:
                                return cval1 > cval2;
                            case ValidationOperator.GreaterThanEqual:
                                return cval1 >= cval2;
                            case ValidationOperator.LessThan:
                                return cval1 < cval2;
                            case ValidationOperator.LessThanEqual:
                                return cval1 <= cval2;
                        }
                        break;

                    case ValidationDataType.Date:

                        DateTime tval1;
                        DateTime.TryParse(propValue1, out tval1);
                        
                        DateTime tval2;
                        DateTime.TryParse(propValue2, out tval2);

                        switch (@operator)
                        {
                            case ValidationOperator.Equal:
                                return tval1 == tval2;
                            case ValidationOperator.NotEqual:
                                return tval1 != tval2;
                            case ValidationOperator.GreaterThan:
                                return tval1 > tval2;
                            case ValidationOperator.GreaterThanEqual:
                                return tval1 >= tval2;
                            case ValidationOperator.LessThan:
                                return tval1 < tval2;
                            case ValidationOperator.LessThanEqual:
                                return tval1 <= tval2;
                        }
                        break;

                    case ValidationDataType.String:

                        var result = string.Compare(propValue1, propValue2, StringComparison.CurrentCulture);

                        switch (@operator)
                        {
                            case ValidationOperator.Equal:
                                return result == 0;
                            case ValidationOperator.NotEqual:
                                return result != 0;
                            case ValidationOperator.GreaterThan:
                                return result > 0;
                            case ValidationOperator.GreaterThanEqual:
                                return result >= 0;
                            case ValidationOperator.LessThan:
                                return result < 0;
                            case ValidationOperator.LessThanEqual:
                                return result <= 0;
                        }
                        break;
                }
                return false;
            };
            
            validationFluentHelper.SetProperty(property);
            validationFluentHelper.SetOtherProperty(otherProperty);
            validationFluentHelper.SetValidater(validateComparer);
            validator.AddValidation(validationFluentHelper);

            return validationFluentHelper;
        }
    }
}