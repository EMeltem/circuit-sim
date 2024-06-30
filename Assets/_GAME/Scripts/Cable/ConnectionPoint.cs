using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Avometer;
using Project.Signals;
using UnityEngine;

public enum ConnectionType { None, GND, V, Node }
public class ConnectionPoint : MonoBehaviour
{
    public ConnectionType Type;
    public List<ElectrictyElement> ElectrictyElements = new();
    public int Limit = 1;
    public bool Available => ElectrictyElements.Count < Limit;

    public void AddElement(ElectrictyElement cable)
    {
        if (Available) { ElectrictyElements.Add(cable); }
    }

    public void RemoveElement(ElectrictyElement cable)
    {
        if (ElectrictyElements.Contains(cable)) { ElectrictyElements.Remove(cable); }
    }

    private void Awake()
    {
        AvometerManager.OnStateChangedEvent += OnStateChanged;
        GameSignals.OnDrawEnd += OnCreatingEnd;
    }

    private void OnDestroy()
    {
        AvometerManager.OnStateChangedEvent -= OnStateChanged;
        GameSignals.OnDrawEnd -= OnCreatingEnd;
    }

    private async void OnCreatingEnd(Vector3 pos, ConnectionPoint cp)
    {
        await Task.Delay(20);
        InteractableIndicator(AvometerManager.CurrentState);
    }

    private void OnStateChanged(AvometerState state)
    {
        InteractableIndicator(state);
    }

    private void InteractableIndicator(AvometerState state)
    {
        var _outlineComponent = GetComponent<Outline>();
        if (state == AvometerState.Draw)
        {
            _outlineComponent.enabled = true;
        }
        else
        {
            _outlineComponent.enabled = false;
        }

        if (state == AvometerState.Draw && Available)
        {
            _outlineComponent.OutlineColor = Color.green;
        }
        else
        {
            _outlineComponent.OutlineColor = Color.red;
        }
    }
}