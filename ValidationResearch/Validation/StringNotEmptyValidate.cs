using System;
using PostSharp.Aspects;

namespace ValidationResearch.Validation
{
    [Serializable]
    public class StringNotEmptyValidate : LocationInterceptionAspect
    {
        private IValidationRule<string> _rule;

        public StringNotEmptyValidate()
        {
            _rule = new StringNotEmptyRule();
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            args.ProceedSetValue();

            if (!_rule.Validate((string)args.Value))
                throw new ArgumentException(_rule.ErrorMessage);
        }
    }
}