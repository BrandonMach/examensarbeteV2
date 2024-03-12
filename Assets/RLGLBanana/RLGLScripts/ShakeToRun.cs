using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(JoyconDemo))]
public class ShakeToRun : JoyconPlayerBase
{
    [Header("Red Light Green Light Player")]
    [SerializeField] float _shakeInput = 0;
    [SerializeField]private float _accelerometerThreshold = 2;

    [SerializeField] float _stepsTaken = 0;
    private float _shakeInputToStepThreshold = 15;

    [SerializeField] TextMeshProUGUI _stepsText;


    [SerializeField] bool _stoppedRunning;
    float _stopWindow = 0.2f;

    [SerializeField] bool _gotCought;

    [SerializeField] GameObject _winnerCrown;
    public bool HasWon;

    void Start()
    {
        base.Start();

        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);



    }

    // Update is called once per frame
    void Update()
    {
        if (!RLGLBananaManager.Instance.GameIsFinished)
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

            if (_stepsTaken >= 30)
            {
                HasWon = true;

            }
            _winnerCrown.SetActive(HasWon);
        }


       

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
