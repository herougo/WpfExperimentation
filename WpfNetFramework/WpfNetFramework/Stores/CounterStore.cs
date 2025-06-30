using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetFramework.Stores
{
    public class CounterStore
    {
        private int _value = 0;

        public int Value {
            get { return _value; }
            set {
                _value = value;
                ValueChanged?.Invoke();
            }
        }
        
        public event Action ValueChanged;

        public void Increment()
        {
            Value++;
        }
    }
}
