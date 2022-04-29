using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Fret
{
    class Tuning
    {
        List<Pitch> Values { get; set; } = new List<Pitch>();
        public int Strings { get { return Values.Count; } }
        public string Name { get; set; }
        public List<Pitch> Pitches { get { return new List<Pitch>(Values); } }

        public Tuning(List<Pitch> values)
        {
            Values = new List<Pitch>(values);
            Name = "custome";
        }

        public Tuning(List<Pitch> values, string name)
        {
            Values = new List<Pitch>(values);
            Name = name;
        }

        public Pitch this[int nth] { get { return Values[nth]; } set { Values[nth] = value; } }
    }
}
