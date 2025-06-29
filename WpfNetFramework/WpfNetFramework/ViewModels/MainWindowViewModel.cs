using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNetFramework.Core;
using WpfNetFramework.Stores;

namespace WpfNetFramework.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        CounterStore _counterStore;
        public int Counter => _counterStore.Value;

        public MainWindowViewModel(CounterStore counterStore)
        {
            _counterStore = counterStore;

            _counterStore.ValueChanged += OnValueChanged;
        }

        private void OnValueChanged()
        {
            OnPropertyChanged(nameof(Counter));
        }
    }
}
