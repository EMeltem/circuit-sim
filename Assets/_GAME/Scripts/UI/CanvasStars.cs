using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.UI;

public class CanvasStars : MonoBehaviour
{
    public static CanvasStars Instance { get; private set; }
    [SerializeField] private List<GameObject> m_Stars;

    private void Awake()
    {
        Instance = this;
    }

    [Button("Test Star Lose")]
    public void OnStarLose()
    {
        if (m_Stars.Count == 0) return;
        var _star = m_Stars[0];
        m_Stars.RemoveAt(0);
        _star.transform.DOScale(Vector3.one * 1.25f, 0.25f)
        .SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            _star.transform.DOScale(Vector3.zero, 0.2f)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                Destroy(_star);
            });
        });
    }

    public int GetStartCount()
    {
        return m_Stars.Count;
    }
}