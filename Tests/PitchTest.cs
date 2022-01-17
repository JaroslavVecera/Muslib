using NUnit.Framework;
using Music;

namespace Tests
{
    public class PitchTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(Note.C, Accidental.Natural, 1, "C1")]
        [TestCase(Note.D, Accidental.Natural, 1, "D1")]
        [TestCase(Note.E, Accidental.Sharp, 0, "E#0")]
        [TestCase(Note.F, Accidental.Flat, 1, "Fb1")]
        public void TestConstructionFromProperties(Note noteName, Accidental accidental, int octave, string expectedLabel)
        {
            Assert.AreEqual(expectedLabel, new Pitch(noteName, accidental, octave).ToString());
        }

        [Test]
        [TestCase(Note.F, 5, "F5")]
        public void TestConstructionFromPropertiesWithImplicitAccidental(Note noteName, int octave, string expectedLabel)
        {
            Assert.AreEqual(expectedLabel, new Pitch(noteName, octave).ToString());
        }

        [Test]
        [TestCase(Note.F, Accidental.Flat, 6, "Fb6")]
        public void TestCopyConstructor(Note noteName, Accidental accidental, int octave, string expectedLabel)
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
        [TestCase(Note.C, Accidental.Natural, 0, "C0")]
        [TestCase(Note.C, Accidental.Flat, 0, "B-1")]
        [TestCase(Note.C, Accidental.DoubleFlat, 0, "A#-1")]
        [TestCase(Note.E, Accidental.Sharp, 0, "F0")]
        [TestCase(Note.E, Accidental.DoubleSharp, 0, "F#0")]
        [TestCase(Note.B, Accidental.DoubleSharp, 5, "C#6")]
        public void TestSimplifying(Note noteName, Accidental accidental, int octave, string expectedLabel)
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
        [TestCase(Note.C, Accidental.Natural, 0, Note.C, Accidental.Natural, 0)]
        [TestCase(Note.B, Accidental.Sharp, 6, Note.C, Accidental.Natural, 7)]
        [TestCase(Note.F, Accidental.DoubleFlat, 4, Note.E, Accidental.Flat, 4)]
        public void TestEquality(Note noteName, Accidental accidental, int octave, Note noteName2, Accidental accidental2, int octave2)
        {
            var pitch1 = new Pitch(noteName, accidental, octave);
            var pitch2 = new Pitch(noteName2, accidental2, octave2);

            Assert.AreEqual(pitch1, pitch2);
        }

        [Test]
        [TestCase(Note.A, Accidental.Sharp, -1, Note.C, Accidental.Natural, 0)]
        [TestCase(Note.B, Accidental.Sharp, 6, Note.C, Accidental.Sharp, 7)]
        [TestCase(Note.C, Accidental.DoubleFlat, 4, Note.E, Accidental.Flat, 4)]
        public void TestComparsion(Note noteName, Accidental accidental, int octave, Note noteName2, Accidental accidental2, int octave2)
        {
            var pitch1 = new Pitch(noteName, accidental, octave);
            var pitch2 = new Pitch(noteName2, accidental2, octave2);

            Assert.IsTrue(pitch1 < pitch2);
        }
    }
}