using System;
using DG.Tweening;
using Project.Signals;
using UnityEngine;

namespace Project.Resistors
{
    public class ResistorRing : MonoBehaviour
    {
        [SerializeField] private ResistorRingType m_Type;
        [SerializeField] private ResistorColor m_ResistorColorData;
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

        private bool m_IsListening = false;
        private void OnBrushSelected(ResistorColor data)
        {
            var _selectState = data.Type == m_Type;
            m_Outline.enabled = _selectState;
            m_IsListening = _selectState;
        }

        private void Start()
        {
            m_Outline = GetComponent<Outline>();
            m_Outline.enabled = false;
        }

        private void OnMouseDown()
        {
            if (!m_IsListening) return;
            m_ResistorColorData = PainterSignals.GetBrushData?.Invoke();
            m_MeshRenderer.material = m_ResistorColorData.Material;
        }

        private ResistorColor m_OnEnterData;
        private Tween m_ScaleTween;
        private void OnMouseEnter()
        {
            if (!m_IsListening) return;
            KillTweenIfIsAlive();
            m_ScaleTween = transform.DOScale(Vector3.one * 1.2f, 0.15f);
            m_OnEnterData = PainterSignals.GetBrushData?.Invoke();
            m_Outline.OutlineColor = m_OnEnterData.Type == m_Type ? Color.green : Color.red;
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