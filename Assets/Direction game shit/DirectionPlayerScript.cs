using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPlayerScript : JoyconPlayerBase
{
    // Start is called before the first frame update

    [SerializeField] Vector3 eulerAngles;
    [SerializeField] Vector3 accelerometer;

    [SerializeField] DirectiongameManager.Direction _playerChoosenDirection;
    void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];


            base.ReadyUp(j);


            eulerAngles = j.GetVector().eulerAngles;
            accelerometer = j.GetAccel();
       

            
            if (accelerometer.x >= 0.5f || accelerometer.x <= -0.5f)
            {
                if(accelerometer.x >= 0.5f)
                {
                    _playerChoosenDirection = DirectiongameManager.Direction.Up;
                }
                if(accelerometer.x <= -0.5f)
                {
                    _playerChoosenDirection = DirectiongameManager.Direction.Down;

                }
                
            }
            else if ((eulerAngles.z > 20 && eulerAngles.z < 120) ||(eulerAngles.z > 200 && eulerAngles.z < 300))
            {

                if(eulerAngles.z > 20 && eulerAngles.z < 120)
                {
                    Debug.Log(this.name + "Right");
                    _playerChoosenDirection = DirectiongameManager.Direction.Right;
                }
                if(eulerAngles.z > 200 && eulerAngles.z < 300)
                {
                    Debug.Log(this.name + "Left");
                    _playerChoosenDirection = DirectiongameManager.Direction.Left;
                }
                
            }










        }
    }
}
