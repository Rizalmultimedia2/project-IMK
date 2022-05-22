using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Controls _controls;
    public GameObject PauseMenu;
    public GameObject ControlMenu;
    public GameObject ConfirmationMenu;
    public bool isPaused;

    private void Awake()
    {
        _controls = new Controls();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.SetActive(true);
        ConfirmationMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void BackMenu(string menuScene)
    {
        PauseMenu.SetActive(false);
        ConfirmationMenu.SetActive(true);
    }
    public void CancelBack(string menuScene)
    {
        ConfirmationMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void MainMenu(string menuScene)
    {
        PauseMenu.SetActive(false);
        ConfirmationMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }

    public void CloseControl(string menuScene)
    {
        Time.timeScale = 1f;
        ControlMenu.SetActive(false);
    }



    public void Update()
    {
        if (_controls.Player.Pause.IsPressed())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
