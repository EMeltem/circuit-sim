using System;
using System.Collections.Generic;
using Project.Avometer;
using UnityEngine;

public class Cable : ElectrictyElement
{
    public LineRenderer Drawer;

    [SerializeField] private float m_Width = 0.1f;
    [SerializeField] private float m_WidthMultiplier = 0.1f;

    private void Awake()
    {
        AvometerManager.OnStateChangedEvent += OnStateChanged;
    }

    private void OnDestroy()
    {
        AvometerManager.OnStateChangedEvent -= OnStateChanged;
    }

    private void OnStateChanged(AvometerState state)
    {
        InteractState(state);
    }

    private void InteractState(AvometerState state)
    {
        if (Drawer.positionCount <= 2) return;
        if (state == AvometerState.Cutting)
        {
            Drawer.widthMultiplier = m_Width * 1.5f;
        }
        else
        {
            Drawer.widthMultiplier = m_Width;
        }
    }

    public List<Vector3> cablePositions = new();
    public void Create(Vector3 input)
    {
        if (cablePositions.Contains(input))
        {
            RemoveCable(input);
            return;
        }
        cablePositions.Add(input);
        Drawer.positionCount = cablePositions.Count;
        Drawer.SetPositions(cablePositions.ToArray());
        Drawer.widthCurve = GetCurve();
        Drawer.widthMultiplier = m_Width;
    }

    public void DiscardCable()
    {
        Debug.LogWarning("Connection point is null");
        cablePositions.Clear();
        Drawer.positionCount = 0;
    }

    public void RemoveCable(Vector3 input)
    {
        if (!cablePositions.Contains(input)) return;
        var _index = cablePositions.IndexOf(input);
        if (_index == -1) return;
        if (Mathf.Abs(_index - cablePositions.Count) < 2) return;
        cablePositions.RemoveRange(_index, cablePositions.Count - _index);
        Drawer.positionCount = cablePositions.Count;
        Drawer.SetPositions(cablePositions.ToArray());
        Drawer.widthCurve = GetCurve();
        Drawer.widthMultiplier = m_Width;
    }

    private AnimationCurve GetCurve()
    {
        return new AnimationCurve(new Keyframe(0, m_WidthMultiplier), new Keyframe(1, m_WidthMultiplier));
    }

    public void Clear()
    {
        cablePositions.Clear();
        Drawer.positionCount = 0;
        ResetConnectionPoints();
    }

    private void ResetConnectionPoints()
    {
        if (StartPoint != null) StartPoint.RemoveElement(this);
        if (EndPoint != null) EndPoint.RemoveElement(this);
        StartPoint = null;
        EndPoint = null;
    }
}