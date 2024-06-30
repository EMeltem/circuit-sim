using Project.Avometer;
using Project.Resistors;
using UnityEngine;

public class ResistorClickDetector : MonoBehaviour
{
    private void OnMouseEnter()
    {
        if (AvometerManager.CurrentState == AvometerState.Draw ||
            AvometerManager.CurrentState == AvometerState.ResistorCreation) return;
        if (!GetComponent<Resistor>().Spawned) return;
        if (ColorPaletteToggler.Instance.Active) return;
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        if (AvometerManager.CurrentState == AvometerState.Draw ||
              AvometerManager.CurrentState == AvometerState.ResistorCreation) return;
        if (!GetComponent<Resistor>().Spawned) return;
        if (ColorPaletteToggler.Instance.Active) return;
        GetComponent<Outline>().enabled = false;
    }

    private void OnMouseDown()
    {
        if (!GetComponent<Resistor>().Spawned) return;
        if (AvometerManager.CurrentState == AvometerState.Cutting)
        {
            GetComponent<Resistor>().DestroyWithConnections();
            return;
        }
        if (AvometerManager.CurrentState != AvometerState.None) return;
        ColorPaletteToggler.Instance.Open();
        GetComponent<Outline>().enabled = false;
    }
}