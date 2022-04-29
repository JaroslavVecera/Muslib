using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class Chord
    {
        public Note Root { get; private set; }
        public Quality Quality { get; private set; }
        public Note Bass { get; private set; }

        public Note Lowest { get { return Bass != null ? Bass : Root; } }

        public List<Note> Notes { get; private set; }

        public Chord(Note root, Quality quality, Note bass)
        {
            Root = root;
            Quality = quality;
            Bass = bass;
            CountNotes();
        }

        public Chord(Note root, Quality quality)
        {
            Root = root;
            Quality = quality;
            Bass = null;
            CountNotes();
        }

        void CountNotes()
        {
            Notes = Quality.Formula.Value.Select(i => Root + i).ToList();
            if (Bass != null && !Notes.Contains(Bass))
                Notes.Add(Bass);
            Notes.Sort((i, j) => i.ToSemitones().CompareTo(j.ToSemitones()));
        }

        public static Chord ParseChord(string expression)
        {
            return ParseChord(expression, new ChordParser());
        }

        public static Chord ParseChord(string expression, IChordParser p)
        {
            bool success = p.Parse(expression);
            if (!success)
                return null;
            return new Chord(p.GetRoot(), p.GetQuality(), p.GetBass());
        }

        public string GetStringNotes()
        {
            string res = "";
            Notes.ForEach(n => res += n.ToString() + " ");
            return res.TrimEnd();
        }

        public override string ToString()
        {
            return Root.ToString() + Quality.ToString() + ((Bass != null) ? ("/" + Bass.ToString()) : "");
        }
    }
}
