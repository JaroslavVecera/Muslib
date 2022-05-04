using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib.Chords
{
    public class SuspendedMember : QualityMember
    {
        public override QualityMemberType Type { get { return QualityMemberType.Sus; } }
        public Sus Sus { get; private set; }

        public SuspendedMember(Sus sus)
        {
            Sus = sus;
        }

        protected override void DoDirectBuilder(FormulaBuilder builder)
        {
            builder.BuildSus(Sus);
        }

        public override string ToString()
        {
            return "sus" + Sus;
        }
    }
}
