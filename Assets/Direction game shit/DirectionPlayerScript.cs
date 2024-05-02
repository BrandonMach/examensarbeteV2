using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPlayerScript : JoyconPlayerBase
{
    // Start is called before the first frame update

    [SerializeField] Vector3 eulerAngles;
    [SerializeField] Vector3 accelerometer;

    public DirectiongameManager.Direction PlayerChoosenDirection;

    public bool CorrectDirection;
    [SerializeField] GameObject _checkmark;

    Joycon j;


    void Start()
    {
        base.Start();
        j = joycons[jc_ind];
        j.Recenter();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (joycons.Count > 0)
        {
           j = joycons[jc_ind];


            base.ReadyUp(j);

            

            eulerAngles = j.GetVector().eulerAngles;
            accelerometer = j.GetAccel();


            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                j.Recenter();
            }
          
           

            ////if (j.GetButtonDown(Joycon.Button.DPAD_UP))
            ////{
            ////    PlayerChoosenDirection = DirectiongameManager.Direction.Button_Up;
            ////}
            ////else
            ////{
            ////    gyroCheck();
            ////}
            ////if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            ////{
            ////    PlayerChoosenDirection = DirectiongameManager.Direction.Button_Down;
            ////}
            ////else
            ////{
            ////    gyroCheck();
            ////}
            ///



            if (accelerometer.x >= 0.9f || accelerometer.x <= -0.9f)
            {





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

                if (eulerAngles.z > 20 && eulerAngles.z < 120)
                {
                    Debug.Log(this.name + "Right");
                    PlayerChoosenDirection = DirectiongameManager.Direction.Right;
                }
                if (eulerAngles.z > 200 && eulerAngles.z < 300)
                {
                    Debug.Log(this.name + "Left");
                    PlayerChoosenDirection = DirectiongameManager.Direction.Left;
                }

            }


            _checkmark.SetActive(CorrectDirection);







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
