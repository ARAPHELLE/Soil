using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
    }

    public void LoadTestWorld()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/ARAPHELLE/Soil");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
