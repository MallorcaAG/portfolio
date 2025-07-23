using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Vector2 movementDirection, holdDirection = new Vector2(0, -1);
    [Header("Graphics")]
    [SerializeField] private Animator animator;
    [SerializeField] private Direction facingDirection = Direction.South;
    [Header("Sensor")]
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private LayerMask includeLayers;
    [SerializeField] private bool isRaycastHit = false, showCastsDebug = false;
    [SerializeField] private Color raycastColor = Color.green;

    #region Caches, References, & Privated Global Variable
    private InputManager inputManager;
    private InputSystem_Actions action;

    private IInteractable currentInteraction;
    #endregion

    #region Initialization
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();

        inputManager = InputManager.Instance;

        action = inputManager.PlayerInputActions;
    }
    #endregion
    #region Runtime
    private void Update()
    {
        ProcessMovementInput();
    }

    private void FixedUpdate()
    {
        Move();

        Raycasting();
    }
    #endregion

    #region Movement
    private void ProcessMovementInput()
    {
        movementDirection = action.Player.Movement.ReadValue<Vector2>().normalized;

        GetFacingDirection();
    }

    private void GetFacingDirection()
    {
        if(movementDirection.x == 0f && movementDirection.y == 0f)
        {
            return;
        }

        holdDirection = movementDirection;

        Debug.Log(movementDirection);

        if (movementDirection.y > 0.1f) //North
        {
            if(movementDirection.x > 0.1f) //East
            {
                facingDirection = Direction.NorthEast;
                return;
            }
            else if(movementDirection.x < -0.1f) //West
            {
                facingDirection = Direction.NorthWest;
                return;
            }

            facingDirection = Direction.North;
            return;
        }
        else if(movementDirection.y < -0.1f) //South
        {
            if (movementDirection.x > 0.1f) //East
            {
                facingDirection = Direction.SouthEast;
                return;
            }
            else if (movementDirection.x < -0.1f) //West
            {
                facingDirection = Direction.SouthWest;
                return;
            }

            facingDirection = Direction.South;
            return;
        }

        if (movementDirection.x > 0.1f) //East
        {
            facingDirection = Direction.East;
            return;
        }
        else if (movementDirection.x < -0.1f) //West
        {
            facingDirection = Direction.West;
            return;
        }
    }


    private void Move()
    {
        rb.linearVelocity = new Vector2(movementDirection.x, movementDirection.y) * movementSpeed;
    }
    #endregion

    #region Interaction Logic
    private void Raycasting()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, holdDirection, interactionDistance, includeLayers);
        if(hit.collider == null)
        {
            currentInteraction = null;
            isRaycastHit = false;
            raycastColor = Color.green;
            return;
        }

        isRaycastHit = true;

        bool interactable = hit.collider.TryGetComponent<IInteractable>(out IInteractable interaction);
        if (interactable)
        {
            raycastColor = Color.magenta;
            currentInteraction = interaction;
        }
        else
        {
            raycastColor = Color.red;
            currentInteraction = null;
        }

    }

    public void InteractionInitiated()
    {
        if(currentInteraction == null)
        {
            return;
        }

        currentInteraction.Interact();
    }

    private void OnDrawGizmos()
    {
        if (!showCastsDebug)
        {
            return;
        }

        if (isRaycastHit)
        {
            Debug.DrawRay(transform.position, holdDirection * interactionDistance, raycastColor);
        }
        else
        {
            Debug.DrawRay(transform.position, holdDirection * interactionDistance, raycastColor);
        }
    }
    #endregion

}
public enum Direction
{
    North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest
}
