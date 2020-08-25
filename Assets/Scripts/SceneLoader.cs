using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneId = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneId + 1);
    }

    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
    }    

    public void QuitGame()
    {
        Application.Quit();
    }
}
