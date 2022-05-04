using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib.Chords
{
    public class Omit : AddMember
    {
        public Omit(int nth, Accidental accidental) : base(nth, accidental) { }

        protected override void DoDirectBuilder(FormulaBuilder builder)
        {
            builder.Omit(Nth, Accidental);
        }

        public override string ToString()
        {
            return "omit" + Accidental.GetLabel() + Nth;
        }
    }
}
