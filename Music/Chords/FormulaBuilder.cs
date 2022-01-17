using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Chords
{
    internal class FormulaBuilder
    {
        ModifiableFormula Formula { get; set; }

        internal FormulaBuilder()
        {
            Formula = new ModifiableFormula();
        }

        
    }
}
