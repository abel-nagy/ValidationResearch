using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ValidationResearch.Validation
{
    public class DataValidityNotifier : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            _propertyErrors.TryGetValue(propertyName, out var returnValue);
            return returnValue;
        }

        public void AddError(string errorMessage, [CallerMemberName] string propertyName = null)
        {
            if (propertyName is null) return;
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors[propertyName] = new List<string>();
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        public void ClearError([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null || !_propertyErrors.TryGetValue(propertyName, out var error))
            {
                return;
            }

            error.Clear();
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}