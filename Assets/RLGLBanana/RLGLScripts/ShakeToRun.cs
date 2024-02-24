using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(JoyconDemo))]
public class ShakeToRun : JoyconPlayerBase
{
    //JoyconDemo jcDemo;
    [SerializeField] float _shakeInput = 0;
    private float _accelerometerThreshold = 4;

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
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene

        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }


        //switch (jc_ind)
        //{
        //    case 0:
        //        _PlayerHUDInfo.position = new Vector3(300, 264, this.transform.position.z);
        //        break;

        //    case 1:
        //        _PlayerHUDInfo.position = new Vector3(750, 264, this.transform.position.z);
        //        break;


        //    default:
        //        break;
        //}



        this.name = "Player " + (1 + jc_ind);
        _PlayerHUDInfo.position = new Vector3(300 + (jc_ind*450), 264, this.transform.position.z);

        switch (jc_ind)
        {
            case 0:
                _playerNameIndicator.color = Color.red;
                break;
            case 1:
                _playerNameIndicator.color = Color.blue;
                break;
            case 2:
                _playerNameIndicator.color = Color.yellow;
                break;
            case 3:
                _playerNameIndicator.color = Color.yellow;
                break;
            default:
                break;
        }

        _playerNameIndicator.text = name;

       
    }

    // Update is called once per frame
    void Update()
    {
        


        if (_gotCought)
        {
            _gotCought = false;
            _stepsTaken = 0; //Kanke mins 5 steg vi får playtest
        }

        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            base.ReadyUp(j);
            //If game type så kan vi återanvända skriptet
            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

        }

       

        PlayerIsShakingJoyCon();
        _stepsText.text = "Steps: " + _stepsTaken;

    }

    private void PlayerIsShakingJoyCon()
    {
        if (accel.x > _accelerometerThreshold)
        {
            if (!RLGLBananaManager.Instance.GreenLight)
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
