using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib.Chords
{
    public interface IQualityMemberParser
    {
        public List<QualityMember> Parse(string expression);
    }
}
