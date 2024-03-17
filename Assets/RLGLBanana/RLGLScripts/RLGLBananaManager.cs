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

    [SerializeField] float _waitTimer;

    [SerializeField] GameObject _warningSign;
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
        _waitTimer = Random.Range(3, 6);
        
        _playerArray = FindObjectsOfType<ShakeToRun>();

        _warningSign.SetActive(false);

    }



    // Update is called once per frame
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


        if(_playerArray.Any(go => go.HasWon == true))
        {
            //Stop the game
            GameIsFinished = true;
            _redLightGreenLIghtText.color = Color.black;
            _redLightGreenLIghtText.text = "Game Is Finished";

        }


        if (StartTheGame && !GameIsFinished)
        {


         

            if (_waitTimer <= 1f) //Warning sing when Guard will trun around
            {
                _warningSign.SetActive(true);



            }
            

            if (_waitTimer > 0 )
            {
                _waitTimer -= Time.deltaTime;

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
        _waitTimer = Random.Range(3, 6);
        

    }
}
