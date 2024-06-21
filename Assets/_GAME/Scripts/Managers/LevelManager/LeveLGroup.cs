using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeveLGroup", menuName = "Managers/Level/LeveLGroup", order = 0)]
public class LeveLGroup : ScriptableObject
{
    public string SceneName;
    public List<LevelData> Levels;
}