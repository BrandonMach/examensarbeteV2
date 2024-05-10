using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

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
    public float timer;

    [SerializeField] GameObject _controls;
    

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
        // readyUpPanel.SetActive(true);
        PlayerInfoBackdropGO.GetComponent<RectTransform>().localScale = new Vector3(1, 2, 1);
    }

    public void UpdatePlayerArray()
    {
        _playerArray = FindObjectsOfType<JoyConCrossyroads>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    
        {
            if (GameOver)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
            else
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }


    void FixedUpdate()
    {
        _controls.SetActive(!StartTheGame);
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true) && !StartTheGame) //Start the game once all player are ready 
        {
            StartTheGame = true;
            // readyUpPanel.SetActive(false);
            //PlayerInfoBackdropGO.SetActive(true);

            StartCoroutine(ResizePlayerInfoBackdrop());
            
            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false
                players.SetStartPos();
            }

            
        }

        if (GameOver)
        {
            GOPanel.SetActive(true);
            winnerNameText.SetText(_playerArray[calculateWinner()].name);
        }
        if (StartTheGame)
        {
            //readyUpPanel.SetActive(false);
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



    public IEnumerator ResizePlayerInfoBackdrop()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerInfoBackdropGO.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    private int calculateWinner()
    {
        float currentMaxScore = 0;
        int winnerIndex = 0;
        for (int i = 0; i < _playerArray.Length; i++)
        {
            if (_playerArray[i].score > currentMaxScore)
            {
                currentMaxScore = _playerArray[i].score;
                winnerIndex = i;
            }
        }
        return winnerIndex;
    }
    
}
