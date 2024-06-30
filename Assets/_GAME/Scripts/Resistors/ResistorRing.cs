using DG.Tweening;
using Project.Signals;
using UnityEngine;

namespace Project.Resistors
{
    public class ResistorRing : MonoBehaviour
    {
        public ResistorRingType Type;
        public ResistorColor ResistorColorData;
        [SerializeField] private MeshRenderer m_MeshRenderer;
        private Outline m_Outline;

        private void Awake()
        {
            PainterSignals.OnBrushSelected += OnBrushSelected;
        }

        private void OnDestroy()
        {
            PainterSignals.OnBrushSelected -= OnBrushSelected;
        }

        [SerializeField] private bool m_AutoLoadOnStart = false;
        private void LoadData()
        {
            if (!m_AutoLoadOnStart) return;
            m_MeshRenderer.material = ResistorColorData.Material;
        }

        private bool m_IsListening = false;
        private bool m_TypeIsValid;
        private void OnBrushSelected(ResistorColor data)
        {
            m_TypeIsValid = (data.AllowedTypes & Type) == Type;
            m_Outline.enabled = true;
            m_IsListening = true;
        }

        private void Start()
        {
            m_Outline = GetComponent<Outline>();
            m_Outline.enabled = false;
            LoadData();
        }

        private void OnMouseDown()
        {
            if (!m_IsListening) return;
            if (!m_TypeIsValid) return;
            ResistorColorData = PainterSignals.GetBrushData?.Invoke();
            m_MeshRenderer.material = ResistorColorData.Material;
        }

        private Tween m_ScaleTween;
        private void OnMouseEnter()
        {
            if (!m_IsListening) return;
            KillTweenIfIsAlive();
            m_ScaleTween = transform.DOScale(Vector3.one * 1.2f, 0.15f);
            m_Outline.OutlineColor = m_TypeIsValid ? Color.green : Color.red;
        }

        private void OnMouseExit()
        {
            if (!m_IsListening) return;
            KillTweenIfIsAlive();
            m_ScaleTween = transform.DOScale(Vector3.one, 0.15f);
            m_Outline.OutlineColor = Color.white;
        }

        private void KillTweenIfIsAlive()
        {
            if (m_ScaleTween != null && m_ScaleTween.IsActive())
            {
                m_ScaleTween.Kill();
            }
        }
    }
}