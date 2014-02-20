#region

using System;
using System.Collections.Generic;
using System.Web.Mvc;

#endregion

namespace Validation.Mvc
{
    public class FluentModelValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            var isPropertyValidation = metadata.ContainerType != null && !String.IsNullOrEmpty(metadata.PropertyName);

            if (isPropertyValidation) yield break;

            var validatorFactory = new ValidatorFactory();
            var validator = validatorFactory.GetValidator(metadata.ModelType, metadata.Model);

            if (validator != null)
                yield return new FluentModelValidator(metadata, context, validator);
        }
    }
}