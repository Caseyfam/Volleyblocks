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
        originalColor = newCamera.backgroundColor;
	    try
        {
            selectedScene = GameObject.Find("PassedObject").GetComponent<SelectedScene>();
            SetSceneObject(selectedScene.GetSceneName());
        }	
        catch
        {
            Debug.LogError("Could not find PassedObject. Did you load through Menu?");
            SetSceneObject("glitch");
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
                isGlitchScene = true;
                glitchAllObjects = StartCoroutine(WaitToGlitch(0.3f));
                StartCoroutine(IncrementGlitch(5f));
                break;
        }
    }

    Coroutine glitchAllObjects;
    int glitchIndex = 0;
    public Camera newCamera;
    private Color originalColor;
    public GameObject rotatingBackground, rotatingBackground2;
    public GameObject glitchFallBlocks, glitchCharRotate, glitchMenuItems;
    private int maxGlitchIndexes = 4;

    bool actionPerformed = false;
    bool isGlitchScene = false;

    void Update()
    {
        if (isGlitchScene)
        {
            switch (glitchIndex)
            {
                default:
                case 0:
                    glitchMenuItems.SetActive(false); // DEBUG
                    if (!actionPerformed)
                    {
                        actionPerformed = true;
                        glitchAllObjects = StartCoroutine(WaitToGlitch(0.3f));
                    }
                    break;
                case 1:
                    if (!actionPerformed)
                    {
                        try
                        {
                            StopCoroutine(glitchAllObjects);
                        }
                        catch
                        {

                        }
                        actionPerformed = true;
                    }
                    rotatingBackground.SetActive(true);
                    rotatingBackground2.SetActive(true);
                    rotatingBackground.transform.Rotate(Vector3.left, 10f * Time.deltaTime);
                    rotatingBackground2.transform.Rotate(Vector3.left, 10f * Time.deltaTime);
                    break;
                case 2:
                    newCamera.backgroundColor = Color.black;
                    glitchFallBlocks.SetActive(true);
                    rotatingBackground.SetActive(false);
                    rotatingBackground2.SetActive(false);
                    break;
                case 3:
                    newCamera.backgroundColor = Color.white;
                    glitchFallBlocks.SetActive(false);
                    glitchCharRotate.SetActive(true);
                    break;
                case 4:
                    glitchCharRotate.SetActive(false);
                    glitchMenuItems.SetActive(true);
                    break;
            }
        }
    }

    IEnumerator IncrementGlitch (float time)
    {
        yield return new WaitForSeconds(time);
        glitchIndex++;
        newCamera.backgroundColor = originalColor;
        actionPerformed = false;
        if (glitchIndex > maxGlitchIndexes)
        {
            glitchIndex = 0;
        }
        if (glitchIndex == 0)
        {
            glitchAllObjects = StartCoroutine(WaitToGlitch(0.3f));
        }
        else
        {
            try
            {
                StopCoroutine(glitchAllObjects);
            }
            catch
            {

            }
            SetGlitchObjectsInactive();
        }
        StartCoroutine(IncrementGlitch(time));
    }

    IEnumerator WaitToGlitch(float time)
    {
        if (glitchIndex == 0)
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
            glitchAllObjects = StartCoroutine(WaitToGlitch(time));
        }
    }

    void SetGlitchObjectsInactive()
    {
        scenes.beachScene.SetActive(false);
        scenes.labScene.SetActive(false);
        scenes.foyerScene.SetActive(false);
        scenes.shrineScene.SetActive(false);
        scenesSetActive = false;
    }

}
