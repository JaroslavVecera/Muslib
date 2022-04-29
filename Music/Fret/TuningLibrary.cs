using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Fret
{
    class TuningLibrary
    {
        public string Name { get; set; }
        public List<Tuning> Tunings { get; set; }

        public Tuning this[string name] { get { return Tunings.FirstOrDefault(t => t.Name == name); } }

        public void Add(Tuning tuning)
        {
            if (Tunings.Any(t => t.Name == tuning.Name))
                throw new ArgumentException("Library already contains tuning with name \"" + tuning.Name + "\"");
            Tunings.Add(tuning);
        }

        public static TuningLibrary StandardLibrary { get { return CreateStandardLibrary(); } }

        static TuningLibrary CreateStandardLibrary()
        {
            TuningLibrary library = new TuningLibrary();
            library.Add(new Tuning(new List<Pitch>() { new Pitch(NoteName.E, 2), new Pitch(NoteName.A, 2), new Pitch(NoteName.D, 3),
                new Pitch(NoteName.G, 3), new Pitch(NoteName.B, 3), new Pitch(NoteName.C, 4)}, "Standard E"));
            return library;
        }
    }
}
