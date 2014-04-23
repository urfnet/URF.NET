#region

using System;

#endregion

namespace Validation
{
    public class ValidatorFactory
    {
        public IValidator GetValidator(Type type, object model)
        {
            var validatorAttributes = type.GetCustomAttributes(typeof (ValidatorAttribute), true);

            if (validatorAttributes.Length > 0)
            {
                var validatorAttribute = (ValidatorAttribute) validatorAttributes[0];
                return Activator.CreateInstance(validatorAttribute.Validator) as IValidator;
            }
            return null;
        }
    }
}