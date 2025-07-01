using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfNetFramework.Commands;
using WpfNetFramework.Core;
using WpfNetFramework.Stores;

namespace WpfNetFramework.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        CounterStore _counterStore;

        public int Counter
        {
            get { return _counterStore.Value; }
        }
        private string _counterText = "0";
        public string CounterText {
            get { return _counterText; }
            set {
                if (_counterText != value)
                {
                    _counterText = value;
                    OnPropertyChanged(nameof(CounterText));
                    ValidateCounterText();
                }
            }
        }
            
        public ICommand IncrementCounterCommand { get; }

        public MainWindowViewModel(CounterStore counterStore)
        {
            _counterStore = counterStore;

            IncrementCounterCommand = new IncrementCounterCommand(_counterStore);

            _counterStore.ValueChanged += OnValueChanged;
        }

        private void ValidateCounterText()
        {
            ClearErrors(nameof(CounterText));

            if (string.IsNullOrWhiteSpace(CounterText))
            {
                AddError(nameof(CounterText), "Value is required.");
            }
            else if (!int.TryParse(CounterText, out int result))
            {
                AddError(nameof(CounterText), "Please enter a valid integer.");
            }
            else
            {
                _counterStore.Value = result;
            }
        }

        #region INotifyDataErrorInfo

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) return null;
            _errors.TryGetValue(propertyName, out List<string> errors);
            return errors;
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                RaiseErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

        protected void RaiseErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        #endregion

        public bool IsValid => !HasErrors;

        private void OnValueChanged()
        {
            OnPropertyChanged(nameof(Counter));
            CounterText = Counter.ToString();
        }
    }
}
