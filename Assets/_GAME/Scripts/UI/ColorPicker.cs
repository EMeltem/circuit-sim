using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        transform.DOScale(Vector3.one * 0.9f, 0.15f)
            .OnComplete(() => transform.DOScale(Vector3.one, 0.15f));
    }
}