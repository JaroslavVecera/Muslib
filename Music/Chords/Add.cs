using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class Add : AddMember
    {
        public Add(int nth, Accidental accidental) : base(nth, accidental) { }

        protected override void DoDirectBuilder(FormulaBuilder builder)
        {
            builder.Add(Nth, Accidental);
        }
    }
}
