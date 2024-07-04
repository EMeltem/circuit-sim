using Project.Signals;
using TMPro;
using UnityEngine;

public class LevelIndexLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text m_LevelText;

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
        m_LevelText.text = $"Seviye {LevelLoader.CurrentLevelIndex + 1}";
    }
}