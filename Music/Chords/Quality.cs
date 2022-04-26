using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class Quality
    {
        public List<QualityMember> Members { get; private set; } = new List<QualityMember>();
        public Formula Formula { get; private set; }

        private Quality() { }

        public static Quality CreateQuality(string expression)
        {
            return CreateQuality(expression, new QualityMemberParser());
        }

        public static Quality CreateQuality(string expression, IQualityMemberParser parser)
        {
            List<QualityMember> members = parser.Parse(expression);
            if (members == null)
                return null;
            return CreateQuality(members);
        }

        public static Quality CreateQuality(List<QualityMember> members)
        {
            FormulaBuilder builder = new FormulaBuilder();
            bool error = false;
            members.ForEach(member =>
            {
                member.DirectBuilder(builder);
                if (builder.Error)
                {
                    error = true;
                    return;
                }
            });
            if (error)
                return null;
            Quality res = new Quality();
            builder.Complete();
            if (builder.Error)
                return null;
            res.Formula = builder.GetResult();
            return res;
        }
    }
}
