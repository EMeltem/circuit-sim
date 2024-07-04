using Project.Resistors;
using Project.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Painter
{
    public class Brush : MonoBehaviour
    {
        [SerializeField] private Image m_Image;
        private void Awake()
        {
            PainterSignals.OnBrushSelected += OnBrushSelected;
            PainterSignals.GetBrushData += GetBrushData;
        }

        private void OnDestroy()
        {
            PainterSignals.OnBrushSelected -= OnBrushSelected;
            PainterSignals.GetBrushData -= GetBrushData;
        }

        private ResistorColor GetBrushData()
        {
            return m_ResistorColorData;
        }

        private ResistorColor m_ResistorColorData;
        private void OnBrushSelected(ResistorColor data)
        {
            m_ResistorColorData = data;
            m_Image.color = data.Material.color;
        }

        private void Update()
        {
            m_Image.transform.position = Input.mousePosition;
        }
    }
}