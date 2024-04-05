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


    [Header("Winner")]
    [SerializeField] GameObject _winnerCrown;
    public bool HasWon;
    float _stepGoal;

    [Header("Animation")]
    [SerializeField] GameObject _playerModel;
    [SerializeField] Animator _anim;
    Vector3 startPos;

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

            //Playe player model Moving animation
            _anim.SetBool("Moving", !_stoppedRunning);
            _stepsText.text = "Steps: " + _stepsTaken;
            

            if (_stepsTaken >= _stepGoal)
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
                _playerModel.transform.position = startPos;
            }

            //
            _stopWindow = 0.2f;
            _stoppedRunning = false;
            _shakeInput++;


            if (_shakeInput >= _shakeInputToStepThreshold)
            {              
                _shakeInput = 0;
                _stepsTaken++;
                _playerModel.transform.position = new Vector3(_playerModel.transform.position.x, _playerModel.transform.position.y, (_playerModel.transform.position.z + 0.5f));
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


    public void SetCharacterModel(GameObject go)
    {
        _playerModel = go;
        startPos = new Vector3(_playerModel.transform.position.x, _playerModel.transform.position.y, _playerModel.transform.position.z);
        _anim = _playerModel.GetComponent<Animator>();
        
    }

    public void SetStepGoal(float stepGoal)
    {
        _stepGoal = stepGoal;
    }
}
