using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerSpawns;
    [SerializeField] GameObject bomberangPrefab;

    [SerializeField] Text timer;
    [SerializeField] Text p1Deaths;
    [SerializeField] Text p2Deaths;


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

        StartNewRound();
    }

    void StartNewRound()
    {
        bomberang.ResetForNewRound();
        bomberang.HitPlayer(players[Random.Range(0, playerCount)]); // Follow a random player
        bomberang.isHeld = false;
        bomberang.transform.position = Vector3.zero;

        for (int i = 0; i < 4; i++)
        {
            if (players[i] != null)
            {
                RespawnPlayer(i);
            }
        }
    }

    void Update()
    {

        if (bomberang.isExploded)
        {
            bomberang.currentPlayer.GetComponent<PlayerController>().deaths++;

            StartNewRound();
        }

        // Update the canvas
        if (bomberang.timer > 5)
        {
            timer.text = ((int)bomberang.timer).ToString();
        }
        else
        {
            timer.text = bomberang.timer.ToString("F2");

        }
        p1Deaths.text = players[0].GetComponent<PlayerController>().deaths.ToString();
        p2Deaths.text = players[1].GetComponent<PlayerController>().deaths.ToString();


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
    }

    void AddPlayer(XboxController number)
    {
        playerCount++; 

        GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players[(int)number - 1] = newPlayer;
        newPlayer.GetComponent<PlayerController>().controllerNumber = number;

        //newPlayer.transform.position = playerSpawns.transform.GetChild((int)number - 1).position;
    }

    void RespawnPlayer(int playerNum)
    {
        if (players[playerNum] == null)
            return;

        players[playerNum].transform.position = playerSpawns.transform.GetChild(playerNum).position;
        players[playerNum].GetComponent<PlayerController>().ResetForNewRound();
    }
}
