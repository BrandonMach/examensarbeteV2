using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class StopwatchManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerHUDElement;

    [SerializeField] float _startTime;
    [SerializeField] float _stopwatch;
    private bool TimeRanOut;

    public GameObject PlayerInfoBackdropGO;
    [SerializeField] GameObject _controls;

    [Header("Microphone")]
    [SerializeField] AudioLoudnessDetection _audioDetector;
    public float loudnessSensibility = 100;
    public float threshold = 0.5f;
    [SerializeField] float loudness;
    [SerializeField] bool isLoud;
    [SerializeField] SpriteRenderer _audioSpriteRenderer;


    [Header("Add more Time")]   
    [SerializeField] float _timeToAdd;
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _timeToAddPrefab;
    bool _addMoreTime;


    float _delayTime;

    [SerializeField] int _maxAddTime;
    [SerializeField] GameObject[] _stopwatchImages;

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

        _delayTime = Random.Range(1.5f, 2.5f);
        PlayerInfoBackdropGO.GetComponent<RectTransform>().localScale = new Vector3(1, 2, 1);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }


    void FixedUpdate()
    {
        _controls.SetActive(!StartTheGame);
        AllPlayersAreReadyCheck();
        MainGameLoop();     
    }

    private void AllPlayersAreReadyCheck()
    {
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true) ) //Start the game once all player are ready 
        {
            StartTheGame = true;

            StartCoroutine(ResizePlayerInfoBackdrop());

            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false

            }
        }
    }

    private void MainGameLoop()
    {
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



            if (!_gameIsFinished)
            {
                AddMoreTime();
            }
          

            //Check if stopwatch time has run out
            CheckIfTimeRanOut();

            //Check if all players have topped time
            AllPlayersHasStoppedTime();

            GameIsFinnished();

            for (int i = 0; i < _stopwatchImages.Length; i++)
            {
                if((i+1) > _maxAddTime)
                {
                    _stopwatchImages[i].SetActive(false);
                }
            }
        }
    }

    void CheckIfTimeRanOut()
    {
        //Check if stopwatch time has run out
        if (_stopwatch <= 0 && !TimeRanOut)
        {
            _gameIsFinished = true;
            TimeRanOut = true;
            Debug.Log("Timer is done");
            Debug.Log("Stopwatch game is finished");

        }
    }

    void AllPlayersHasStoppedTime()
    {
        if (_playerArray.All(go => go.PlayerHasStoppedTime == true) && _playerArray.Length >= 2 && !_gameIsFinished) //Has to be atleast 2 players in the game
        {
            _gameIsFinished = true;
        }
    }

    void GameIsFinnished()
    {
        if (_gameIsFinished && !_announceWinner)
        {
            _gameIsFinishedText.SetActive(true);
            _announceWinner = true;
            CheckWinner();
        }
    }

    void CheckWinner()
    {
        System.Array.Sort(_playerArray, (a, b) => { return a._stoppedTime.CompareTo(b._stoppedTime); }); //Sort player array so that the player with closest to 0 is number one in the array
        foreach (var players in _playerArray)
        {
            //Debug.LogWarning(players._stoppedTime);
            if (players._stoppedTime != Mathf.Infinity)
            {
                players.PlayerTimeText.text = players._stoppedTime.ToString("0.00");
            }
            else
            {
                players.PlayerTimeText.text = "NULL";
            }
           
            players.PlayerTimeText.gameObject.SetActive(true);

        }
       
        //Place crown over player 
        _playerArray[0].IsWinner = true;
        
    }

    public void JoyconPlayerStopTime( JoyconStopwatchPlayer player)
    {
        if (!TimeRanOut)
        {
            float playerStopTime = _stopwatch;      
            player._stoppedTime = playerStopTime; //Assign the player with the stopped time
        }
    }

    void AddMoreTime()
    {
        // Multiply so that loudness easier to work with
        loudness = _audioDetector.GetLoudnessFromMicrophone() * loudnessSensibility * 5;



        if ((loudness) <= threshold)
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, loudness);
        }
        else
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, 80);
        }



        //if loudness is negative set to 0 as mininum and prevent more time to be added until it has atleast reseted
        if (loudness < threshold)
        {
            loudness = 0;
            isLoud = false;
        }
        else
        {
            isLoud = true;
        }

        //Prevent time to be added everytime peak is hit
        if (_addMoreTime)
        {
            _delayTime -= Time.deltaTime;
            if (_delayTime < 0)
            {
                
                _addMoreTime = false;
                
                _delayTime = Random.Range(1.5f, 2.5f);
            }
            
        }

        //if loudness is more than threashold and hasn't added time => add extra time and spawn HUD element that shows how much time got added
        if (loudness > threshold && !_addMoreTime && _maxAddTime > 0)
        {
            _maxAddTime--;
            _addMoreTime = true;
            _stopwatch += /*_timeToAdd*/ (int)loudness / 10;


            #region Add Time HUD visual

           // Debug.LogError("Add " + _timeToAdd + " seconds");
            GameObject tempAddTimePrefab = Instantiate(_timeToAddPrefab);
            tempAddTimePrefab.GetComponent<TextMeshProUGUI>().text = "+"+ ((int)loudness / 10).ToString();
            //tempAddTimePrefab.transform.parent = _canvas.transform;

            tempAddTimePrefab.transform.SetParent(_canvas.transform);
            tempAddTimePrefab.transform.localScale = new Vector3(1, 1, 1);
            //Position y is determinated by animation
            tempAddTimePrefab.transform.localPosition = new Vector3(70, 0, 0);

            #endregion
        }



    }


    public IEnumerator ResizePlayerInfoBackdrop()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerInfoBackdropGO.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }



}
