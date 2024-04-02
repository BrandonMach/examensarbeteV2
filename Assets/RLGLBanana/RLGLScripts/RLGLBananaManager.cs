using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;


public class RLGLBananaManager : MonoBehaviour
{
    //public int _playerCount = 0;


    #region Singelton
    static RLGLBananaManager _instance;
    public static RLGLBananaManager Instance { get => _instance; set => _instance = value; }
    #endregion


    [SerializeField] ShakeToRun[] _playerArray;
    public bool StartTheGame; //Only start the game when all player have joined
    public bool GameIsFinished;

    #region Red Light Green Light
    [SerializeField] TextMeshProUGUI _redLightGreenLIghtText;
    [SerializeField] public  bool GreenLight;

    [SerializeField] float _CatchWaitTimer;

    [SerializeField] GameObject _warningSign;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject[] _playerModels;
    [SerializeField] float StepGoal;

    bool _spacePressed;
    #endregion

    

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of RLGLBananaManager found");
            return;
        }
        Instance = this;
      

      
    }


    void Start()
    {
        _CatchWaitTimer = Random.Range(3, 6);
        
        _playerArray = FindObjectsOfType<ShakeToRun>();



        for (int i = 0; i < _playerArray.Length; i++)
        {
            //The Findobjects finds the players highest number playe to lowers 4->1
            _playerArray[_playerArray.Length-(1+i)].SetCharacterModel(Instantiate(_playerModels[i], _spawnPoints[i]));
            _playerArray[i].SetStepGoal(StepGoal);
        }

        _warningSign.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {

        //Start the game once all player are ready 
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) 
        {
            StartTheGame = true;
            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false

            }
        }


        if(_playerArray.Any(go => go.HasWon == true))
        {
            //Stop the game
            GameIsFinished = true;
            _redLightGreenLIghtText.color = Color.black;
            _redLightGreenLIghtText.text = "Game Is Finished";

        }


        if (StartTheGame && !GameIsFinished)
        {



            // Warning sing when Guard will turn around
            if (_CatchWaitTimer <= 1f ) 
            {
                _warningSign.SetActive(true);

            }

            // If audience presses space activate stop
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Audience pressed space");
                _spacePressed = true;
                StartCoroutine(AudienceStop());

                
            }
            
            // Count down Catch time and after set it to green light
            if (_CatchWaitTimer > 0 && !_spacePressed)
            {
                _CatchWaitTimer -= Time.deltaTime;

                GreenLight = true;

            }
            else
            {
                StartCoroutine(PlayersCanMove());

            }






            if (GreenLight)
            {
                _redLightGreenLIghtText.color = Color.green;
                _redLightGreenLIghtText.text = "You can move now";
            }
            else
            {
                _redLightGreenLIghtText.color = Color.red;
                _redLightGreenLIghtText.text = "Stop! Don't Move";
            }
        }


       
    }

    IEnumerator PlayersCanMove()
    {
        GreenLight = false;
        _warningSign.SetActive(false);
        yield return new WaitForSeconds(Random.Range(1.3f,3.5f));
        _CatchWaitTimer = Random.Range(3, 6);
        

    }

    IEnumerator AudienceStop()
    {
        GreenLight = false;
        yield return new WaitForSeconds(Random.Range(1.3f, 3.5f));
        _CatchWaitTimer = Random.Range(3, 6);
        _spacePressed = false;


    }
}
