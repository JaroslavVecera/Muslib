using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public interface IQualityMemberParser
    {
        public List<QualityMember> Parse(string expression);
    }
}
