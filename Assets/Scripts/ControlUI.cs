using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Controls _controls;
    public GameObject ControlMenu;
    public GameObject PauseMenu;
    public bool isPaused;

    private void Awake()
    {
        _controls = new Controls();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        Debug.Log(PlayerPrefs.GetFloat("bearSpeed"));
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    public void CloseControl(string menuScene)
    {
        Time.timeScale = 1f;
        ControlMenu.SetActive(false);
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenControl(string menuScene)
    {
        Time.timeScale = 0f;
        ControlMenu.SetActive(true);
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Update()
    {

    }
}
