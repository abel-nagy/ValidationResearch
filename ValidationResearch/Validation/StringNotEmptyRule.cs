using System;

namespace ValidationResearch.Validation
{
    [Serializable]
    public class StringNotEmptyRule : IValidationRule<string>
    {
        public string ErrorMessage { get; private set; }

        public bool Validate(string value)
        {
            if (!string.IsNullOrWhiteSpace(value)) return true;
            ErrorMessage = "Value cannot be empty!";
            return false;
        }
    }
}