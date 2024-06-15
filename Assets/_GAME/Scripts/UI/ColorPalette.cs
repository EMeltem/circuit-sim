using System.Collections.Generic;
using Project.Resistors;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public List<ResistorColor> Colors;
    public Transform Container;
    public ColorPicker ColorPickerPrefab;

    private void Start()
    {
        foreach (var color in Colors)
        {
            var picker = Instantiate(ColorPickerPrefab, Container);
            picker.Init(color);
        }
    }
}