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
        Instance = this;
    }

    private void OnDestroy()
    {
        GameSignals.OnDrawStart -= OnMatClick;
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

    private void CreateResistor(Vector3 pos)
    {
        if (m_ResistorInstance == null) return;
        var resistor = m_ResistorInstance;
        m_ResistorInstance = null;
        resistor.transform.position = pos;
        resistor.transform.rotation = Quaternion.Euler(0, 0, 0);
        Cursor.visible = true;
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
                m_ResistorInstance.transform.position = hit.point;
            }
        }
    }
}