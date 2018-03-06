using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerSpawns;
    [SerializeField] GameObject bomberangPrefab;

    GameObject[] players;
    GameObject bomb;

    int playerCount = 0;

    void Awake()
    {
        players = new GameObject[4] { null, null, null, null };
    }

    private void Start()
    {
        AddPlayer(XboxController.First);
        AddPlayer(XboxController.Second);

        int a = Random.Range(0, playerCount);
        Debug.Log(a, gameObject);
        CreateBomb(a);
    }

    void Update()
    {

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
        bomb = Instantiate(bomberangPrefab, Vector3.zero, bomberangPrefab.transform.rotation);
 
        //bomb.GetComponent<Bomberang>() players[playerNum];
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
