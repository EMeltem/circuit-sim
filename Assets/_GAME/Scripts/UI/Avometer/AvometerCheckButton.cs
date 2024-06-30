using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AvometerCheckButton : CheckButton
{
    private void Start()
    {
        m_CheckButton.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        m_CheckButton.onClick.RemoveListener(OnClick);
    }
    
    private void OnClick()
    {
        if (!m_IsClickValid) return;
        m_IsClickValid = false;
        CheckIsCorrect();
    }

    private bool m_IsClickValid = true;
    private async void CheckIsCorrect()
    {
        var _psus = FindObjectsByType<PSU>(FindObjectsSortMode.None);
        foreach (var psu in _psus)
        {
            psu.CheckConnections();
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        m_IsClickValid = true;
    }
}