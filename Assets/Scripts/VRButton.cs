using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
[RequireComponent(typeof(AudioSource))]
public class VRButton : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private float pressDistance = 0.05f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private Color pressedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;

    [Header("Audio")]
    [SerializeField] private AudioClip pressSound;
    [SerializeField] private float soundVolume = 0.7f;

    private XRSimpleInteractable interactable;
    private AudioSource audioSource;
    private MeshRenderer buttonRenderer;
    private Vector3 originalPosition;
    private Vector3 pressedPosition;
    private bool isPressed = false;
    private Material buttonMaterial;
    private Color originalColor;

    private void Awake()
    {
        // Get required components
        interactable = GetComponent<XRSimpleInteractable>();
        audioSource = GetComponent<AudioSource>();
        buttonRenderer = GetComponent<MeshRenderer>();

        if (buttonRenderer != null)
        {
            buttonMaterial = buttonRenderer.material;
            originalColor = buttonMaterial.color;
        }

        // Setup audio source
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.volume = soundVolume;
        }

        originalPosition = transform.localPosition;
        pressedPosition = originalPosition - new Vector3(0, pressDistance, 0);
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
            interactable.selectExited.AddListener(OnButtonReleased);
            interactable.hoverEntered.AddListener(OnButtonHovered);
            interactable.hoverExited.AddListener(OnButtonHoverEnd);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnButtonPressed);
            interactable.selectExited.RemoveListener(OnButtonReleased);
            interactable.hoverEntered.RemoveListener(OnButtonHovered);
            interactable.hoverExited.RemoveListener(OnButtonHoverEnd);
        }
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (!isPressed)
        {
            isPressed = true;

            // Animate button press
            StopAllCoroutines();
            StartCoroutine(AnimateButtonPress());

            // Play sound
            PlayButtonSound();

            // Change button color
            if (buttonMaterial != null)
            {
                buttonMaterial.color = pressedColor;
            }

            // You can add your custom action here
            OnButtonClicked();

            // Start return animation after delay
            Invoke(nameof(ReleaseButton), 0.1f);
        }
    }

    private void OnButtonReleased(SelectExitEventArgs args)
    {
        // Button is released naturally
    }

    private void OnButtonHovered(HoverEnterEventArgs args)
    {
        // Optional: visual feedback when hovering
        if (buttonMaterial != null && !isPressed)
        {
            buttonMaterial.color = Color.Lerp(originalColor, pressedColor, 0.5f);
        }
    }

    private void OnButtonHoverEnd(HoverExitEventArgs args)
    {
        // Reset hover effect
        if (buttonMaterial != null && !isPressed)
        {
            buttonMaterial.color = originalColor;
        }
    }

    private void ReleaseButton()
    {
        if (isPressed)
        {
            isPressed = false;
            StartCoroutine(AnimateButtonReturn());

            // Reset color
            if (buttonMaterial != null)
            {
                buttonMaterial.color = originalColor;
            }
        }
    }

    private System.Collections.IEnumerator AnimateButtonPress()
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.localPosition;

        while (elapsedTime < 0.05f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 0.05f;
            transform.localPosition = Vector3.Lerp(startPos, pressedPosition, t);
            yield return null;
        }

        transform.localPosition = pressedPosition;
    }

    private System.Collections.IEnumerator AnimateButtonReturn()
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.localPosition;

        while (elapsedTime < 0.2f)
        {
            elapsedTime += Time.deltaTime * returnSpeed;
            float t = elapsedTime / 0.2f;
            transform.localPosition = Vector3.Lerp(startPos, originalPosition, t);
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    private void PlayButtonSound()
    {
        if (audioSource != null && pressSound != null)
        {
            audioSource.PlayOneShot(pressSound, soundVolume);
        }
        else if (audioSource != null && pressSound == null)
        {
            // Create a simple beep sound if no audio clip is assigned
            Debug.Log("Button pressed! (No sound assigned)");
        }
    }

    private void OnButtonClicked()
    {
        // This is where you put your custom logic
        Debug.Log("Button was clicked with VR controller!");

        // Example: Show a message
        // UIManager.Instance.ShowMessage("Info Kiosk activated!");

        // You can add more functionality here:
        // - Open info panel
        // - Trigger animation
        // - Load next scene
        // - etc.
    }
}