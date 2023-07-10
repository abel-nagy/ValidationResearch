using System;

namespace ValidationResearch.Validation
{
    public interface IValidationRule<in T> where T : IComparable, IComparable<T>
    {
        string ErrorMessage { get; }
        bool Validate(T value);
    }
}