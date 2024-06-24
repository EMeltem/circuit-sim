using System;
using UnityEngine;

namespace Project.Resistors
{
    [CreateAssetMenu(fileName = "ResistorData", menuName = "Database/ResistorData")]
    public class ResistorColor : ScriptableObject
    {
        public ResistorRingType AllowedTypes;
        public string Name;
        public Material Material;
        public double Value;
        public double ToleranceValue;
    }
}