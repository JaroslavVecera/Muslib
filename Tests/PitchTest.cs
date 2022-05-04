using NUnit.Framework;
using Muslib;

namespace Tests
{
    public class PitchTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(NoteName.C, Accidental.Natural, 1, "C1")]
        [TestCase(NoteName.D, Accidental.Natural, 1, "D1")]
        [TestCase(NoteName.E, Accidental.Sharp, 0, "E#0")]
        [TestCase(NoteName.F, Accidental.Flat, 1, "Fb1")]
        public void TestConstructionFromProperties(NoteName noteName, Accidental accidental, int octave, string expectedLabel)
        {
            Assert.AreEqual(expectedLabel, new Pitch(noteName, accidental, octave).ToString());
        }

        [Test]
        [TestCase(NoteName.F, 5, "F5")]
        public void TestConstructionFromPropertiesWithImplicitAccidental(NoteName noteName, int octave, string expectedLabel)
        {
            Assert.AreEqual(expectedLabel, new Pitch(noteName, octave).ToString());
        }

        [Test]
        [TestCase(NoteName.F, Accidental.Flat, 6, "Fb6")]
        public void TestCopyConstructor(NoteName noteName, Accidental accidental, int octave, string expectedLabel)
        {
            Assert.AreEqual(expectedLabel, new Pitch(new Pitch(noteName, accidental, octave)).ToString());
        }

        [Test]
        [TestCase(0, "C0")]
        [TestCase(54, "F#4")]
        public void TestConstructionFromSemitones(int semitones, string expectedLabel)
        {
            Assert.AreEqual(expectedLabel, new Pitch(semitones).ToString());
        }

        [Test]
        [TestCase(NoteName.C, Accidental.Natural, 0, "C0")]
        [TestCase(NoteName.C, Accidental.Flat, 0, "B-1")]
        [TestCase(NoteName.C, Accidental.DoubleFlat, 0, "A#-1")]
        [TestCase(NoteName.E, Accidental.Sharp, 0, "F0")]
        [TestCase(NoteName.E, Accidental.DoubleSharp, 0, "F#0")]
        [TestCase(NoteName.B, Accidental.DoubleSharp, 5, "C#6")]
        public void TestSimplifying(NoteName noteName, Accidental accidental, int octave, string expectedLabel)
        {
            var pitch = new Pitch(noteName, accidental, octave);
            pitch.SimplifyEnharmonically();
            Assert.IsTrue(pitch.IsEnharmonicallySimplified);
            Assert.AreEqual(expectedLabel, pitch.ToString());
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(0, -5)]
        [TestCase(1, -13)]
        [TestCase(90, -125)]
        [TestCase(-80, 100)]
        public void TestTransposition(int semitones, int transposition)
        {
            var pitch = new Pitch(semitones);
            pitch.Transpose(transposition);
            Assert.AreEqual(semitones + transposition, pitch.Semitones);
        }

        [Test]
        [TestCase(NoteName.C, Accidental.Natural, 0, NoteName.C, Accidental.Natural, 0)]
        [TestCase(NoteName.B, Accidental.Sharp, 6, NoteName.C, Accidental.Natural, 7)]
        [TestCase(NoteName.F, Accidental.DoubleFlat, 4, NoteName.E, Accidental.Flat, 4)]
        public void TestEquality(NoteName noteName, Accidental accidental, int octave, NoteName noteName2, Accidental accidental2, int octave2)
        {
            var pitch1 = new Pitch(noteName, accidental, octave);
            var pitch2 = new Pitch(noteName2, accidental2, octave2);

            Assert.AreEqual(pitch1, pitch2);
        }

        [Test]
        [TestCase(NoteName.A, Accidental.Sharp, -1, NoteName.C, Accidental.Natural, 0)]
        [TestCase(NoteName.B, Accidental.Sharp, 6, NoteName.C, Accidental.Sharp, 7)]
        [TestCase(NoteName.C, Accidental.DoubleFlat, 4, NoteName.E, Accidental.Flat, 4)]
        public void TestComparsion(NoteName noteName, Accidental accidental, int octave, NoteName noteName2, Accidental accidental2, int octave2)
        {
            var pitch1 = new Pitch(noteName, accidental, octave);
            var pitch2 = new Pitch(noteName2, accidental2, octave2);

            Assert.IsTrue(pitch1 < pitch2);
        }
    }
}