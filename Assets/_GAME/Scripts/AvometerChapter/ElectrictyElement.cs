using UnityEngine;

public class ElectrictyElement : MonoBehaviour
{
    public ConnectionPoint StartPoint;
    public ConnectionPoint EndPoint;

    public virtual (double resistor, double tolerance) CalculateResistance()
    {
        return (0, 0);
    }
}