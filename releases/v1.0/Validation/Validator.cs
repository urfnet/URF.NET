#region

using System.Collections.Generic;

#endregion

namespace Validation
{
    public class Validator<TModel> : IValidator
    {
        private readonly List<ValidationResult> _validationResults;
        private readonly List<Validation<TModel>> _validations;

        public Validator()
        {
            _validations = new List<Validation<TModel>>();
            _validationResults = new List<ValidationResult>();
        }

        public List<ValidationResult> Validate(object model)
        {
            foreach (var validation in _validations)
            {
                validation.OnValidating();
                var validater = validation.GetValidater();

                if (!validater((TModel) model))
                    _validationResults.Add(validation.GetValidationResult());
            }

            return _validationResults;
        }

        public Validation<TModel> AddValidation(Validation<TModel> validationRule)
        {
            _validations.Add(validationRule);
            return validationRule;
        }
    }
}