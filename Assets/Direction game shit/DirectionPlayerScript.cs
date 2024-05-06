using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirectionPlayerScript : JoyconPlayerBase
{
    // Start is called before the first frame update

    [SerializeField] Vector3 eulerAngles;
    [SerializeField] Vector3 accelerometer;

    public DirectiongameManager.Direction PlayerChoosenDirection;

    public bool CorrectDirection;
    [SerializeField] GameObject _checkmark;

    Joycon j;

    public bool HaveRecenter;
    public GameObject RecenterText;

    [SerializeField] TextMeshProUGUI _tutorialText;

    void Start()
    {
        base.Start();
        j = joycons[jc_ind];
        j.Recenter();

        RecenterText.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (joycons.Count > 0)
        {
           j = joycons[jc_ind];


            base.ReadyUp(j);

            

            eulerAngles = j.GetVector().eulerAngles;
            accelerometer = j.GetAccel();


            if (j.GetButtonDown(Joycon.Button.SHOULDER_2) || j.GetButtonDown(Joycon.Button.SHOULDER_1))
            {
                j.Recenter();
                HaveRecenter = true;
                RecenterText.SetActive(false);

            }



            if (accelerometer.x >= 0.9f || accelerometer.x <= -0.9f)
            {


                HaveRecenter = false;


                if (accelerometer.x >= 0.9f)
                {
                    PlayerChoosenDirection = DirectiongameManager.Direction.Up;
                    
                }
                if (accelerometer.x <= -0.9f)
                {
                    PlayerChoosenDirection = DirectiongameManager.Direction.Down;
                    

                }

            }
            else if ((eulerAngles.z > 20 && eulerAngles.z < 120) || (eulerAngles.z > 200 && eulerAngles.z < 300))
            {

                HaveRecenter = false;
                if (eulerAngles.z > 20 && eulerAngles.z < 120)
                {
                    Debug.Log(this.name + "Right");
                    PlayerChoosenDirection = DirectiongameManager.Direction.Right;
                    _tutorialText.text = "RIGHT";
                }
                if (eulerAngles.z > 200 && eulerAngles.z < 300)
                {
                    Debug.Log(this.name + "Left");
                    PlayerChoosenDirection = DirectiongameManager.Direction.Left;
                    _tutorialText.text = "LEFT";
                }

            }


            _checkmark.SetActive(CorrectDirection);


            if (!DirectiongameManager.Instance.StartTheGame)
            {
                switch (PlayerChoosenDirection)
                {
                    case DirectiongameManager.Direction.Up:
                        _tutorialText.text = "UP";
                        break;
                    case DirectiongameManager.Direction.Down:
                        _tutorialText.text = "DOWN";
                        break;
                    case DirectiongameManager.Direction.Left:
                        _tutorialText.text = "LEFT";
                        break;
                    case DirectiongameManager.Direction.Right:
                        _tutorialText.text = "RIGHT";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _tutorialText.gameObject.SetActive(false);
            }






        }



    }

    void gyroCheck()
    {

        if (accelerometer.x >= 1f || accelerometer.x <= -1f)
        {





            if (accelerometer.x >= 1f)
            {
                PlayerChoosenDirection = DirectiongameManager.Direction.Up;
            }
            if (accelerometer.x <= -1f)
            {
                PlayerChoosenDirection = DirectiongameManager.Direction.Down;

            }

        }
    }



}
