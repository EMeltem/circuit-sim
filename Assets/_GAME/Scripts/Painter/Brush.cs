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
        }

        private void OnDestroy()
        {
            PainterSignals.OnBrushSelected -= OnBrushSelected;
        }

        private void OnBrushSelected(ResistorColor data)
        {
            m_Image.color = data.Material.color;
        }

        private void Update()
        {
            // Set the image position to the mouse position
            m_Image.transform.position = Input.mousePosition;
        }
    }
}