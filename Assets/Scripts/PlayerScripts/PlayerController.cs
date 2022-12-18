using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public static bool ControlEnabled = true;
    public InputMaster controls;
    public Transform groundCheck;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;

    [SerializeField] [Range(-1000, 1000)] private float movementSpeed = 100f;
    [SerializeField] [Range(-1000, 1000)] private float gravity = -9.81f;
    [SerializeField] [Range(-1000, 1000)] private float jumpHeight = 2.4f;
    [SerializeField] [Range(-1000, 1000)] private float holdJumpDelta = 6f;
    [SerializeField] [Range(-1000, 1000)] private float publicErrorMargin = 6f;

    private Vector3 velocity;
    private Vector2 move;
    private CharacterController controller;
    private bool isGrounded;
    private bool isHoldingJump;

    private ItemInHand[] items;
    [HideInInspector] public ItemInHand selectedItem;


    private void Start()
    {
        if (Instance != null)
        {
            Instance.gameObject.SetActive(false);
            //Destroy(gameObject);
            //return;
        }
        Instance = this;
        controls = new InputMaster();
        controls.Enable();
        controller = GetComponent<CharacterController>();
        items = GetComponentsInChildren<ItemInHand>(true);
    }
    private void OnEnable()
    {
        if (controls != null) controls.Enable();
    }
    private void OnDisable()
    {
        if (controls != null) controls.Disable();
    }
    public static void SetControl(bool value) => ControlEnabled = value;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            PlayerStateUI.Instance.TakeDamage(0.75f);
            return;
        }

        if (other.CompareTag("MonsterDamage"))
        {
            PlayerStateUI.Instance.TakeDamage(1.5f);
            return;
        }
    }
    private void Update()
    {
        ScapeMenuCheck();

        if (!ControlEnabled)
        {
            MouseLook.Instance.Running(false);
            return;
        }
        Gravity();
        PlayerMovement();
        Jump();

        PlayerAction();
    }

    private void ScapeMenuCheck()
    {
        if (controls.Player.Pause.WasPressedThisFrame())
        {
            if (!PauseUI.IsShown)
                PauseUI.Show();
            else
                PauseUI.Hide();

        }
    }

    void PlayerAction()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (selectedItem != null)
                selectedItem.UseItem();
        }
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void PlayerMovement()
    {
        move = controls.Player.Movment.ReadValue<Vector2>();
        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        if (movement.normalized.sqrMagnitude < 0.01f)
        {
            MouseLook.Instance.Running(false);
            StepsSound.StopSteps();
            return;
        }

        float targetSpeed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed *= 2;
            MouseLook.Instance.Running(true);
            StepsSound.PlaySteps(0.3f);
        }
        else
        {
            MouseLook.Instance.Running(false);
            StepsSound.PlaySteps(0.7f);

        }

        if (!isGrounded) StepsSound.StopSteps();

        controller.Move(movement * targetSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        //if (!isGrounded) return;


        if (controls.Player.Jump.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isHoldingJump = true;

        }

        else if (controls.Player.Jump.IsPressed() && isHoldingJump)
        {
            velocity.y += holdJumpDelta * Time.deltaTime;
            if (NumIsCloseToTarget(velocity.y, 0, publicErrorMargin))
            {
                isHoldingJump = false;
            }
        }

        else if (controls.Player.Jump.WasReleasedThisFrame())
        {
            isHoldingJump = false;
        }
    }

    public void SwitchTool(ItemData itemData)
    {
        //Debug.Log(itemType.ToString());
        if (selectedItem != null) selectedItem.gameObject.SetActive(false);

        if (itemData == null)
        {
            selectedItem = null;
            return;
        }

        foreach (ItemInHand item in items)
        {
            if (item.data == itemData)
            {
                item.gameObject.SetActive(true);
                selectedItem = item;
                return;
            }
            //item.gameObject.SetActive(false);
        }
    }

    private bool NumIsCloseToTarget(float valueToBeVerified, float target, float errorMargin)
    => (valueToBeVerified > target - errorMargin) && (valueToBeVerified < target + errorMargin);
}

