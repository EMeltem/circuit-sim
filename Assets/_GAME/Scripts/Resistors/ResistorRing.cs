using Project.Signals;
using UnityEngine;

namespace Project.Resistors
{
    public class ResistorRing : MonoBehaviour
    {
        [SerializeField] private ResistorColor m_ResistorColor;
        [SerializeField] private MeshRenderer m_MeshRenderer;
        private void OnMouseDown()
        {
            m_ResistorColor = PainterSignals.GetBrushData?.Invoke();
            m_MeshRenderer.material = m_ResistorColor.Material;
            Debug.Log($"ResistorRing.OnMouseDown: {m_ResistorColor.name}");
        }
    }
}