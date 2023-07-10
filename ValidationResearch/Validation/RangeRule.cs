using System;

namespace ValidationResearch.Validation
{
    [Serializable]
    public class RangeRule<T> : IValidationRule<T> where T : IComparable, IComparable<T>
    {
        private IRange<T> _range;
        
        public RangeRule(IRange<T> range)
        {
            _range = range;
        }

        public string ErrorMessage { get; private set; }

        public bool Validate(T value)
        {
            ErrorMessage = string.Empty;

            if (_range.Minimum.CompareTo(_range.Maximum) <= -1)
            {
                return true;
            }

            ErrorMessage = "Minimum must be less than or equal to maximum.";
            return false;
        }
    }
}