using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class FormulaBuilder
    {
        ModifiableFormula Formula { get; set; }
        List<Modifier> Modifiers { get; set; } = new List<Modifier>();
        List<Extention> Extentions { get; set; } = new List<Extention>();
        bool Fifth { get; set; } = false;

        QualityMemberType State { get; set; }
        bool CanSus { get { return !Modifiers.Any(mod => mod != Modifier.Major); } }
        List<Sus> Suss { get; set; } = new List<Sus>();
        bool BuildedAlts { get; set; }
        Dictionary<int, List<NonzeroAccidental>> Alts { get; set; } = new Dictionary<int, List<NonzeroAccidental>>();

        public void BuildSus(Sus sus)
        {
            if (CanSus)
            {
                int susDiff = sus == Sus.Sus2 ? -2 : 1;
                if (Suss.Count == 0)
                    Formula.RemoveThird();
                if (Suss.Contains(sus) || !Formula.Add(Chords.Formula.ToSemitones(3) + susDiff))
                    Error = true;
                Suss.Add(sus);
            }
            else
                Error = true;
        }

        public bool Error { get; private set; } = false;
        bool BuildedModifiers { get; set; } = false;

        public FormulaBuilder()
        {
            Clear();
        }

        public Formula GetResult()
        {
            return Formula;
        }

        public void Clear()
        {
            Formula = new ModifiableFormula();
            Modifiers = new List<Modifier>();
            Extentions = new List<Extention>();
            Fifth = false;
            State = QualityMemberType.Modifier;
            Suss = new List<Sus>();
            BuildedAlts = false;
            Alts = new Dictionary<int, List<NonzeroAccidental>>();
            Error = false;
            BuildedModifiers = false;
        }

        public void Complete()
        {
            BuildModifiers();
            BuildAlts();
        }

        public void IncreaseState(QualityMemberType state)
        {
            bool stateIsLesser = state < State;
            State = state;
            if (stateIsLesser)
                Error = true;
            else
                DoStateSpecificBehaviour();
        }

        public void AddModifier(Modifier modifier)
        {
            if (Modifiers.Count == 2 || Modifiers.Contains(modifier))
                Error = true;
            else
                Modifiers.Add(modifier);
        }

        public void Extend(Extention ext, Accidental accidental)
        {
            if (ext == Extention.fifth)
                MakeFifth(accidental);
            else
                ExtendPositive(ext, accidental);
        }

        void MakeFifth(Accidental accidental)
        {
            if (Fifth || Extentions.Count > 0 || accidental != Accidental.Natural)
                Error = true;
            Fifth = true;
        }

        void ExtendPositive(Extention ext, Accidental accidental)
        {
            if (Fifth || (Extentions.Count > 0 && ext <= Extentions.Last()))
            {
                Error = true;
                return;
            }
            if (Extentions.Count == 0)
            {
                if (ext == Extention.sixth || ext == Extention.seventh)
                {
                    if (accidental != 0)
                        Error = true;
                }
                else
                    Formula.Add(Chords.Formula.ToSemitones(7));
            }
            Extentions.Add(ext);
            Formula.Add((int)(Chords.Formula.ToSemitones((int)ext) + accidental));
        }

        public void DoStateSpecificBehaviour()
        {
            if (State > QualityMemberType.Extention)
                BuildModifiers();
            if (State > QualityMemberType.Alt)
                BuildAlts();
        }

        void BuildModifiers()
        {
            if (BuildedModifiers)
                return;
            else
                BuildedModifiers = true;
            ModFinder finder = new ModFinder();
            List<int> mask = new List<int>();
            if (Extentions.Count == 0)
                mask = finder.FindSimple(Modifiers);
            else if (Extentions.First() == Extention.sixth)
                mask = finder.FindSixth(Modifiers);
            else
                mask = finder.FindSeventh(Modifiers);

            if (mask == null)
                Error = true;
            else
                Formula.AddMask(mask);
        }

        void BuildAlts()
        {
            if (BuildedAlts)
                return;
            else
                BuildedAlts = true;
            foreach (var pair in Alts)
            {
                int flats = 0;
                int sharps = 0;
                pair.Value.ForEach(val =>
                {
                    if (val > 0)
                        sharps += (int)val;
                    else
                        flats += (int)val;
                });
                if (!Formula.Remove(Chords.Formula.ToSemitones(pair.Key))
                 || !Formula.Add(Chords.Formula.ToSemitones(pair.Key) + flats)
                 || !Formula.Add(Chords.Formula.ToSemitones(pair.Key) + sharps))
                    Error = true;
            }
        }

        public void Alt(int nth, NonzeroAccidental accidental)
        {
            if (nth < 2)
                Error = true;
            if (Alts[nth] == null)
                Alts[nth] = new List<NonzeroAccidental>();
            Alts[nth].Add(accidental);
        }

        public void Add(int nth, Accidental accidental)
        {
            if (!Formula.Add(Chords.Formula.ToSemitones(nth) + (int)accidental))
                Error = true;
        }

        public void Omit(int nth, Accidental accidental)
        {
            if (!Formula.Remove(Chords.Formula.ToSemitones(nth) + (int)accidental))
                Error = true;
        }
    }
}
