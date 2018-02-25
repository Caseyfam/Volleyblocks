using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreenButtons : MonoBehaviour {

    public void StoryRetry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void StoryContinue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        Destroy(GameObject.Find("PassedObject"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
