using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Muslib.Fret
{
    public class FingeringGroup : INotifyPropertyChanged
    {
        public Fingering Full { get; set; }
        public List<Fingering> Subsets { get; set; } = new List<Fingering>();
        public bool AnySubsets { get { return Subsets.Count > 1; } }

        public FingeringGroup(Fingering full)
        {
            Full = full;
            Add(Full);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(Fingering f)
        {
            Subsets.Add(f);
        }

        public bool IsSubset(Pattern p)
        {
            return Full.Pattern.IsSubset(p);
        }
    }
}
