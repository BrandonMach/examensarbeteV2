using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


[RequireComponent(typeof(PlayerInput))] 
public class StopwatchPlayerScript : MonoBehaviour
{

    public bool StopTime = false;
    bool _hasStoppedTime;
    public bool PlayerHasStoppedTime { get => _hasStoppedTime; }
    [SerializeField] TextMeshProUGUI _playerNameText;

    [SerializeField] TextMeshProUGUI _playerTimeText;

    float _stoppedTime = Mathf.Infinity;
    public float GetStoppedTime { get => _stoppedTime; set => _stoppedTime = value; }

    public bool PlayerIsReady;
    public GameObject ReadyUpUI;

    public void OnStopTime(InputAction.CallbackContext context)
    {
        StopTime = context.ReadValueAsButton();
        StopTime = context.action.triggered;
        
    }

    public void OnPlayerIsReady(InputAction.CallbackContext context)
    {
        PlayerIsReady = true;
        
        ReadyUpUI.gameObject.GetComponent<ReadyUpScript>().IsReady();
       
        
    }

    void Start()
    {
        if (GetComponent<PlayerInput>().playerIndex == 0)
        {

            this.name = "Player 1";

            _playerNameText.color = Color.red;


        }

        if (GetComponent<PlayerInput>().playerIndex == 1)
        {

            this.name = "Player 2";
            _playerNameText.color = Color.blue;
            _playerNameText.GetComponent<RectTransform>().position = new Vector3(750, 264, this.transform.position.z);

        }

        _playerNameText.text = name;

    }

    

    // Update is called once per frame
    void Update()
    {
        

        if ( StopwatchManager.Instance.StartTheGame && StopTime && !_hasStoppedTime ) //Game must be started to ba able to stop time
        {
            _hasStoppedTime = true;
            Debug.LogError("hej från " + this.name);
            StopwatchManager.Instance.PlayerStopTime(_playerTimeText, this); //Sen time varibale to get changed by the manager 
        }
    }

    
}
