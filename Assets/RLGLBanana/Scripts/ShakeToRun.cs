using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(JoyconDemo))]
public class ShakeToRun : JoyconPlayerBase
{
    //JoyconDemo jcDemo;
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
        //jcDemo = GetComponent<JoyconDemo>();


        switch (jc_ind)
        {
            case 0:
                _playerNameIndicator.text = "Player 1";
                _playerNameIndicator.color = Color.red;
                _PlayerHUDInfo.position = new Vector3(0, 264, this.transform.position.z);

                name = "Player 1";
                break;

            case 1:
                _playerNameIndicator.text = "Player 2";
                _playerNameIndicator.color = Color.blue;
                _PlayerHUDInfo.position = new Vector3(700, 264, this.transform.position.z);

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

    }

    private void PlayerIsShakingJoyCon()
    {
        if (accel.x > _accelerometerThreshold)
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
