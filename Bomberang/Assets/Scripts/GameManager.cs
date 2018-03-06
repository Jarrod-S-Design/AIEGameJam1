using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerLegs;
    [SerializeField] GameObject player1BodyPrefab;
    [SerializeField] GameObject player2BodyPrefab;
    [SerializeField] GameObject playerSpawns;
    [SerializeField] GameObject bomberangPrefab;

    [SerializeField] Text timer;
    [SerializeField] Text p1Deaths;
    [SerializeField] Text p2Deaths;
    [Header("Variables")]
    [SerializeField] int maxRounds;
    [SerializeField] float newRoundStartTime;

    int roundCount;

    GameObject[] players;
    GameObject bombGO;

    List<PlayerController> deadPlayers = new List<PlayerController>();

    [HideInInspector]
    public Bomberang bomberang;

    int playerCount;

    float roundTimer;

    bool gameWon;

    int winningPlayerNumber;

    enum GameState
    {
        Menu = 0,
        RoundStart,
        Round,
        RoundEnd,
        GameEnd
    }

    GameState gameState;

    void Awake()
    {
        // Make sure there is only on gameManager
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        players = new GameObject[4] { null, null, null, null };
        playerCount = 0;
        roundTimer = 0;

        gameState = 0;
        gameWon = false;

        winningPlayerNumber = -1;
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        players = new GameObject[4] { null, null, null, null };
        playerCount = 0;
        roundCount = 0;

        gameWon = false;
        winningPlayerNumber = -1;

        AddPlayer(XboxController.First);
        AddPlayer(XboxController.Second);

        CreateBomb(Random.Range(0, playerCount));

        roundTimer = newRoundStartTime;
        gameState = GameState.RoundStart;
    }

    void EndGame()
    {
        // Destroy all of the player objects
        for (int i = 0; i < 4; i++)
        {
            if (players[i] != null)
            {
                Destroy(players[i]);
            }
        }
        players = new GameObject[4] { null, null, null, null };
        playerCount = 0;
    }

    void StartNewRound()
    {
        gameState = GameState.Round;

        roundCount++;

        // Setup the bomberang
        bomberang.ResetForNewRound();
        bomberang.HitPlayer(players[Random.Range(0, playerCount)]); // Follow a random player
        bomberang.isHeld = false;
        bomberang.transform.position = Vector3.zero;

        // Respawn all of the players
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

        switch (gameState)
        {
            case GameState.Menu:

                break;
            case GameState.RoundStart:
                UpdateRoundStart();
                break;
            case GameState.Round:
                UpdateRound();
                break;
            case GameState.RoundEnd:
                UpdateRoundEnd();
                break;
            case GameState.GameEnd:
                UpdateGameEnd();
                break;
            default:
                break;
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

    void UpdateRoundStart()
    {
        if (roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;
        }
        else
        {
            StartNewRound();
        }
    }

    void UpdateRound()
    {
        // Check for dead Players
        if (deadPlayers.Count > 0)
        {
            foreach (var player in deadPlayers)
                player.deaths++;

            deadPlayers.Clear();

            StartNewRound();
        }

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
    }

    void UpdateRoundEnd()
    {
        // Check if a player has won
        for (int i = 0; i < 4; i++)
        {
            // If the player has won more than half the max rounds
            if (players[i].GetComponent<PlayerController>().deaths >= Mathf.Ceil(maxRounds / 2.0f))
            {

            }
        }


        // Wait before starting a new round
        if (roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;
        }
        else
        {
            StartNewRound();
        }
    }

    void UpdateGameEnd()
    {

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
        PlayerController newPlayerController = newPlayer.GetComponent<PlayerController>();
        newPlayerController.controllerNumber = number;
        Instantiate(playerLegs, newPlayer.transform); // Create the legs
        GameObject newPlayerBody = null;
        switch (number)
        {
            case XboxController.All:
                break;
            case XboxController.First:
                newPlayerBody = Instantiate(player1BodyPrefab, newPlayer.transform.GetChild(0)); // Create the body
                break;
            case XboxController.Second:
                newPlayerBody = Instantiate(player2BodyPrefab, newPlayer.transform.GetChild(0)); // Create the body
                break;
            case XboxController.Third:
                break;
            case XboxController.Fourth:
                break;
            default:
                break;
        }

        newPlayerController.animator = newPlayerBody.GetComponent<Animator>();


        //newPlayer.transform.position = playerSpawns.transform.GetChild((int)number - 1).position;
    }

    void RespawnPlayer(int playerNum)
    {
        if (players[playerNum] == null)
            return;

        players[playerNum].transform.position = playerSpawns.transform.GetChild(playerNum).position;
        players[playerNum].GetComponent<PlayerController>().ResetForNewRound();
    }

    public void PlayerDied(PlayerController player)
    {
        deadPlayers.Add(player);
    }

}
