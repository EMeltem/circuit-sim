using System;
using System.Collections.Generic;
using UnityEngine;

public class CableCreatorLR : MonoBehaviour
{
    [SerializeField] private LineRenderer m_Drawer;
    [SerializeField] private float m_Width = 0.1f;
    [SerializeField] private float m_WidthMultiplier = 0.1f;

    private void Awake()
    {
        GameSignals.OnDrawStart += OnCreatingStart;
        GameSignals.OnDraw += CreateCable;
        GameSignals.OnDrawEnd += OnCreatingEnd;
    }

    private void OnDestroy()
    {
        GameSignals.OnDrawStart -= OnCreatingStart;
        GameSignals.OnDraw -= CreateCable;
        GameSignals.OnDrawEnd -= OnCreatingEnd;
    }

    protected bool m_IsCreating = false;
    private void OnCreatingStart(Vector3 input, ConnectionPoint connectionPoint)
    {
        if (connectionPoint == null)
        {
            Debug.LogWarning("Connection point is null");
            return;
        }
        m_IsCreating = true;
        cablePositions.Clear();
        CreateCable(input);
    }

    private void OnCreatingEnd(Vector3 input, ConnectionPoint connectionPoint)
    {
        if (connectionPoint == null)
        {
            Debug.LogWarning("Connection point is null");
            cablePositions.Clear();
            m_Drawer.positionCount = 0;
            m_IsCreating = false;
            return;
        }
    }


    public List<Vector3> cablePositions = new();
    private void CreateCable(Vector3 input)
    {
        if (!m_IsCreating) return;
        if (cablePositions.Contains(input)) return;
        cablePositions.Add(input);
        m_Drawer.positionCount = cablePositions.Count;
        m_Drawer.SetPositions(cablePositions.ToArray());
        m_Drawer.widthCurve = GetCurve();
        m_Drawer.widthMultiplier = m_Width;
    }

    private AnimationCurve GetCurve()
    {
        return new AnimationCurve(new Keyframe(0, m_WidthMultiplier), new Keyframe(1, m_WidthMultiplier));
    }
}