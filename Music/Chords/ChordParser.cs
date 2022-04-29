using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class ChordParser : IChordParser
    {
        public Note Root { get; private set; }
        public Quality Quality { get; private set; }
        public Note Bass { get; private set; }


        public Note GetRoot()
        {
            return Root;
        }

        public Note GetBass()
        {
            return Bass;
        }

        public Quality GetQuality()
        {
            return Quality;
        }

        void Clear()
        {
            Root = null;
            Bass = null;
            Quality = null;
        }

        public bool Parse(string expression)
        {
            Clear();
            expression = expression.TrimStart();
            if (expression.Length < 1)
                return false;
            NoteName? rootName = NoteNameExtensions.FromString(expression[0]);
            if (!rootName.HasValue)
                return false;
            expression = expression.Remove(0, 1);
            Accidental accidental = Accidental.Natural;
            if (expression.Length > 0 && (expression[0] == '#' || expression[0] == 'b'))
            {
                accidental = expression[0] == '#' ? Accidental.Sharp : Accidental.Flat;
                expression = expression.Remove(0, 1).TrimStart();
            }
            Root = new Note(rootName.Value, accidental);
            List<string> split = expression.Split('/').ToList();
            if (split.Count > 2)
                return false;
            Quality q = Music.Chords.Quality.CreateQuality(split[0]);
            if (q == null)
                return false;
            Quality = q;
            if (split.Count == 2)
            {
                string bassExpr = split[1].Trim();
                if (bassExpr.Length < 1)
                    return false;
                NoteName? bassName = NoteNameExtensions.FromString(bassExpr[0]);
                if (!bassName.HasValue)
                    return false;
                bassExpr = bassExpr.Remove(0, 1);
                Accidental bassAccidental = Accidental.Natural;
                if (bassExpr.Length > 0 && (bassExpr[0] == '#' || bassExpr[0] == 'b'))
                {
                    bassAccidental = bassExpr[0] == '#' ? Accidental.Sharp : Accidental.Flat;
                    bassExpr = bassExpr.Remove(0, 1).TrimStart();
                }
                if (bassExpr.Length > 0)
                    return false;
                Bass = new Note(bassName.Value, bassAccidental);
            }
            return true;
        }
    }
}
