using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScene : MonoBehaviour {

    SelectedScene selectedScene;
    // Use this for initialization
    bool scenesSetActive = false;

    public GameObject[] allObjects;

    [System.Serializable]
    public class Scenes
    {
        public GameObject beachScene, shrineScene, foyerScene, labScene;
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
            case "foyer":
                scenes.foyerScene.SetActive(true);
                break;
            case "lab":
                scenes.labScene.SetActive(true);
                break;
            case "glitch":
                StartCoroutine(WaitToGlitch(0.3f));
                break;
        }
    }


    IEnumerator WaitToGlitch(float time)
    {
        if (!scenesSetActive)
        {
            scenes.beachScene.SetActive(true);
            scenes.labScene.SetActive(true);
            scenes.foyerScene.SetActive(true);
            scenes.shrineScene.SetActive(true);
            scenesSetActive = true;
        }
        yield return new WaitForSeconds(time);
        GameObject selected = allObjects[Random.Range(0, allObjects.Length - 1)];
        selected.SetActive(!selected.activeSelf);
        try
        {
            selected.GetComponent<SpriteRenderer>().sortingOrder = -5;
        }
        catch { }
        StartCoroutine(WaitToGlitch(time));
    }
	
}
