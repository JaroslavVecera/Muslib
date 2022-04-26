using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    class ModFinder
    {
        Dictionary<List<Modifier>, List<int>> _seventh = new Dictionary<List<Modifier>, List<int>>();
        Dictionary<List<Modifier>, List<int>> _sixth = new Dictionary<List<Modifier>, List<int>>();
        Dictionary<List<Modifier>, List<int>> _simple = new Dictionary<List<Modifier>, List<int>>();

        public ModFinder()
        {
            AddSimple();
            AddSixth();
            AddSeventh();
        }

        public List<int> FindSimple(List<Modifier> mods)
        {
            return Find(mods, _simple);
        }

        public List<int> FindSeventh(List<Modifier> mods)
        {
            return Find(mods, _seventh);
        }

        public List<int> FindSixth(List<Modifier> mods)
        {
            return Find(mods, _sixth);
        }

        List<int> Find(List<Modifier> mods, Dictionary<List<Modifier>, List<int>> dict)
        {
            var key = dict.Keys.FirstOrDefault(mods1 => mods.SequenceEqual(mods1));
            if (key == null)
                return null;
            return dict[key];
        }

        public void AddSeventh()
        {
            _seventh.Add(new List<Modifier>(), new List<int>());
            _seventh.Add(new List<Modifier>() { Modifier.Major }, new List<int>() { 0, 0, 0, 1 });
            _seventh.Add(new List<Modifier>() { Modifier.Minor }, new List<int>() { 0, -1 });
            _seventh.Add(new List<Modifier>() { Modifier.Minor, Modifier.Major }, new List<int>() { 0, -1, 0, 1 });
            _seventh.Add(new List<Modifier>() { Modifier.Augmented }, new List<int>() { 0, 0, 1 });
            _seventh.Add(new List<Modifier>() { Modifier.Diminished }, new List<int>() { 0, -1, -1, -1 });
            _seventh.Add(new List<Modifier>() { Modifier.Diminished, Modifier.Major }, new List<int>() { 0, -1, -1, 1 });
            _seventh.Add(new List<Modifier>() { Modifier.Augmented, Modifier.Major }, new List<int>() { 0, 0, 1, 1 });
        }

        public void AddSixth()
        {
            _sixth.Add(new List<Modifier>(), new List<int>());
            _sixth.Add(new List<Modifier>() { Modifier.Major }, new List<int>());
            _sixth.Add(new List<Modifier>() { Modifier.Minor }, new List<int>() { 0, -1 });
            _sixth.Add(new List<Modifier>() { Modifier.Augmented }, new List<int>() { 0, 0, 1 });
            _sixth.Add(new List<Modifier>() { Modifier.Diminished }, new List<int>() { 0, -1, -1 });
            _sixth.Add(new List<Modifier>() { Modifier.Minor, Modifier.Major }, new List<int>() { 0, -1 });
            _sixth.Add(new List<Modifier>() { Modifier.Diminished, Modifier.Major }, new List<int>() { 0, -1, -1 });
            _sixth.Add(new List<Modifier>() { Modifier.Augmented, Modifier.Major }, new List<int>() { 0, 0, 1 });
        }

        public void AddSimple()
        {
            _simple.Add(new List<Modifier>(), new List<int>());
            _simple.Add(new List<Modifier>() { Modifier.Major }, new List<int>());
            _simple.Add(new List<Modifier>() { Modifier.Minor }, new List<int>() { 0, -1 });
            _simple.Add(new List<Modifier>() { Modifier.Augmented }, new List<int>() { 0, 0, 1 });
            _simple.Add(new List<Modifier>() { Modifier.Diminished }, new List<int>() { 0, -1, -1 });
        }
    }
}
