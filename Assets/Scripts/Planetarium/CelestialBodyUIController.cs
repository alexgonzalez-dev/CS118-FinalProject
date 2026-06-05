using UnityEngine;
using TMPro;

public class CelestialBodyUIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    void Start()
    {
        if (uiCanvas != null) uiCanvas.SetActive(false);
    }

    public void DisplayCelestialBodyInfo(CelestialBodyData data)
    {
        nameText.text = data.celestialBodyName;
        typeText.text = $"Type: {data.celestialBodyType}";
        descriptionText.text = data.description;

        if (uiCanvas != null && !uiCanvas.activeSelf) uiCanvas.SetActive(true);
    }

}
