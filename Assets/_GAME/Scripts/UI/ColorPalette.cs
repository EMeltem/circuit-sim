using System.Collections.Generic;
using Project.Resistors;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public List<ResistorColor> ColorData;
    public Transform Container;
    [Header("Prefabs")]
    public ColorPicker ColorPickerPrefab;
    public GameObject m_Seperator;

    private void Start()
    {
        var _abcData = ColorData.FindAll(x => x.Type == ResistorRingType.ABC);
        var _tData = ColorData.FindAll(x => x.Type == ResistorRingType.T);

        foreach (var color in _abcData)
        {
            var picker = Instantiate(ColorPickerPrefab, Container);
            picker.Init(color);
        }
        Instantiate(m_Seperator, Container);
        foreach (var color in _tData)
        {
            var picker = Instantiate(ColorPickerPrefab, Container);
            picker.Init(color);
        }
    }
}