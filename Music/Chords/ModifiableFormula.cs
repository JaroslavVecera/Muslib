using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class ModifiableFormula : Formula
    {
        public override List<int> Value { get { return _value; } }

        public ModifiableFormula() : this(new List<int> { 0, 4, 7 }) { }

        public ModifiableFormula(List<int> value)
        {
            Value = new List<int>(value);
        }

        public bool Add(int n)
        {
            if (Contains(n))
                return false;
            Value.Add(n);
            return true;
        }

        public void Remove(int n)
        {
            Value.Remove(n);
        }

        public void RemoveThird()
        {
            Value.RemoveAt(1);
        }

        public void AddMask(List<int> mask)
        {
            int i = 0;
            foreach (int m in mask)
                Value[i++] += m;
            RemoveDuplicities();
        }
    }
}
