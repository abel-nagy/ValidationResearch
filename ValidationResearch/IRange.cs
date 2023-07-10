using System;

namespace ValidationResearch
{
    public interface IRange<T> where T : IComparable, IComparable<T>
    {
        T Minimum { get; set; }
        T Maximum { get; set; }
    }
}