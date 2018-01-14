using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnLoss : MonoBehaviour {

    Board thisBoard;

	// Use this for initialization
	void Start ()
    {
        thisBoard = GetComponent<Board>();
	}
	
	public void ExplodeBoard()
    {
        GameObject parentObj = thisBoard.gameObject;

        foreach (Transform child in parentObj.transform)
        {
            if (child)
            {
                if (!child.name.Equals("ColumnHighlight") && !child.name.Equals("LeftWall") && !child.name.Equals("RightWall") && !child.name.Equals("ComboText") && !child.name.Equals("Backing"))
                {
                    child.gameObject.AddComponent<Rigidbody>();
                    child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), 0f));
                }
            }
        }
    }
}
