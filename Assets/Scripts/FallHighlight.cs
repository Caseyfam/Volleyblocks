using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHighlight : MonoBehaviour {

    public GameObject highlight;

    private Vector3 singleCol = new Vector3(4f, 44.36835f, 4f);
    private Vector3 multiCol = new Vector3(8f, 44.36835f, 4f);


    public void UpdateHighlight(GameObject topBlock, GameObject bottomBlock, string orientation)
    {
        switch (orientation)
        {
            default:
            case "UP":
            case "DOWN":
                highlight.transform.position = new Vector3(bottomBlock.transform.position.x, highlight.transform.position.y, highlight.transform.position.z);
                highlight.transform.localScale = singleCol;
                break;
            case "LEFT":
                highlight.transform.position = new Vector3(bottomBlock.transform.position.x - 0.4f, highlight.transform.position.y, highlight.transform.position.z);
                highlight.transform.localScale = multiCol;
                break;
            case "RIGHT":
                highlight.transform.position = new Vector3(bottomBlock.transform.position.x + 0.4f, highlight.transform.position.y, highlight.transform.position.z);
                highlight.transform.localScale = multiCol;
                break;
        }
    }
}
