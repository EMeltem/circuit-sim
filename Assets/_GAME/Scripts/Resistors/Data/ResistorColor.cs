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
        public float Value;
        public float ToleranceValue;
    }
}