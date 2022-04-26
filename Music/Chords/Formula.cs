using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class Formula : IEquatable<Formula>
    {
        protected List<int> _value;
        public virtual List<int> Value { get { return new List<int>(_value); } protected set { _value = value; } }
        static Dictionary<int, int> NthToSemitones { get; } = new Dictionary<int, int>() {
            {1, 0 }, { 2, 2}, {3, 4 }, {4, 5 }, {5, 7 }, { 6, 9 }, {7, 10 } };

        public Formula() : this(new List<int> { 0, 4, 7 }) { }

        public Formula(List<int> value)
        {
            Value = value.Select(v => v % 12).ToList();
            RemoveDuplicities();
        }

        public bool Contains(int n)
        {
            return Value.Contains(n % 12);
        }
        
        protected void RemoveDuplicities()
        {
            Value = Value.Union(new List<int>()).ToList();
        }

        public static int ToSemitones(int nth)
        {
            return NthToSemitones[((nth - 1) % 7) + 1];
        }

        public bool Equals(Formula other)
        {
            List<int> a = new List<int>(Value);
            List<int> b = new List<int>(other.Value);
            a.Sort();
            b.Sort();
            return a.SequenceEqual(b);
        }
    }
}
