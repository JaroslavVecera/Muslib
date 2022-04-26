using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public abstract class QualityMember
    {
        public abstract QualityMemberType Type { get; }

        public static string ToString(List<QualityMember> members)
        {
            return string.Join(" ", members);
        }

        public void DirectBuilder(FormulaBuilder builder)
        {
            builder.IncreaseState(Type);
            if (!builder.Error)
                DoDirectBuilder(builder);
            builder.DoStateSpecificBehaviour();
        }

        protected abstract void DoDirectBuilder(FormulaBuilder builder);
    }
}
