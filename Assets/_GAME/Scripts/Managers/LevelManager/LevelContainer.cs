using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManager", menuName = "Managers/Level/LevelManager", order = 0)]
public class LevelContainer : ScriptableObject
{
    public List<LeveLGroup> LeveLGroups;

    public int GetLevelGroupCount()
    {
        return LeveLGroups.Count;
    }

    public LeveLGroup GetCurrentGroup(int index)
    {
        return LeveLGroups[index];
    }

public LevelData GetLevelData(int groupIndex, int levelIndex)
    {
        return LeveLGroups[groupIndex].Levels[levelIndex];
    }
}