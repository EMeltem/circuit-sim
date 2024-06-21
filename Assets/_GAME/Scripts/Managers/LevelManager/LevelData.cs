using Project.Signals;
using UnityEngine;

public abstract class LevelData : ScriptableObject
{
    [TextArea(3, 10), SerializeField] private string m_QuestDescription;
    public abstract void Initialize();
    public virtual string GetQuestDescription()
    {
        return m_QuestDescription;
    }

    public int GetStarCount(string saveID)
    {
        return PlayerPrefs.GetInt(saveID, 0);
    }

    public void SaveStars(string saveID, int stars)
    {
        PlayerPrefs.SetInt(saveID, stars);
    }

    public bool IsCompleted(string saveID)
    {
        var _saveKey = saveID + "_completed";
        return PlayerPrefs.GetInt(_saveKey, 0) == 1;
    }

    public virtual void CompleteLevel(string saveID)
    {
        var _stars = CanvasStars.Instance.GetStartCount();
        SaveStars(saveID, _stars);
        GameSignals.OnLevelCompleted.Invoke(this, true);
        IsCompleted(saveID);
    }

    public virtual void FailLevel()
    {
        GameSignals.OnLevelCompleted.Invoke(this, false);
    }
}

public struct LevelCompleteArgs
{
    public LevelData LevelData;
    public int Stars;
    public bool IsWin;
}