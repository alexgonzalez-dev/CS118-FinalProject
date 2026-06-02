using UnityEngine;
using TMPro;
using System;

public class CelestialBodyUIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Positioning Offset")]
    [SerializeField] private Vector3 offsetFromCelestialBody = new Vector3(0, 2f, -1f);

    private Transform _activeCelestialBodyTransform;

    void Start()
    {
        if (uiPanel != null) uiPanel.SetActive(false);
    }

    void LateUpdate()
    {
        if (uiPanel.activeSelf && _activeCelestialBodyTransform != null)
        {
            transform.position = _activeCelestialBodyTransform.position + offsetFromCelestialBody;

            if (Camera.main != null)
            {
                transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                                Camera.main.transform.rotation * Vector3.up);
            }
        }
    }

    public void DisplayCelestialBodyInfo(CelestialBodyData data, Vector3 hitPosition)
    {
        nameText.text = data.celestialBodyName;
        typeText.text = $"Type: {data.celestialBodyType}";
        descriptionText.text = data.description;

        _activeCelestialBodyTransform = data.transform;
        uiPanel.SetActive(true);
    }

}
