# Muslib

C# library for easy work with music notations and concepts.

**Provides:**

- Parsing chords from its name
- Calculating fingerings of chords for various types of string instruments

## Parsing chords

The Muslib provides an easy way to parse chord names. It supports big amount of quality types and their combinations. 

### What chords are supported?

We support a wide range of chords and their names. Namely:

- **Major** chords
- **Minor** chords
- **Diminished** chords
- **Augmented** chords
- **5th**, **6th**, **7th**, **9th** etc. chords
- **Suspended** chords
- Extended and reduced chords using notation with **add** and **omit**

**And any possible combination of the aforementioned ones.**

As an example, for a name `cmM79add11b9`, the parser returns a chord containing notes C C# D# F G and B.

This library also provides higher abstraction, in which you can build chords from its root quality parts in code along with prepared interface for parser. So one can easily create own parser to support for example different notation and pass it to the `ParseChord` method of the `Chord` class.

## Compute fingerings

Muslib is also able to compute all possible fingerings of some chord acording to given options. The options include setting of the string instrument such as its tuning and also settings of the desired fingering itself.