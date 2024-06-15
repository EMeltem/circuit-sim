using UnityEngine;
using UnityEngine.Events;

public class GameSignals
{
    public static UnityAction<Vector3, ConnectionPoint> OnDrawStart = delegate { };
    public static UnityAction<Vector3, ConnectionPoint> OnDrawEnd = delegate { };
    public static UnityAction<Vector3> OnDraw = delegate { };
}