using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardSpawnerScript : MonoBehaviour
{
    public GameObject gameBoard;
    public void spawnGameBoard()
    {
        Instantiate(gameBoard, transform.position, transform.rotation);
    }
}
