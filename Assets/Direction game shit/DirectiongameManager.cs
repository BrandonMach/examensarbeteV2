
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DirectiongameManager : MonoBehaviour
{
    #region Singelton

    static DirectiongameManager _instance;

    public DirectiongameManager Instance
    {
        get => _instance;
        set => _instance = value;
    }

    #endregion


    [SerializeField] DirectionPlayerScript[] _playerArray;
    [SerializeField] bool StartTheGame;
    [SerializeField] RectTransform _arrowImageRectTransform;
    float[] _arrowAngleDirections = { 0f, 90f, 180f, 270f };

    bool _generateRandomDirection;
    [SerializeField] bool _followArrowDirection;

    [SerializeField] float chooseDirectionDelay = 3f;
    [SerializeField] float countUp = 0;

    [SerializeField] float healt = 0; //Should be double the amount of players connected

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [Header("Direction Gameloop Variables")] 
    public Direction _currentSelectedDirection;
    Direction _setTextDirection;

    [SerializeField] TextMeshProUGUI _directionText;
    [SerializeField] TextMeshProUGUI _healthText;

    bool _gameOver;

    private void Awake()
    {
        
    }

    void Start()
    {
        _playerArray = FindObjectsOfType<DirectionPlayerScript>();
        ChooseRandomDirection();

        healt = _playerArray.Length * 2;
    }


    private void AllPlayersAreReadyCheck()
    {
        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) //Start the game once all player are ready 
        {
            StartTheGame = true;

            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false

            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        AllPlayersAreReadyCheck();
        _healthText.text = healt.ToString() + "x";

        if (StartTheGame && !_gameOver)
        {
            
            if (!_generateRandomDirection)
            {
                _generateRandomDirection = true;
                ChooseRandomDirection();
            }
            else
            {
                countUp += Time.deltaTime;
                if(countUp >= chooseDirectionDelay)
                {
                    _generateRandomDirection = false;
                    countUp = 0;
                    CheckIfPlayerIsWrongDirection();
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _followArrowDirection = true;
            }
            else
            {
                _followArrowDirection = false;
            }


            if (_followArrowDirection)
            {
                _arrowImageRectTransform.GetComponent<Image>().color = Color.green;
                _directionText.color = Color.white;
            }
            else
            {
                _directionText.color = Color.green;
                _arrowImageRectTransform.GetComponent<Image>().color = Color.white;
            }

            foreach (var player in _playerArray)
            {
                if(player.GetComponent<DirectionPlayerScript>().PlayerChoosenDirection == _currentSelectedDirection)
                {
                    player.GetComponent<DirectionPlayerScript>().CorrectDirection = true;
                }
                else
                {
                    player.GetComponent<DirectionPlayerScript>().CorrectDirection = false;
                }
            }


            if(healt <= 0)
            {
                _directionText.text = "Game Over!";
                _gameOver = true;
            }

        }
    }


    private void ChooseRandomDirection()
    {

        


        _arrowImageRectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, _arrowAngleDirections[Random.Range(0, _arrowAngleDirections.Length)]));
        _setTextDirection = (Direction)Random.Range(0, 4);


        switch (_setTextDirection)
        {
            case Direction.Up:
                _directionText.text = "UP";
                break;
            case Direction.Down:
                _directionText.text = "DOWN";
                break;
            case Direction.Left:
                _directionText.text = "LEFT";
                break;
            case Direction.Right:
                _directionText.text = "RIGHT";
                break;
            default:
                break;
        }

        if (!_followArrowDirection)
        {
            _currentSelectedDirection = _setTextDirection;
        }
        else
        {

            Debug.Log("_followArrowDirection = true");
            if (_arrowImageRectTransform.rotation == Quaternion.Euler(0, 0, 0f))
            {
                _currentSelectedDirection = Direction.Up;
            }
            if (_arrowImageRectTransform.rotation == Quaternion.Euler(0,0, 90f))
            {
                _currentSelectedDirection = Direction.Left;
                Debug.Log("Go left");
            }
            if (_arrowImageRectTransform.rotation == Quaternion.Euler(0, 0, 180f))
            {
                _currentSelectedDirection = Direction.Down;
            }
            if (_arrowImageRectTransform.rotation == Quaternion.Euler(0, 0, 270f))
            {
                _currentSelectedDirection = Direction.Right;
            }

   
        }


        


    }


    void CheckIfPlayerIsWrongDirection()
    {
        foreach (var player in _playerArray)
        {

            if (player.GetComponent<DirectionPlayerScript>().PlayerChoosenDirection != _currentSelectedDirection)
            {
                healt--;
            }
        }
    }
  



}