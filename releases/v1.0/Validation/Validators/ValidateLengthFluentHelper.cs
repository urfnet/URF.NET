using System;
using System.Linq.Expressions;

namespace Validation.Validators
{
    public class ValidateLengthFluentHelper<TModel> : Validation<TModel>
    {
        private int _max;
        private int _min;

        public override void OnValidating()
        {
            if (String.IsNullOrEmpty(GetErrorMessage()))
                SetErrorMessage(GetPropertyName() + " must be between " + _min + " and " + _max + " characters long.");
        }

        public int GetMin()
        {
            return _min;
        }

        public int GetMax()
        {
            return _max;
        }

        public new ValidateLengthFluentHelper<TModel> SetProperty(Expression<Func<TModel, object>> propertySelector)
        {
            base.SetProperty(propertySelector);
            return this;
        }

        public ValidateLengthFluentHelper<TModel> WithMin(int min)
        {
            _min = min;
            return this;
        }

        public ValidateLengthFluentHelper<TModel> WithMax(int max)
        {
            _max = max;
            return this;
        }
    }
}