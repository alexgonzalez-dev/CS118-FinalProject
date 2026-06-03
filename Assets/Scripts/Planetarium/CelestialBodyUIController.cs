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

    void Start()
    {
        if (uiPanel != null) uiPanel.SetActive(false);
    }

    public void DisplayCelestialBodyInfo(CelestialBodyData data)
    {
        nameText.text = data.celestialBodyName;
        typeText.text = $"Type: {data.celestialBodyType}";
        descriptionText.text = data.description;

        if (uiPanel != null && !uiPanel.activeSelf) uiPanel.SetActive(true);
    }

}
