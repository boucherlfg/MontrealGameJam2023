using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
