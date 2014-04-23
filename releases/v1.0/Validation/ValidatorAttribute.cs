#region

using System;

#endregion

namespace Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidatorAttribute : Attribute
    {
        private readonly Type _validator;

        public ValidatorAttribute(Type validator)
        {
            _validator = validator;
        }


        public Type Validator
        {
            get { return _validator; }
        }
    }
}