using System;
using Cysharp.Threading.Tasks;
using Project.Avometer;
using Project.Resistors;
using Project.Signals;
using UnityEngine;

public class ResistorCreator : MonoBehaviour
{
    public GameObject ResistorPrefab;
    private GameObject ResistorParent;
    public static ResistorCreator Instance;

    private void Awake()
    {
        GameSignals.OnDrawStart += OnMatClick;
        AvometerManager.OnStateChangedEvent += OnStateChanged;
        Instance = this;
    }

    private void OnDestroy()
    {
        GameSignals.OnDrawStart -= OnMatClick;
        AvometerManager.OnStateChangedEvent -= OnStateChanged;
    }

    private void OnStateChanged(AvometerState state)
    {
        if (state != AvometerState.ResistorCreation)
        {
            if (m_ResistorInstance != null)
            {
                Destroy(m_ResistorInstance.gameObject);
                m_ResistorInstance = null;
            }
        }
    }

    private void OnMatClick(Vector3 pos, ConnectionPoint cp)
    {
        if (AvometerManager.CurrentState == AvometerState.ResistorCreation)
        {
            CreateResistor(pos);
        }
    }

    private void Start()
    {
        ResistorParent = new GameObject("ResistorParent");
    }

    private async void CreateResistor(Vector3 pos)
    {
        if (m_ResistorInstance == null) return;
        var resistor = m_ResistorInstance;
        m_ResistorInstance = null;
        resistor.transform.position = pos;
        resistor.transform.rotation = Quaternion.Euler(0, 0, 0);
        Cursor.visible = true;
        await UniTask.DelayFrame(1);
        resistor.OnSpawn();
        Avometer_Toolbox.Instance.OnDefaultButtonClicked();
    }

    private Resistor m_ResistorInstance;
    public void ReadyPrefab()
    {
        if (m_ResistorInstance != null) return;
        m_ResistorInstance = Instantiate(ResistorPrefab, Vector3.zero, Quaternion.identity).GetComponent<Resistor>();
    }

    private void Update()
    {
        if (m_ResistorInstance != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Draw")))
            {
                Cursor.visible = false;
                m_ResistorInstance.transform.position = hit.point;
                m_ResistorInstance.gameObject.SetActive(true);
            }
            else
            {
                Cursor.visible = true;
                m_ResistorInstance.gameObject.SetActive(false);
            }
        }
    }
}