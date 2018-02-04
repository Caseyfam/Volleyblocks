using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedScene : MonoBehaviour {

    private string sceneName = "";

    public void SetSceneName(string scene)
    {
        sceneName = scene;
    }

    public string GetSceneName()
    {
        return sceneName;
    }
}
