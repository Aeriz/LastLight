using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {

    public Canvas pauseMenu;
    public GameObject altStart;
    public GameObject altExit;

    CursorLockMode wantedMode;

    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        altStart.SetActive(false);
        altExit.SetActive(false);

        Cursor.visible = false;

        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.gameObject.active == false)
        {
            Time.timeScale = 1 - Time.timeScale;
            pauseMenu.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit()
    {
        Application.LoadLevel(0);
    }

    public void ResumeGameEnter()
    {
        altStart.SetActive(true);
    }

    public void ResumeGameExit()
    {
        altStart.SetActive(false);
    }

    public void ExitEnter()
    {
        altExit.SetActive(true);
    }

    public void ExitExit()
    {
        altExit.SetActive(false);
    }
}
