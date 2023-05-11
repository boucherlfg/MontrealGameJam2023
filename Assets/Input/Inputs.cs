using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_ANDROID
public class Inputs : SingletonBehaviour<Inputs>
{
    public FixedJoystick joystick;
    public Toggle fire;
    public Toggle sprint;
    public Button potion;
    public Button switchLeft;
    public Button switchRight;
    public Button pause;

    private Controls controls;
    protected override void Awake()
    {
        base.Awake();
        controls = new Controls();
        controls.Player.Enable();

        fire.onValueChanged.AddListener(Fire);
        sprint.onValueChanged.AddListener(Sprint);
        switchLeft.onClick.AddListener(SwitchLeft);
        switchRight.onClick.AddListener(SwitchRight);
        potion.onClick.AddListener(Potion);
        pause.onClick.AddListener(Pause);
    }

    private void Pause()
    {
        Paused?.Invoke();
    }

    private void Potion()
    {
        Potioned?.Invoke();
    }

    private void SwitchRight()
    {
        Switched?.Invoke(1);
    }

    private void SwitchLeft()
    {
        Switched?.Invoke(-1);
    }

    private void Fire(bool value)
    {
        Clicked?.Invoke(value);
    }

    private void Sprint(bool value)
    {
        Sprinted(value);
    }

    public event Action<int> Switched; //
    public event Action<bool> Sprinted;//
    public event Action Potioned; //
    public event Action Paused;
    public event Action<bool> Clicked; //

    public Vector2 Move => joystick.Direction.normalized;
}
#else
public class Inputs : Singleton<Inputs>
{
    private Controls controls;
    public Inputs()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.Click.performed += ClickActive;
        controls.Player.Click.canceled += Click_Inactive;
        controls.Player.Pause.performed += OnPause;
        controls.Player.Potion.performed += OnPotion;
        controls.Player.Sprint.performed += OnSprintActive;
        controls.Player.Sprint.canceled += OnSprintInactive;
        controls.Player.Switch.performed += OnSwitch;
        
    }

    private void Click_Inactive(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Clicked?.Invoke(false);
    }

    private void OnSprintInactive(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Sprinted?.Invoke(false);
    }
    private void OnSwitch(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var value = Mathf.RoundToInt(obj.ReadValue<float>());
        Switched?.Invoke(value);
    }
    private void OnSprintActive(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Sprinted?.Invoke(true);
    }
    private void OnPotion(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Potioned?.Invoke();
    }
    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Paused?.Invoke();
    }
    private void ClickActive(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Clicked?.Invoke(true);
    }

    public event System.Action<int> Switched;
    public event System.Action<bool> Sprinted;
    public event System.Action Potioned;
    public event System.Action Paused;
    public event System.Action<bool> Clicked;

    public Vector2 Direction => controls.Player.Direction.ReadValue<Vector2>();
    public Vector2 Move => controls.Player.Move.ReadValue<Vector2>();
    public Vector2 MousePosition => controls.Player.MousePos.ReadValue<Vector2>();
}
#endif