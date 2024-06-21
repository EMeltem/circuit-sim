using System;
using Project.Signals;
using TMPro;
using UnityEngine;

public class QuestDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text m_QuestText;

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
        m_QuestText.text = data.GetQuestDescription();
    }
}