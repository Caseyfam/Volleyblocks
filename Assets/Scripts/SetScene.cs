using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScene : MonoBehaviour {

    SelectedScene selectedScene;
	// Use this for initialization

    [System.Serializable]
    public class Scenes
    {
        public GameObject beachScene, shrineScene;
    }
    public Scenes scenes = new Scenes();

	void Awake ()
    {
	    try
        {
            selectedScene = GameObject.Find("PassedObject").GetComponent<SelectedScene>();
            SetSceneObject(selectedScene.GetSceneName());
        }	
        catch
        {
            Debug.LogError("Could not find PassedObject. Did you load through Menu?");
        }
	}

    void SetSceneObject(string sceneName)
    {
        switch (sceneName)
        {
            default:
            case "beach":
                scenes.beachScene.SetActive(true);
                break;
            case "shrine":
                scenes.shrineScene.SetActive(true);
                break;
        }
    }
	
}
