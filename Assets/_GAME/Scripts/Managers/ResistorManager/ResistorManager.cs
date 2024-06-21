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
        var _values = ResistorColorDataDictionary.Values.All(data => data.AllowedTypes.HasFlag(type));
        var _rng = Random.Range(0, ResistorColorDataDictionary.Count);
        return ResistorColorDataDictionary.Values.ElementAt(_rng);
    }
}