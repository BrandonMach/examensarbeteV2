using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Analytics;


public class CrossyroadsManager : MonoBehaviour
{

    #region Singelton
    static CrossyroadsManager _instance;
    public static CrossyroadsManager Instance { get => _instance; set => _instance = value; }
    #endregion

    [SerializeField] JoyConCrossyroads[] _playerArray;

    bool declaredWinner;
    string winnerName;
    public bool StartTheGame; //Only start the game when all player have joined
    public bool GameOver;
    public GameObject GOPanel;
    public GameObject PlayerInfoBackdropGO;
    public GameObject readyUpPanel;
    public TMP_Text winnerNameText, timerTxt;
    float timer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;

    }
    void Start()
    {
        readyUpPanel.SetActive(true);
        PlayerInfoBackdropGO.SetActive(false);
        timer = 60;
    }

    public void UpdatePlayerArray()
    {
        _playerArray = FindObjectsOfType<JoyConCrossyroads>();
    }


    void FixedUpdate()
    {
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) //Start the game once all player are ready 
        {
            StartTheGame = true;
            readyUpPanel.SetActive(false);
            PlayerInfoBackdropGO.SetActive(true);
            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false

            }
        }

        if (GameOver)
        {
            GOPanel.SetActive(true);
            winnerNameText.SetText(winnerName);
        }
        if (StartTheGame)
        {
            readyUpPanel.SetActive(false);
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                EndGame();
            }
            timerTxt.SetText(timer.ToString("#.00"));
        }
       
    }

    private void EndGame()
    {
        //Anything that should happen when the game ends
        GameOver = true;
        GOPanel.SetActive(true);
    }
}
