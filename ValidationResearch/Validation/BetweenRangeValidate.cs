using System;
using PostSharp.Aspects;

namespace ValidationResearch.Validation
{
    [Serializable]
    public class BetweenRangeValidate : LocationInterceptionAspect
    {
        private IValidationRule<double> _rule;

        public BetweenRangeValidate(double min, double max)
        {
            _rule = new BetweenRangeRule<double>(min, max);
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            args.ProceedSetValue();

            if (!_rule.Validate((double)args.Value))
                throw new ArgumentException(_rule.ErrorMessage);
        }
    }
}