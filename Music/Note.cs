using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib
{
    public class Note : IEquatable<Note>
    {
        public NoteName Name { get; private set; }
        public Accidental Accidental { get; private set; }

        public Note(NoteName name, Accidental accidental)
        {
            Name = name;
            Accidental = accidental;
            Simplify();
        }

        public Note(NoteName name)
        {
            Name = name;
            Accidental = Accidental.Natural;
        }

        public int ToSemitones()
        {
            return (int)Name + (int)Accidental;
        }

        void Simplify()
        {
            if (Accidental == Accidental.DoubleSharp)
            {
                Name = Name.Succesor();
                Accidental = Accidental.Natural;
            }
            else if (Accidental == Accidental.DoubleFlat)
            {
                Name = Name.Ancestor();
                Accidental = Accidental.Natural;
            }
            else if (Accidental == Accidental.Flat)
            {
                Accidental = (Name == NoteName.C || Name == NoteName.F) ? Accidental.Natural : Accidental.Sharp;
                Name = Name.Ancestor();
            }
        }

        public Note Successor()
        {
            return new Note(Name, Accidental - 1);
        }

        public Note Ancestor()
        {
            return new Note(Name, Accidental + 1);
        }

        public static Note operator +(Note n1, int i)
        {
            int sem = (n1.ToSemitones() + i) % 12;
            if (sem < 0)
                sem += 12;
            if (Enum.GetValues<NoteName>().Cast<int>().ToList().Contains(sem))
                return new Note((NoteName)sem, Accidental.Natural);
            else
                return new Note((NoteName)(sem - 1), Accidental.Sharp);
        }

        public static Note operator -(Note n1, int i)
        {
            return n1 + (-i);
        }

        public override bool Equals(object note)
        {
            if (note is not Note)
                return false;
            return Equals((Note)note);
        }

        public bool Equals(Note note)
        {
            return Name == note.Name && Accidental == note.Accidental;
        }

        public override string ToString()
        {
            return Name.GetLabel() + Accidental.GetLabel();
        }
    }
}
