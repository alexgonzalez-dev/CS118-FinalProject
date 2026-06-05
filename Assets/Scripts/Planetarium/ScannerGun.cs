using Unity.VisualScripting;
using UnityEngine;

public class ScannerGun : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private float scanRange = 100f;
    [SerializeField] private LayerMask planetLayer;

    [Header("UI Reference")]
    [SerializeField] private CelestialBodyUIController uiController;

    [Header("Visual Effects")]
    [SerializeField] private LineRenderer laserVisual;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scanSuccessSound;


    public void FireScanner()
    {
        Ray ray = new Ray(muzzleTransform.position, muzzleTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, scanRange, planetLayer))
        {
            if (hit.collider.TryGetComponent<CelestialBodyData>(out var celestialBodyData))
            {
                uiController.DisplayCelestialBodyInfo(celestialBodyData);

                if (audioSource != null && scanSuccessSound != null)
                {
                    audioSource.PlayOneShot(scanSuccessSound);
                }
            }
        }
    }
}
