using System;
using System.Collections.Generic;

namespace Muslib
{
    public enum NoteName
    {
       C = 0,
       D = 2,
       E = 4,
       F = 5,
       G = 7,
       A = 9,
       B = 11,
    }

    public static class NoteNameExtensions
    {
        static Dictionary<char, NoteName> StringToNames { get; } = new Dictionary<char, NoteName>()
        { 
            { 'C', NoteName.C },
            { 'D', NoteName.D },
            { 'E', NoteName.E },
            { 'F', NoteName.F },
            { 'G', NoteName.G },
            { 'A', NoteName.A },
            { 'B', NoteName.B },
        };

        public static NoteName? FromString(char label)
        {
            label = char.ToUpper(label);
            if (StringToNames.ContainsKey(label))
                return StringToNames[label];
            else
                return null;
        }

        public static string GetLabel(this NoteName n)
        {
            return n.ToString();
        }

        public static NoteName Ancestor(this NoteName n)
        {
            switch (n)
            {
                case NoteName.F:
                    return NoteName.E;
                case NoteName.C:
                    return NoteName.B;
                default:
                    return n - 2;
            }
        }

        public static NoteName Succesor(this NoteName n)
        {
            switch (n)
            {
                case NoteName.E:
                    return NoteName.F;
                case NoteName.B:
                    return NoteName.C;
                default:
                    return n + 2;
            }
        }
    }
}
