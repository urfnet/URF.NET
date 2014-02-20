namespace Validation.Validators
{
    public class ValidateRegexFluentHelper<TModel> : Validation<TModel>
    {
        private string _pattern;

        public string GetPattern()
        {
            return _pattern;
        }

        public ValidateRegexFluentHelper<TModel> SetPattern(string pattern)
        {
            _pattern = pattern;
            return this;
        }
    }
}