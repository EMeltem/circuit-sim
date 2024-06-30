using System;
using System.Collections.Generic;
using Project.Utilities;
using UnityEngine;

namespace Project.Resistors
{
    [Flags]
    public enum ResistorRingType
    {
        NONE = 0,
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2,
        T = 1 << 3
    }

    public class Resistor : ElectrictyElement
    {
        public static Resistor Instance { get; private set; }
        [SerializeField] private List<ResistorRing> m_ResistorRings;

        private void Awake()
        {
            Instance = this;
            Cursor.visible = false;
        }

        public bool Spawned { get; private set; }
        public void OnSpawn()
        {
            Spawned = true;
        }

        public override (double resistor, double tolerance) CalculateResistance()
        {
            var A = m_ResistorRings.Find(ring => ring.Type == ResistorRingType.A).ResistorColorData;
            var B = m_ResistorRings.Find(ring => ring.Type == ResistorRingType.B).ResistorColorData;
            var C = m_ResistorRings.Find(ring => ring.Type == ResistorRingType.C).ResistorColorData;
            var T = m_ResistorRings.Find(ring => ring.Type == ResistorRingType.T).ResistorColorData;
            return Utils.CalculateResistance(A, B, C, T);
        }

        public void DestroyWithConnections()
        {
            if (StartPoint != null)
            {
                var _garbage = new List<ElectrictyElement>();
                var _elements = new List<ElectrictyElement>(StartPoint.ElectrictyElements);
                foreach (var element in _elements)
                {
                    if (element == null) continue;
                    element.EndPoint.ElectrictyElements.Remove(this);
                    element.StartPoint.ElectrictyElements.Remove(this);
                    if (element == this) continue;
                    _garbage.Add(element);
                }
                _garbage.ForEach(e => Destroy(e.gameObject));
            }

            if (EndPoint != null)
            {
                var _garbage = new List<ElectrictyElement>();
                var _elements = new List<ElectrictyElement>(EndPoint.ElectrictyElements);
                foreach (var element in _elements)
                {
                    if (element == null) continue;
                    element.StartPoint.ElectrictyElements.Remove(this);
                    element.EndPoint.ElectrictyElements.Remove(this);
                    if (element == this) continue;
                    _garbage.Add(element);
                }
                _garbage.ForEach(e => Destroy(e.gameObject));
            }

            Destroy(gameObject);
        }
    }
}