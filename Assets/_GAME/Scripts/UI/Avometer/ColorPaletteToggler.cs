using Project.Avometer;
using Project.Signals;
using UnityEngine;

public class ColorPaletteToggler : MonoBehaviour
{
    [SerializeField] private GameObject m_ColorPalette;
    public bool Active => m_ColorPalette.activeSelf;
    public static ColorPaletteToggler Instance;

    private void Awake()
    {
        m_ColorPalette.SetActive(false);
        Instance = this;
    }

    public void Open()
    {
        m_ColorPalette.SetActive(true);
        Avometer_Toolbox.Instance.CloseToolbox();
    }

    public void Close()
    {
        m_ColorPalette.SetActive(false);
        Avometer_Toolbox.Instance.OpenToolbox();
    }
}