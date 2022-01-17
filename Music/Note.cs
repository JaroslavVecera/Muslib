using System;

namespace Music
{
    public enum Note
    {
       C = 0,
       D = 2,
       E = 4,
       F = 5,
       G = 7,
       A = 9,
       B = 11,
    }

    public static class NoteExtensions
    {
        public static string GetLabel(this Note n)
        {
            return n.ToString();
        }
    }
}
