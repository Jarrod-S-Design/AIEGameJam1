﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerSpawns;
    [SerializeField] GameObject bomberangPrefab;

    GameObject[] players;
    GameObject bombGO;
    Bomberang bomberang;

    int playerCount = 0;

    void Awake()
    {
        players = new GameObject[4] { null, null, null, null };
    }

    private void Start()
    {
        AddPlayer(XboxController.First);
        AddPlayer(XboxController.Second);

        CreateBomb(Random.Range(0, playerCount));
    }

    void Update()
    {

        if (bomberang.isExploded)
        {

        }

        // Character selection

        //// For each player number
        //for (int i = 0; i < 4; i++)
        //{
        //    XboxController num = (XboxController)(i + 1);

        //    if (players[i] == null && XCI.GetButton(XboxButton.A, num))
        //    {
        //        AddPlayer(num);
        //    }
        //}

        //if (players[0] == null && Input.GetKeyDown(KeyCode.Z))
        //{
        //    AddPlayer(XboxController.First);
        //}
        //if (players[1] == null && Input.GetKeyDown(KeyCode.X))
        //{
        //    AddPlayer(XboxController.Second);
        //}
        //if (players[2] == null && Input.GetKeyDown(KeyCode.C))
        //{
        //    AddPlayer(XboxController.Third);
        //}
        //if (players[3] == null && Input.GetKeyDown(KeyCode.V))
        //{
        //    AddPlayer(XboxController.Fourth);
        //}

    }

    void CreateBomb(int playerNum)
    {
        // Create the bomb
        bombGO = Instantiate(bomberangPrefab, Vector3.zero, bomberangPrefab.transform.rotation);
        bomberang = bombGO.GetComponent<Bomberang>();
        bomberang.HitPlayer(players[playerNum]);
    }

    void AddPlayer(XboxController number)
    {
        playerCount++; 

        GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players[(int)number - 1] = newPlayer;
        newPlayer.GetComponent<PlayerController>().controllerNumber = number;

        newPlayer.transform.position = playerSpawns.transform.GetChild((int)number - 1).position;
    }

    void RespawnPlayer(int playerNum)
    {
        players[playerNum].transform.position = playerSpawns.transform.GetChild(playerNum).position;
    }
}
