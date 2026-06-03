using UnityEngine;

public class CelestialOrbit : MonoBehaviour
{
    [Header("Orbit Configuration")]
    [SerializeField] private Transform sunTransform;
    [SerializeField] private float orbitRadius = 5f;
    [SerializeField] private float orbitSpeed = 10f;

    [Header("Rotation")]
    [SerializeField] private float axialRotationSpeed = 15f;

    private float _runningPhase;

    void Start()
    {
        if (sunTransform == null)
        {
            Debug.LogWarning($"Sun Transform assignment missing on {gameObject.name}. Finding by name...");
            GameObject sun = GameObject.Find("Sun");
            if (sun != null) sunTransform = sun.transform;
        }

        if (sunTransform != null)
        {
            Vector3 offset = transform.position - sunTransform.position;
            _runningPhase = Mathf.Atan2(offset.z, offset.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, axialRotationSpeed * Time.deltaTime, Space.Self);

        _runningPhase += orbitSpeed * Time.deltaTime * 0.1f;

        float x = sunTransform.position.x + Mathf.Cos(_runningPhase) * orbitRadius;
        float z = sunTransform.position.z + Mathf.Sin(_runningPhase) * orbitRadius;

        transform.position = new Vector3(x, transform.position.y, z);
    }


}
