using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class ExtentionMember : QualityMember
    {
        public override QualityMemberType Type { get { return QualityMemberType.Extention; } }
        public Extention Extention { get; private set; }
        bool IsExtending { get { return Extention > Extention.fifth; } }
        public Accidental Accidental { get; private set; }

        public ExtentionMember(Extention extention, Accidental accidental)
        {
            Extention = extention;
            Accidental = accidental;
        }

        protected override void DoDirectBuilder(FormulaBuilder builder)
        {
            builder.Extend(Extention, Accidental);
        }
    }
}
