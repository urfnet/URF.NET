#region



#endregion

using System;
using System.Globalization;
using Northwind.Entity.Models;
using Validation;

namespace Northwind.Entity.Validators
{
    public class CustomerValidator : Validator<Customer>
    {
        // Name funcs for easier readibilty
        private readonly Func<Customer, bool> _companyNameRequired = c => 
            !string.IsNullOrEmpty(c.CompanyName);

        // Name funcs for easier readibilty
        private readonly Func<Customer, bool> _customerIdRequired = c => 
            !string.IsNullOrEmpty(c.CustomerID.ToString(CultureInfo.InvariantCulture));

        public CustomerValidator()
        {
            // example of adding validation by passing in generic delgate of Func<TModel, bool>
            //this.ValidateRequired(c => c.AccountNumber);

            // example of using out of the box ValidateRange extension with fluent helper implementation
            //this.ValidateRange()
            //    .SetProperty(c => c.CustomerID)
            //    .WithDataType(ValidationDataType.Integer)
            //    .WithMax(1)
            //    .WithMin(0);



            // example of adding validation by passing in generic delgate of Func<TModel, bool>
            this.AddValidation()
                .SetValidater(_customerIdRequired)
                .SetProperty(c => c.CustomerID);

            // example of adding validation by passing in generic delgate of Func<TModel, bool>
            this.AddValidation()
                .SetValidater(_companyNameRequired)
                .SetProperty(c => c.CompanyName);

            //example of adding validation by passing inline Func<TModel, bool>
            this.AddValidation()
                .SetValidater(c => !string.IsNullOrEmpty(c.Address))
                .SetErrorMessage("Address must have a value.")
                .SetProperty(c => c.Address);
        }
    }
}