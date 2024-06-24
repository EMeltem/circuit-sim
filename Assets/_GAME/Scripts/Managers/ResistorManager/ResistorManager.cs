using System.Collections.Generic;
using System.Linq;
using Project.Resistors;
using UnityEngine;

public class ResistorManager
{
    private static Dictionary<string, ResistorColor> ResistorColorDataDictionary;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        LoadResourcess();
        Debug.Log("ResistorManager Initialized : " + ResistorColorDataDictionary.Count);
    }

    private static void LoadResourcess()
    {
        var _dataList = Resources.LoadAll<ResistorColor>("Resistor");
        ResistorColorDataDictionary = _dataList.ToDictionary(data => data.Name);
    }

    public static ResistorColor GetResistorColorData(string name)
    {
        if (ResistorColorDataDictionary.TryGetValue(name, out var data))
        {
            return data;
        }
        Debug.LogError("Resistor Color Data Not Found : " + name);
        return null;
    }

    public static ResistorColor GetRandomDataWithFlag(ResistorRingType type)
    {
        var _valueList = ResistorColorDataDictionary.Values.ToList();
        var _validValues = _valueList.Where(data => (data.AllowedTypes & type) == type).ToList();
        var _randomIndex = Random.Range(0, _validValues.Count);
        return _validValues[_randomIndex];
    }
}