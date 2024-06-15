using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class ColorPicker : MonoBehaviour
{
    public ResistorColor ColorData;
    public Image Image;

    public void Init(ResistorColor color)
    {
        ColorData = color;
        SetColor();
    }

    private void SetColor()
    {
        Image.color = ColorData.Material.color;
    }

    public void OnClick()
    {
        ClickAnimation();
        SendBurshsignal();
    }

    private void ClickAnimation()
    {
        transform.DOScale(Vector3.one * 0.9f, 0.15f)
            .OnComplete(() => transform.DOScale(Vector3.one, 0.15f));
    }

    private void SendBurshsignal()
    {
        PainterSignals.OnBrushSelected?.Invoke(ColorData);
    }
}