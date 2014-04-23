using System.Collections.Generic;

namespace Validation
{
    public interface IValidator
    {
        List<ValidationResult> Validate(object model);
    }
}