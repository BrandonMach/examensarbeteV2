using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(JoyconDemo))]
public class ShakeToRun : MonoBehaviour
{
    JoyconDemo jcDemo;
    [SerializeField] float _shakeInput = 0;
    private float _accelerometerThreshold = 6;

    [SerializeField] float _stepsTaken = 0;
    private float _shakeInputToStepThreshold = 15;
    [SerializeField] RectTransform _PlayerHUDInfo;
    [SerializeField] TextMeshProUGUI _stepsText;
    [SerializeField] TextMeshProUGUI _playerNameIndicator;

    [SerializeField] bool _stoppedRunning;
    float _stopWindow = 0.2f;

    [SerializeField] bool _gotCought;
    void Start()
    {
        jcDemo = GetComponent<JoyconDemo>();

        if(jcDemo.jc_ind == 0)
        {
            _playerNameIndicator.text = "Player 1";
            _PlayerHUDInfo.localPosition = new Vector3(-285, 76, 0);
        }

        switch (jcDemo.jc_ind)
        {
            case 0:
                _playerNameIndicator.text = "Player 1";
                _playerNameIndicator.color = Color.red;
                _PlayerHUDInfo.position = new Vector3(-0, 0, 0);

                name = "Player 1";
                break;

            case 1:
                _playerNameIndicator.text = "Player 2";
                _playerNameIndicator.color = Color.blue;
                _PlayerHUDInfo.position = new Vector3(100, 200, 0);

                name = "Player 2";
                break;


            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _stepsText.text = "Steps: " + _stepsTaken;


        if (_gotCought)
        {
            _gotCought = false;
            _stepsTaken = 0; //Kanke mins 5 steg vi får playtest
        }

        PlayerIsShakingJoyCon();

        //if (jcDemo.accel.x > _accelerometerThreshold)
        //{
        //    if (RLGLBananaManager.Instance.GreenLight)
        //    {
        //        Debug.LogError(name + " have been cought. Go back");
        //        _gotCought = true;
        //    }

        //    _stopWindow = 0.2f;
        //    _stoppedRunning = false;
        //    Debug.LogError("Hej jag skakas");
        //    _shakeInput++;


        //    if(_shakeInput >= _shakeInputToStepThreshold)
        //    {
        //        _shakeInput = 0;
        //        Debug.LogWarning("ta ett steg fram");
        //        _stepsTaken++;

        //    }
        //}
        //else
        //{
        //    _stopWindow -= Time.deltaTime;
        //    if (_stopWindow <= 0)
        //    {
        //        _stoppedRunning = true;
        //        _shakeInput = 0;
        //    }
        //}
    }

    private void PlayerIsShakingJoyCon()
    {
        if (jcDemo.accel.x > _accelerometerThreshold)
        {
            if (RLGLBananaManager.Instance.GreenLight)
            {
                Debug.LogError(name + " have been cought. Go back");
                _gotCought = true;
            }

            _stopWindow = 0.2f;
            _stoppedRunning = false;
            Debug.LogError("Hej jag skakas");
            _shakeInput++;


            if (_shakeInput >= _shakeInputToStepThreshold)
            {
                _shakeInput = 0;
                Debug.LogWarning("ta ett steg fram");
                _stepsTaken++;

            }
        }
        else
        {
            _stopWindow -= Time.deltaTime;
            if (_stopWindow <= 0)
            {
                _stoppedRunning = true;
                _shakeInput = 0;
            }
        }
    }
}
