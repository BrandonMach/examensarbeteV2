using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(JoyconDemo))]
public class ShakeToRun : JoyconPlayerBase
{
    [Header("Red Light Green Light Player")]
    [SerializeField] float _shakeInput = 0;
    [SerializeField]private float _accelerometerThreshold;

    [SerializeField] float _stepsTaken = 0;
    private float _shakeInputToStepThreshold = 15;

    [SerializeField] TextMeshProUGUI _stepsText;


    [SerializeField] bool _stoppedRunning;
    float _stopWindow = 0.2f;

    [SerializeField] public  bool GotCought;

    [SerializeField] int _stepsPunishemnt;


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

        _accelerometerThreshold = 3;

        _stepsPunishemnt = 7;

    }


    void Update()
    {

        

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

        //Tutorial
        if (!RLGLBananaManager.Instance.GameIsFinished)
        {
            PlayerIsShakingJoyCon();
            _anim.SetBool("Moving", !_stoppedRunning);
            _stepsText.text = "Steps: " + _stepsTaken;
        }
        else
        {
            _anim.SetBool("Moving", false);
        }
        


        if (!RLGLBananaManager.Instance.GameIsFinished && RLGLBananaManager.Instance.StartTheGame)
        {


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

    public void EndTutorial()
    {
        _playerModel.transform.position = startPos;
        _stepsTaken = 0;

    }

    private void PlayerIsShakingJoyCon()
    {

       
        if (accel.x > _accelerometerThreshold)
        {
            //Players can only be gought § time each Stop state
            if (!RLGLBananaManager.Instance.GreenLight )
            {
                if (!GotCought)
                {
                    Debug.LogError(name + " have been cought. Go back");
                    GotCought = true;
                    if (_stepsTaken >= _stepsPunishemnt)
                    {
                        _playerModel.transform.position = new Vector3(_playerModel.transform.position.x, _playerModel.transform.position.y, (_playerModel.transform.position.z - (50 * 5 / _stepGoal)));
                        _stepsTaken -= _stepsPunishemnt;
                    }
                    else
                    {
                        _playerModel.transform.position = startPos;
                        _stepsTaken = 0;
                    }
                }
                //else
                //{
                //    _playerModel.transform.position = _playerModel.transform.position;
                //    _stepsTaken = _stepsTaken;
                //}
               
               
            }
            else
            {
                //
                _stopWindow = 0.2f;
                _stoppedRunning = false;
                _shakeInput++;


                if (_shakeInput >= _shakeInputToStepThreshold)
                {
                    _shakeInput = 0;
                    _stepsTaken++;
                    _playerModel.transform.position = new Vector3(_playerModel.transform.position.x, _playerModel.transform.position.y, (_playerModel.transform.position.z + (50 / _stepGoal)));
                }
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
