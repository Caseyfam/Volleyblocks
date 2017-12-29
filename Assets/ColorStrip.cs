using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStrip : MonoBehaviour
{
    private int numSegments = 0;
    private List<Color32> storedColors = new List<Color32>();

    private Color32[] allColors = new Color32[23];

    private List<GameObject> storedSegments = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        allColors[0] = new Color32(255, 255, 255, 255); // white
        allColors[1] = new Color32(255, 0, 0, 255); // red
        allColors[2] = new Color32(255, 150, 0, 255); // orange
        allColors[3] = new Color32(255, 255, 0, 255); // yellow
        allColors[4] = new Color32(0, 255, 0, 255); // green
        allColors[5] = new Color32(0, 0, 255, 255); // blue
        allColors[6] = new Color32(0, 255, 255, 255); // cyan
        allColors[7] = new Color32(150, 0, 255, 255); // purple
        allColors[8] = new Color32(255, 100, 100, 255); // light red
        allColors[9] = new Color32(255, 150, 100, 255); // light orange
        allColors[10] = new Color32(255, 255, 100, 255); // light yellow
        allColors[11] = new Color32(100, 255, 100, 255); // light green
        allColors[12] = new Color32(100, 255, 255, 255); // light cyan
        allColors[13] = new Color32(100, 100, 255, 255); // light blue
        allColors[14] = new Color32(150, 100, 255, 255); // light purple
        allColors[15] = new Color32(255, 175, 175, 255); // lightest red
        allColors[16] = new Color32(255, 150, 175, 255); // lightest orange
        allColors[17] = new Color32(255, 255, 175, 255); // lightest yellow
        allColors[18] = new Color32(100, 255, 175, 255); // lightest green
        allColors[19] = new Color32(175, 255, 255, 255); // lightest cyan
        allColors[20] = new Color32(175, 175, 255, 255); // lightest blue
        allColors[21] = new Color32(150, 175, 255, 255); // lightest purple
        allColors[22] = new Color32(0, 0, 0, 255); // black
    }
	
	public void UpdateStrip(Color32 passedColor)
    {
        foreach(GameObject seg in storedSegments)
        {
            Destroy(seg);
        }

        if (!storedColors.Contains(passedColor))
        {
            for (int i = 0; i < allColors.Length; i++)
            {
                if (allColors[i].Equals(passedColor))
                {
                    if (i > numSegments)
                    {
                        numSegments = i;
                    }
                    break;
                }
            }

            for (int i = 0; i <= numSegments; i++)
            {
                if (!storedColors.Contains(allColors[i]))
                {
                    storedColors.Add(allColors[i]);
                }
            }
        }

        for (int i = 1; i <= numSegments; i++)
        {
            // Trying to position color strip segments

            GameObject segment = (GameObject)Instantiate(Resources.Load("ColorStripSegment"));
            segment.transform.position = new Vector2(0f, 0f);
            segment.transform.localScale = new Vector2((Screen.width/10) / numSegments, 1f);

            Vector2 lastPosition = Vector2.zero;
            if (i == 1)
            {
                segment.transform.position = new Vector2(0f, 0f);
            }
            else
            {
                segment.transform.position = new Vector2(lastPosition.x + i, 0f);
            }
            lastPosition = segment.transform.position;

            segment.GetComponent<SpriteRenderer>().color = allColors[i];
            segment.name = "segment" + i;
            storedSegments.Add(segment);

        }
        /*
        if (!storedColors.Contains(passedColor))
        {
            Debug.Log(passedColor);
            storedColors.Add(passedColor);
            numSegments++;
        }
        */
    }

}
