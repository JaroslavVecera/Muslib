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

    }
}