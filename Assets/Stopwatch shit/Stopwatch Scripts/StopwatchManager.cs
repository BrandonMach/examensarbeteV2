using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class StopwatchManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerHUDElement;

    [SerializeField] float _startTime;
    [SerializeField] float _stopwatch;
    private bool TimeRanOut;


    [Header("Microphone")]
    [SerializeField] AudioLoudnessDetection _audioDetector;
    public float loudnessSensibility = 100;
    public float threshold = 0.5f;
    [SerializeField] float loudness;

    [Header("Add more Time")]   
    [SerializeField] float _timeToAdd;
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _timeToAddPrefab;
    bool _addMoreTime;



    #region Singelton
    static StopwatchManager _instance;
    public static StopwatchManager Instance { get => _instance; set => _instance = value; }
    #endregion



    [SerializeField] JoyconStopwatchPlayer[] _playerArray;

   

    

    public bool StartTheGame; //Only start the game when all player have joined


    [Header("Game is finished")]
    bool _gameIsFinished = false;
    bool _announceWinner;
    [SerializeField] GameObject _gameIsFinishedText;
    [SerializeField] RectTransform _crownRectTransform;

    [SerializeField] Animator _textAnim;
    bool _startTextFade;


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
        _stopwatch = _startTime;
        _playerArray = FindObjectsOfType<JoyconStopwatchPlayer>();
        _gameIsFinishedText.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {

        if(_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) //Start the game once all player are ready 
        {
            StartTheGame = true;
            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false
                
            }
        }

        if (StartTheGame)     
        {

            if (!_startTextFade)
            {
                _startTextFade = true;
                _textAnim.SetTrigger("StartFade");
            }

            _timerHUDElement.text = _stopwatch.ToString("0.0");


            // As long as Time hasn't run out reduce the stopwatch
            if (!TimeRanOut && !_gameIsFinished)
            {
                _stopwatch -= Time.deltaTime;
            }


            AddMoreTime();

            //Check if stopwatch time has run out
            if (_stopwatch <= 0 && !TimeRanOut)
            {
                _gameIsFinished = true;
                TimeRanOut = true;
                Debug.Log("Timer is done");
                Debug.Log("Stopwatch game is finished");

            }

            //Check if all players have topped time
            if (_playerArray.All(go => go.PlayerHasStoppedTime == true) && _playerArray.Length >= 2 && !_gameIsFinished) //Has to be atleast 2 players in the game
            {
                _gameIsFinished = true;
            }




            if (_gameIsFinished && !_announceWinner)
            {
                _gameIsFinishedText.SetActive(true);
                _announceWinner = true;
                CheckWinner();
            }


        }

    }

    public void CheckWinner()
    {
        System.Array.Sort(_playerArray, (a, b) => { return a.GetStoppedTime.CompareTo(b.GetStoppedTime); }); //Fort player array so that the player with closest to 0 is number one in the array
        foreach (var item in _playerArray)
        {
            Debug.LogWarning(item.GetStoppedTime);
        }
        Debug.LogWarning(_playerArray[0].gameObject.name + " is the winner");
        //Place crown over player 
        _playerArray[0].IsWinner = true;
        
    }



    public void JoyconPlayerStopTime(TextMeshProUGUI playerText, JoyconStopwatchPlayer player)
    {
        if (!TimeRanOut)
        {

            float playerStopTime = _stopwatch;

            playerText.text = playerStopTime.ToString("0.00");
            player.GetStoppedTime = playerStopTime; //Assign the player with teh stopped time
        }


    }


    void AddMoreTime()
    {
        // Multiply so that loudness easier to work with
        loudness = _audioDetector.GetLoudnessFromMicrophone() * loudnessSensibility;

        //if loudness is negative set to 0 as mininum and prevent more time to be added until it has atleast reseted
        if(loudness < threshold)
        {
            loudness = 0;
            _addMoreTime = false;
        }

        //if loudness is more than threashold and hasn't added time => add extra time and spawn HUD element that shows how much time got added
        if (loudness > threshold && !_addMoreTime)
        {
            _addMoreTime = true;
            _stopwatch += _timeToAdd;


            #region Add Time HUD visual

            Debug.LogError("Add " + _timeToAdd + " seconds");
            GameObject tempAddTimePrefab = Instantiate(_timeToAddPrefab);
            tempAddTimePrefab.transform.parent = _canvas.transform;
            tempAddTimePrefab.transform.localScale = new Vector3(1, 1, 1);
            tempAddTimePrefab.transform.localPosition = new Vector3(80, 140, 0);

            #endregion
        }



    }

    

    
}
