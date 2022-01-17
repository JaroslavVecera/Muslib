using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class Formula
    {
        protected List<int> _value;
        public virtual List<int> Value { get { return new List<int>(_value); } protected set { _value = value; } }

        public Formula() : this(new List<int> { 0, 4, 7 }) { }

        public Formula(List<int> value)
        {
            Value = new List<int>(value);
        }

        public bool Contains(int n)
        {
            return Value.Contains(n);
        }
        
        protected void RemoveDuplicities()
        {
            Value.Union(new List<int>());
        }
    }
}
