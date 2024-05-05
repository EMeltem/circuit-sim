using System;
using System.Collections.Generic;
using UnityEngine;

public class CableCreator : MonoBehaviour
{
    [SerializeField] private GameObject cablePiecePrefab;
    [SerializeField] private GameObject cableConnectorPrefab;
    [SerializeField] private float resolution = 0.1f;

    private void Awake()
    {
        GameSignals.OnClick += CreateCable;
    }

    private void OnDestroy()
    {
        GameSignals.OnClick -= CreateCable;
    }

    public List<Vector3> cablePositions = new List<Vector3>();
    public List<GameObject> cableObjects = new List<GameObject>();
    private void CreateCable(Vector3 pos)
    {
        if (cablePositions.Contains(pos)) { return; }
        if (IsCablePositionsValid(pos) == false) { return; }
        var _rotation = _lastDirection == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(_lastDirection);
        var _piece = Instantiate(cablePiecePrefab, pos, _rotation);
        cableObjects.Add(_piece);
        cablePositions.Add(pos);
        AdjustRotation(_piece);
    }

    private Vector3 _lastDirection = Vector3.zero;
    private void AdjustRotation(GameObject cablePiece)
    {
        if (cablePositions.Count < 3) { return; }
        var _last2Positions = cablePositions.GetRange(cablePositions.Count - 2, 2);
        var _direction = Mathf.Abs(_last2Positions[0].x - _last2Positions[1].x) > 0 ? Vector3.forward : Vector3.right;
        if (_lastDirection == _direction) { return; }
        cablePiece.transform.rotation = Quaternion.LookRotation(_direction);
        _lastDirection = _direction;
        var _garbage = cableObjects[cableObjects.Count - 2];
        Destroy(_garbage);
        var _index = cableObjects.Count - 2;
        if (_index < 2) return;
        cableObjects[_index] = Instantiate(cableConnectorPrefab, _last2Positions[0], Quaternion.identity);
    }

    private bool IsCablePositionsValid(Vector3 pos)
    {
        if (cablePositions.Count == 0) { return true; }
        Vector3 lastPos = cablePositions[cablePositions.Count - 1];
        var _xDistance = Mathf.Abs(lastPos.x - pos.x);
        var _zDistance = Mathf.Abs(lastPos.z - pos.z);
        return Mathf.Approximately(_xDistance, resolution) || Mathf.Approximately(_zDistance, resolution);
    }
}