using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class StopwatchPlayerScript : MonoBehaviour
{

    public bool StopTime = false;
    bool _hasStoppedTime;
    public bool PlayerHasStoppedTime { get => _hasStoppedTime; }
    [SerializeField] TextMeshProUGUI _playerText;

    float _stoppedTime = Mathf.Infinity;
    public float GetStoppedTime { get => _stoppedTime; set => _stoppedTime = value; }


    public void OnStopTime(InputAction.CallbackContext context)
    {
        StopTime = context.ReadValueAsButton();
        StopTime = context.action.triggered;
        
    }

    void Start()
    {

     
    }

    

    // Update is called once per frame
    void Update()
    {


        if (StopTime && !_hasStoppedTime)
        {
            _hasStoppedTime = true;
            Debug.LogError("hej från " + this.name);
            StopwatchManager.Instance.PlayerStopTime(_playerText, this);
        }
    }

    
}
