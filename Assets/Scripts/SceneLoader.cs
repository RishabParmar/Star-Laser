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

    public void LoadNextSceneWithDelay()
    {
        int currentSceneId = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(WaitForDelay(currentSceneId));        
    }

    private IEnumerator WaitForDelay(int sceneId)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneId + 1);
    }

    public void RestartTheGame()
    {
        Enemy.score = 0;
        SceneManager.LoadScene(0);
    }    

    public void QuitGame()
    {
        Application.Quit();
    }
}
