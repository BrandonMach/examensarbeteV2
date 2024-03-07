using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceJoyconPlayer : JoyconPlayerBase
{
    // Start is called before the first frame update
    new public void Start()
    {
        gyro = new Vector3 (0, 0, 0);
        accel = new Vector3 (0, 0, 0);
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            gyro = j.GetGyro();

            accel = j.GetAccel();

            orientation = j.GetVector();

            gameObject.transform.rotation = orientation;

        }
    }
}
