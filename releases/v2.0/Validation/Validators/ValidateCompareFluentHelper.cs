using System;
using System.Linq.Expressions;

namespace Validation.Validators
{
    public class ValidateCompareFluentHelper<TModel> : Validation<TModel>
    {
        private Expression<Func<TModel, object>> _otherPropertySelector;
        private ValidationDataType _validationDataType;
        private ValidationOperator _validationOperator;

        public override void OnValidating()
        {
            if(String.IsNullOrEmpty(GetErrorMessage()))
                SetErrorMessage(GetPropertyName() + " must be " + _validationOperator+ " than " + GetOtherProperty().GetPropertyName());
        }

        public new ValidateCompareFluentHelper<TModel> SetErrorMessage(string errorMessage)
        {
            base.SetErrorMessage(errorMessage);
            return this;
        }


        public ValidateCompareFluentHelper<TModel> SetOtherProperty(Expression<Func<TModel, object>> otherPropertySelector)
        {
            _otherPropertySelector = otherPropertySelector;
            return this;
        }

        public Expression<Func<TModel, object>> GetOtherProperty()
        {
            return _otherPropertySelector;
        }

        public ValidateCompareFluentHelper<TModel> WithOperator(ValidationOperator @operator)
        {
            _validationOperator = @operator;
            return this;
        }

        public ValidationOperator GetOperator()
        {
            return _validationOperator;
        }

        public ValidateCompareFluentHelper<TModel> WithDataType(ValidationDataType validationDataType)
        {
            _validationDataType = validationDataType;
            return this;
        }

        public ValidationDataType GetDataType()
        {
            return _validationDataType;
        }
    }
}