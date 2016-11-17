using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {

    public Canvas pauseMenu;
    public GameObject altStart;
    public GameObject altExit;

    // Use this for initialization
    void Start() {
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
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        Cursor.visible = false;
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
