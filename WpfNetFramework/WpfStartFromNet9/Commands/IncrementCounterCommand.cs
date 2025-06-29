using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WpfStartFromNet9.Core;
using WpfStartFromNet9.Stores;

namespace WpfStartFromNet9.Commands
{
    public class IncrementCounterCommand : CommandBase
    {
        private CounterStore _counterStore;

        public IncrementCounterCommand(CounterStore counterStore)
        {
            _counterStore = counterStore;
        }

        public override void Execute(object parameter)
        {
            _counterStore.Increment();
        }
    }
}
