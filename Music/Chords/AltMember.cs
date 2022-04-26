﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    public class AltMember : QualityMember
    {
        public override QualityMemberType Type { get { return QualityMemberType.Alt; } }

        public NonzeroAccidental Accidental { get; private set; }
        public int Nth { get; private set; }

        public AltMember(int nth, NonzeroAccidental acc)
        {
            Nth = nth;
            Accidental = acc;
        }

        protected override void DoDirectBuilder(FormulaBuilder builder)
        {
            builder.Alt(Nth, Accidental);
        }
    }
}