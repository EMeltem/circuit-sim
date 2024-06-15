using System.Collections.Generic;
using UnityEngine;


namespace Project.Resistors
{
    public enum ResistorRingType { NONE, ABC, T }
    public class Resistor : MonoBehaviour
    {
        [SerializeField] private List<ResistorRing> m_ResistorRings;
    }
}