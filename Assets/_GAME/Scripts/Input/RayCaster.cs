using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public LayerMask layerMask;
    public float maxDistance = 100f;
    public float Resolution = 0.1f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
            {
                Vector3 pos = hit.point;
                pos.y = hit.collider.transform.position.y;
                pos.x = Mathf.Round(pos.x / Resolution) * Resolution;
                pos.z = Mathf.Round(pos.z / Resolution) * Resolution;
                GameSignals.OnClick(pos);
            }
        }
    }
}