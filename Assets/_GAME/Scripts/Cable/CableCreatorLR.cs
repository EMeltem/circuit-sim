using System;
using System.Collections.Generic;
using Project.Signals;
using UnityEngine;

public class CableCreatorLR : MonoBehaviour
{
    [SerializeField] private Cable m_Cable;
    public static CableCreatorLR Instance { get; private set; }
    public bool Active;

    private void Awake()
    {
        GameSignals.OnDrawStart += OnCreatingStart;
        GameSignals.OnDraw += CreateCable;
        GameSignals.OnDrawEnd += OnCreatingEnd;
        Instance = this;
    }

    private void OnDestroy()
    {
        GameSignals.OnDrawStart -= OnCreatingStart;
        GameSignals.OnDraw -= CreateCable;
        GameSignals.OnDrawEnd -= OnCreatingEnd;
    }

    private bool m_IsDrawing;
    private void OnCreatingStart(Vector3 input, ConnectionPoint cp)
    {
        if (!Active) return;        
        if (cp == null)
        {
            Debug.LogWarning("Connection point is null");
            return;
        }
        if (!cp.Available) return;
        m_IsDrawing = true;
        cablePositions.Clear();
        CreateCable(input);
        m_Cable.StartPoint = cp;
        cp.AddElement(m_Cable);
    }

    public List<Cable> Cables = new();
    private void OnCreatingEnd(Vector3 input, ConnectionPoint cp)
    {
        if (!Active) return;
        m_IsDrawing = false;
        if (cp == null)
        {
            DiscardCable();
            return;
        }
        if (!cp.Available)
        {
            DiscardCable();
            return;
        }
        CreateInstance(cp);
    }

    private void DiscardCable()
    {
        Debug.LogWarning("Connection point is null");
        m_Cable.Clear();
    }

    private void CreateInstance(ConnectionPoint cp)
    {
        cp.AddElement(m_Cable);
        m_Cable.EndPoint = cp;
        Cables.Add(m_Cable);
        m_Cable = Instantiate(m_Cable, m_Cable.transform.parent);
        m_Cable.Clear();
    }

    public List<Vector3> cablePositions = new();
    private void CreateCable(Vector3 input)
    {
        if(!m_IsDrawing) return;
        if (!Active) return;
        m_Cable.Create(input);
    }
}