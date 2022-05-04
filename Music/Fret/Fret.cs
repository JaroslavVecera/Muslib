using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib.Fret
{
    class Fret
    {
        public FingeringOptions Options { get; private set; }
        public int MaxFret { get; set; }
        public Tuning Tuning { get; set; }

        public Fret(FingeringOptions opt, Tuning tuning, int maxFret)
        {
            Options = opt;
            Tuning = tuning;
            MaxFret = maxFret;
        }
         
        public List<Pattern> ListPatterns(List<Note> notes, Note lowest)
        {
            if (notes == null || notes.Count == 0)
                return null;
            if (!notes.Contains(lowest))
                throw new ArgumentException("Argument notes does not cantain " + lowest + ".");
            List<int>[] allPositions = new List<int>[Tuning.Strings];

            for (int i = 0; i < Tuning.Strings; i++)
            {
                allPositions[i] = new List<int>() { -1 };
                for (int pos = 0; pos <= MaxFret; pos++)
                    if (notes.Contains(Tuning[i].Note + pos))
                        allPositions[i].Add(pos);
            }
            return GeneratePatterns(notes, allPositions, lowest);
        }

        List<Pattern> GeneratePatterns(List<Note> notes, List<int>[] positions, Note lowest)
        { 
            List<Pattern> res = new List<Pattern>();
            GeneratePatternsRec(-1, -1, new List<int>(), positions, res);
            res.RemoveAll(pattern => !ProperChordPattern(notes, pattern, lowest));
            res.Sort(new Comparison<Pattern>(ComparePatterns));
            return res;
        }

        int ComparePatterns(Pattern p1, Pattern p2)
        {
            if (p1.Min < p2.Min)
                return -1;
            else if (p1.Min > p2.Min)
                return 1;
            else if (p1.Unmuted > p2.Unmuted)
                return -1;
            else return 1;
        }

        void GeneratePatternsRec(int min, int max, List<int> pattern, List<int>[] positions, List<Pattern> result)
        {
            int newmin = min, newmax = max;
            if (pattern.Count == Tuning.Strings)
                result.Add(new Pattern(pattern));
            else
            {
                foreach (int p in positions[pattern.Count])
                {
                    if (p > 0)
                    {
                        if (min == -1)
                            newmax = newmin = p;
                        else
                        {
                            newmin = Math.Min(min, p);
                            newmax = Math.Max(max, p);
                        }
                        if (Math.Abs(newmin - newmax) > Options.FretRange)
                            continue;
                    }
                    pattern.Add(p);
                    GeneratePatternsRec(newmin, newmax, pattern, positions, result);
                    pattern.RemoveAt(pattern.Count - 1);
                }
            }
        }

        bool ProperChordPattern(List<Note> notes, Pattern p, Note lowest)
        {
            return p.Unmuted > 2 && IsInRange(p)  && ContainsAllNotes(notes, p) && IsUniform(p) && IsRootLowest(lowest, p) && SetFingersAreEnaught(p);
        }

        bool ContainsAllNotes(List<Note> notes, Pattern p)
        {
            List<Note> paternNotes = PatternToPitches(p).Select(p => p.Note).ToList();
            return notes.All(n => paternNotes.Contains(n));
        }

        bool IsInRange(Pattern p)
        {
            return p.Max - p.Min + 1 <= Options.FretRange;
        }

        bool IsUniform(Pattern p)
        {
            int state = 0;
            for(int i = 0; i < Tuning.Strings; i++)
                if ((state % 2 == 0 && p[i] > -1) || (state % 2 == 1 && p[i] == -1))
                    state++;
            return state < 3;
        }

        bool IsRootLowest(Note desiredLowest, Pattern p)
        {
            Pitch lowestPitch = PatternToPitches(p).OrderBy(pitch => pitch.Semitones).First();
            return lowestPitch.Note == desiredLowest;
        }

        bool SetFingersAreEnaught(Pattern p)
        {
            int count = p.Unmuted;
            if (count <= Options.RequiredFingers)
            {
                if (count >= Options.RequiredFingers && SetFingersAreEnaughtWithBarre(p) && p.Count(f => f == p.Min) > 1)
                    p.Barre = true;
                return true;
            }
            p.Barre = true;
            return SetFingersAreEnaughtWithBarre(p);
        }

        bool SetFingersAreEnaughtWithBarre(Pattern p)
        {
            int min = p.Min;
            int barreIndex = p.IndexOf(min);
            return p.CanBarreFrom(barreIndex) && (p.Count(f => f > min) + 1 <= Options.RequiredFingers);
        }

        List<Pitch> PatternToPitches(Pattern p)
        {
            List<Pitch> pitches = new List<Pitch>();
            for (int i = 0; i < p.Size; i++)
                if (p[i] > -1)
                {
                    Pitch pitch = new Pitch(Tuning[i]);
                    pitch.Transpose(p[i]);
                    pitches.Add(pitch);
                }
            return pitches;
        }
    }
}
