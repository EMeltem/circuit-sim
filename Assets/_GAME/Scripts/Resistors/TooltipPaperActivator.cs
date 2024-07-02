using System;
using Project.Signals;
using UnityEngine;

public class TooltipPaperActivator : MonoBehaviour
{
    private void Awake()
    {
        GameSignals.OnLevelStarted += OnLevelStarted;
    }

    private void OnDestroy()
    {   
        GameSignals.OnLevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted(LevelData data)
    {
        var _state = ((ResistorLevelData)data).TooltipPaperEnabled;
        gameObject.SetActive(_state);
    }
}