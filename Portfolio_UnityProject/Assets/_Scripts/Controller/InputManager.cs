using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

enum InputMode
{
    GAMEPLAY, UI
}

public class InputManager : PersistentSingleton<InputManager>
{
    [Header("Settings")]
    [SerializeField] private InputMode inputMode;
    [SerializeField] private float interactionButtonTimerDuration = 5f;
    [Header("GameEvents")]
    [SerializeField] private GameEvent onInteractionButtonPressed;
    [Header("References")]
    [SerializeField] private GameObject MobileUI;

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    private PlayerInput playerInput;
    private InputSystem_Actions playerInputActions;
    private bool mobileBrowserDetected = false;

    private Timer interactionButtonTimer;
    private bool disableInteractionBtnFunctionality = false;
    #region Getters/Setters
    public InputSystem_Actions PlayerInputActions {  get { return playerInputActions; } }
    #endregion

    #region Initialization
    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    private void Initialize()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new InputSystem_Actions();

        if(isRunningOnMobile())
        {
            mobileBrowserDetected = true;
            Instantiate(MobileUI);
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += InteractionButtonPressed;
        interactionButtonTimer = new Timer(interactionButtonTimerDuration);

        inputMode = InputMode.GAMEPLAY;

    }

    private bool isRunningOnMobile()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        return IsMobile();
    #else
        return false;
    #endif
    }
    #endregion

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            UIMode();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            GameplayMode();
        }

        InteractionButtonTimerRunner();
    }

    #region InputModes
    public void UIMode()
    {
        //Unity Component Method
        playerInput.SwitchCurrentActionMap("UI");

        //C# Method
        playerInputActions.Player.Disable();
        playerInputActions.UI.Enable();

        inputMode = InputMode.UI;
    }

    public void GameplayMode()
    {
        //Unity Component Method
        playerInput.SwitchCurrentActionMap("Player");

        //C# Method
        playerInputActions.Player.Enable();
        playerInputActions.UI.Disable();

        inputMode = InputMode.GAMEPLAY;
    }

    public void Pause()
    {
        UIMode();
    }

    public void Resume()
    {
        GameplayMode();
    }
    #endregion

    #region Buttons
    public void InteractionButtonPressed(InputAction.CallbackContext context)
    {
        if(disableInteractionBtnFunctionality)
        {
            return;
        }

        if (!context.performed)
        {
            return;
        }

        interactionButtonTimer.ResetTimer();
        interactionButtonTimer.Started = true;
        onInteractionButtonPressed.Raise(this, 0);

        //Debug.Log("Pressed" + context.phase);
    }

    public void Submit(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        //Debug.Log("Submit" + context.phase);
    }
    #endregion

    #region TimerRunners
    public void InteractionButtonTimerRunner()
    {
        if (interactionButtonTimer.Started)
        {
            if (interactionButtonTimer.Running(Time.deltaTime))
            {
                disableInteractionBtnFunctionality = true;
            }
            else
            {
                disableInteractionBtnFunctionality = false;
                interactionButtonTimer.Started = false;
            }
        }
    }
    #endregion

}
