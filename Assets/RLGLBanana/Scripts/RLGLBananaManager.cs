using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PlayerInputManager))]
public class RLGLBananaManager : MonoBehaviour
{
    public int _playerCount = 0;


    #region Singelton
    static RLGLBananaManager _instance;
    public static RLGLBananaManager Instance { get => _instance; set => _instance = value; }
    #endregion

    PlayerInputManager _playerInputManager;


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
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;
        _playerInputManager = GetComponent<PlayerInputManager>();

        

      
    }


    void Start()
    {
        
        _waitTimer = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(_waitTimer <= 1f) //Warning sing when Guard will trun around
        {
            _warningSign.SetActive(true);

        }

        if(_waitTimer > 0)
        {
            _waitTimer -= Time.deltaTime;

            GreenLight = true;

        }
        else
        {
            StartCoroutine(PlayersCanMove());
              
        }


        if (_playerCount >= _playerInputManager.maxPlayerCount)
        {
            _playerCount = _playerInputManager.maxPlayerCount;
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

    IEnumerator PlayersCanMove()
    {
        GreenLight = false;
        _warningSign.SetActive(false);
        yield return new WaitForSeconds(Random.Range(1,3.5f));
        _waitTimer = Random.Range(2, 6);
        

    }
}
