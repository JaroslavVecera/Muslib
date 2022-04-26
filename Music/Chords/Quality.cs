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
            return CreateQuality(parser.Parse(expression));
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
            res.Formula = builder.GetResult();
            return res;
        }
    }
}
