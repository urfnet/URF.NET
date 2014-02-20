#region

using System;

#endregion

namespace Validation.Validators
{
    public class ValidateRangeFluentHelper<TModel> : Validation<TModel>
    {
        private int _max;
        private int _min;
        private ValidationDataType _validationDataType;

        public override void OnValidating()
        {
            if (String.IsNullOrEmpty(GetErrorMessage()))
                SetErrorMessage(GetPropertyName() + " must be between " + GetMin() + " and " + GetMax());
        }

        public ValidateRangeFluentHelper<TModel> WithDataType(ValidationDataType validationDataType)
        {
            _validationDataType = validationDataType;
            return this;
        }

        public ValidationDataType GetDataType()
        {
            return _validationDataType;
        }

        public ValidateRangeFluentHelper<TModel> WithMin(int min)
        {
            _min = min;
            return this;
        }

        public int GetMin()
        {
            return _min;
        }

        public ValidateRangeFluentHelper<TModel> WithMax(int max)
        {
            _max = max;
            return this;
        }

        public int GetMax()
        {
            return _max;
        }
    }
}