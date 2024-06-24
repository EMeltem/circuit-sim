using DG.Tweening;
using Project.Signals;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OnFocusBehaviours : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] m_CanvasGroups;
    [SerializeField] private Volume m_Volume;
    private DepthOfField m_Dof;

    private void Awake()
    {
        GameSignals.OnFocusStateChanged += OnFocusStateChanged;
        m_Volume.profile.TryGet(out m_Dof);
    }

    private void OnDestroy()
    {
        GameSignals.OnFocusStateChanged -= OnFocusStateChanged;
    }

    private void OnFocusStateChanged(int state)
    {
        FadeCanvasGroups(state);
        FadeDof(state);
    }

    private void FadeDof(int state, float duration = 1f)
    {
        DOTween.To(() => m_Dof.focalLength.value, x => m_Dof.focalLength.value = x, state == 0 ? 0f : 87f, duration);
    }

    private void FadeCanvasGroups(int state)
    {
        if (state == 0)
        {
            foreach (var canvasGroup in m_CanvasGroups)
            {
                canvasGroup.DOFade(1f, 0.5f);
            }
        }
        else if (state == 1)
        {
            foreach (var canvasGroup in m_CanvasGroups)
            {
                canvasGroup.DOFade(0f, 0.5f);
            }
        }
    }
}