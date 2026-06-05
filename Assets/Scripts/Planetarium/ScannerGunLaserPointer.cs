using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ScannerGunLaserPointer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [Header("Laser Settings")]
    [SerializeField] private float maxLaserDistance = 50f;
    [SerializeField] private LayerMask exclusionLayers;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);

        RaycastHit hit; 
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLaserDistance, ~exclusionLayers))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Vector3 endPosition = transform.position + (transform.forward * maxLaserDistance);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}
