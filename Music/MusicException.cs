using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public class MusicException : Exception
    {
        public MusicException() : base() { }

        public MusicException(string message) : base(message) { }
    }
}
