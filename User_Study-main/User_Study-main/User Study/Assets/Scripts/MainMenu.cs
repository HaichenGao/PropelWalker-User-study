using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void HandleStartButtonSceneOne()
    {
        SceneManager.LoadScene("-1.5, -1");
    }

    public void HandleStartButtonSceneTwo()
    {
        SceneManager.LoadScene("0, -1");
    }

    public void HandleStartButtonSceneThree()
    {
        SceneManager.LoadScene("0, 1");
    }

    public void HandleStartButtonSceneFour()
    {
        SceneManager.LoadScene("1.5, -1");
    }

    public void HandleStartButtonSceneFive()
    {
        SceneManager.LoadScene("1.5, 1");
    }

    public void HandleStartButtonSceneSix()
    {
        SceneManager.LoadScene("3, 1");
    }

    public void HandleStartButtonDLC()
    {
        SceneManager.LoadScene("DLC");
    }

    public void HandleStartTraining()
    {
        SceneManager.LoadScene("Training");
    }
}
