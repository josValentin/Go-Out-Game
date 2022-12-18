using UnityEngine;
using ProjectUtils;
public class MouseLook : MonoBehaviour
{
    public static MouseLook Instance;

    [SerializeField] private float minSensitivity = 2;
    [SerializeField] private float maxSensitivity = 15;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Animator camAnim;
    [SerializeField] private float interactableRayDistance;
    [SerializeField] private LayerMask interactableMask;

    private InputMaster controls;
    private Vector2 mouseLook;
    private float Xrotation = 0;
    private Interactable currentInteraction;

    private void Awake()
    {
        if (Instance != null) Instance.gameObject.SetActive(false);

        Instance = this;
        controls = new InputMaster();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Running(bool value) => camAnim.SetBool("Running", value);

    public void Shake(float shakeTime = 0.4f)
    {
        camAnim.SetBool("Shaking", true);
        this.ActionAfterTime(shakeTime, StopShake);
    }
    public void StopShake() => camAnim.SetBool("Shaking", false);
    public void Die() => camAnim.SetTrigger("Die");

    private void Update()
    {
        if (!PlayerController.ControlEnabled) return;
        Look();
        InteractionDetection();
    }
    private void Look()
    {
        mouseLook = controls.Player.Look.ReadValue<Vector2>();
        float sensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, Options.CameraSensitivity);
        float mouseX = mouseLook.x * sensitivity * Time.deltaTime;
        float mouseY = mouseLook.y * sensitivity * Time.deltaTime;
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90, 90);
        transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    private void OnEnable()
    {
        if (controls != null) controls.Enable();
    }
    private void OnDisable()
    {
        if (controls != null) controls.Disable();
    }


    private void InteractionDetection()
    {
        bool interactionFound = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactableRayDistance, interactableMask);

        if (interactionFound)
        {
            Interactable newInteraction = hit.transform.GetComponentInParent<Interactable>();
            if (!newInteraction.enabled)
            {
                currentInteraction = null;
                return;
            }

            if (currentInteraction != newInteraction)
            {
                Debug.Log("New interaction found!");
                currentInteraction = newInteraction;
                InteractionUI.ShowInteraction(currentInteraction);
            }

            if (Input.GetKeyDown(currentInteraction.keyToPress))
            {
                //...
                currentInteraction.Evaluate(PlayerController.Instance.selectedItem);
                currentInteraction = null;
                InteractionUI.Hide();

            }
        }
        else
        {
            if (currentInteraction != null)
            {
                InteractionUI.Hide();
                currentInteraction = null;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * interactableRayDistance);
    }
}
