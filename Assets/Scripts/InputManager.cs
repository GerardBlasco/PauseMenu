using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] InputActionAsset movementMap;
    [SerializeField] InputActionAsset menuesMap;

    public InputAction horizontalX_ia;
    public InputAction horizontalZ_ia;
    public InputAction sprint_ia;
    public InputAction shoot_ia;
    public InputAction pauseMenu_ia;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

        EnableInput();

        horizontalX_ia = movementMap.FindActionMap("Movement").FindAction("HorizontalX");
        horizontalZ_ia = movementMap.FindActionMap("Movement").FindAction("HorizontalZ");
        sprint_ia = movementMap.FindActionMap("Movement").FindAction("Sprint");
        shoot_ia = movementMap.FindActionMap("Attack").FindAction("Shoot");
        pauseMenu_ia = menuesMap.FindActionMap("PauseMenu").FindAction("ToggleMenu");
    }

    public void EnableInput()
    {
        movementMap.Enable();
        menuesMap.Enable();
    }

    public void DisableInput()
    {
        movementMap.Disable();
        menuesMap.Disable();
    }
}
