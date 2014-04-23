#region

using System;
using System.Linq.Expressions;

#endregion

namespace Validation
{
    public class ValidationResult
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Validation<TModel>
    {
        private string _errorMessage;
        private string _propertyName;
        private Func<TModel, bool> _validater;
        private Expression<Func<TModel, object>> _property;

        public Expression<Func<TModel, object>> GetProperty()
        {
            return _property;
        }

        public Validation<TModel> SetProperty(Expression<Func<TModel, object>> property)
        {
            _property = property;
            return this;
        }

        public string GetPropertyName()
        {
            var memberExpression = GetProperty().Body as MemberExpression ?? ((UnaryExpression) GetProperty().Body).Operand as MemberExpression;

            if (memberExpression == null)
            {
                _propertyName = default(string);
                return _propertyName;
            }

            _propertyName = memberExpression.Member.Name;

            return _propertyName;
        }

        public Validation<TModel> SetErrorMessage(string errorMessage)
        {
            _errorMessage = errorMessage;
            return this;
        }

        public Validation<TModel> SetValidater(Func<TModel, bool> validater)
        {
            _validater = validater;
            return this;
        }

        public Func<TModel, bool> GetValidater()
        {
            return _validater;
        }

        public string GetErrorMessage()
        {
            return _errorMessage;
        }

        public ValidationResult GetValidationResult()
        {
            return new ValidationResult {ErrorMessage = GetErrorMessage(), PropertyName = GetPropertyName()};
        }

        public virtual void OnValidating()
        {
            if (string.IsNullOrEmpty(_errorMessage))
                _errorMessage = GetPropertyName() + " is not valid";
        }}
}