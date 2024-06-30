using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PSU : ElectrictyElement
{
    [SerializeField] private TMP_Text m_AmperageText;
    [SerializeField] private TMP_Text m_VoltageText;
    public float Voltage = 25.0f;
    public bool IsOn { get; private set; }
    public void TurnOn()
    {
        IsOn = true;
        var _resistor = CalculateTotalResistance();
        Debug.Log($"Total resistance in the circuit: {_resistor} Ohm");
        CalculateCurrent();
    }

    public void TurnOff()
    {
        IsOn = false;
    }

    public void UpdateVoltage(float voltage)
    {
        Voltage = voltage;
        m_VoltageText.text = $"{voltage:F0}V PSU";
    }

    public void CheckConnections()
    {
        Debug.Log("Checking connections");
        if (StartPoint.ElectrictyElements.Count == 0 || EndPoint.ElectrictyElements.Count == 0)
        {
            TurnOff();
            Debug.Log("No connection detected");
            return;
        }

        foreach (var cable in StartPoint.ElectrictyElements)
        {
            if (cable == null) continue;
            if (cable.StartPoint.Type == ConnectionType.GND)
            {
                OnShortCircuit();
                return;
            }
        }

        foreach (var cable in EndPoint.ElectrictyElements)
        {
            if (cable == null) continue;
            if (cable.StartPoint.Type == ConnectionType.V)
            {
                OnShortCircuit();
                return;
            }
        }

        TurnOn();
    }

    private float CalculateTotalResistance()
    {
        var visited = new HashSet<ElectrictyElement>();
        var queue = new Queue<ElectrictyElement>();
        float totalResistance = 0.0f;

        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var currentElement = queue.Dequeue();
            if (visited.Contains(currentElement)) continue;

            visited.Add(currentElement);
            totalResistance += (float)currentElement.CalculateResistance().resistor;

            foreach (var nextElement in currentElement.StartPoint.ElectrictyElements)
            {
                if (!visited.Contains(nextElement))
                {
                    queue.Enqueue(nextElement);
                }
            }

            foreach (var nextElement in currentElement.EndPoint.ElectrictyElements)
            {
                if (!visited.Contains(nextElement))
                {
                    queue.Enqueue(nextElement);
                }
            }
        }

        return totalResistance;
    }

    private void CalculateCurrent()
    {
        float totalResistance = CalculateTotalResistance();

        if (totalResistance == 0)
        {
            OnShortCircuit();
            return;
        }

        float current = Voltage / totalResistance;
        m_AmperageText.text = $"{current:F2} A";
        Debug.Log($"Current in the circuit: {current} A");
    }


    private void OnShortCircuit()
    {
        TurnOff();
        //TODO : Show short circuit animation
        Debug.Log("Short circuit detected");
    }
}