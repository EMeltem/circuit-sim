using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LevelEndStar : MonoBehaviour
{
    [SerializeField] private GameObject m_Star;

    public async UniTask OpenWithAnimation(float duration = 0.25f)
    {
        m_Star.transform.localScale = Vector3.zero;
        m_Star.SetActive(true);
        m_Star.transform.DOScale(Vector3.one * 1.25f, duration)
            .SetEase(Ease.OutBack);
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
    }

    public void Close()
    {
        m_Star.SetActive(false);
    }
}