using System;
using UnityEngine;
using UnityEngine.UI;

public class AvometerCheckButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        var _psus = FindObjectsByType<PSU>(FindObjectsSortMode.None);
        foreach (var psu in _psus)
        {
            psu.CheckConnections();
        }
    }
}