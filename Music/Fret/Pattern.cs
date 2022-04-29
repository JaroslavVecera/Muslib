using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Fret
{
    public class Pattern
    {
        List<int> Frets { get; set; }
        public int Unmuted { get { return Frets.Count(x => x != -1); } }
        public int Max { get { return Frets.Max(); } }
        public int Min { get { return GetMin(); } }
        public bool Barre { get; set; } = false;
        public int Size { get { return Frets.Count; } }

        public Pattern(List<int> frets)
        {
            Frets = new List<int>(frets);
        }

        public int this[int nth] { get { return Frets[nth]; } }

        int GetMin()
        {
            int min = Max;
            foreach (int pos in Frets)
                if (pos > 0 && pos < min)
                    min = pos;
            return min;
        }

        public override string ToString()
        {
            string res = "{ " + FretToString(Frets[0]);
            for (int i = 1; i < Size; i++)
                res += ", " + FretToString(Frets[i]);
            return res + " }";
        }

        public int Count(Func<int, bool> predicate)
        {
            return Frets.Count(predicate);
        }

        public int IndexOf(int val)
        {
            return Frets.IndexOf(val);
        }

        public int FindLastIndex(Predicate<int> predicate)
        {
            return Frets.FindLastIndex(predicate);
        }

        public bool CanBarreFrom(int fromNth)
        {
            return !Frets.GetRange(fromNth, Size - fromNth).Contains(0);
        }

        public bool IsSubset(Pattern p)
        {
            for (int i = 0; i < Size; i++)
            {
                if (p[i] > -1 && Frets[i] != p[i])
                    return false;
            }
            return true;
        }

        static string FretToString(int fret)
        {
            return fret == -1 ? "x" : fret.ToString();
        }
    }
}
