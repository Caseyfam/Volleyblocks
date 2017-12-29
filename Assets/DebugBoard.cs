using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBoard : MonoBehaviour {

    public string targetName;
    public static int rows = 12, columns = 6;
    public Sprite falseSprite, trueSprite, driveSprite;

    GameObject[,] storedObjects = new GameObject[rows, columns];
    Board board;

    void Awake()
    {
        board = GameObject.Find(targetName).GetComponent<Board>();
    }

	// Use this for initialization
	void Start ()
    {
        GameObject debugBox;
		for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                debugBox = (GameObject)Instantiate(Resources.Load("DebugBox"));
                debugBox.transform.SetParent(transform);
                debugBox.transform.localPosition = new Vector3(0.8f * i, -0.8f * j);
                storedObjects[j, i] = debugBox;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpriteRenderer blockRenderer;
		for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                blockRenderer = storedObjects[j, i].GetComponent<SpriteRenderer>();
                if (board.boardBools[j, i])
                {
                    blockRenderer.sprite = trueSprite;
                }
                else
                {
                    blockRenderer.sprite = falseSprite;
                }
                
                try
                {
                    switch (board.boardBlocks[j, i].GetComponent<Block>().blockColor)
                    {
                        case "Red":
                            blockRenderer.color = Color.red;
                            break;
                        case "Yellow":
                            blockRenderer.color = Color.yellow;
                            break;
                        case "Green":
                            blockRenderer.color = Color.green;
                            break;
                        case "Blue":
                            blockRenderer.color = Color.blue;
                            break;
                        case "RedDrive":
                            blockRenderer.color = Color.red;
                            break;
                        case "YellowDrive":
                            blockRenderer.color = Color.yellow;
                            break;
                        case "GreenDrive":
                            blockRenderer.color = Color.green;
                            break;
                        case "BlueDrive":
                            blockRenderer.color = Color.blue;
                            break;
                        default:
                            blockRenderer.color = Color.white;
                            break;
                    }
                }
                catch
                {
                    blockRenderer.color = Color.white;
                }
                
            }
        }
	}
}
