using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelNavigator : MonoBehaviour
{
    [SerializeField] private int m_LevelGroupIndex;
    [SerializeField] private Button m_LevelButtonPf;
    [SerializeField] protected Transform m_LevelButtonParent;

    private void Start()
    {
        CreateStartButtons();
    }

    private void CreateStartButtons()
    {
        var _levelGroup = LevelLoader.LevelContainer.GetCurrentGroup(m_LevelGroupIndex);
        for (int i = 0; i < _levelGroup.Levels.Count; i++)
        {
            var _levelData = LevelLoader.LevelContainer.GetLevelData(m_LevelGroupIndex, i);
            var _index = i;
            var _levelButton = Instantiate(m_LevelButtonPf, m_LevelButtonParent);
            _levelButton.GetComponentInChildren<TMP_Text>().text = (_index + 1).ToString();
            var _levelId = LevelLoader.GenereatedLevelID(m_LevelGroupIndex, _index);
            _levelButton.GetComponent<LevelSelectionStars>().SelectStars(_levelData.GetStarCount(_levelId));
            _levelButton.onClick.AddListener(() => LoadLevel(_index));
        }
    }

    private void LoadLevel(int index)
    {
        LevelLoader.LoadLevel(m_LevelGroupIndex, index).Forget();
    }
}