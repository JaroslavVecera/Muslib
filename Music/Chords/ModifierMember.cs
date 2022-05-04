using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib.Chords
{
    public class ModifierMember : QualityMember
    {
        public override QualityMemberType Type { get { return QualityMemberType.Modifier; } }
        public Modifier Modifier { get; private set; }

        public ModifierMember(Modifier modifier)
        {
            Modifier = modifier;
        }

        protected override void DoDirectBuilder(FormulaBuilder builder)
        {
            builder.AddModifier(Modifier);
        }

        public override string ToString()
        {
            return Modifier.GetLabel();
        }
    }
}
