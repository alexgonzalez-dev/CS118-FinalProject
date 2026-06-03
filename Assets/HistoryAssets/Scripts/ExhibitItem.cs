using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExhibitItem : MonoBehaviour
{
    public GameObject infoCanvas;
    public Renderer[] meshRenderers;
    public Color highlightColor = new Color(1f, 0.85f, 0.3f);
    public AudioSource audioSource;
    public AudioClip popSound;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grabInteractable;
    private Color[] _originalColors;

    void Start()
    {
        infoCanvas.SetActive(false);

        _originalColors = new Color[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
            _originalColors[i] = meshRenderers[i].material.color;

        _grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Highlight on hover (ray pointing at it)
        _grabInteractable.hoverEntered.AddListener(OnHoverEnter);
        _grabInteractable.hoverExited.AddListener(OnHoverExit);

        // Info canvas on grab
        _grabInteractable.selectEntered.AddListener(OnPickUp);
        _grabInteractable.selectExited.AddListener(OnPutDown);
    }

    void LateUpdate()
    {
        if (infoCanvas == null || !infoCanvas.activeSelf) return;

        Transform cam = Camera.main.transform;
        infoCanvas.transform.LookAt(cam);
        infoCanvas.transform.Rotate(0, 180f, 0);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        foreach (var r in meshRenderers)
            r.material.color = highlightColor;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material.color = _originalColors[i];
    }

    void OnPickUp(SelectEnterEventArgs args)
    {
        infoCanvas.SetActive(true);

        if (audioSource != null && popSound != null)
            audioSource.PlayOneShot(popSound);
    }

    void OnPutDown(SelectExitEventArgs args)
    {
        infoCanvas.SetActive(false);
    }

    void OnDestroy()
    {
        _grabInteractable.hoverEntered.RemoveListener(OnHoverEnter);
        _grabInteractable.hoverExited.RemoveListener(OnHoverExit);
        _grabInteractable.selectEntered.RemoveListener(OnPickUp);
        _grabInteractable.selectExited.RemoveListener(OnPutDown);
    }
}