using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public static ScreenShaker Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float duration, float magnitude)
    {
        DOTween.Shake(() => Camera.main.transform.position, pos => Camera.main.transform.position = pos, duration, magnitude, 10, 90, false);
    }
}