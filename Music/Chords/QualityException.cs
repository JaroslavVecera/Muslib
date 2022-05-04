using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib.Chords
{
    public class QualityException : MusicException
    {
        public QualityException(List<QualityMember> qualityMembers, int unexpectedMemberIndex, string message)
            : base(BuildMessage(qualityMembers, unexpectedMemberIndex, message)) { }

        public QualityException(List<QualityMember> qualityMembers, int unexpectedMemberIndex)
            : base(BuildMessage(qualityMembers, unexpectedMemberIndex, "")) { }

        static string BuildMessage(List<QualityMember> qualityMembers, int unexpectedMemberIndex, string message)
        {
            string membersPrefix = QualityMember.ToString(qualityMembers.Take(unexpectedMemberIndex + 1).ToList());
            string description = $"Unexpected member {qualityMembers[unexpectedMemberIndex]} in {membersPrefix}.";
            if (!string.IsNullOrEmpty(message))
                description += Environment.NewLine + message;
            return description;
        }
    }
}
