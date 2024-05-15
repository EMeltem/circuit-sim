using UnityEngine;
using UnityEngine.Events;

public class RayCaster : MonoBehaviour
{
    public LayerMask DrawLayer;
    public LayerMask ConnectionLayer;

    public float maxDistance = 100f;
    public float Resolution = 0.1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawingCaster((input) => GameSignals.OnDrawStart(input, CheckConnection()));
        }

        if (Input.GetMouseButton(0))
        {
            DrawingCaster((input) => GameSignals.OnDraw(input));
        }

        if (Input.GetMouseButtonUp(0))
        {
            DrawingCaster((input) => GameSignals.OnDrawEnd(input, CheckConnection()));
        }
    }

    private void DrawingCaster(UnityAction<Vector3> callback)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, DrawLayer))
        {
            var pos = hit.point;
            pos.y = hit.collider.transform.position.y;
            pos.x = Mathf.Round(pos.x / Resolution) * Resolution;
            pos.z = Mathf.Round(pos.z / Resolution) * Resolution;
            callback(pos);
        }
    }

    private ConnectionPoint CheckConnection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, ConnectionLayer))
        {
            return hit.collider.TryGetComponent(out ConnectionPoint connectionPoint) ? connectionPoint : null;
        }
        return null;
    }
}