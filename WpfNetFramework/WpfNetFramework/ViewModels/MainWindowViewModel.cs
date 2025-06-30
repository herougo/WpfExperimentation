using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfNetFramework.Commands;
using WpfNetFramework.Core;
using WpfNetFramework.Stores;

namespace WpfNetFramework.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        CounterStore _counterStore;
        public int Counter {
            get { return _counterStore.Value; }
            set {
                if (_counterStore.Value != value)
                {
                    _counterStore.Value = value;
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

        private void OnValueChanged()
        {
            OnPropertyChanged(nameof(Counter));
        }
    }
}
