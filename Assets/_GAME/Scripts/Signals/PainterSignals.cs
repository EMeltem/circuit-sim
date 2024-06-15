using System;
using UnityEngine;
using UnityEngine.Events;

public static class PainterSignals
{
    public static UnityAction<ResistorColor> OnBrushSelected = delegate { };
}