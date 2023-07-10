using System;

namespace ValidationResearch.Validation
{
    [Serializable]
    public class BetweenRangeRule<T> : IValidationRule<T> where T : IComparable, IComparable<T>
    {
        private readonly T _min, _max;

        public BetweenRangeRule(T min, T max)
        {
            _min = min;
            _max = max;
        }

        public string ErrorMessage { get; private set; }

        public bool Validate(T value)
        {
            ErrorMessage = string.Empty;

            if (value.CompareTo(_min) != -1 && value.CompareTo(_max) != 1)
            {
                return true;
            }

            ErrorMessage = $"Value must be between {_min} and {_max}.";
            return false;
        }
    }
}