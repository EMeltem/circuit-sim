using System.Collections.Generic;
using Project.Resistors;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public List<ResistorColor> AbcData;
    public List<ResistorColor> TData;
    public Transform Container;
    [Header("Prefabs")]
    public ColorPicker ColorPickerPrefab;
    public GameObject m_Seperator;

    private void Start()
    {
        foreach (var color in AbcData)
        {
            var picker = Instantiate(ColorPickerPrefab, Container);
            picker.Init(color);
        }
        Instantiate(m_Seperator, Container);
        foreach (var color in TData)
        {
            var picker = Instantiate(ColorPickerPrefab, Container);
            picker.Init(color);
        }
    }
}