using UnityEngine;

[CreateAssetMenu(fileName = "AvometerLevelData", menuName = "Managers/Level/LevelData/AvometerLevelData", order = 0)]
public class AvometerLevelData : LevelData
{
    public float VoltageValue;
    public float AmperValue;
    public int MinResistorCount;

    public override void Initialize()
    {
        FindObjectOfType<PSU>().UpdateVoltage(VoltageValue);
    }

    public override string GetQuestDescription()
    {
        var _message = base.GetQuestDescription();
        _message = _message.Replace("{0}", AmperValue.ToString());
        _message = _message.Replace("{1}", MinResistorCount.ToString());
        return _message;
    }
}