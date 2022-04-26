using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public interface IChordParser
    {
        public Note GetRoot();
        public Note GetBass();
        public Formula GetQuality();
        public bool Parse(string expression);
    }
}
