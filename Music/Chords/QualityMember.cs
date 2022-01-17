using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public struct QualityMember
    {

        public static string ToString(List<QualityMember> members)
        {
            return string.Join(" ", members);
        }
    }
}
