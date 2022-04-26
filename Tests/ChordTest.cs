using NUnit.Framework;
using Music;
using Music.Chords;
using System.Collections.Generic;

namespace Tests
{
    public class ChordTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("C", "C E G")]
        [TestCase("B", "D# F# B")]
        [TestCase("C#", "C# F G#")]
        [TestCase("Cb", "D# F# B")]
        [TestCase("d#", "D# G A#")]
        [TestCase("db", "C# F G#")]
        public void SimpleChords(string name, string notes)
        {
            Chord chord = Chord.ParseChord(name);
            Assert.AreEqual(notes, chord.ToString());
        }

        [Test]
        [TestCase("I")]
        [TestCase("+")]
        [TestCase("C##")]
        [TestCase("C//")]
        [TestCase("d/")]
        [TestCase("d #")]
        public void SimpleChordsFail(string name)
        {
            Chord chord = Chord.ParseChord(name);
            Assert.IsNull(chord);
        }

        [Test]
        [TestCase("C/c", "C E G")]
        [TestCase("C/B", "C E G B")]
        [TestCase("C/C#", "C C# E G")]
        [TestCase("e/fb", "E G# B")]
        [TestCase("e / fb ", "E G# B")]
        public void BassChords(string name, string notes)
        {
            Chord chord = Chord.ParseChord(name);
            Assert.AreEqual(notes, chord.ToString());
        }

        [Test]
        [TestCase("e / f b ")]
        [TestCase("e / fbf ")]
        [TestCase("e / fb f ")]
        [TestCase("e / + ")]
        public void BassChordsFail(string name)
        {
            Chord chord = Chord.ParseChord(name);
            Assert.IsNull(chord);
        }

        [Test]
        [TestCase("c7 ", "C E G A#")]
        [TestCase(" c9 ", "C D E G A#")]
        [TestCase(" c79 ", "C D E G A#")]
        [TestCase(" c11 ", "C E F G A#")]
        [TestCase(" c711 ", "C E F G A#")]
        [TestCase(" c7911 ", "C D E F G A#")]
        [TestCase(" c911 ", "C D E F G A#")]
        [TestCase(" c13 ", "C E G A A#")]
        [TestCase(" c713 ", "C E G A A#")]
        [TestCase(" c7913 ", "C D E G A A#")]
        [TestCase(" c913 ", "C D E G A A#")]
        [TestCase(" c71113 ", "C E F G A A#")]
        [TestCase(" c791113 ", "C D E F G A A#")]
        [TestCase(" c91113 ", "C D E F G A A#")]
        public void ExtendedChords(string name, string notes)
        {
            Chord chord = Chord.ParseChord(name);
            Assert.AreEqual(notes, chord.ToString());
        }

        [Test]
        [TestCase("c10 ")]
        [TestCase("c7 15 ")]
        [TestCase("c2 ")]
        public void ExtendedChordsFail(string name)
        {
            Chord chord = Chord.ParseChord(name);
            Assert.IsNull(chord);
        }
    }
}