using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    abstract public class AddMember : QualityMember
    {
        public override QualityMemberType Type { get { return QualityMemberType.AltOrAdd; } }

        public int Nth { get; private set; }
        public Accidental Accidental { get; private set; }

        public AddMember(int nth, Accidental accidental)
        {
            Nth = nth;
            Accidental = accidental;
        }
    }
}
