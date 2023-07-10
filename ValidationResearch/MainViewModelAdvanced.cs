using System;
using System.Collections;
using System.ComponentModel;
using ValidationResearch.Validation;

namespace ValidationResearch
{
    internal class MainViewModelAdvanced : NotifyBase, INotifyDataErrorInfo
    {
        private readonly DataValidityNotifier _validityNotifier;

        public MainViewModelAdvanced()
        {
            _validityNotifier = new DataValidityNotifier();
            _validityNotifier.ErrorsChanged += (sender, args) => OnErrorsChanged(args.PropertyName);
        }

        private double _age;
        [BetweenRangeValidate(0, 100)]
        public double Age
        {
            get => _age;
            set
            {
                if (value.Equals(_age)) return;
                _age = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        [StringNotEmptyValidate]
        public string Name
        {
            get => _name;
            set
            {
                if (value.Equals(_name)) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private Range<double> _payRange = new Range<double>(new BetweenRangeRule<double>(190, 20000));
        public Range<double> PayRange
        {
            get => _payRange;
            set
            {
                if (value.Equals(_payRange)) return;
                _payRange = value;
                OnPropertyChanged();
            }
        }

        #region INotifyDataErrorInfo implementation

        public IEnumerable GetErrors(string propertyName) => _validityNotifier.GetErrors(propertyName);

        public bool HasErrors => _validityNotifier.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}