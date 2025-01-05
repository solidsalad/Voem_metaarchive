using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WhiteboardController : MonoBehaviour
{
    private GameObject whiteboard;
    private Vector3 originalSize;
    public Vector3 smallSize = new Vector3(0.5f, 0.5f, 0.1f);
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isSmall = false;

    private Rigidbody rb;
    private MeshCollider meshCollider;
    private BoxCollider boxCollider;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        InitializeWhiteboard();
    }

    public void InitializeWhiteboard()
    {
        // Find the whiteboard by its tag
        whiteboard = GameObject.FindGameObjectWithTag("Whiteboard");

        if (whiteboard == null)
        {
            Debug.LogError("No GameObject with the tag 'Whiteboard' was found.");
            return;
        }

        originalSize = whiteboard.transform.localScale;
        originalPosition = whiteboard.transform.position;
        originalRotation = whiteboard.transform.rotation;
        rb = whiteboard.GetComponent<Rigidbody>();
        meshCollider = whiteboard.GetComponent<MeshCollider>();
        boxCollider = whiteboard.GetComponent<BoxCollider>();
        grabInteractable = whiteboard.GetComponent<XRGrabInteractable>();

        if (boxCollider != null)
        {
            boxCollider.enabled = false; // Ensure the box collider is initially disabled
        }

        if (grabInteractable != null)
        {
            grabInteractable.enabled = false; // Ensure the XR Grab Interactable is initially disabled
        }
    }

    public void ToggleSize()
    {
        if (whiteboard == null)
        {
            Debug.LogError("Whiteboard not found. Reinitializing.");
            InitializeWhiteboard();

            if (whiteboard == null)
            {
                Debug.LogError("Failed to find Whiteboard after reinitialization.");
                return;
            }
        }

        // Make the whiteboard smaller and grabable
        whiteboard.transform.localScale = smallSize;
        rb.useGravity = true;

        // Disable the mesh collider and enable the box collider
        if (meshCollider != null)
        {
            meshCollider.enabled = false;
        }
        rb.isKinematic = false;
        if (boxCollider != null)
        {
            Debug.Log("Enabled");
            boxCollider.enabled = true;
        }

        // Enable the XR Grab Interactable
        if (grabInteractable != null)
        {
            grabInteractable.enabled = true;
        }

        // Apply a slight forward and downward force
        Vector3 forceDirection = whiteboard.transform.forward * 2.0f + Vector3.up * 5.0f;
        rb.AddForce(forceDirection, ForceMode.Impulse);

        // Untag the whiteboard
        whiteboard.tag = "Untagged";
    }
}
