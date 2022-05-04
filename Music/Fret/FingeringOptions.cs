using System;
using System.Collections.Generic;
using System.Linq;

namespace Muslib.Fret
{
    public class FingeringOptions
    {

        public int DefaultFretRange { get; set; } = 4;
        public int DefaultRequiredFingers { get; set; } = 4;

        int _fretRange;
        int _requiredFingers;
        public int FretRange { get { return _fretRange; } set { _fretRange = value; NotifyChange(); } }
        public int RequiredFingers { get { return _requiredFingers; } set { _requiredFingers = value; NotifyChange(); } }

        public event Action OnOptionsChanged;
        bool _notify = true;

        public FingeringOptions()
        {
            ResetPreferences();
        }

        public void NotifyChange()
        {
            if (_notify)
                OnOptionsChanged?.Invoke();
        }

        public void ResetPreferences()
        {
            _notify = false;
            ApplyDefault();
            _notify = true;
            NotifyChange();
        }

        void ApplyDefault()
        { 
            FretRange = DefaultFretRange;
            RequiredFingers = DefaultRequiredFingers;
        }
    }
}