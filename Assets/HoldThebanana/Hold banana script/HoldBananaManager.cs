using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;


public class HoldBananaManager : MonoBehaviour
{

    #region Singelton
    static HoldBananaManager _instance;
    public HoldBananaManager Instance { get => _instance; set => _instance = value; }
    #endregion




   

    [SerializeField] HoldBananaPlayerScript[] _playerArray;
    public bool StartTheGame; //Only start the game when all player have joined

    public bool AddPlayerPoints;



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

        _playerArray = FindObjectsOfType<HoldBananaPlayerScript>();


    }

    void Update()
    {




        //Check if all players have ready up
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) //Start the game once all player are ready 
        {
            StartTheGame = true;
            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false
                AddPlayerPoints = true;
            }
        }

        if (AddPlayerPoints)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var player in _playerArray)
                {
                    if(player.GetComponent<JoyconPlayerBase>().jc_ind == 0 && Input.GetKey(KeyCode.Space))
                    {
                        player.GetComponent<HoldBananaPlayerScript>().AddPoints();
                        //If key stops gettig hold down go to nect player
                    }
                    
                }
            }
            
        }

        



    }


    void CheckIfAllPlayersAreReady()
    {

    }
}
