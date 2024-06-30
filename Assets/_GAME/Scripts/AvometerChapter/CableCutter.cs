using System;
using Project.Avometer;
using Project.Signals;
using UnityEngine;

public class CableCutter : MonoBehaviour
{
    [SerializeField] private float m_CutDistance = 0.1f;
    private void Awake()
    {
        GameSignals.OnDrawStart += OnMatClick;
    }

    private void OnDestroy()
    {
        GameSignals.OnDrawStart -= OnMatClick;
    }

    private void OnMatClick(Vector3 pos, ConnectionPoint cp)
    {
        if (AvometerManager.CurrentState == AvometerState.Cutting)
        {
            CutCable(pos, cp);
        }
    }

    private void CutCable(Vector3 pos, ConnectionPoint cp)
    {
        var _cables = CableCreatorLR.Instance.Cables;
        foreach (var cable in _cables)
        {
            if (cable == null) continue;
            var positions = new Vector3[cable.Drawer.positionCount];
            cable.Drawer.GetPositions(positions);
            for (int i = 0; i < positions.Length - 1; i++)
            {
                if (Vector3.Distance(positions[i], pos) < m_CutDistance)
                {
                    cable.Clear();
                    Destroy(cable.gameObject);
                    return;
                }
            }
        }
    }
}