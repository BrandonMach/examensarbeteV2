using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalancePlayer : JoyconPlayerBase
{
    
    [SerializeField] GameObject _winnerCrown;
    [SerializeField] GameObject _mainBody;


    void Start()
    {
        base.Start();
        _winnerCrown.SetActive(false);

        //Place player relative to the middle of the screen
        switch (jc_ind)
        {
            case 0:
                gameObject.transform.position = new Vector3(-1f, -2, 0);
                _mainBody.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 1:
                gameObject.transform.position = new Vector3(1f, -2, 0);
                _mainBody.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 2:
                gameObject.transform.position = new Vector3(0, 0, 0);
                _mainBody.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case 3:
                gameObject.transform.position = new Vector3(0, 0, 0);
                _mainBody.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

           
          

            gameObject.transform.rotation = orientation;
           



           

            base.ReadyUp(j);


            orientation = j.GetVector();

            gameObject.transform.Rotate(90, 0, 0, Space.World);
            //// Gyro values: x, y, z axis values (in radians per second)
            //gyro = j.GetGyro();

            //// Accel values:  x, y, z axis values (in Gs)
            //accel = -j.GetAccel();






        }

        
    }
}
