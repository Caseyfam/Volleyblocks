using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Delete))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
