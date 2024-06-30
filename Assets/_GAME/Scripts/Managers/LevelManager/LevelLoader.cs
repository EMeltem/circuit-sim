using Cysharp.Threading.Tasks;
using Project.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader
{
    public static LevelContainer LevelContainer;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        Debug.Log("LevelLoader Initialized");
        LoadLevelManager();
    }

    private static void LoadLevelManager()
    {
        LevelContainer = Resources.Load<LevelContainer>("LeveLContainer/LevelManager");
        Debug.Log("LevelManager Loaded : " + LevelContainer.GetLevelGroupCount());
    }

    public async static void LoadMenu()
    {
        await SceneManager.LoadSceneAsync("LevelSelection");
    }

    public static LevelData CurrentLevelData { get; private set; }
    private static string CurrentLevelID { get; set; }
    public static string GenereatedLevelID(int groupIndex, int levelIndex)
         => $"GI_{groupIndex}_LI_{levelIndex}";

    public static int CurrentGroupIndex { get; private set; }
    public static int CurrentLevelIndex { get; private set; }
    public async static UniTask LoadLevel(int groupIndex, int levelIndex)
    {
        CurrentGroupIndex = groupIndex;
        CurrentLevelIndex = levelIndex;

        CurrentLevelID = $"GI_{groupIndex}_LI_{levelIndex}";
        CurrentLevelData = LevelContainer.GetLevelData(groupIndex, levelIndex);
        var _levelGroup = LevelContainer.GetCurrentGroup(groupIndex);
        await SceneManager.LoadSceneAsync(_levelGroup.SceneName);
        await UniTask.DelayFrame(10);
        CurrentLevelData.Initialize();
        GameSignals.OnLevelStarted.Invoke(CurrentLevelData);
    }

    public async static UniTask ReloadLevel()
    {
        await LoadLevel(CurrentGroupIndex, CurrentLevelIndex);
    }

    public static async UniTask LoadNextLevel()
    {
        if (CurrentLevelIndex + 1 < LevelContainer.GetCurrentGroup(CurrentGroupIndex).Levels.Count)
        {
            await LoadLevel(CurrentGroupIndex, CurrentLevelIndex + 1);
        }
        else
        {
            await LoadLevel(CurrentGroupIndex + 1, 0);
        }
    }

    public static void CompleteLevel()
    {
        CurrentLevelData.CompleteLevel(CurrentLevelID);
    }

    public static void FailLevel()
    {
        CurrentLevelData.FailLevel();
    }
}