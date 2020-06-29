using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;





public class Level : MonoBehaviour
{

    float delayTime = 1.5f;



    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadGame()
    {
        FindObjectOfType<GameScore>().ResetGame();
        SceneManager.LoadScene("Game");
       
    }

    public void LoadGameOver()
    {
        StartCoroutine(PlayerDie());
       
    }

    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
