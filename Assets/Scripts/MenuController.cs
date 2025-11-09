using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum MenuState { OPEN, CLOSED, OPENING, CLOSING }
    [SerializeField] MenuState menuState = MenuState.CLOSED;

    InputManager inputManager;

    [SerializeField] CanvasGroup pauseMenu;

    float timeToOpenClose = 0.1f;
    float timeInCurrentState = 0;

    private void Start()
    {
        inputManager = InputManager.instance;
        pauseMenu = GetComponentInChildren<CanvasGroup>();
    }

    public void OnMenuChanged()
    {
        switch (menuState)
        {
            case MenuState.OPEN:
                pauseMenu.interactable = false;
                ChangeState(MenuState.CLOSING);
                break;
            case MenuState.CLOSED:
                pauseMenu.interactable = true;
                ChangeState(MenuState.OPENING);
                break;
            case MenuState.OPENING:
                break;
            case MenuState.CLOSING:
                break;
        }
    }

    private void Update()
    {
        if (inputManager.pauseMenu_ia.triggered)
        {
            OnMenuChanged();
        }
        timeInCurrentState += Time.deltaTime;
        UpdateCurrentState();
    }

    private void UpdateCurrentState()
    {
        switch (menuState)
        {
            case MenuState.OPEN:
                Time.timeScale = 0;
                break;
            case MenuState.CLOSED:
                Time.timeScale = 1;
                break;
            case MenuState.OPENING:
                if (timeInCurrentState < timeToOpenClose)
                {
                    float progress = timeInCurrentState / timeToOpenClose;
                    
                    pauseMenu.alpha = Mathf.Lerp(0, 1, progress);
                    Mathf.RoundToInt(pauseMenu.alpha);
                }
                else
                {
                    pauseMenu.alpha = 1;
                    ChangeState(MenuState.OPEN);
                }
                break;
            case MenuState.CLOSING:
                Time.timeScale = 1;
                if (timeInCurrentState < timeToOpenClose)
                {
                    float progress = timeInCurrentState / timeToOpenClose;

                    pauseMenu.alpha = Mathf.Lerp(1, 0, progress);
                    Mathf.RoundToInt(pauseMenu.alpha);
                }
                else
                {
                    pauseMenu.alpha = 0;
                    ChangeState(MenuState.CLOSED);
                }
                break;
        }
    }

    void ChangeState(MenuState newState)
    {
        ExitState();
        EnterState(newState);
        menuState = newState;
        timeInCurrentState = 0;
    }

    void EnterState(MenuState newState)
    {
        switch (menuState)
        {
            case MenuState.OPEN:
                menuState = MenuState.CLOSING;
                break;
            case MenuState.CLOSED:
                menuState = MenuState.OPENING;
                break;
            case MenuState.OPENING:
                break;
            case MenuState.CLOSING:
                break;
        }
    }

    void ExitState()
    {
        switch (menuState)
        {
            case MenuState.OPEN:
                break;
            case MenuState.CLOSED:
                break;
            case MenuState.OPENING:
                break;
            case MenuState.CLOSING:
                break;
        }
    }

    public void Interacted()
    {
        Debug.Log("Ha interactuado");
    }
}
