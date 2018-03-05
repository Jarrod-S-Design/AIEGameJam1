using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerSpawns;

    GameObject[] players;

    int playerCount = 0;

    void Awake()
    {
        players = new GameObject[4] { null, null, null, null };

    }

    void Update()
    {

        // Character selection

        // For each player number
        for (int i = 0; i < 4; i++)
        {
            XboxController num = (XboxController)(i + 1);

            if (players[i] == null && XCI.GetButton(XboxButton.A, num))
            {
                AddPlayer(num);
            }
        }

        if (players[0] == null && Input.GetKeyDown(KeyCode.Z))
        {
            AddPlayer(XboxController.First);
        }
        if (players[1] == null && Input.GetKeyDown(KeyCode.X))
        {
            AddPlayer(XboxController.Second);
        }
        if (players[2] == null && Input.GetKeyDown(KeyCode.C))
        {
            AddPlayer(XboxController.Third);
        }
        if (players[3] == null && Input.GetKeyDown(KeyCode.V))
        {
            AddPlayer(XboxController.Fourth);
        }

    }

    void AddPlayer(XboxController number)
    {
        playerCount++; 

        GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players[(int)number - 1] = newPlayer;
        newPlayer.GetComponent<PlayerController>().controllerNumber = number;

        newPlayer.transform.position = playerSpawns.transform.GetChild((int)number - 1).position;
    }
}
