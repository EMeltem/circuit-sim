using System;
using UnityEngine;

namespace Project.Resistors
{
    [CreateAssetMenu(fileName = "ResistorData", menuName = "Database/ResistorData")]
    public class ResistorColor : ScriptableObject
    {
        public ResistorRingType Type;
        public string Name;
        public Material Material;
        public int Value;
        public double Multiplier => Math.Pow(10, Value);
    }
}