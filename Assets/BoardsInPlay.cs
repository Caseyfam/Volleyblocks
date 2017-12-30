using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardsInPlay : MonoBehaviour {

    public Board leftBoard, rightBoard;

    public GameObject playerBoard, player2Board, leftCPU, rightCPU;

    void Awake()
    {
        // try catch get player vs opponent info from passed object
        // then set correct boards
        playerBoard.SetActive(false);
        player2Board.SetActive(false);
        leftCPU.SetActive(false);
        rightCPU.SetActive(false);
        try
        {
            switch (GameObject.Find("PassedObject").GetComponent<PassedMenu>().versus)
            {
                case "Player VS Player":
                    playerBoard.SetActive(true);
                    player2Board.SetActive(true);
                    leftBoard = playerBoard.GetComponent<Board>();
                    rightBoard = player2Board.GetComponent<Board>();
                    break;
                case "CPU VS CPU":
                    leftCPU.SetActive(true);
                    rightCPU.SetActive(true);
                    leftBoard = leftCPU.GetComponent<Board>();
                    rightBoard = rightCPU.GetComponent<Board>();
                    break;
                case "Player VS CPU":
                default:
                    playerBoard.SetActive(true);
                    rightCPU.SetActive(true);
                    leftBoard = playerBoard.GetComponent<Board>();
                    rightBoard = rightCPU.GetComponent<Board>();
                    break;
            }
        }
        catch
        {

        }
    }
}
