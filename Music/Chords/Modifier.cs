using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib.Chords
{
    public enum Modifier
    {
        Major,
        Minor,
        Augmented,
        Diminished
    }

    public static class ModifierExtensions
    {
        public static string GetLabel(this Modifier a)
        {
            switch (a)
            {
                case Modifier.Major:
                    return "M";
                case Modifier.Minor:
                    return "m";
                case Modifier.Augmented:
                    return "aug";
                case Modifier.Diminished:
                    return "dim";
                default:
                    return "";
            }
        }
    }
}
