using System;
using Project.Resistors;
using UnityEngine.Events;

namespace Project.Signals
{
    public static class PainterSignals
    {
        public static UnityAction<ResistorColor> OnBrushSelected = delegate { };
        public static Func<ResistorColor> GetBrushData = delegate { return null; };
    }
}