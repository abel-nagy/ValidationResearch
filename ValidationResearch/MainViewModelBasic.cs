using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ValidationResearch
{
    internal class MainViewModelBasic : NotifyBase, INotifyDataErrorInfo
    {
        private double _age;
        public double Age
        {
            get => _age;
            set
            {
                if (value.Equals(_age)) return;
                _age = value;
                ClearError();

                if (Age <= 0 || Age > 100) AddError("Invalid age! Age must be between 0 and 100!");

                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value.Equals(_name)) return;
                _name = value;
                ClearError();

                if (string.IsNullOrWhiteSpace(Name) || Name.Length < 3)
                    AddError("Invalid Name! Name must contain at least 3 characters!");

                OnPropertyChanged();
            }
        }

        #region INotifyDataErrorInfo Implementation

        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            _propertyErrors.TryGetValue(propertyName, out var returnValue);
            return returnValue;
        }

        private void AddError(string errorMessage, [CallerMemberName] string propertyName = null)
        {
            if (propertyName is null) return;
            if (!_propertyErrors.ContainsKey(propertyName)) _propertyErrors[propertyName] = new List<string>();

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void ClearError([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null || !_propertyErrors.TryGetValue(propertyName, out var error)) return;
            error.Clear();
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}