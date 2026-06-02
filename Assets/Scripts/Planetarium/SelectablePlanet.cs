using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SelectablePlanet : MonoBehaviour
{
    [Header("Orbital Paramenters")]
    public float orbitalDistanceAU = 1.0f;
    public float orbitalPeriodDays = 365f;
    public float timeScale = 10f;

    [Header("Interaction Settings")]
    public GameObject uiPanel;
    public float flySpeed = 5f;
    public float inspectionScale = 0.5f;

    [Header("Optional Orbit Center")]
    public Transform orbitCenter;

    private float currentAngle;
    private bool isGrabbed = false;
    private Transform handTransform;
    private Vector3 originalScale;
    private XRSimpleInteractable simpleInteractable;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAngle = Random.Range(0f, 360f);
        originalScale = transform.localScale;

        if (uiPanel != null) uiPanel.SetActive(false);

        simpleInteractable = GetComponent<XRSimpleInteractable>();
        simpleInteractable.selectEntered.AddListener(OnPlanetSelected);
        simpleInteractable.selectExited.AddListener(OnPlanetReleased);
    }

    void OnDestroy()
    {
        simpleInteractable.selectEntered.RemoveListener(OnPlanetSelected);
        simpleInteractable.selectExited.RemoveListener(OnPlanetReleased);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed)
        {
            DoOrbit();
        }
        else
        {
            HoldInHand();
        }
    }

    void DoOrbit()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * flySpeed);
        
        float targetPeriodInSeconds = orbitalPeriodDays / timeScale;
        float angularSpeed = 360f / targetPeriodInSeconds;

        currentAngle += angularSpeed * Time.deltaTime;
        currentAngle %= 360f;

        float radians = currentAngle * Mathf.Deg2Rad;
        float x = orbitalDistanceAU * Mathf.Cos(radians);
        float z = orbitalDistanceAU * Mathf.Sin(radians);

        Vector3 center = (orbitCenter != null) ? orbitCenter.position : Vector3.zero;
        transform.position = center + new Vector3(x, 0f, z);
    }

    void HoldInHand()
    {
        if (handTransform == null) return;

        Vector3 targetPosition = handTransform.position + (handTransform.forward * 0.3f);
        transform.position = Vector3.Lerp(transform.position, targetPosition , Time.deltaTime * flySpeed);

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(inspectionScale, inspectionScale, inspectionScale), Time.deltaTime * flySpeed);

        transform.Rotate(Vector3.up, 20f * Time.deltaTime, Space.Self);
    }

    private void OnPlanetSelected(SelectEnterEventArgs args)
    {
        handTransform = args.interactorObject.transform;
        isGrabbed = true;

        if (uiPanel != null) uiPanel.SetActive(true);
    }

    private void OnPlanetReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
        handTransform = null;

        if (uiPanel != null) uiPanel.SetActive(false);
    }
}
