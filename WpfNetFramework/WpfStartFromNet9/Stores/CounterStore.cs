using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStartFromNet9.Stores
{
    public class CounterStore
    {
        private int _value = 0;

        public int Value { get { return _value; } }
        
        public event Action ValueChanged;

        public void Increment()
        {
            _value++;
            ValueChanged?.Invoke();
        }
    }
}
