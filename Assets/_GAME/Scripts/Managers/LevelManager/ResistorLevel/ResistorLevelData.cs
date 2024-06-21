using NaughtyAttributes;
using Project.Resistors;
using Project.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "ResistorLevelData", menuName = "Managers/Level/LevelData/ResistorLevelData", order = 0)]
public class ResistorLevelData : LevelData
{
    public bool RngEnabled;
    [HideIf("RngEnabled")] public float Value;
    [HideIf("RngEnabled")] public int Tolerance;
    public (float resistor, float tolerance) ValueTolerancePair;

    public override void Initialize()
    {
        ValueTolerancePair = CacheValue();
    }

    private (float resistor, float tolerance) CacheValue()
    {
        if (!RngEnabled) return (Value, Tolerance);

        var A = ResistorManager.GetRandomDataWithFlag(ResistorRingType.A);
        var B = ResistorManager.GetRandomDataWithFlag(ResistorRingType.B);
        var C = ResistorManager.GetRandomDataWithFlag(ResistorRingType.C);
        var T = ResistorManager.GetRandomDataWithFlag(ResistorRingType.T);
        return Utils.CalculateResistance(A, B, C, T);
    }

    public override string GetQuestDescription()
    {
        var _message = base.GetQuestDescription();
        _message = _message.Replace("{0}", Utils.SimplfyNumber(ValueTolerancePair.resistor));
        _message = _message.Replace("{1}", ValueTolerancePair.tolerance.ToString());
        return _message;
    }
}