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


    public void OnStopTime(InputAction.CallbackContext context)
    {
        StopTime = context.ReadValueAsButton();
        StopTime = context.action.triggered;
        
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
            _playerNameText.GetComponent<RectTransform>().position = new Vector3(780, 137, this.transform.position.z);

        }

        _playerNameText.text = name;

    }

    

    // Update is called once per frame
    void Update()
    {


        if (StopTime && !_hasStoppedTime)
        {
            _hasStoppedTime = true;
            Debug.LogError("hej från " + this.name);
            StopwatchManager.Instance.PlayerStopTime(_playerTimeText, this); //Sen time varibale to get changed by the manager 
        }
    }

    
}
