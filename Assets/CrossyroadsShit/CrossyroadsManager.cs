using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;


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
    public GameObject readyUpPanel;
    public TMP_Text winnerNameText;

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
        
    }

    public void UpdatePlayerArray()
    {
        _playerArray = FindObjectsOfType<JoyConCrossyroads>();
    }


    void Update()
    {
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) //Start the game once all player are ready 
        {
            StartTheGame = true;
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
        }
    }

    public void DeclareWinner(string name)
    {
        if (!declaredWinner)
        {
            winnerName = name;
        }
    }
}
