using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ValidationResearch.Validation;

namespace ValidationResearch
{
    public class Range<T> : NotifyBase, INotifyDataErrorInfo, IRange<T> where T : IComparable, IComparable<T>
    {
        private readonly IValidationRule<T> _rangeRule;
        private readonly IValidationRule<T>[] _customValidationRules;
        private readonly DataValidityNotifier _validityNotifier;

        public Range(params IValidationRule<T>[] customValidationRules)
        {
            _customValidationRules = customValidationRules;
            _rangeRule = new RangeRule<T>(this);
            _validityNotifier = new DataValidityNotifier();
            _validityNotifier.ErrorsChanged += (sender, args) => OnErrorsChanged(args.PropertyName);
        }

        private T _minimum;
        public T Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                OnPropertyChanged();
                ValidateValue(Minimum);
                ValidateValue(Maximum, nameof(Maximum));
            }
        }

        private T _maximum;
        public T Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                OnPropertyChanged();
                ValidateValue(Maximum);
                ValidateValue(Minimum, nameof(Minimum));
            }
        }
        
        private void ValidateValue(T value, [CallerMemberName] string propertyName = null)
        {
            _validityNotifier.ClearError(propertyName);

            if (!_rangeRule.Validate(value))
                _validityNotifier.AddError(_rangeRule.ErrorMessage, propertyName);

            foreach (var rule in _customValidationRules.Where(rule => !rule.Validate(value)))
            {
                _validityNotifier.AddError(rule.ErrorMessage, propertyName);
            }

            OnErrorsChanged(propertyName);
        }

        #region INotifyDataErrorInfo implementation

        public IEnumerable GetErrors(string propertyName) => _validityNotifier.GetErrors(propertyName);

        public bool HasErrors => _validityNotifier.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}