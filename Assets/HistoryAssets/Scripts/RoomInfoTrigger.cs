using UnityEngine;

public class RoomInfoTrigger : MonoBehaviour
{
    public GameObject infoCanvas;
    public AudioSource audioSource;
    public AudioClip popSound;
    public string playerTag = "Player";

    void Start()
    {
        if (infoCanvas != null)
            infoCanvas.SetActive(false);
    }

    void LateUpdate()
    {
        if (infoCanvas == null || !infoCanvas.activeSelf) return;

        Transform cam = Camera.main.transform;
        infoCanvas.transform.LookAt(cam);
        infoCanvas.transform.Rotate(0, 180f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        infoCanvas.SetActive(true);
        if (audioSource != null && popSound != null)
            audioSource.PlayOneShot(popSound);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        infoCanvas.SetActive(false);
    }
}