using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenceManager : MonoBehaviour
{
    #region Singelton
    static DefenceManager _instance;
    public static DefenceManager Instance { get => _instance; set => _instance = value; }
    #endregion

    [SerializeField] DefenceJoyconPlayer[] _playerArray;

    public int lives;
    public float timer;
    public bool gameOver, startGame;
    public TMPro.TextMeshProUGUI timerTxt;
    public TMPro.TextMeshProUGUI livestxt;
    public GameObject gameOverPanel, VictoryPanel;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;

    }
    // Start is called before the first frame update
    public void TakeDamage()
    {
        lives--;
    }

    public void UpdatePlayerArray()
    {
        _playerArray = FindObjectsOfType<DefenceJoyconPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) //Start the game once all player are ready 
        {
            startGame = true;
            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false

            }
        }
        if (startGame)
        {
            if (timer > 0 && lives > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                EndGame();
            }
            timerTxt.SetText(timer.ToString("#.00"));
            livestxt.SetText(lives.ToString());
        }
    }

    private void EndGame()
    {
        //Anything that should happen when the game ends
        if (lives <= 0)
        {
            gameOverPanel.SetActive(true);
        }
        else if(timer <= 0)
        {
            VictoryPanel.SetActive(true);
        }
        gameOver = true;
    }
}
