using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardsInPlay : MonoBehaviour {

    public Board leftBoard, rightBoard;

    void Awake()
    {
        // try catch get player vs opponent info from passed object
        // then set correct boards
    }
}
