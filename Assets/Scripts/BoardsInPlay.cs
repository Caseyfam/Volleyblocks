using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardsInPlay : MonoBehaviour {

    public Board leftBoard, rightBoard;
    public GameObject leftCPU, rightCPU, leftPlayer, rightPlayer;
    private string playerCase = "Player VS CPU";
    void Awake()
    {
        // try catch get player vs opponent info from passed object
        // then set correct boards

        leftCPU.SetActive(false);
        rightCPU.SetActive(false);
        leftPlayer.SetActive(false);
        rightPlayer.SetActive(false);

        try
        {
            playerCase = GameObject.Find("PassedObject").GetComponent<Passed>().playersInPlay;
            switch (playerCase)
            {
                default:
                case "Player VS CPU":
                    leftPlayer.SetActive(true);
                    leftBoard = leftPlayer.GetComponent<Board>();
                    rightCPU.SetActive(true);
                    rightBoard = rightCPU.GetComponent<Board>();
                    break;
                case "Player VS Player":
                    leftPlayer.SetActive(true);
                    leftBoard = leftPlayer.GetComponent<Board>();
                    rightPlayer.SetActive(true);
                    rightBoard = rightPlayer.GetComponent<Board>();
                    break;
                case "CPU VS CPU":
                    leftCPU.SetActive(true);
                    leftBoard = leftCPU.GetComponent<Board>();
                    rightCPU.SetActive(true);
                    rightBoard = rightCPU.GetComponent<Board>();
                    break;
            }
        }
        catch
        {
            Debug.LogError("ERROR: Could not set up which players are in play.");
        }
    }
}
