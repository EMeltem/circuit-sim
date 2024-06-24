using DG.Tweening;
using Project.Signals;
using UnityEngine;

public class Focusable : MonoBehaviour
{
    [SerializeField] private Transform m_Original;
    [SerializeField] private Transform m_Focus;
    private Outline m_Outline;
    private byte m_State = 0; // 0: Original, 1: Focus

    private void Awake()
    {
        m_Outline = GetComponent<Outline>();
        m_Outline.enabled = false;
    }

    private Sequence m_AnimationSequence;
    private bool m_IsAnimationActive;
    private void OnMouseDown()
    {
        if (m_IsAnimationActive) return;
        m_IsAnimationActive = true;
        if (m_AnimationSequence != null) m_AnimationSequence.Kill();

        if (m_State == 0) MoveToFocus();
        else if (m_State == 1) MoveToOriginal();
        GameSignals.OnFocusStateChanged(m_State);
    }

    private void MoveToFocus()
    {
        m_State = 1;
        m_AnimationSequence = DOTween.Sequence();
        m_AnimationSequence.Append(transform.DOMove(m_Focus.position, 0.75f).SetEase(Ease.OutBack));
        m_AnimationSequence.Insert(0f, transform.DORotate(m_Focus.rotation.eulerAngles, 0.75f).SetEase(Ease.OutBack));

        m_AnimationSequence.OnComplete(() =>
        {
            m_IsAnimationActive = false;
        });
    }

    private void MoveToOriginal()
    {
        m_State = 0;
        m_AnimationSequence = DOTween.Sequence();
        m_AnimationSequence.Append(transform.DOMove(m_Original.position, 0.75f));
        m_AnimationSequence.Insert(0f, transform.DORotate(m_Original.rotation.eulerAngles, 0.75f));

        m_AnimationSequence.OnComplete(() =>
        {
            m_IsAnimationActive = false;
        });
    }

    private void OnMouseEnter()
    {
        m_Outline.enabled = true;
    }

    private void OnMouseExit()
    {
        m_Outline.enabled = false;
    }
}