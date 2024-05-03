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
    [SerializeField] Transform[] _spawnPoints = new Transform[4];
    [SerializeField] GameObject[] _playerModels = new GameObject[4];
    [SerializeField] GameObject[] _tilesIndicators = new GameObject[4];
    [SerializeField] float StepGoal;

    bool _audienceInfluence;
    bool _showWarningSign;

    [SerializeField] GameObject _spotLight;
    #endregion


    [Header("Microphone")]
    [SerializeField] AudioLoudnessDetection _audioDetector;
    public float loudnessSensibility = 100;
    public float threshold = 0.5f;
    [SerializeField] float loudness;

    [SerializeField] bool isLoud;
    [SerializeField] SpriteRenderer _audioSpriteRenderer;
    [SerializeField] float ambientThreashold;



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

            //Activate player tile indicators
            _tilesIndicators[i].SetActive(true);
            //Set the players step goal
            _playerArray[i].SetStepGoal(StepGoal);
        }

        _warningSign.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {

        CheckLoudness();
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



            // Warning sign when Guard will turn around
            //if (_CatchWaitTimer <= 1f ) 
            //{
            //    _warningSign.SetActive(true);

            //}

            
            if ( AudienceIsLoud())
            {

                _audienceInfluence = true;



            }

            _warningSign.SetActive(_showWarningSign);


            // Count down Catch time and after set it to green light
            if (_CatchWaitTimer > 0 && !_audienceInfluence)
            {
                _CatchWaitTimer -= Time.deltaTime;

                GreenLight = true;
                

            }
            else if(!_showWarningSign)
            {    
                StartCoroutine(ShowWarningSign());
            }



            if (GreenLight)
            {
                _redLightGreenLIghtText.color = Color.green;
                _redLightGreenLIghtText.text = "Move";
                foreach (var players in _playerArray)
                {
                    players.GotCought = false;
                }
            }
            else
            {
                _redLightGreenLIghtText.color = Color.red;
                _redLightGreenLIghtText.text = "Stop! Don't Move";

               
                
            }

            _spotLight.GetComponent<Animator>().SetBool("Scan", !GreenLight);
        }


       
    }


    IEnumerator Stop()
    {
        GreenLight = false;
        
        yield return new WaitForSeconds(Random.Range(1.3f, 3.5f));
        if ( AudienceIsLoud())
        {
            StartCoroutine(Stop());
        }
        else
        {
            _audienceInfluence = false;
            _CatchWaitTimer = Random.Range(3, 6);
            _showWarningSign = false;
        }
    }
    IEnumerator ShowWarningSign()
    {
        _showWarningSign = true;
       
        Debug.LogError("Show Stop sign");
        yield return new WaitForSeconds(1);
        Debug.LogError("Apa");
        StartCoroutine(Stop());
    }


    void CheckLoudness()
    {
        // Multiply so that loudness easier to work with
        loudness = _audioDetector.GetLoudnessFromMicrophone() * loudnessSensibility * 5;

        if ((loudness * 2f) <= 80)
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, loudness * 2f);
        }
        else
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, 80);
        }

        //if loudness is negative set to 0 as mininum and prevent more time to be added until it has atleast reseted
        //if (loudness < threshold)
        //{
        //    loudness = 0;
        //    isLoud = false;
        //}
        //else
        //{
        //    isLoud = true;
        //}


    }

    bool AudienceIsLoud()
    {
        // Multiply so that loudness easier to work with
        loudness = _audioDetector.GetLoudnessFromMicrophone() * loudnessSensibility * 5;

        //if loudness is negative set to 0 as mininum and prevent more time to be added until it has atleast reseted

        if((loudness) <= threshold)
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, loudness );
        }
        else
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, 80);
        }
       

        if (loudness < threshold)
        {
            loudness = 0;
            isLoud = false;
            return false;
        }
        else
        {
            isLoud = true;
            return true;
        }
    }
}
