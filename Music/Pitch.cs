using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public struct Pitch : IComparable<Pitch>, IEquatable<Pitch>
    {
        public Note NoteName { get; set; }
        public Accidental Accidental { get; set; }
        public int Octave { get; set; }
        public int Semitones { get { return 12 * Octave + (int)NoteName + (int)Accidental; } }
        public bool IsEnharmonicallySimplified { get { return Accidental == Accidental.Natural || Accidental == Accidental.Sharp; } }

        public Pitch(int semitones)
        {
            int indexOfLowerC = (semitones / 12) * 12;
            if (semitones < 0)
                indexOfLowerC -= 12;
            int differenceFromLowerC = semitones - indexOfLowerC;
            bool isNatural = Enum.IsDefined(typeof(Note), differenceFromLowerC);
            int indexOfNaturalTone = isNatural ? differenceFromLowerC : differenceFromLowerC - 1;

            NoteName = (Note)indexOfNaturalTone;
            Octave = indexOfLowerC / 12;
            Accidental = isNatural ? Accidental.Natural : Accidental.Sharp;
        }

        public Pitch(Note noteName, Accidental accidental, int octave)
        {
            NoteName = noteName;
            Octave = octave;
            Accidental = accidental;
        }

        public Pitch(Note noteName, int octave) : this(noteName, Accidental.Natural, octave) { }

        public Pitch(Pitch pitch) : this(pitch.NoteName, pitch.Accidental, pitch.Octave) { }

        public void SimplifyEnharmonically()
        {
            Pitch simplification = new Pitch(Semitones);
            CopyFromPitch(simplification);
        }

        public void Transpose(int semitones)
        {
            if (semitones == 0)
                return;
            Pitch transposed = new Pitch(Semitones + semitones);
            CopyFromPitch(transposed);
        }

        void CopyFromPitch(Pitch pitch)
        {
            NoteName = pitch.NoteName;
            Accidental = pitch.Accidental;
            Octave = pitch.Octave;
        }

        public override bool Equals(object pitch)
        {
            if (pitch is not Pitch)
                return false;
            return Equals((Pitch)pitch);
        }

        public bool Equals(Pitch other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return Semitones;
        }

        public int CompareTo(Pitch other)
        {
            return Semitones.CompareTo(other.Semitones);
        }

        public static bool operator ==(Pitch left, Pitch right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Pitch left, Pitch right)
        {
            return !Equals(left, right);
        }

        public static bool operator <(Pitch left, Pitch right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >(Pitch left, Pitch right)
        {
            return left.CompareTo(right) == 1;
        }

        public override string ToString()
        {
            return NoteExtensions.GetLabel(NoteName) + AccidentalExtensions.GetLabel(Accidental) + Octave;
        }
    }
}
