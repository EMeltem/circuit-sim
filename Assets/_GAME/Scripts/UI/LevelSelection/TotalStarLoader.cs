using TMPro;
using UnityEngine;

public class TotalStarLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text m_StarText;

    private void Start()
    {
        LoadStars(out var _stars);
        UpdateText(_stars);
    }

    private void LoadStars(out int stars)
    {
        stars = 0;
        for (var i = 0; i < LevelLoader.LevelContainer.GetLevelGroupCount(); i++)
        {
            for (var j = 0; j < LevelLoader.LevelContainer.GetCurrentGroup(i).Levels.Count; j++)
            {
                var _levelId = LevelLoader.GenereatedLevelID(i, j);
                stars += LevelLoader.LevelContainer.GetLevelData(i, j).GetStarCount(_levelId);
            }
        }
    }

    private void UpdateText(int stars)
    {
        m_StarText.text = stars.ToString();
    }
}